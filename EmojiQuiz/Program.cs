namespace EmojiQuiz;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Db.EnsureCreated();
        Db.SeedFromFile();
        Application.Run(new MainForm());
    }
}

