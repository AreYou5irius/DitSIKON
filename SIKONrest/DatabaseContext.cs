namespace SIKONrest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountToEvent> AccountToEvent { get; set; }
        public virtual DbSet<Autist> Autist { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<TimeToEvent> TimeToEvent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Event)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.Owner_ID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Question)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.Account_ID);

            modelBuilder.Entity<AccountToEvent>()
                .Property(e => e.Account_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Autist>()
                .Property(e => e.ContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<Autist>()
                .HasMany(e => e.Account)
                .WithOptional(e => e.Autist)
                .HasForeignKey(e => e.Autist_ID);

            modelBuilder.Entity<Event>()
                .Property(e => e.Owner_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Speaker)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.AccountToEvent)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.Event_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Question)
                .WithOptional(e => e.Event)
                .HasForeignKey(e => e.Event_ID);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.TimeToEvent)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.Event_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Account_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Event)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.Room_ID);
        }
    }
}
