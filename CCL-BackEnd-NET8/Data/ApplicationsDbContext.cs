using CCL_BackEnd_NET8.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CCL_BackEnd_NET8.Data
{
	public class ApplicationsDbContext: DbContext
	{
		public ApplicationsDbContext(DbContextOptions<ApplicationsDbContext> options ): base(options)
		{

		}

		// paso de los modelos
		public DbSet<Producto> Prodocto { get; set; }
    }
}

