using EducationPortal.Data;
using EducationPortal.Start;
using System.Data.Entity;

namespace EducationPortal.Repository
{
    public class EducationPortalContext : DbContext
    {
        public EducationPortalContext() : base("EducationPortalDb") { }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Material> Materials { get; set; }

        public IDbSet<Video> Videos { get; set; }
        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Book> Books { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("Video");

            modelBuilder.Entity<Article>().ToTable("Article");

            modelBuilder.Entity<Book>().ToTable("Book");

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials)
                .WithMany(m => m.Courses)
                .Map(uc =>
                {
                    uc.MapLeftKey("CourseId");
                    uc.MapRightKey("MaterialId");
                    uc.ToTable("CourseMaterial");
                });

            modelBuilder.Entity<Material>()
                .HasMany(s => s.Skills)
                .WithMany(m => m.Materials)
                .Map(uc =>
                {
                    uc.MapLeftKey("MaterialId");
                    uc.MapRightKey("SkillId");
                    uc.ToTable("MaterialSkill");
                });

            modelBuilder.Entity<UserCourse>().HasKey(q =>
                new
                {
                    q.UserId,
                    q.CourseId
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserCourse)
                .WithRequired()
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.UserCourse)
                .WithRequired()
                .HasForeignKey(uc => uc.CourseId);

            modelBuilder.Entity<UserSkill>().HasKey(q => 
            new
            {
                q.UserId,
                q.SkillId
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSkill)
                .WithRequired()
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<Skill>()
                .HasMany(s => s.UserSkill)
                .WithRequired()
                .HasForeignKey(us => us.SkillId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
