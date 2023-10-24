using _24_10_23.Models;
using System;
using System.Text.RegularExpressions;

namespace _24_10_23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Quiz> quizList = new List<Quiz>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("  ================  ");
                Console.WriteLine("   =------------=   ");
                Console.WriteLine("        Menu        ");
                Console.WriteLine("   =------------=   ");
                Console.WriteLine("  ================  ");
                Console.WriteLine("--------------------");
                Console.WriteLine("| [1] Create Quiz  |");
                Console.WriteLine("--------------------");
                Console.WriteLine("--------------------");
                Console.WriteLine("| [2] Solve a Quiz |");
                Console.WriteLine("--------------------");
                Console.WriteLine("--------------------");
                Console.WriteLine("| [3] Show Quizzes |");
                Console.WriteLine("--------------------");
                Console.WriteLine("--------------------");
                Console.WriteLine("| [0] Quit         |");
                Console.WriteLine("--------------------");
                Console.Write(">>>");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("=--------------=");
                    Console.WriteLine(" Invalid input");
                    Console.WriteLine("=--------------=");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            CreateQuiz(quizList);
                            break;
                        case 2:
                            SolveQuiz(quizList);
                            break;
                        case 3:
                            ShowQuizzes(quizList);
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("=---------------=");
                            Console.WriteLine(" Invalid choice");
                            Console.WriteLine("=---------------=");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("=------------------=");
                    Console.WriteLine(" An error occurred: " + ex.Message);
                    Console.WriteLine("=------------------=");
                    break;
                }
            }
        }
        static void CreateQuiz(List<Quiz> quizList)
        {
            try
            {
                nameRestart:
                Console.WriteLine("=--------------------=");
                Console.WriteLine(" Enter the quiz name: ");
                Console.WriteLine("=--------------------=");
                Console.Write(">>>");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("=--------------=");
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("=--------------=");
                    goto nameRestart;
                }

                numQuestionsRestart:
                Console.WriteLine("=------------------------------=");
                Console.WriteLine(" Enter the number of questions: ");
                Console.WriteLine("=------------------------------=");
                Console.Write(">>>");

                if (!int.TryParse(Console.ReadLine(), out int numQuestions) || numQuestions < 1)
                {
                    Console.WriteLine("=--------------=");
                    Console.WriteLine(" Invalid input");
                    Console.WriteLine("=--------------=");
                    goto numQuestionsRestart;
                }

                List<Question> questions = new List<Question>();

                for (int i = 0; i < numQuestions; i++)
                {
                    questionTextRestart:
                    Console.WriteLine("=--------------------------=");
                    Console.WriteLine($" Enter question {i + 1}: ");
                    Console.WriteLine("=--------------------------=");
                    Console.Write(">>>");
                    string questionText = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(questionText))
                    {
                        Console.WriteLine("=--------------=");
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("=--------------=");
                        goto questionTextRestart;
                    }

                    List<string> variants = new List<string>();
                    for (int j = 0; j < 4; j++)
                    {
                        variantsTextRestart:
                        Console.WriteLine("=-----------------------=");
                        Console.WriteLine($" Enter option {j + 1}: ");
                        Console.WriteLine("=-----------------------=");
                        Console.Write(">>>");
                        string variantsText = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(variantsText))
                        {
                            Console.WriteLine("=--------------=");
                            Console.WriteLine("Invalid input");
                            Console.WriteLine("=--------------=");
                            goto variantsTextRestart;
                        }
                        variants.Add(variantsText);
                    }

                    correctOptionRestart:
                    Console.WriteLine("=---------------------------------------=");
                    Console.WriteLine(" Enter the correct option (1, 2, 3, 4): ");
                    Console.WriteLine("=---------------------------------------=");
                    Console.Write(">>>");

                    if (!int.TryParse(Console.ReadLine(), out int correctOption) || correctOption < 1 || correctOption > 4)
                    {
                        Console.WriteLine("=--------------=");
                        Console.WriteLine(" Invalid input");
                        Console.WriteLine("=--------------=");
                        goto correctOptionRestart;
                    }

                    Question question = new Question(questionText, variants, correctOption);
                    questions.Add(question);
                }

                Quiz quiz = new Quiz(name, questions);
                quizList.Add(quiz);
                Console.WriteLine("=---------------------------=");
                Console.WriteLine(" Quiz created successfully!");
                Console.WriteLine("=---------------------------=");
            }
            catch (Exception ex)
            {
                Console.WriteLine("=----------------------------------------=");
                Console.WriteLine(" Error occurred while creating the quiz: " + ex.Message);
                Console.WriteLine("=----------------------------------------=");
            }
        }
        static void SolveQuiz(List<Quiz> quizList)
        {
            try
            {
                quizIdRestart:
                Console.WriteLine("=-----------------------------------=");
                Console.WriteLine(" Enter the ID of the quiz to solve: ");
                Console.WriteLine("=-----------------------------------=");

                ShowQuizzes(quizList);

                Console.Write(">>>");
                if (!int.TryParse(Console.ReadLine(), out int quizId))
                {
                    Console.WriteLine("=--------------=");
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("=--------------=");
                    goto quizIdRestart;
                }

                var quizToSolve = quizList.Find(q => q.Id == quizId);

                if (quizToSolve == null)
                {
                    Console.WriteLine("=---------------=");
                    Console.WriteLine("Quiz not found.");
                    Console.WriteLine("=---------------=");
                    return;
                }

                userAnswerRestart:
                int score = 0;

                foreach (var question in quizToSolve.Questions)
                {
                    Console.WriteLine(question.Text);
                    for (int i = 0; i < question.Variants.Count; i++)
                    {
                        Console.WriteLine("=-------------------------------=");
                        Console.WriteLine($"{i + 1}. {question.Variants[i]}");
                        Console.WriteLine("=-------------------------------=");
                    }
                    
                    Console.WriteLine("=--------------------------------=");
                    Console.WriteLine(" Enter your answer (1, 2, 3, 4): ");
                    Console.WriteLine("=--------------------------------=");
                    Console.Write(">>>");
                    if (!int.TryParse(Console.ReadLine(), out int userAnswer) || userAnswer < 1 || userAnswer > 4)
                    {
                        Console.WriteLine("=--------------=");
                        Console.WriteLine("Invalid input");
                        Console.WriteLine("=--------------=");
                        goto userAnswerRestart;
                    }

                    if (userAnswer == question.CorrectVariant)
                    {
                        score++;
                    }
                }
                Console.WriteLine("=---------------------------------------------------------=");
                Console.WriteLine($" You scored {score} out of {quizToSolve.Questions.Count}.");
                Console.WriteLine("=---------------------------------------------------------=");

            }
            catch (Exception ex)
            {
                Console.WriteLine("=--------------------------------------=");
                Console.WriteLine("Error occurred while solving the quiz: " + ex.Message);
                Console.WriteLine("=--------------------------------------=");
            }
        }

        static void ShowQuizzes(List<Quiz> quizList)
        {
            try
            {
                Console.WriteLine("=---------=");
                Console.WriteLine(" Quizzes:");
                Console.WriteLine("=---------=");
                foreach (var quiz in quizList)
                {
                    Console.WriteLine("=------------------------------------=");
                    Console.WriteLine($"| ID: {quiz.Id}, Name: {quiz.Name} |");
                    Console.WriteLine("=------------------------------------=");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("=-------------------------------------=");
                Console.WriteLine("Error occurred while showing quizzes: " + ex.Message);
                Console.WriteLine("=-------------------------------------=");
            }
        }
    }
}