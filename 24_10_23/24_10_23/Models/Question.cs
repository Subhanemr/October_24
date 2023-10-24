using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_10_23.Models
{
    internal class Question
    {
        private static int CountId;
        public int Id = 1;
        public string Text { get; set; }
        public List<string> Variants { get; set; }
        public int CorrectVariant { get; set; }

        public Question(string text, List<string> variants, int correctVariant)
        {
            Id = ++CountId;
            Text = text;
            Variants = variants;
            CorrectVariant = correctVariant;
        }
    }
}
