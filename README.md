<div align="center">

# 🎬 Emoji Quiz
### Угадай фильм по эмодзи
Десктопная викторина на **C# + WinForms + SQLite + Entity Framework Core**

![.NET](https://img.shields.io/badge/.NET-10-purple?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=csharp)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite)
![EF Core](https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge)

---

Приложение показывает эмодзи, которые описывают фильм.  
Задача игрока — выбрать правильный вариант ответа.

</div>

# ✨ Возможности

- 🎮 Режим игры
- 🎲 Случайные вопросы
- ✅ Четыре варианта ответа
- 📈 Подсчет очков
- ➕ Добавление собственных вопросов
- 💾 Хранение данных в SQLite
- ⚡ Автоматическое создание базы данных при первом запуске

---

# 🛠️ Используемые технологии

- C#
- .NET 10
- Windows Forms
- SQLite
- Entity Framework Core
- LINQ

---

# 🚀 Запуск проекта

## 1. Клонировать репозиторий

```bash
git clone https://github.com/USERNAME/EmojiQuiz.git
cd EmojiQuiz
```

---

## 2. Установить зависимости

```bash
dotnet restore
```

---

## 3. Подготовить данные

Положите файл `movies_ru_emoji.tsv` в папку `EmojiQuiz` рядом с `.csproj`.  
При первом запуске автоматически будет создан файл `quiz.db` и импортированы все вопросы.

---

## 4. Запустить приложение

```bash
cd EmojiQuiz
dotnet run
```

---

# 🎮 Как играть

1. Нажмите **Играть**
2. Посмотрите на эмодзи
3. Выберите один из четырех вариантов
4. Получайте очки за правильные ответы

---

# 👨‍💻 Режим администратора

Администратор может:
- добавить новые вопросы;
- указать эмодзи;
- написать правильный ответ;
- выбрать категорию.

После сохранения вопрос сразу становится доступен в игре.

---

# 📂 Структура проекта

```text
EmojiQuiz
│
├── Program.cs
├── MainForm.cs
├── GameForm.cs
├── AdminForm.cs
├── Db.cs
├── QuizContext.cs
├── Question.cs
│
├── movies_ru_emoji.tsv
│
├── prepare_data.py
├── README.md
└── .gitignore
```

---

# 🐍 Подготовка данных (Python)

Используется отдельный скрипт для создания базы вопросов.

Необходимые библиотеки:

```bash
pip install pandas
```

Скачайте:
- **Movies_to_Emojis.csv** (Kaggle)
- **title.basics.tsv**
- **title.akas.tsv**

Положите все три файла рядом со скриптом `prepare_data.py` и выполните:

```bash
python prepare_data.py
```

На выходе получится файл `movies_ru_emoji.tsv` — скопируйте его в папку `EmojiQuiz`.

---

# 📚 Источники данных

- [IMDb Datasets](https://datasets.imdbws.com/)
- [Kaggle Movies to Emojis Dataset](https://www.kaggle.com/datasets/tarundalal/movies-to-emojis-dataset)

---

<div align="center">
</div>
