using Microsoft.EntityFrameworkCore;

namespace SportClub
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Member { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Sport> Sport { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=clubsportdb;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Member entity
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(255).HasColumnName("first_name");
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(255).HasColumnName("last_name");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20).HasColumnName("phone");

                // Removed MembershipId configuration
            });

            // Configure the Sport entity
            modelBuilder.Entity<Sport>(entity =>
            {
                entity.ToTable("sport");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255).HasColumnName("name");
                entity.Property(e => e.Description).HasMaxLength(1000).HasColumnName("description");
            });

            // Configure the Membership entity
            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("membership");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Type).IsRequired().HasMaxLength(255).HasColumnName("type");
                entity.Property(e => e.SportId).HasColumnName("sport_id");
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)").HasColumnName("price");
                entity.Property(e => e.DurationMonths).IsRequired().HasColumnName("duration_months");



            });

            // Configure the Subscription entity
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("subscription");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.MemberId).HasColumnName("member_id");
                entity.Property(e => e.MembershipId).HasColumnName("membership_id");

                entity.Property(e => e.StartDate).IsRequired().HasColumnName("start_date");
                entity.Property(e => e.EndDate).IsRequired().HasColumnName("end_date");
            });

            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255).HasColumnName("password");
            });
        }
    }
}
