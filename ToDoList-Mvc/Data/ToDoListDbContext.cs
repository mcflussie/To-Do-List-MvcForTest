using System;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList_Mvc.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
    : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskToDo> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskToDo>()
                .HasOne<User>()
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
