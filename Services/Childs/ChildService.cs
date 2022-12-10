using Kindergarten2.Data;

namespace Kindergarten2.Services.Childs
{
	using Kindergarten2.Models.Childs;
	using System.Collections.Generic;
	using System.Linq;


	public class ChildService : IChildService
	{
		private readonly KindergartenDbContext data;

		public ChildService(KindergartenDbContext data)
			=> this.data = data;
		public ChildQueryServiceModel All(string group, string searchTerm, ChildSorting sorting, int currentPage, int childrenPerPage)
		{
			var childrenQuery = this.data.Children.AsQueryable();

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

			var children = childrenQuery
							.Skip((currentPage - 1) * childrenPerPage)
							.Take(childrenPerPage)
							.Select(c => new ChildServiceModel
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

			return new ChildQueryServiceModel
			{
				TotalChildren = totalChildren,
				ChildrenPerPage = childrenPerPage,
				Children = children,
				CurrentPage = currentPage
			};
		}

		public IEnumerable<string> AllChildGroups()
			=> this.data
				.Groups
				.Select(c => c.Name)
				.Distinct()
				.OrderBy(gn => gn)
				.ToList();


	}
}

//var user = repo.All<User>()
//			  .Where(u => u.Id == userId)
//			  .Include(u => u.Cart)
//			  .ThenInclude(c => c.Products)
//	  .FirstOrDefault();