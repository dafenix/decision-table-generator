namespace DecisionMatrix
{
    class Choice
    {
        public int Id { get; set; }

        public string ChoiceText { get; set; }

        public Decision When { get; set; }

        public override string ToString()
        {
            return $"{Id} : {ChoiceText}";
        }
    }
}