namespace EmojiQuiz;

public class MainForm : Form
{
    private readonly Label labelTitle;
    private readonly Button buttonPlay;
    private readonly Button buttonAdmin;

    public MainForm()
    {
        Text            = "Emoji Quiz";
        Size            = new Size(400, 300);
        StartPosition   = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox     = false;
        BackColor       = Color.FromArgb(30, 30, 40);

        labelTitle = new Label
        {
            Text      = "🎮 Угадай по эмодзи",
            Font      = new Font("Segoe UI Emoji", 18, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize  = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size      = new Size(380, 80),
            Location  = new Point(10, 30)
        };

        buttonPlay = MakeButton("▶  Играть",
            Color.FromArgb(60, 180, 100), new Point(80, 130), new Size(240, 55));
        buttonPlay.Click += (_, _) => new GameForm().Show();

        buttonAdmin = MakeButton("⚙  Администратор",
            Color.FromArgb(70, 70, 90), new Point(80, 200), new Size(240, 45));
        buttonAdmin.Click += (_, _) => new AdminForm().ShowDialog();

        Controls.AddRange(new Control[] { labelTitle, buttonPlay, buttonAdmin });
    }

    private static Button MakeButton(string text, Color bg, Point loc, Size size)
    {
        var b = new Button
        {
            Text      = text,
            Font      = new Font("Segoe UI Emoji", 12, FontStyle.Bold),
            BackColor = bg,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Location  = loc,
            Size      = size,
            Cursor    = Cursors.Hand
        };
        b.FlatAppearance.BorderSize = 0;
        return b;
    }
}