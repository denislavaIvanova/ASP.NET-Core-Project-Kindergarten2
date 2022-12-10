

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Infrastructure;
	using Kindergarten2.Models.Childs;
	using Kindergarten2.Services.Childs;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;

	public class ChildsController : Controller
	{
		private readonly KindergartenDbContext data;

		private readonly IChildService children;

		public ChildsController(KindergartenDbContext data, IChildService children)
		{
			this.data = data;
			this.children = children;
		}

		public IActionResult All([FromQuery] AllChildsQueryModel query)
		{
			var queryResult = this.children.All(query.Group,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				AllChildsQueryModel.ChildrenPerPage);

			var childrenGroup = this.children.AllChildGroups();

			query.TotalChildren = queryResult.TotalChildren;
			query.Children = queryResult.Children;
			query.Groups = childrenGroup;

			return View(query);

		}

		[Authorize]

		public IActionResult Add()
		{
			if (!this.UserIsParent())
			{
				return RedirectToAction(nameof(ParentsController.Become), "Parents");
			}

			return View(new AddChildFormModel
			{

				Groups = this.GetChildGroups(),
				ECAs = this.GetChildECAs(),
				Menus = this.GetChildMenus(),
				Trips = this.GetChildTrips()
			});

		}



		[HttpPost]
		[Authorize]

		public IActionResult Add(AddChildFormModel child)
		{
			var parentId = this.data
				.Parents
				.Where(p => p.UserId == this.User.GetId())
				.Select(p => p.Id)
				.FirstOrDefault();

			if (parentId == 0)
			{
				return RedirectToAction(nameof(ParentsController.Become), "Parents");
			}

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
				TripId = child.TripId,
				ParentId = parentId
			};


			this.data.Children.Add(childData);

			this.data.SaveChanges();

			return RedirectToAction(nameof(All));


		}

		private bool UserIsParent()
			=> this.data
				.Parents
				.Any(p => p.UserId == this.User.GetId());
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

