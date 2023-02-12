﻿using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
	public class Context:IdentityDbContext<AppUser,AppRole,int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=(LocalDB)\\MSSQLLocalDB;database=TraversalDB;integrated security=true;");
		}
		public DbSet<About> Abouts { get; set; }
		public DbSet<AboutCare> AboutCares { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Destination> Destinations { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<Guide> Guides { get; set; }
		public DbSet<SubAbout> SubAbouts { get; set; }
		public DbSet<Subscribe> Subscribes { get; set; }
		public DbSet<Testimonial> Testimonials { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
