namespace EmojiQuiz;

public class AdminForm : Form
{
    private readonly Label labelTitle;
    private readonly Label labelEmoji;
    private readonly Label labelAnswer;
    private readonly Label labelCategory;
    private readonly TextBox textEmoji;
    private readonly TextBox textAnswer;
    private readonly TextBox textCategory;
    private readonly Button buttonAdd;
    private readonly Button buttonBack;

    public AdminForm()
    {
        Text            = "Администратор";
        Size            = new Size(420, 380);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        BackColor       = Color.FromArgb(30, 30, 40);

        labelTitle = new Label
        {
            Text      = "➕ Добавить вопрос",
            Font      = new Font("Segoe UI Emoji", 15, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize  = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size      = new Size(400, 50),
            Location  = new Point(10, 15)
        };

        labelEmoji    = MakeLabel("Эмодзи (например 🦁👑):", 80);
        textEmoji     = MakeTextBox(105);
        labelAnswer   = MakeLabel("Ответ (русское название):", 150);
        textAnswer    = MakeTextBox(175);
        labelCategory = MakeLabel("Категория (необязательно):", 220);
        textCategory  = MakeTextBox(245);

        buttonAdd = new Button
        {
            Text      = "✔  Добавить",
            Font      = new Font("Segoe UI", 12, FontStyle.Bold),
            Size      = new Size(170, 45),
            Location  = new Point(30, 300),
            BackColor = Color.FromArgb(60, 180, 100),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand
        };
        buttonAdd.FlatAppearance.BorderSize = 0;
        buttonAdd.Click += ButtonAdd_Click;

        buttonBack = new Button
        {
            Text      = "✖  Назад",
            Font      = new Font("Segoe UI", 12),
            Size      = new Size(150, 45),
            Location  = new Point(220, 300),
            BackColor = Color.FromArgb(70, 70, 90),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand
        };
        buttonBack.FlatAppearance.BorderSize = 0;
        buttonBack.Click += (_, _) => Close();

        Controls.AddRange(new Control[]
        {
            labelTitle,
            labelEmoji,   textEmoji,
            labelAnswer,  textAnswer,
            labelCategory, textCategory,
            buttonAdd,    buttonBack
        });
    }

    private static Label MakeLabel(string text, int y) => new()
    {
        Text      = text,
        Font      = new Font("Segoe UI", 11),
        ForeColor = Color.FromArgb(180, 200, 255),
        AutoSize  = true,
        Location  = new Point(30, y)
    };

    private static TextBox MakeTextBox(int y) => new()
    {
        Font        = new Font("Segoe UI Emoji", 12),
        Size        = new Size(340, 32),
        Location    = new Point(30, y),
        BackColor   = Color.FromArgb(50, 50, 65),
        ForeColor   = Color.White,
        BorderStyle = BorderStyle.FixedSingle
    };

    private void ButtonAdd_Click(object? sender, EventArgs e)
    {
        string emoji    = textEmoji.Text.Trim();
        string answer   = textAnswer.Text.Trim();
        string category = textCategory.Text.Trim();

        if (emoji == "" || answer == "")
        {
            MessageBox.Show("Заполните поля «Эмодзи» и «Ответ».", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Db.Add(emoji, answer, category);
        MessageBox.Show($"Добавлено: {emoji} — {answer}", "Готово",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        textEmoji.Clear();
        textAnswer.Clear();
        textCategory.Clear();
        textEmoji.Focus();
    }
}