using Microsoft.EntityFrameworkCore;

namespace EmojiQuiz;

class QuizContext : DbContext
{
    public DbSet<Question> Questions => Set<Question>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=quiz.db");
    }
}
