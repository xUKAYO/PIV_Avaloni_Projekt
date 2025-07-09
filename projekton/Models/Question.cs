using System.Collections.Generic;
namespace QuizAppAvalonia.Models

{
    public class Question
    {
        public string Text { get; set; } = "";
        public List<string> Answers { get; set; } = new();
        public string CorrectAnswer { get; set; } = "";

        public Question() { }

        public Question(string text, List<string> answers, string correctAnswer)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}