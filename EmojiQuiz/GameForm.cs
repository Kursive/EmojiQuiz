namespace EmojiQuiz;

public class GameForm : Form
{
    private readonly Label labelEmoji;
    private readonly Label labelScore;
    private readonly Label labelResult;
    private readonly Button[] answerButtons = new Button[4];

    private static readonly Random Rng = new();
    private Question? current;
    private int score = 0;
    private System.Windows.Forms.Timer? answerTimer;

    private static readonly Color BgNormal  = Color.FromArgb(52, 52, 72);
    private static readonly Color BgCorrect = Color.FromArgb(52, 168, 83);
    private static readonly Color BgWrong   = Color.FromArgb(192, 57, 57);

    public GameForm()
    {
        Text            = "Игра";
        Size            = new Size(520, 510);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        BackColor       = Color.FromArgb(28, 28, 40);

        labelEmoji = new Label
        {
            Font      = new Font("Segoe UI Emoji", 56),
            ForeColor = Color.White,
            AutoSize  = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Bounds    = new Rectangle(10, 10, 480, 120)
        };

        labelScore = new Label
        {
            Text      = "Счёт: 0",
            Font      = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(160, 190, 255),
            AutoSize  = false,
            TextAlign = ContentAlignment.MiddleRight,
            Bounds    = new Rectangle(10, 138, 480, 28)
        };

        labelResult = new Label
        {
            Text      = "",
            Font      = new Font("Segoe UI Emoji", 11),
            ForeColor = Color.FromArgb(255, 215, 80),
            AutoSize  = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Bounds    = new Rectangle(10, 170, 480, 32)
        };

        Point[] positions =
        {
            new(20, 215), new(270, 215),
            new(20, 330), new(270, 330)
        };

        for (int i = 0; i < 4; i++)
        {
            int idx = i;
            answerButtons[i] = new Button
            {
                Font      = new Font("Segoe UI", 11),
                Size      = new Size(210, 100),
                Location  = positions[i],
                BackColor = BgNormal,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            answerButtons[i].FlatAppearance.BorderColor = Color.FromArgb(90, 90, 115);
            answerButtons[i].Click += (_, _) => CheckAnswer(answerButtons[idx].Text);
        }

        var controls = new List<Control> { labelEmoji, labelScore, labelResult };
        controls.AddRange(answerButtons);
        Controls.AddRange(controls.ToArray());

        NextQuestion();
    }

    void NextQuestion()
    {
        current = Db.GetRandom();
        if (current == null)
        {
            labelEmoji.Text  = "😔";
            labelResult.Text = "База пустая";
            foreach (var b in answerButtons) b.Visible = false;
            return;
        }

        labelEmoji.Text  = current.Emoji;
        labelResult.Text = "";

        var options = Db.GetWrongAnswers(current.Answer, 3);
        options.Add(current.Answer);
        Shuffle(options);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < options.Count)
            {
                answerButtons[i].Text      = options[i];
                answerButtons[i].Visible   = true;
                answerButtons[i].Enabled   = true;
                answerButtons[i].BackColor = BgNormal;
            }
            else
            {
                answerButtons[i].Visible = false;
            }
        }
    }

    void CheckAnswer(string chosen)
    {
        if (current == null) return;

        foreach (var b in answerButtons) b.Enabled = false;

        bool correct = chosen == current.Answer;

        foreach (var b in answerButtons)
        {
            if (b.Text == current.Answer) b.BackColor = BgCorrect;
            else if (b.Text == chosen)    b.BackColor = BgWrong;
        }

        labelResult.Text = correct ? "✅ Верно!" : $"❌ Неверно — правильный ответ: {current.Answer}";
        labelScore.Text  = $"Счёт: {score += (correct ? 1 : 0)}";

        if (answerTimer == null)
        {
            answerTimer = new System.Windows.Forms.Timer { Interval = 1200 };
            answerTimer.Tick += (_, _) => { answerTimer.Stop(); NextQuestion(); };
        }
        answerTimer.Stop();
        answerTimer.Start();
    }

    void Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) answerTimer?.Dispose();
        base.Dispose(disposing);
    }
}