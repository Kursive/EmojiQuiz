using Microsoft.EntityFrameworkCore;

namespace EmojiQuiz;

static class Db
{
    // Создать базу и таблицу
    public static void EnsureCreated()
    {
        using var ctx = new QuizContext();
        ctx.Database.EnsureCreated();
    }

    // Сколько вопросов в базе
    public static int Count()
    {
        using var ctx = new QuizContext();
        return ctx.Questions.Count();
    }

    
    public static void Add(string emoji, string answer, string category)
    {
        using var ctx = new QuizContext();
        ctx.Questions.Add(new Question { Emoji = emoji, Answer = answer, Category = category ?? "" });
        ctx.SaveChanges();
    }

    
    public static Question? GetRandom()
    {
        using var ctx = new QuizContext();
        return ctx.Questions
            .OrderBy(q => EF.Functions.Random())
            .FirstOrDefault();
    }

    //  из  категории
    public static Question? GetRandom(string category)
    {
        using var ctx = new QuizContext();
        return ctx.Questions
            .Where(q => q.Category == category)
            .OrderBy(q => EF.Functions.Random())
            .FirstOrDefault();
    }

    // Несколько случайных НЕправильных ответов 
    public static List<string> GetWrongAnswers(string correct, int count)
    {
        using var ctx = new QuizContext();
        return ctx.Questions
            .Where(q => q.Answer != correct)
            .OrderBy(q => EF.Functions.Random())
            .Select(q => q.Answer)
            .Take(count)
            .ToList();
    }

    
    public static List<string> GetCategories()
    {
        using var ctx = new QuizContext();
        return ctx.Questions
            .Select(q => q.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

   
    public static void SeedFromFile(string path)
    {
        if (Count() > 0) return;
        if (!File.Exists(path)) return;

        using var ctx = new QuizContext();
        foreach (var line in File.ReadLines(path).Skip(1))
        {
            var p = line.Split('\t'); 
            if (p.Length < 3) continue;
            ctx.Questions.Add(new Question
            {
                Emoji = p[2].Trim(),
                Answer = p[1].Trim(),
                Category = "Фильмы"
            });
        }
        ctx.SaveChanges();
    }
}
