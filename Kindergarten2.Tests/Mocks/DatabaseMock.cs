

using Kindergarten2.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kindergarten2.Test.Mocks
{
	public static class DatabaseMock
	{
		public static KindergartenDbContext Instance
		{
			get
			{
				var dbContextOptions = new DbContextOptionsBuilder<KindergartenDbContext>()
					.UseInMemoryDatabase(Guid.NewGuid().ToString())
					.Options;

				return new KindergartenDbContext(dbContextOptions);
			}

		}
	}
}
