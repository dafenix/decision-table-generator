using System.Collections.Generic;

namespace DecisionMatrix
{
    class Decision
    {
        public int _decisionId { get; set; }

        public string _description { get; set; }

        public List<Choice> _Choices { get; set; } = new List<Choice>();

        public override string ToString()
        {
            var cStr = "";
            foreach (var choice in _Choices)
            {
                cStr += choice;
                cStr += "\n";
            }

            return $"{_decisionId} : {_description}, \n\t choices: {cStr}";
        }
    }
}