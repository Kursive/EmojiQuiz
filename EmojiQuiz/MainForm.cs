namespace EmojiQuiz;

public class MainForm : Form
{
    private Label labelTitle;
    private Button buttonPlay;
    private Button buttonAdmin;

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
      
        Text = "Emoji Quiz";
        Size = new Size(400, 300);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        BackColor = Color.FromArgb(30, 30, 40);

      
        labelTitle = new Label
        {
            Text = "🎮 Угадай по эмодзи",
            Font = new Font("Segoe UI Emoji", 18, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Size = new Size(380, 80),
            Location = new Point(10, 30)
        };

       
        buttonPlay = new Button
        {
            Text = "▶  Играть",
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            Size = new Size(240, 55),
            Location = new Point(80, 130),
            BackColor = Color.FromArgb(60, 180, 100),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        buttonPlay.FlatAppearance.BorderSize = 0;
        buttonPlay.Click += (s, e) => new GameForm().Show();

        
        buttonAdmin = new Button
        {
            Text = "⚙  Администратор",
            Font = new Font("Segoe UI", 12),
            Size = new Size(240, 45),
            Location = new Point(80, 200),
            BackColor = Color.FromArgb(70, 70, 90),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        buttonAdmin.FlatAppearance.BorderSize = 0;
        buttonAdmin.Click += (s, e) => new AdminForm().ShowDialog();

        Controls.AddRange(new Control[] { labelTitle, buttonPlay, buttonAdmin });
    }
}
