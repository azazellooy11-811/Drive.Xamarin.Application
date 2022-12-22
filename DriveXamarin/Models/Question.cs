using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DriveXamarin.Models
{
    public class Question
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("questionCategory")]
        public string QuestionCategory { get; set; }

        [JsonProperty("driveCategory")]
        public string DriveCategory { get; set; }

        [JsonProperty("file")]
        public File File { get; set; }

        [JsonProperty("correctAnswer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("answers")]
        public List<Answer> Answers { get; set; }
        [JsonProperty("errorUsers")]
        public List<User> ErrorUsers { get; set; }


        public static bool IsAnswerCorrect(string chosenAnswer, Question question)
        {
            //Direct string comparison to see if the correct answer has been chosen
            return chosenAnswer == question.CorrectAnswer;
        }

        public static List<Question> PullXRandomQuestions(List<Question> inputList, int count)
        {
            //Function to return n quiz questions randomly from an inputted list in a random order
            var listsize = inputList.Count;
            var indicesToGet = new List<int>();
            var integerList = new List<int>();
            //Generate list of integers as large as the number of required questions
            for (var i = 0; i < listsize; i++) integerList.Add(i);
            var rnd = new Random();
            for (var i = 0; i < count; i++)
            {
                //Randomly draw from that list of integers a number which is equal to the number required
                var randomindex = rnd.Next(0, integerList.Count);
                indicesToGet.Add(integerList[randomindex]);
                integerList.Remove(indicesToGet[i]);
            }

            var OutputList = new List<Question>();
            foreach (var i in indicesToGet)
                //Randomly pull questions by index which match the random integers inputted
                OutputList.Add(inputList[i]);
            return OutputList;
        }

        
    }
}