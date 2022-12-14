
namespace Kindergarten2.Services.Teachers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.Teachers;
	using System.Collections.Generic;
	using System.Linq;

	public class TeacherService : ITeacherService
	{
		private readonly KindergartenDbContext data;

		public TeacherService(KindergartenDbContext data)
			=> this.data = data;


		public TeacherQueryServiceModel All(
			string specialization,
			string searchTerm,
			TeacherSorting sorting,
			int currentPage,
			int teachersPerPage)
		{
			var teachersQuery = this.data.Teachers.AsQueryable();

			if (!string.IsNullOrWhiteSpace(specialization))
			{
				teachersQuery = teachersQuery.Where(t => t.Specialization == specialization);

			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				teachersQuery = teachersQuery.Where(t =>
				  (t.FirstName + " " + t.LastName).ToLower().Contains(searchTerm.ToLower()) ||
				  t.Introduction.ToLower().Contains(searchTerm.ToLower()));
			}

			teachersQuery = sorting switch
			{
				TeacherSorting.Experience => teachersQuery.OrderByDescending(t => t.Experience),
				TeacherSorting.FirstNameAndLastName => teachersQuery.OrderBy(t => t.FirstName).ThenBy(t => t.LastName),
				TeacherSorting.DateCreated or _ => teachersQuery.OrderByDescending(t => t.Id)
			};

			var totalTeachers = teachersQuery.Count();

			var teachers = teachersQuery
				.Skip((currentPage - 1) * teachersPerPage)
				.Take(teachersPerPage)
				.Select(t => new TeacherServiceModel
				{
					Id = t.Id,
					FirstName = t.FirstName,
					LastName = t.LastName,
					Experience = t.Experience,
					Specialization = t.Specialization,
					Introduction = t.Introduction,
					Group = t.Group.Name,
					ImageUrl = t.ImageUrl
				})
				.ToList();

			return new TeacherQueryServiceModel
			{
				TotalTeachers = totalTeachers,
				Teachers = teachers,
				TeachersPerPage = teachersPerPage,
				CurrentPage = currentPage

			};
		}

		public IEnumerable<string> AllTeacherSpecializations()
				=> this.data
						.Teachers
						.Select(t => t.Specialization)
						.Distinct()
						.OrderBy(spec => spec)
						.ToList();

		public int Create(int groupId,
			string firstName,
			string lastName,
			int experience,
			string specialization,
			string introduction,
			string imageUrl)
		{
			var teacherData = new Teacher
			{
				GroupId = groupId,
				FirstName = firstName,
				LastName = lastName,
				Experience = experience,
				Specialization = specialization,
				Introduction = introduction,
				ImageUrl = imageUrl

			};

			this.data.Teachers.Add(teacherData);

			this.data.SaveChanges();

			return teacherData.Id;
		}

		public TeacherDetailsServiceModel Details(int id)
			=> this.data
			.Teachers.Where(t => t.Id == id)
			.Select(t => new TeacherDetailsServiceModel
			{
				Id = t.Id,
				FirstName = t.FirstName,
				LastName = t.LastName,
				Specialization = t.Specialization,
				Introduction = t.Introduction,
				Experience = t.Experience,
				Group = t.Group.Name,
				GroupId = t.GroupId,
				ImageUrl = t.ImageUrl
			}).FirstOrDefault();

		public bool Edit(int id,
			string firstName,
				string lastName,
				string specialization,
				string introduction,
				int experience,
				string imageUrl,
				int groupId)
		{
			var teacherData = this.data.Teachers.Find(id);

			if (teacherData == null)
			{
				return false;
			}

			teacherData.FirstName = firstName;
			teacherData.LastName = lastName;
			teacherData.Specialization = specialization;
			teacherData.Introduction = introduction;
			teacherData.Experience = experience;
			teacherData.GroupId = groupId;
			teacherData.ImageUrl = imageUrl;

			this.data.SaveChanges();

			return true;
		}

		public IEnumerable<TeacherGroupServiceModel> GetTeacherGroups()
		=> this.data
			.Groups
			.Select(t => new TeacherGroupServiceModel
			{
				Id = t.Id,
				Name = t.Name,

			}).ToList();

		public bool GroupExist(int groupId)
		=> this.data.Groups.Any(g => g.Id == groupId);

		public List<LatestTeacherServiceModel> Latest()
			=> this.data
					.Teachers
					.OrderBy(t => t.FirstName)
					.ThenBy(t => t.LastName)
					.Select(t => new LatestTeacherServiceModel
					{
						Id = t.Id,
						FirstName = t.FirstName,
						LastName = t.LastName,
						Experience = t.Experience,
						Specialization = t.Specialization,
						Introduction = t.Introduction,
						ImageUrl = t.ImageUrl
					})
					.Take(3)
					.ToList();
	}
}
