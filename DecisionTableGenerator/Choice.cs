namespace DecisionMatrix
{
    class Choice
    {
        public int _id { get; set; }

        public string _choice { get; set; }

        public Decision _when { get; set; }

        public override string ToString()
        {
            return $"{_id} : {_choice}";
        }
    }
}