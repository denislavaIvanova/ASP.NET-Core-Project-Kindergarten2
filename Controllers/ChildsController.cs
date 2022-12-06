

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Childs;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;

	public class ChildsController : Controller
	{
		private readonly KindergartenDbContext data;

		public ChildsController(KindergartenDbContext data)
			=> this.data = data;

		public IActionResult All([FromQuery] AllChildsQueryModel query)
		{
			var childrenQuery = this.data.Children.AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.Group))
			{
				childrenQuery = childrenQuery.Where(x => x.Group.Name == query.Group);
			}

			if (!string.IsNullOrWhiteSpace(query.SearchTerm))
			{
				childrenQuery = childrenQuery.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(query.SearchTerm.ToLower()));
			}

			childrenQuery = query.Sorting switch
			{
				ChildSorting.Age => childrenQuery.OrderBy(c => c.Age),
				ChildSorting.FirstNameAndLastName => childrenQuery.OrderBy(c => c.FirstName).ThenBy(c => c.LastName),
				ChildSorting.DateCreated or _ => childrenQuery.OrderByDescending(c => c.Id)

			};

			var totalChildren = childrenQuery.Count();

			var children = childrenQuery
							.Skip((query.CurrentPage - 1) * AllChildsQueryModel.ChildrenPerPage)
							.Take(AllChildsQueryModel.ChildrenPerPage)
							.Select(c => new ChildListingViewModel
							{
								Id = c.Id,
								FirstName = c.FirstName,
								LastName = c.LastName,
								MiddleName = c.MiddleName,
								Age = c.Age,
								Group = c.Group.Name,
								ECA = c.ECA.Title,
								Trip = c.Trip.PlaceToVisit,
								Menu = c.Menu.MenuType

							}).ToList();

			var childrenGroup = this.data
				.Groups
				.Select(c => c.Name)
				.Distinct()
				.OrderBy(gn => gn)
				.ToList();

			query.TotalChildren = totalChildren;
			query.Children = children;
			query.Groups = childrenGroup;

			return View(query);

		}

		public IActionResult Add() => View(new AddChildFormModel
		{

			Groups = this.GetChildGroups(),
			ECAs = this.GetChildECAs(),
			Menus = this.GetChildMenus(),
			Trips = this.GetChildTrips(),
		});



		[HttpPost]
		public IActionResult Add(AddChildFormModel child)
		{

			if (!this.data.Groups.Any(g => g.Id == child.GroupId)
				|| !this.data.ECAs.Any(e => e.Id == child.ECAId)
				|| !this.data.Menus.Any(m => m.Id == child.MenuId)
				|| !this.data.Trips.Any(t => t.Id == child.TripId))
			{
				this.ModelState.AddModelError(nameof(child.GroupId), "Group does not exist!");
				this.ModelState.AddModelError(nameof(child.ECAId), "Extracurricular activity does not exist!");
				this.ModelState.AddModelError(nameof(child.MenuId), "Menu does not exist!");
				this.ModelState.AddModelError(nameof(child.TripId), "Trip does not exist!");
			}



			if (!ModelState.IsValid)
			{
				child.Groups = this.GetChildGroups();
				child.ECAs = this.GetChildECAs();
				child.Trips = this.GetChildTrips();
				child.Menus = this.GetChildMenus();

				return View(child);
			}


			var childData = new Child
			{
				FirstName = child.FirstName,
				MiddleName = child.MiddleName,
				LastName = child.LastName,
				Age = child.Age,
				ECAId = child.ECAId,
				MenuId = child.MenuId,
				GroupId = child.GroupId,
				TripId = child.TripId
			};


			this.data.Children.Add(childData);

			this.data.SaveChanges();

			return RedirectToAction("Index", "Home");

		}

		private IEnumerable<ChildGroupViewModel> GetChildGroups()
			=> this.data
			.Groups.Select(c => new ChildGroupViewModel
			{
				Id = c.Id,
				Name = c.Name

			}).ToList();

		private IEnumerable<ChildECAViewModel> GetChildECAs()
			=> this.data
			.ECAs.Select(c => new ChildECAViewModel
			{
				Id = c.Id,
				Title = c.Title

			}).ToList();

		private IEnumerable<ChildMenuViewModel> GetChildMenus()
			=> this.data
			.Menus.Select(c => new ChildMenuViewModel
			{
				Id = c.Id,
				MenuType = c.MenuType

			}).ToList();

		private IEnumerable<ChildTripViewModels> GetChildTrips()
			=> this.data
			.Trips.Select(c => new ChildTripViewModels
			{
				Id = c.Id,
				PlaceToVisit = c.PlaceToVisit
			})
			.ToList();

	}
}

