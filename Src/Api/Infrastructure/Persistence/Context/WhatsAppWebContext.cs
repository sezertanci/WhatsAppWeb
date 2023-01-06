using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Persistence.Context
{
    public class WhatsAppWebContext : DbContext
    {
        public WhatsAppWebContext()
        {

        }

        public WhatsAppWebContext(DbContextOptions options) : base(options)
        {

        }
        public const string DEFAULT_SCHEMA = "dbo";

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<UserChatMessage> UserChatMessages { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
             //   optionsBuilder.UseSqlServer("server=DESKTOP-R31O7VH\\SQLEXPRESS;Database=WhatsAppWeb;integrated security=true;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSave();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(i => (BaseEntity)i.Entity);

            PrepareAddedEntities(addEntities);
        }

        private static void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach(var entity in entities)
            {
                if(entity.CreatedDate == DateTime.MinValue)
                    entity.CreatedDate = DateTime.Now;
            }
        }
    }
}
