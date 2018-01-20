namespace Billiards.API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BilliardsModel : DbContext
    {
        public BilliardsModel()
            : base("name=BilliardsModel")
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameUser> GameUsers { get; set; }
        public virtual DbSet<HandicapMatrix> HandicapMatrices { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.GameUsers)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Match>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Match)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Games)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.WinnerUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.GameUsers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User1Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Matches1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.User2Id)
                .WillCascadeOnDelete(false);
        }
    }
}
