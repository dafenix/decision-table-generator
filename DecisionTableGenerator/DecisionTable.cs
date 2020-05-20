using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionMatrix
{
    class DecisionTable
    {
        private Dictionary<int, List<Choice>> _tab;
        private List<Decision> _decisions;
        private Parser _parser = new Parser();
        public void Create()
        {
            _tab = new Dictionary<int, List<Choice>>();
            _decisions = _decisions.ToList();
            var i = 0;
            foreach (var dec in _decisions)
            {
                var choices = new List<Choice>(dec._Choices);

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
                var res = $"{_decisions[d]._description}({_decisions[d]._decisionId})|";
                foreach (var choice in _tab[d])
                {
                    res += choice._choice;
                    res += "|";
                }

                ;
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

                    if (choice == null)
                    {
                        break;
                    }
                }

                if (choice != null)
                {
                    choice._when = _decisions.Find(d => d._decisionId == _parser.ParseDecisionId(rawInput));
                    Console.WriteLine($"Added decision {choice._when} to choice {choice}");
                    break;
                }
            }
        }
        
        private Choice FindForId(Decision dec, int id)
        {
            return dec._Choices.Find(c => c._id == id);
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

        public void Clear()
        {
            _decisions.Clear();
            _tab.Clear();
        }

        public void Add(Decision dec)
        {
            _decisions.Add(dec);
        }

        public Decision Find(int id)
        {
            return _decisions.Single(dec => dec._decisionId == id);
        }

        public void Remove(Decision decision)
        {
            _decisions.Remove(decision);
        }
    }
}