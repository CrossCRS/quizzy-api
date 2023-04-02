using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Entities;

namespace QuizzyAPI.Data; 

public class QuizzyContext : DbContext {
    public DbSet<Quiz> Quizzes { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;

    public QuizzyContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Quiz>()
            .Property(q => q.HideAnswers)
            .HasDefaultValue(false);
        
        modelBuilder.Entity<Question>()
            .Property(q => q.Points)
            .HasDefaultValue(1);

        // Setup test data
        modelBuilder.Entity<Quiz>().HasData(
            new Quiz() {
                Id = 1, Title = "Test Quiz", Description = "A test quiz", HideAnswers = false
            },
            new Quiz() {
                Id = 2, Title = "Test Quiz No Answers", Description = "A test quiz with hidden answers", HideAnswers = true
            });
        
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