using FreKE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreKE.Persistence.Contexts
{
    public class FreKEDbContext : DbContext
    {
        public FreKEDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<PriceOffer> priceoffers { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<JobCategory> jobcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User - Job (1:N)
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(u => u.PostedJobs)
                .WithOne(uj => uj.Employer)
                .HasForeignKey(uj => uj.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);

            //User - PriceOffer (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.PriceOffers)
                .WithOne(po => po.Worker)
                .HasForeignKey(po => po.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            //User - Comment (1:N) Given
            modelBuilder.Entity<User>()
                .HasMany(u => u.CommentsGiven)
                .WithOne(uc => uc.CommentedBy)
                .HasForeignKey(uc => uc.CommentedById)
                .OnDelete(DeleteBehavior.Cascade);
            //User - Comment (1:N) Received
            modelBuilder.Entity<User>()
                .HasMany(u => u.CommentsReceived)
                .WithOne(uc => uc.CommentedTarget)
                .HasForeignKey(uc => uc.CommentedTargetId)
                .OnDelete(DeleteBehavior.Cascade);

            //User - Like (1:N) Given
            modelBuilder.Entity<User>()
                .HasMany(u => u.LikesGiven)
                .WithOne(ul => ul.LikedBy)
                .HasForeignKey(ul => ul.LikedById)
                .OnDelete(DeleteBehavior.Cascade);
            //User - Like (1:N) Received
            modelBuilder.Entity<User>()
                .HasMany(u => u.LikesReceived)
                .WithOne(ul => ul.LikedUser)
                .HasForeignKey(ul => ul.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Job - JobCategory (1:N)
            modelBuilder.Entity<JobCategory>()
                .HasMany(j => j.Jobs)
                .WithOne(jj => jj.JobCategory)
                .HasForeignKey(jj => jj.JobCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //Job - PriceOffer (1:N)
            modelBuilder.Entity<Job>()
                .HasMany(j => j.PriceOffers)
                .WithOne(jp => jp.Job)
                .HasForeignKey(jp => jp.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
