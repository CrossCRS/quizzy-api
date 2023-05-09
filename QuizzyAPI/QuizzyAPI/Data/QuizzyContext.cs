using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Entities;
using QuizzyAPI.Identity;

namespace QuizzyAPI.Data;

public class QuizzyContext : IdentityDbContext<QuizzyUser, QuizzyRole, int> {
    public DbSet<Quiz> Quizzes { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;

    public QuizzyContext(DbContextOptions<QuizzyContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Setup Identity test data
        var hasher = new PasswordHasher<QuizzyUser>();

        // Roles
        modelBuilder.Entity<QuizzyRole>().HasData(
            new QuizzyRole {
                Id = 1,
                Name = Constants.Roles.ADMINISTRATOR,
                NormalizedName = Constants.Roles.ADMINISTRATOR.ToUpper()
            },
            new QuizzyRole {
                Id = 2,
                Name = Constants.Roles.USER,
                NormalizedName = Constants.Roles.USER.ToUpper()
            },
            new QuizzyRole {
                Id = 3,
                Name = Constants.Roles.DEMO,
                NormalizedName = Constants.Roles.DEMO.ToUpper()
            });

        // Users
        modelBuilder.Entity<QuizzyUser>().HasData(
            new QuizzyUser {
                Id = 1,
                UserName = "Tester",
                NormalizedUserName = "TESTER",
                Email = "tester@example.com",
                NormalizedEmail = "TESTER@EXAMPLE.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = new Guid().ToString(),
            });

        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int> {
                RoleId = 3,
                UserId = 1
            });

        // Setup Quizzy test data
        modelBuilder.Entity<Quiz>()
            .Property(q => q.HideAnswers)
            .HasDefaultValue(false);
        
        modelBuilder.Entity<Question>()
            .Property(q => q.Points)
            .HasDefaultValue(1);

        modelBuilder.Entity<Quiz>().HasData(
            new Quiz() {
                Id = 1, Title = "Test Quiz", Description = "A test quiz", AuthorId = 1, HideAnswers = false
            },
            new Quiz() {
                Id = 2, Title = "Test Quiz No Answers", Description = "A test quiz with hidden answers", AuthorId = 1, HideAnswers = true
            });

        for (var i = 3; i < 53; i++) {
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz() {
                    Id = i, Title = $"Empty Quiz #{i - 2}", Description = "An empty quiz", AuthorId = 1, HideAnswers = false
                });
        }
        
        modelBuilder.Entity<Question>().HasData(
            new Question() {
                Id = 1, QuizId = 1, Text = "Question number 1", Points = 1,
            },
            new Question() {
                Id = 2, QuizId = 1, Text = "Question number 2", Points = 1,
            },
            new Question() {
                Id = 3, QuizId = 1, Text = "Question number 3", Points = 1,
            },
            new Question() {
                Id = 4, QuizId = 2, Text = "Quiz 2 Question", Points = 1,
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
                Id = 7, QuestionId = 2, Text = "Incorrect answer 3", IsCorrect = false
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