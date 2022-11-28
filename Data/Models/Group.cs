
namespace Kindergarten2.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	public class Group
	{
		public int Id { get; init; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Range(4, 20)]
		public int ChildrenCount { get; set; }

		public int CategoryId { get; set; }

		public Category Category { get; init; }

		public IEnumerable<Child> Children { get; init; } = new List<Child>();

		public IEnumerable<Teacher> Teachers { get; init; } = new List<Teacher>();
	}
}
