using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASPNETCore5Demo.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        public override int SaveChanges()
        {
            SaveChangeHandler(this);
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SaveChangeHandler(this);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SaveChangeHandler(this);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangeHandler(this);
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SaveChangeHandler(ContosoUniversityContext context)
        {
            var entities = context.ChangeTracker.Entries();

            foreach (var entity in entities)
            {
                var entityType = entity.Entity.GetType();
                var props = entityType.GetProperties();

                if (entity.State == EntityState.Modified)
                {
                    // add DateModified time  if exist DateModified column
                    if (props.Any(p => p.Name.Equals("DateModified", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        entity.CurrentValues.SetValues(new { DateModified = DateTime.Now });
                    }
                }
                else if (entity.State == EntityState.Deleted)
                {
                    // update the flag  if exist IsDelete column
                    if (props.Any(p => p.Name.Equals("IsDeleted", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        entity.State = EntityState.Modified;
                        entity.CurrentValues.SetValues(new { IsDeleted = true });
                    }
                }
            }
        }
    }
}
