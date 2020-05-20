using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DecisionMatrix
{
    class Persistence
    {
        public void Save(string rawInput, string content)
        {
            var path = rawInput.Remove(rawInput.IndexOf(Program.ACTION_SAVE, StringComparison.Ordinal), Program.ACTION_SAVE.Length).Trim();
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("failed to write actions to disk " + e);
            }
        }

        public ICollection<string> Load(string rawInput)
        {
            var path = rawInput.Remove(rawInput.IndexOf(Program.ACTION_LOAD, StringComparison.Ordinal), Program.ACTION_LOAD.Length).Trim();
            var readLines = File.ReadLines(path);
            return readLines.ToList();
        }
    }
}