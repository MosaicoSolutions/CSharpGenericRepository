using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Annotations;

namespace MosaicoSolutions.GenericRepository.Data.Contexts
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions options) : base(options)
        { }

        protected WriteDbContext()
        { }

        public override int SaveChanges() 
            => this.SaveChanges(acceptAllChangesOnSuccess: true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected virtual void BeforeSaveChanges()
        {
            CreatedAtAttribute();
            LastUpdatedAtAttribute();
            RowVersionAttribute();
        }

        private void CreatedAtAttribute()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                                                           .GetProperties()
                                                                           .Any(p => p.IsDefined(typeof(CreatedAtAttribute), true) && p.PropertyType == typeof(DateTime))))
            {
                var property = entry.Entity.GetType()
                                           .GetProperties()
                                           .FirstOrDefault(p => p.IsDefined(typeof(CreatedAtAttribute), true) && p.PropertyType == typeof(DateTime));

                var propertyName = property?.Name;
                var useUtc = property?.GetCustomAttributes(true)
                                      .OfType<CreatedAtAttribute>()
                                      .Select(a => a.UseUtc)
                                      .FirstOrDefault() ?? false;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(propertyName).CurrentValue = useUtc ? DateTime.UtcNow : DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Property(propertyName).IsModified = false;
                        break;
                }
            }
        }

        private void LastUpdatedAtAttribute()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                                                           .GetProperties()
                                                                           .Any(p => p.IsDefined(typeof(LastUpdatedAtAttribute), true) && p.PropertyType == typeof(DateTime?))))
            {
                var property = entry.Entity.GetType()
                                           .GetProperties()
                                           .FirstOrDefault(p => p.IsDefined(typeof(LastUpdatedAtAttribute), true) && p.PropertyType == typeof(DateTime?));

                var propertyName = property?.Name;
                var useUtc = property?.GetCustomAttributes(true)
                                      .OfType<LastUpdatedAtAttribute>()
                                      .Select(a => a.UseUtc)
                                      .FirstOrDefault() ?? false;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(propertyName).CurrentValue = null;
                        break;

                    case EntityState.Modified:
                        entry.Property(propertyName).CurrentValue = useUtc ? DateTime.UtcNow : DateTime.Now;
                        break;
                }
            }
        }

        private void RowVersionAttribute()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                                                           .GetProperties()
                                                                           .Any(p => p.IsDefined(typeof(RowVersionAttribute), true) && p.PropertyType == typeof(int))))
            {
                var propertyName = entry.Entity.GetType()
                                               .GetProperties()
                                               .FirstOrDefault(p => p.IsDefined(typeof(RowVersionAttribute), true) && p.PropertyType == typeof(int))
                                               ?.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(propertyName).CurrentValue = 1;
                        break;

                    case EntityState.Modified:
                        if (int.TryParse(entry.Property(propertyName).CurrentValue.ToString(), out int currentValue))
                            entry.Property(propertyName).CurrentValue = currentValue + 1;
                        break;
                }
            }
        }
    }
}