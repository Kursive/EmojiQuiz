namespace EmojiQuiz;

public class GameForm : Form
{
    private Label labelEmoji;
    private Label labelScore;
    private Label labelResult;
    private Button button1, button2, button3, button4;

    static readonly Random rng = new();
    Question? current;
    int score = 0;

    public GameForm()
    {
        InitializeComponent();
        NextQuestion();
    }

    private void InitializeComponent()
    {
       
        Text = "Игра";
        Size = new Size(500, 480);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        BackColor = Color.FromArgb(30, 30, 40);

        
        labelEmoji = new Label
        {
            Font = new Font("Segoe UI Emoji", 52),
            ForeColor = Color.White,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(480, 110),
            Location = new Point(10, 15)
        };

        
        labelScore = new Label
        {
            Text = "Счёт: 0",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Color.FromArgb(180, 200, 255),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleRight,
            Size = new Size(460, 30),
            Location = new Point(10, 130)
        };

        
        labelResult = new Label
        {
            Text = "",
            Font = new Font("Segoe UI Emoji", 11),
            ForeColor = Color.FromArgb(255, 220, 100),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(460, 35),
            Location = new Point(10, 165)
        };

       
        var positions = new Point[]
        {
            new(20, 210), new(260, 210),
            new(20, 310), new(260, 310)
        };
        var btns = new Button[4];
        for (int i = 0; i < 4; i++)
        {
            btns[i] = new Button
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(200, 80),
                Location = positions[i],
                BackColor = Color.FromArgb(55, 55, 75),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btns[i].FlatAppearance.BorderColor = Color.FromArgb(100, 100, 130);
            btns[i].FlatAppearance.BorderSize = 1;
        }
        button1 = btns[0]; button2 = btns[1];
        button3 = btns[2]; button4 = btns[3];

        button1.Click += (s, e) => CheckAnswer(button1.Text);
        button2.Click += (s, e) => CheckAnswer(button2.Text);
        button3.Click += (s, e) => CheckAnswer(button3.Text);
        button4.Click += (s, e) => CheckAnswer(button4.Text);

        Controls.AddRange(new Control[]
        {
            labelEmoji, labelScore, labelResult,
            button1, button2, button3, button4
        });
    }

    void NextQuestion()
    {
        current = Db.GetRandom();
        if (current == null)
        {
            labelEmoji.Text = "😔";
            labelResult.Text = "База пустая";
            return;
        }

        labelEmoji.Text = current.Emoji;
        labelResult.Text = "";

        var options = Db.GetWrongAnswers(current.Answer, 3);
        options.Add(current.Answer);
        Shuffle(options);

        var buttons = new[] { button1, button2, button3, button4 };
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < options.Count)
            {
                buttons[i].Text = options[i];
                buttons[i].Visible = true;
                buttons[i].BackColor = Color.FromArgb(55, 55, 75);
            }
            else
            {
                buttons[i].Visible = false;
            }
        }
    }

    void CheckAnswer(string chosen)
    {
        if (current == null) return;

        var buttons = new[] { button1, button2, button3, button4 };

        if (chosen == current.Answer)
        {
            score++;
            labelResult.Text = "✅ Верно!";
            
            foreach (var b in buttons)
                if (b.Text == chosen) b.BackColor = Color.FromArgb(60, 160, 80);
        }
        else
        {
            labelResult.Text = "❌ Неверно — это: " + current.Answer;
           
            foreach (var b in buttons)
            {
                if (b.Text == chosen) b.BackColor = Color.FromArgb(180, 50, 50);
                if (b.Text == current.Answer) b.BackColor = Color.FromArgb(60, 160, 80);
            }
        }

        labelScore.Text = "Счёт: " + score;

        
        var timer = new System.Windows.Forms.Timer { Interval = 1000 };
        timer.Tick += (s, e) => { timer.Stop(); NextQuestion(); };
        timer.Start();
    }

    void Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
