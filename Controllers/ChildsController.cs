

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Infrastructure;
	using Kindergarten2.Models.Childs;
	using Kindergarten2.Services.Childs;
	using Kindergarten2.Services.Parents;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class ChildsController : Controller
	{
		private readonly IParentService parents;
		private readonly IChildService children;

		public ChildsController(IChildService children,
			IParentService parents)
		{
			this.children = children;
			this.parents = parents;
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

		public IActionResult Mine()
		{
			var myChildren = this.children.ByUser(this.User.Id());

			return View(myChildren);
		}

		public IActionResult Details(int id, string information)
		{
			var child = this.children.Deatails(id);

			if (information != child.GetInformation())
			{
				return BadRequest();
			}

			return View(child);

		}

		//[Authorize]
		//public ActionResult Delete(int id)
		//{
		//	var child = this.children.Deatails(id);

		//	return View(child);
		//}

		//// POST: /Movies/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, ChildFormModel child)
		//{
		//	var userId = this.User.Id();

		//	if (!this.parents.IsParent(userId) && !User.IsAdmin())
		//	{
		//		return RedirectToAction(nameof(ParentsController.Become), "Parents");
		//	}

		//	var childData = this.children.Deatails(id);

		//	if (childData.UserId != userId && !User.IsAdmin())
		//	{
		//		return Unauthorized();
		//	}

		//	this.children.Delete(id);
		//	return RedirectToAction(nameof(Mine));
		//}

		[Authorize]

		public IActionResult Add()
		{
			if (!this.parents.IsParent(this.User.Id()))
			{
				return RedirectToAction(nameof(ParentsController.Become), "Parents");
			}

			return View(new ChildFormModel
			{

				Groups = this.children.GetChildGroups(),
				ECAs = this.children.GetChildECAs(),
				Menus = this.children.GetChildMenus(),
				Trips = this.children.GetChildTrips()
			});

		}

		[HttpPost]
		[Authorize]

		public IActionResult Add(ChildFormModel child)
		{
			var parentId = this.parents.IdByUser(this.User.Id());

			if (parentId == 0)
			{
				return RedirectToAction(nameof(ParentsController.Become), "Parents");
			}

			if (!this.children.GroupExist(child.GroupId)
				|| !this.children.ECAExist(child.ECAId)
				|| !this.children.MenuExist(child.MenuId)
				|| !this.children.TripExist(child.TripId))
			{
				this.ModelState.AddModelError(nameof(child.GroupId), "Group does not exist!");
				this.ModelState.AddModelError(nameof(child.ECAId), "Extracurricular activity does not exist!");
				this.ModelState.AddModelError(nameof(child.MenuId), "Menu does not exist!");
				this.ModelState.AddModelError(nameof(child.TripId), "Trip does not exist!");
			}



			if (!ModelState.IsValid)
			{
				child.Groups = this.children.GetChildGroups();
				child.ECAs = this.children.GetChildECAs();
				child.Trips = this.children.GetChildTrips();
				child.Menus = this.children.GetChildMenus();

				return View(child);
			}

			var childId = this.children.Create(child.FirstName,
				child.MiddleName,
				child.LastName,
				child.Age,
				child.ECAId,
				child.MenuId,
				child.GroupId,
				child.TripId,
				parentId);

			return RedirectToAction(nameof(Details));
		}

		[Authorize]
		public IActionResult Edit(int id)
		{
			var userId = this.User.Id();

			if (!this.parents.IsParent(userId) && !User.IsAdmin())
			{
				return RedirectToAction(nameof(ParentsController.Become), "Parents");
			}

			var child = this.children.Deatails(id);

			if (child.UserId != userId && !User.IsAdmin())
			{
				return Unauthorized();
			}

			return View(new ChildFormModel
			{
				FirstName = child.FirstName,
				MiddleName = child.MiddleName,
				LastName = child.LastName,
				Age = child.Age,
				GroupId = child.GroupId,
				MenuId = child.MenuId,
				TripId = child.TripId,
				ECAId = child.ECAId,
				Groups = this.children.GetChildGroups(),
				ECAs = this.children.GetChildECAs(),
				Menus = this.children.GetChildMenus(),
				Trips = this.children.GetChildTrips()
			});
		}

		[Authorize]
		[HttpPost]

		public IActionResult Edit(int id, ChildFormModel child)
		{
			var parentId = this.parents.IdByUser(this.User.Id());

			if (parentId == 0 && !User.IsAdmin())
			{
				return RedirectToAction(nameof(ParentsController.Become), "Parents");
			}

			if (!this.children.GroupExist(child.GroupId)
				|| !this.children.ECAExist(child.ECAId)
				|| !this.children.MenuExist(child.MenuId)
				|| !this.children.TripExist(child.TripId))
			{
				this.ModelState.AddModelError(nameof(child.GroupId), "Group does not exist!");
				this.ModelState.AddModelError(nameof(child.ECAId), "Extracurricular activity does not exist!");
				this.ModelState.AddModelError(nameof(child.MenuId), "Menu does not exist!");
				this.ModelState.AddModelError(nameof(child.TripId), "Trip does not exist!");
			}



			if (!ModelState.IsValid)
			{
				child.Groups = this.children.GetChildGroups();
				child.ECAs = this.children.GetChildECAs();
				child.Trips = this.children.GetChildTrips();
				child.Menus = this.children.GetChildMenus();

				return View(child);
			}

			if (!this.children.ChildIsByParent(id, parentId) && !User.IsAdmin())
			{
				return Unauthorized();
			}

			var childIsEdited = this.children.Edit(
					id,
					child.FirstName,
					child.MiddleName,
					child.LastName,
					child.Age,
					child.ECAId,
					child.MenuId,
					child.GroupId,
					child.TripId);

			return RedirectToAction(nameof(All));

		}

	}
}

