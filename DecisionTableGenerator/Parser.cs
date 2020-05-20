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
            var d2c = rawInput.Split(":");
            return d2c[0];
        }
        
        // Link - choice1:decisionName1
        public int ParseDecisionId(string rawLinkInput)
        {
            var choicesAndDecisionId = rawLinkInput.Split(":");
            return int.Parse(choicesAndDecisionId[1]);
        }

        public string[] ParseChoices(string rawInput)
        {
            var d2c = rawInput.Split(":");
            var rawChoices = d2c[1].Split("/");
            return rawChoices;
        }
    }
}