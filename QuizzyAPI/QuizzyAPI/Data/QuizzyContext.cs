using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Entities;
using QuizzyAPI.Identity;

namespace QuizzyAPI.Data;

public class QuizzyContext : IdentityDbContext<QuizzyUser, QuizzyRole, Guid> {
    public DbSet<Quiz> Quizzes { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;

    public QuizzyContext(DbContextOptions<QuizzyContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // TODO: Add users/roles using user manager
        
        // Setup Identity test data
        var hasher = new PasswordHasher<QuizzyUser>();

        // Roles
        modelBuilder.Entity<QuizzyRole>().HasData(
            new QuizzyRole {
                Id = Guid.Parse("6f412d88-fe9c-46d2-8aec-203b2daea4b8"),
                Name = Constants.Roles.ADMINISTRATOR,
                NormalizedName = Constants.Roles.ADMINISTRATOR.ToUpper()
            },
            new QuizzyRole {
                Id = Guid.Parse("8291235a-5fce-483a-8ffc-1f0ca833dc13"),
                Name = Constants.Roles.USER,
                NormalizedName = Constants.Roles.USER.ToUpper()
            },
            new QuizzyRole {
                Id = Guid.Parse("a0ce7899-3bb3-4b19-b0d8-136e2cbe3663"),
                Name = Constants.Roles.DEMO,
                NormalizedName = Constants.Roles.DEMO.ToUpper()
            });

        // Users
        modelBuilder.Entity<QuizzyUser>().HasData(
            new QuizzyUser {
                Id = Guid.Parse("7fe77cdd-aef2-4a34-a93c-fe84e2eeecfa"),
                UserName = "Tester",
                NormalizedUserName = "TESTER",
                Email = "tester@example.com",
                NormalizedEmail = "TESTER@EXAMPLE.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = new Guid().ToString(),
            },
            new QuizzyUser {
                Id = Guid.Parse("b28706a2-c61f-4067-a3f2-03e0b967fb32"),
                UserName = "AnotherTester",
                NormalizedUserName = "ANOTHERTESTER",
                Email = "anothertester@example.com",
                NormalizedEmail = "ANOTHERTESTER@EXAMPLE.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = new Guid().ToString(),
            });
        
        // User guids should be auto-generated
        modelBuilder.Entity<QuizzyUser>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> {
                RoleId = Guid.Parse("a0ce7899-3bb3-4b19-b0d8-136e2cbe3663"),
                UserId = Guid.Parse("7fe77cdd-aef2-4a34-a93c-fe84e2eeecfa")
            },
            new IdentityUserRole<Guid> {
                RoleId = Guid.Parse("a0ce7899-3bb3-4b19-b0d8-136e2cbe3663"),
                UserId = Guid.Parse("b28706a2-c61f-4067-a3f2-03e0b967fb32")
            });

        // Setup Quizzy test data
        modelBuilder.Entity<Quiz>()
            .Property(q => q.HideAnswers)
            .HasDefaultValue(false);

        modelBuilder.Entity<Question>()
            .Property(q => q.Points)
            .HasDefaultValue(1);
        
        // Quiz guids should be auto-generated
        modelBuilder.Entity<Quiz>()
            .Property(q => q.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz() {
                Id = Guid.Parse("048eeefd-2fcf-49b2-9b17-a040b67be06c"), Title = "Test Quiz", Description = "A test quiz", AuthorId = Guid.Parse("7fe77cdd-aef2-4a34-a93c-fe84e2eeecfa"), HideAnswers = false
            },
            new Quiz() {
                Id = Guid.Parse("3d387bcf-3564-4fb3-8cc2-2e1e5a3a8e81"), Title = "Test Quiz No Answers", Description = "A test quiz with hidden answers", AuthorId = Guid.Parse("7fe77cdd-aef2-4a34-a93c-fe84e2eeecfa"), HideAnswers = true
            });

        for (var i = 3; i < 53; i++) {
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz() {
                    Id = Guid.NewGuid(), Title = $"Empty Quiz #{i - 2}", Description = "An empty quiz", AuthorId = Guid.Parse("b28706a2-c61f-4067-a3f2-03e0b967fb32"), HideAnswers = false
                });
        }

        modelBuilder.Entity<Question>().HasData(
            new Question() {
                Id = 1, QuizId = Guid.Parse("048eeefd-2fcf-49b2-9b17-a040b67be06c"), Text = "Question number 1", Points = 1,
            },
            new Question() {
                Id = 2, QuizId = Guid.Parse("048eeefd-2fcf-49b2-9b17-a040b67be06c"), Text = "Question number 2", Points = 1,
            },
            new Question() {
                Id = 3, QuizId = Guid.Parse("048eeefd-2fcf-49b2-9b17-a040b67be06c"), Text = "Question number 3", Points = 1,
            },
            new Question() {
                Id = 4, QuizId = Guid.Parse("3d387bcf-3564-4fb3-8cc2-2e1e5a3a8e81"), Text = "Quiz 2 Question", Points = 1,
            });

        modelBuilder.Entity<Answer>().HasData(
            new Answer() {
                Id = 1, QuestionId = 1, Text = "Incorrect answer 1", IsCorrect = false
            },
            new Answer() {
                Id = 2, QuestionId = 1, Text = "Incorrect answer 2", IsCorrect = false
            },
            new Answer() {
                Id = 3, QuestionId = 1, Text = "Incorrect answer 3", IsCorrect = false
            },
            new Answer() {
                Id = 4, QuestionId = 1, Text = "Correct answer", IsCorrect = true
            },

            new Answer() {
                Id = 5, QuestionId = 2, Text = "Incorrect answer 1", IsCorrect = false
            },
            new Answer() {
                Id = 6, QuestionId = 2, Text = "Correct answer", IsCorrect = true
            },
            new Answer() {
                Id = 7, QuestionId = 2, Text = "Incorrect answer 2", IsCorrect = false
            },
            new Answer() {
                Id = 8, QuestionId = 2, Text = "Incorrect answer 3", IsCorrect = false
            },

            new Answer() {
                Id = 9, QuestionId = 3, Text = "Correct answer", IsCorrect = true
            },
            new Answer() {
                Id = 10, QuestionId = 3, Text = "Incorrect answer 1", IsCorrect = false
            },
            new Answer() {
                Id = 11, QuestionId = 3, Text = "Incorrect answer 2", IsCorrect = false
            },
            new Answer() {
                Id = 12, QuestionId = 3, Text = "Incorrect answer 3", IsCorrect = false
            },

            new Answer() {
                Id = 13, QuestionId = 4, Text = "Incorrect answer", IsCorrect = false
            },
            new Answer() {
                Id = 14, QuestionId = 4, Text = "Correct answer", IsCorrect = true
            });

        base.OnModelCreating(modelBuilder);
    }
}
