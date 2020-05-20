using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace DecisionMatrix
{
    public class Program
    {
        private List<string> _addDecisionActions = new List<string>();

        public static string ACTION_ADD = "add";
        public static string ACTION_LINK = "link";
        public static string ACTION_SAVE = "save";
        public static string ACTION_LOAD = "load";
        public static string ACTION_CREATE = "create";
        
        private Parser _parser = new Parser();
        private Persistence _persistence = new Persistence();
        private DecisionTable _decisionTable;

        public Program()
        {
            _decisionTable = new DecisionTable(_parser);
        }

        static void Main(string[] args)
        {
            UsageInfos.PrintHelp();
            
            var prog = new Program();

            do
            {
                prog.Dispatch(Console.ReadLine());
            } while (true);
        }

        private void Dispatch(string rawInput)
        {
            if (rawInput.StartsWith(ACTION_ADD))
            {
                _addDecisionActions.Add(rawInput);
                var rawDecision = _parser.ParseRawDecision(rawInput, ACTION_ADD);
                _decisionTable.AddDecision(rawDecision);
            }
            else if (rawInput.StartsWith(ACTION_LINK))
            {
                var rawDecision = _parser.ParseRawDecision(rawInput, ACTION_LINK);
                _decisionTable.Link(rawDecision);
            }
            else if (rawInput.StartsWith("list"))
            {
                List();
            }
            else if (rawInput.StartsWith(ACTION_CREATE))
            {
                CreateDecisionTable();
            }
            else if (rawInput.StartsWith("remove"))
            {
                _decisionTable.RemoveDecision(rawInput);
            }
            else if (rawInput.StartsWith(ACTION_SAVE))
            {
                Save(rawInput);
            }
            else if (rawInput.StartsWith(ACTION_LOAD))
            {
                Load(rawInput);
            }
            else if (rawInput == "exit")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Unknown command: " + rawInput);
            }
        }

        private void Save(string rawInput)
        {
            var aggregate = _addDecisionActions.Aggregate((s1, s2) => s1 + "\n" + s2);
            _persistence.Save(rawInput, aggregate);
        }

        private void Load(string rawInput)
        {
            _decisionTable.RemoveDecisions();
            var readLines = _persistence.Load(rawInput);
            foreach (var rl in readLines)
            {
                Dispatch(rl);
            }
        }

        private void CreateDecisionTable()
        {
            _decisionTable.Create();
            _decisionTable.Print();
        }

        private void List()
        {
            _decisionTable.PrintDecisions();
        }
    }
}