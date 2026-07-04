using Microsoft.EntityFrameworkCore;

namespace EmojiQuiz;

static class Db
{
    private const string DbFileName      = "quiz.db";
    private const string SeedFileName    = "movies_ru_emoji.tsv";
    private const string DefaultCategory = "Фильмы";
    private const string NoCategory      = "Без категории";

    private static string DbPath   => Path.Combine(AppContext.BaseDirectory, DbFileName);
    private static string SeedPath => Path.Combine(AppContext.BaseDirectory, SeedFileName);

    public static void EnsureCreated()
    {
        using var ctx = new QuizContext(DbPath);
        ctx.Database.EnsureCreated();
    }

    public static int Count()
    {
        using var ctx = new QuizContext(DbPath);
        return ctx.Questions.Count();
    }

    public static void Add(string emoji, string answer, string category)
    {
        using var ctx = new QuizContext(DbPath);
        ctx.Questions.Add(new Question
        {
            Emoji    = emoji,
            Answer   = answer,
            Category = string.IsNullOrWhiteSpace(category) ? NoCategory : category
        });
        ctx.SaveChanges();
    }

    public static Question? GetRandom()
    {
        using var ctx = new QuizContext(DbPath);
        return ctx.Questions
            .OrderBy(q => EF.Functions.Random())
            .FirstOrDefault();
    }

    public static List<string> GetWrongAnswers(string correct, int count)
    {
        using var ctx = new QuizContext(DbPath);
        return ctx.Questions
            .Where(q => q.Answer != correct)
            .Select(q => q.Answer)
            .Distinct()
            .OrderBy(q => EF.Functions.Random())
            .Take(count)
            .ToList();
    }

    public static void SeedFromFile()
    {
        if (Count() > 0) return;
        if (!File.Exists(SeedPath)) return;

        using var ctx = new QuizContext(DbPath);
        foreach (var line in File.ReadLines(SeedPath).Skip(1))
        {
            var p = line.Split('\t');
            if (p.Length < 3) continue;
            ctx.Questions.Add(new Question
            {
                Emoji    = p[2].Trim(),
                Answer   = p[1].Trim(),
                Category = DefaultCategory
            });
        }
        ctx.SaveChanges();
    }
}