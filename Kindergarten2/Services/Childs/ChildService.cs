using Kindergarten2.Data;

namespace Kindergarten2.Services.Childs
{
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Childs;
	using System.Collections.Generic;
	using System.Linq;


	public class ChildService : IChildService
	{
		private readonly KindergartenDbContext data;

		public ChildService(KindergartenDbContext data)
			=> this.data = data;
		public ChildQueryServiceModel All(string group = null,
			string searchTerm = null,
			ChildSorting sorting = ChildSorting.DateCreated,
			int currentPage = 1,
			int childrenPerPage = int.MaxValue,
			bool confirmedOnly = true)
		{
			var childrenQuery = this.data.Children.Where(c => !confirmedOnly || c.IsConfirmed);

			if (!string.IsNullOrWhiteSpace(group))
			{
				childrenQuery = childrenQuery.Where(x => x.Group.Name == group);
			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				childrenQuery = childrenQuery.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(searchTerm.ToLower()));
			}

			childrenQuery = sorting switch
			{
				ChildSorting.Age => childrenQuery.OrderBy(c => c.Age),
				ChildSorting.FirstNameAndLastName => childrenQuery.OrderBy(c => c.FirstName).ThenBy(c => c.LastName),
				ChildSorting.DateCreated or _ => childrenQuery.OrderByDescending(c => c.Id)

			};

			var totalChildren = childrenQuery.Count();

			var children = GetChildren(childrenQuery
							.Skip((currentPage - 1) * childrenPerPage)
							.Take(childrenPerPage));

			return new ChildQueryServiceModel
			{
				TotalChildren = totalChildren,
				ChildrenPerPage = childrenPerPage,
				Children = children,
				CurrentPage = currentPage
			};
		}

		public int Create(string firstName,
			string middleName,
			string lastName,
			int age,
			int ECAId,
			int menuId,
			int groupId,
			int tripId,
			int parentId)
		{
			var childData = new Child
			{
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Age = age,
				ECAId = ECAId,
				MenuId = menuId,
				GroupId = groupId,
				TripId = tripId,
				ParentId = parentId,
				IsConfirmed = false
			};


			this.data.Children.Add(childData);

			this.data.SaveChanges();

			return childData.Id;
		}

		public bool Edit(
			int id,
			string firstName,
			string middleName,
			string lastName,
			int age,
			int ECAId,
			int menuId,
			int groupId,
			int tripId,
			bool isConfirmed)
		{
			var childData = this.data.Children.Find(id);

			if (childData == null)
			{
				return false;
			}

			childData.FirstName = firstName;
			childData.MiddleName = middleName;
			childData.LastName = lastName;
			childData.Age = age;
			childData.ECAId = ECAId;
			childData.MenuId = menuId;
			childData.GroupId = groupId;
			childData.TripId = tripId;
			childData.IsConfirmed = isConfirmed;

			this.data.SaveChanges();

			return true;
		}


		public ChildDetailsServiceModel Deatails(int id)
			=> this.data
			.Children
			.Where(c => c.Id == id)
			.Select(c => new ChildDetailsServiceModel
			{
				Id = c.Id,
				FirstName = c.FirstName,
				LastName = c.LastName,
				MiddleName = c.MiddleName,
				Age = c.Age,
				GroupId = c.GroupId,
				GroupName = c.Group.Name,
				ECAName = c.ECA.Title,
				TripName = c.Trip.PlaceToVisit,
				MenuName = c.Menu.MenuType,
				ParentFirstName = c.Parent.FirstName,
				ParentLastName = c.Parent.LastName,
				ParentId = c.ParentId,
				UserId = c.Parent.UserId

			}).FirstOrDefault();

		public IEnumerable<string> AllChildGroups()
			=> this.data
				.Groups
				.Select(c => c.Name)
				.Distinct()
				.OrderBy(gn => gn)
				.ToList();

		public IEnumerable<ChildServiceModel> ByUser(string userId)
			=> GetChildren(this.data
			.Children
			.Where(c => c.Parent.UserId == userId));

		public bool ChildIsByParent(int childId, int parentId)
			=> this.data
			.Children
			.Any(c => c.Id == childId && c.ParentId == parentId);


		private static IEnumerable<ChildServiceModel> GetChildren(IQueryable<Child> childQuery)
			=> childQuery
			.Select(c => new ChildServiceModel
			{
				Id = c.Id,
				FirstName = c.FirstName,
				LastName = c.LastName,
				MiddleName = c.MiddleName,
				Age = c.Age,
				GroupName = c.Group.Name,
				ECAName = c.ECA.Title,
				TripName = c.Trip.PlaceToVisit,
				MenuName = c.Menu.MenuType,
				IsConfirmed = c.IsConfirmed,

			}).ToList();

		public IEnumerable<ChildGroupServiceModel> GetChildGroups()
		=> this.data
			.Groups.Select(c => new ChildGroupServiceModel
			{
				Id = c.Id,
				Name = c.Name

			}).ToList();

		public bool GroupExist(int groupId)
			=> this.data
			.Groups
			.Any(g => g.Id == groupId);

		public IEnumerable<ChildECAServiceModel> GetChildECAs()
		=> this.data
			.ECAs.Select(c => new ChildECAServiceModel
			{
				Id = c.Id,
				Title = c.Title

			}).ToList();

		public bool ECAExist(int ECAId)
			=> this.data
			.ECAs
			.Any(e => e.Id == ECAId);


		public IEnumerable<ChildMenuServiceModel> GetChildMenus()
		=> this.data
			.Menus.Select(c => new ChildMenuServiceModel
			{
				Id = c.Id,
				MenuType = c.MenuType

			}).ToList();

		public bool MenuExist(int menuId)
		=> this.data
			.Menus
			.Any(e => e.Id == menuId);

		public IEnumerable<ChildTripServiceModels> GetChildTrips()
		=> this.data
			.Trips.Select(c => new ChildTripServiceModels
			{
				Id = c.Id,
				PlaceToVisit = c.PlaceToVisit
			})
			.ToList();

		public bool TripExist(int tripId)
		=> this.data
			.Trips
			.Any(e => e.Id == tripId);

		public void Delete(int id)
		{
			var childToDelete = this.data.Children.Find(id);

			this.data.Children.Remove(childToDelete);

			this.data.SaveChanges();
		}

		public void ChangeVisibility(int childId)
		{
			var child = this.data.Children.Find(childId);

			child.IsConfirmed = !child.IsConfirmed;

			this.data.SaveChanges();
		}
	}
}

