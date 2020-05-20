using System;

namespace DecisionMatrix
{
    public class UsageInfos
    {
        public static void PrintHelp()
        {
            Console.WriteLine("Decision Matrix");
            Console.WriteLine("Add a decision with the following syntax: add decisionName:choice*[/]");
            Console.WriteLine("Link a decision to a choice: link idPathToChoiceDelSlash:decisionId ");
            Console.WriteLine("List all decisions with: list");
            Console.WriteLine("Create the whole decision table with: create [decisionDescription=true/false] [decisionId=true/false] ");
            Console.WriteLine("Write decisions to disk: save pathToFilesystem");
            Console.WriteLine("Load decisions from disk: load pathToFilesystem");
            Console.WriteLine("Remove all decisions: remove");
            Console.WriteLine("Remove decision: remove decisionId");
            Console.WriteLine("Exit application: exit");
        }
    }
}