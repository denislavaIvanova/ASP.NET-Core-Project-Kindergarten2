﻿namespace Kindergarten2.Data.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations;

	public class User : IdentityUser
	{
		[MaxLength(40)]
		public string FullName { get; set; }
	}
}
