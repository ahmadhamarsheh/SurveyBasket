

using SurveyBasket.API.Persistence.EntitiesConfiguration;

namespace SurveyBasket.API.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) 
        : IdentityDbContext<ApplicationUser>(options)
    {   
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PollConfiguration());

            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(x => x.GetForeignKeys())
                .Where(x => x.DeleteBehavior == DeleteBehavior.Cascade && !x.IsOwnership);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (var entry in entries)
            {
                var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedById).CurrentValue = currentUserId;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
