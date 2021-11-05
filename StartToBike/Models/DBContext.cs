using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StartToBike.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountCatalog> AccountCatalog { get; set; }
        public virtual DbSet<Challenge> Challenge { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<Quest> Quest { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<AccountTour> AccountTour { get; set; }
        public virtual DbSet<Friend> Friend { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Friend)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.Friend1Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Friend1)
                .WithRequired(e => e.Account1)
                .HasForeignKey(e => e.Friend2Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountTour)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Challenge)
                .WithMany(e => e.Account)
                .Map(m => m.ToTable("AccountChallenge").MapLeftKey("AccountId").MapRightKey("ChallengeId"));

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Quest)
                .WithMany(e => e.Account)
                .Map(m => m.ToTable("AccountQuest").MapLeftKey("AccountId").MapRightKey("QuestId"));

            modelBuilder.Entity<AccountCatalog>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.AccountCatalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Challenge>()
                .HasMany(e => e.Tour)
                .WithMany(e => e.Challenge)
                .Map(m => m.ToTable("ChallengeTour").MapLeftKey("ChallengeId").MapRightKey("TourId"));

            modelBuilder.Entity<Performance>()
                .HasMany(e => e.AccountTour)
                .WithRequired(e => e.Performance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quest>()
                .HasMany(e => e.Tour)
                .WithMany(e => e.Quest)
                .Map(m => m.ToTable("QuestTour").MapLeftKey("QuestId").MapRightKey("TourId"));

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.AccountTour)
                .WithRequired(e => e.Tour)
                .HasForeignKey(e => e.PerformanceId)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<StartToBike.Models.Training> Trainings { get; set; }

        public System.Data.Entity.DbSet<StartToBike.Models.TrainingAccount> TrainingAccounts { get; set; }
    }
}
