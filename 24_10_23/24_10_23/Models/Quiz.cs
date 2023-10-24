using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_10_23.Models
{
    internal class Quiz
    {
        private static int CountId;
        public int Id = 1;
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string name, List<Question> questions)
        {
            Name = name;
            Questions = questions;
            Id = ++CountId;
        }
    }
}
