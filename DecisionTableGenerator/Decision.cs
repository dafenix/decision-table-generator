using System.Collections.Generic;

namespace DecisionMatrix
{
    class Decision
    {
        public int DecisionId { get; set; }

        public string Description { get; set; }

        public List<Choice> Choices { get; set; } = new List<Choice>();

        public override string ToString()
        {
            var cStr = "";
            foreach (var choice in Choices)
            {
                cStr += choice;
                cStr += "\n";
            }

            return $"{DecisionId} : {Description}, \n\t choices: {cStr}";
        }
    }
}