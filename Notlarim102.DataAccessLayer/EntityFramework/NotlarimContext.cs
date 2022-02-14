using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim102.Entity;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    public class NotlarimContext:DbContext
    {
        public DbSet<NotlarimUser> NotlarimUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotlarimUser>().MapToStoredProcedures();
            modelBuilder.Entity<Category>().MapToStoredProcedures();
            modelBuilder.Entity<Note>().MapToStoredProcedures();
            modelBuilder.Entity<Comment>().MapToStoredProcedures();
            modelBuilder.Entity<Liked>().MapToStoredProcedures();

            //modelBuilder.Entity<Category>()
            //    .HasMany(n => n.Notes)
            //    .WithRequired(n => n.Category)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Note>()
            //    .HasMany(n => n.Comments)
            //    .WithRequired(n => n.Note)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Note>()
            //    .HasMany(n => n.Likes)
            //    .WithRequired(n => n.Note)
            //    .WillCascadeOnDelete(true);

        }

        public NotlarimContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

    }
}
