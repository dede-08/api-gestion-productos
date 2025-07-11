﻿using Microsoft.EntityFrameworkCore;
using api_gestion_productos.Models;

namespace api_gestion_productos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        
    }
}
