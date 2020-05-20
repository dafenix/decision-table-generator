using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMatrix
{
    class DecisionTable
    {
        private readonly Parser _parser;
        private readonly Dictionary<int, List<Choice>> _tab;
        private readonly List<Decision> _decisions = new List<Decision>();

        private int _choiceSeq;
        private int _decisionSeq;

        public DecisionTable(Parser parser)
        {
            _parser = parser;
            _tab = new Dictionary<int, List<Choice>>();
        }
        public void Create()
        {
            _tab.Clear();
            var i = 0;
            foreach (var dec in _decisions)
            {
                var choices = new List<Choice>(dec.Choices);

                _tab.Add(i, choices);

                if (i > 0)
                {
                    for (int iCnt = i; iCnt > 0; iCnt--)
                    {
                        _tab[iCnt - 1] = DoubleElements(_tab[iCnt - 1], choices.Count - 1);
                    }

                    var lCnt = _tab[i - 1].Count / choices.Count;
                    var tmp = new List<Choice>(choices);
                    for (int ln = 1; ln < lCnt; ln++)
                    {
                        choices.AddRange(tmp);
                    }
                }

                i++;
            }
        }

        public void Print()
        {
            for (int d = 0; d < _tab.Count; d++)
            {
                var res = $"{_decisions[d].Description}({_decisions[d].DecisionId})|";
                res += string.Join("|", _tab[d].Select(choice => choice.ChoiceText));
                Console.WriteLine(res);
            }
        }
        
        public void Link(string rawInput)
        {
            var pathParts = _parser.ParseChoices(rawInput);

            foreach (var dec in _decisions)
            {
                Choice choice = null;
                foreach (var part in pathParts)
                {
                    choice = FindForId(dec, int.Parse(part));

                    if (choice == null) { break; }
                }

                if (choice != null)
                {
                    choice.When = _decisions.Find(d => d.DecisionId == _parser.ParseDecisionId(rawInput));
                    Console.WriteLine($"Added decision {choice.When} to choice {choice}");
                    break;
                }
            }
        }
        
        private Choice FindForId(Decision dec, int id)
        {
            return dec.Choices.Find(c => c.Id == id);
        }

        private List<T> DoubleElements<T>(List<T> doubleThis, int count = 1)
        {
            var res = new List<T>();
            foreach (var dt in doubleThis)
            {
                // add the original element
                res.Add(dt);

                // and now the wanted count of copies
                for (int i = 0; i < count; i++)
                {
                    res.Add(dt);
                }
            }

            return res;
        }

        public void RemoveDecisions()
        {
            _decisions.Clear();
            _tab.Clear();
            Console.WriteLine("Removed all decisions");
        }

        public void RemoveDecision(string rawInput)
        {
            var keyWordAndPossibleId = rawInput.Split(" ");
            if (keyWordAndPossibleId.Length > 1)
            {
                int.TryParse(keyWordAndPossibleId[1], out var decId);
                RemoveDecision(decId);
                return;
            }

            RemoveDecisions();
        }
        private void RemoveDecision(int id)
        {
            var decision = Find(id);
            if (decision == null)
            {
                Console.WriteLine("Did not found decision with id " + id);
                return;
            }

            Remove(decision);
            Console.WriteLine($"Removed decision {decision.Description}");
        }

                
        // Add - decisionName1:choice1/choice2/choice3
        public void AddDecision(string rawInput)
        {
            var rawChoices = _parser.ParseChoices(rawInput);
            var choices = rawChoices.Select(choice => new Choice() {ChoiceText = choice, Id = _choiceSeq++}).ToList();

            var dec = new Decision()
            {
                DecisionId = _decisionSeq++,
                Description = _parser.ParseDecisionDescription(rawInput),
                Choices = choices
            };
            _decisions.Add(dec);
            Console.WriteLine("Decision: " + dec);
        }

        private Decision Find(int id)
        {
            return _decisions.Single(dec => dec.DecisionId == id);
        }

        private void Remove(Decision decision)
        {
            _decisions.Remove(decision);
        }

        public void PrintDecisions()
        {
            _decisions.ForEach(Console.WriteLine);
        }
    }
}