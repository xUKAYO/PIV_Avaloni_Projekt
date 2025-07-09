using QuizAppAvalonia.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Timers;
using Avalonia.Threading;

namespace QuizAppAvalonia.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Question> _questions;
    private int _currentQuestionIndex;
    private string _questionText = string.Empty;
    private ObservableCollection<string> _answers = new();
    private string _result = string.Empty;
    private int _score;
    private bool _isQuizFinished;
    private Timer _timer;
    private int _timeLeft;
    private string? _selectedAnswer;
    private bool _isAnswerCorrect;

    public ICommand SelectAnswerCommand { get; }
    public ICommand RestartQuizCommand { get; }

    public MainWindowViewModel()
    {
        _questions = new ObservableCollection<Question>
        {
            new Question("Co to jest Avalonia?", new List<string> { "Framework .NET", "Silnik gry", "Typ chmury", "Biblioteka JS" }, "Framework .NET"),
            new Question("W którym roku powstał Uniwersytet Bielsko-Bialski?", new List<string> { "2021", "2018", "2000", "1999" }, "2021"),
            new Question("Jak brzmi skrót uczelni Uniwersytetu Bielsko-Bialskiego?", new List<string> { "UBB", "UŚBB", "UBBIA", "UCB" }, "UBB"),
            new Question("Która z tych uczelni została przekształcona w UBB?", new List<string> { "Akademia Techniczno-Humanistyczna", "Politechnika Śląska", "Uniwersytet Ekonomiczny", "Akademia Bielska" }, "Akademia Techniczno-Humanistyczna"),
            new Question("Jakie kolory dominują w logo UBB?", new List<string> { "Czerwony i biały", "Niebieski i pomarańczowy", "Zielony i czarny", "Fioletowy i żółty" }, "Niebieski i pomarańczowy"),
            new Question("Jak nazywa się system informatyczny używany przez studentów UBB?", new List<string> { "USOS", "SOTS", "MAX", "Moodle" }, "SOTS"),
            new Question("W którym mieście znajduje się siedziba UBB?", new List<string> { "Cieszyn", "Katowice", "Bielsko-Biała", "Kraków" }, "Bielsko-Biała"),
            new Question("Jak nazywa się platforma e-learningowa wykorzystywana na UBB?", new List<string> { "Blackboard", "Teams", "Zoom", "Moodle" }, "Moodle"),
            new Question("Ile wydziałów posiada Uniwersytet Bielsko-Bialski?", new List<string> { "3", "4", "5", "6" }, "4"),
            new Question("Który kierunek NIE jest prowadzony na UBB?", new List<string> { "Informatyka", "Zarządzanie", "Psychologia", "Mechanika i Budowa Maszyn" }, "Psychologia"),
            new Question("Które z tych międzynarodowych programów wspiera UBB?", new List<string> { "ERASMUS+", "Fulbright", "DAAD", "TEMPUS" }, "ERASMUS+"),
            new Question("W jakim języku piszemy Avalonię?", new List<string> { "Python", "C#", "JavaScript", "C++" }, "C#"),
            new Question("Avalonia to biblioteka do...", new List<string> { "Tworzenia aplikacji webowych", "Tworzenia aplikacji desktopowych", "Zarządzania bazą danych", "Tworzenia gier 3D" }, "Tworzenia aplikacji desktopowych")
        };

        SelectAnswerCommand = new RelayCommand(obj => SelectAnswer(obj!));
        RestartQuizCommand = new RelayCommand(_ => RestartQuiz());

        _currentQuestionIndex = 0;
        _score = 0;

        _timer = new Timer(1000);
        _timer.Elapsed += TimerElapsed;

        TimeLeft = 10;
        LoadQuestion();
        _timer.Start();
    }
    
    private string _correctAnswer = string.Empty;
    public string CorrectAnswer
    {
        get => _correctAnswer;
        set
        {
            _correctAnswer = value;
            OnPropertyChanged();
        }
    }

    public int TimeLeft
    {
        get => _timeLeft;
        set
        {
            _timeLeft = value;
            OnPropertyChanged();
        }
    }

    public string? SelectedAnswer
    {
        get => _selectedAnswer;
        set
        {
            _selectedAnswer = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsCorrectSelectedAnswer));
        }
    }

    public string QuestionText
    {
        get => _questionText;
        set { _questionText = value; OnPropertyChanged(); }
    }

    public ObservableCollection<string> Answers
    {
        get => _answers;
        set { _answers = value; OnPropertyChanged(); }
    }

    public string Result
    {
        get => _result;
        set { _result = value; OnPropertyChanged(); }
    }

    public string QuestionCounter => $"Pytanie {_currentQuestionIndex + 1} z {_questions.Count}";

    public int Score
    {
        get => _score;
        set { _score = value; OnPropertyChanged(); }
    }

    public bool IsQuizFinished
    {
        get => _isQuizFinished;
        set { _isQuizFinished = value; OnPropertyChanged(); }
    }

    public bool IsCorrectSelectedAnswer
    {
        get => _isAnswerCorrect;
        set
        {
            _isAnswerCorrect = value;
            OnPropertyChanged();
        }
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        if (TimeLeft > 0)
        {
            TimeLeft--;
        }
        else
        {
            _timer.Stop();
            Dispatcher.UIThread.Invoke(() =>
            {
                Result = "Czas minął!";
                LoadNextQuestionWithDelay();
            });
        }
    }

    private void LoadQuestion()
    {
        if (_currentQuestionIndex < _questions.Count)
        {
            var q = _questions[_currentQuestionIndex];
            QuestionText = q.Text;
            Answers = new ObservableCollection<string>(q.Answers);
            CorrectAnswer = q.CorrectAnswer; // <-- TU MA BYĆ
            Result = string.Empty;
            SelectedAnswer = null;
            TimeLeft = 10;
            IsQuizFinished = false;
            OnPropertyChanged(nameof(QuestionCounter));
            _timer.Start();
        }
        else
        {
            QuestionText = $"Koniec quizu!\nTwój wynik: {_score}/{_questions.Count}";
            Answers = new ObservableCollection<string>();
            Result = $"Twój wynik: {_score}/{_questions.Count}";
            IsQuizFinished = true;

        }
    }


    private void SelectAnswer(object answer)
    {
        if (_currentQuestionIndex >= _questions.Count)
            return;

        _timer.Stop();

        SelectedAnswer = answer.ToString();
        var correctAnswer = _questions[_currentQuestionIndex].CorrectAnswer;

        IsCorrectSelectedAnswer = SelectedAnswer == correctAnswer;

        if (IsCorrectSelectedAnswer)
        {
            Score++;
            Result = "Dobra odpowiedź!";
        }
        else
        {
            Result = $"Zła odpowiedź! Poprawna to: {correctAnswer}";
        }

        LoadNextQuestionWithDelay();
    }

    private void LoadNextQuestionWithDelay()
    {
        Task.Delay(1000).ContinueWith(_ =>
            Dispatcher.UIThread.Invoke(() =>
            {
                _currentQuestionIndex++;
                LoadQuestion();
            }));
    }

    private void RestartQuiz()
    {
        _currentQuestionIndex = 0;
        _score = 0;
        TimeLeft = 10;
        _timer.Start();
        LoadQuestion();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string name = null!) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
