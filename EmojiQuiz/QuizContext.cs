using Microsoft.EntityFrameworkCore;

namespace EmojiQuiz;

class QuizContext : DbContext
{
    private readonly string _dbPath;

    public QuizContext(string dbPath)
    {
        _dbPath = dbPath;
    }

    public DbSet<Question> Questions => Set<Question>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");
}
