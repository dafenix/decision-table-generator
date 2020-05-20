using System;

namespace DecisionMatrix
{
    class Parser
    {
        public string ParseRawDecision(string rawInput, string action)
        {
            if (rawInput == null) throw new ArgumentNullException(nameof(rawInput));
            var indexOf = rawInput.IndexOf(action, StringComparison.Ordinal);
            return rawInput.Substring(indexOf + action.Length);
        }

        public string ParseDecisionDescription(string rawInput)
        {
            var parts = rawInput.Split(":");
            return parts[0];
        }
        
        public int ParseDecisionId(string rawLinkInput)
        {
            var choicesAndDecisionId = rawLinkInput.Split(":");
            return int.Parse(choicesAndDecisionId[1]);
        }

        public string[] ParseChoices(string rawInput)
        {
            var parts = rawInput.Split(":");
            var rawChoices = parts[1].Split("/");
            return rawChoices;
        }
    }
}