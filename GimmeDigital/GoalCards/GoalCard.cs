namespace GimmeDigital
{
    public delegate bool Criterion(IEnumerable<DraftCard> draftPool);

    public class GoalCard(IEnumerable<Criterion> criteria, string text, int reward) : Card
    {
        public string GoalText { get; init; } = text;
        public int Reward { get; init; } = reward;
        private IEnumerable<Criterion> AllCriteria { get; init; } = criteria;

        public bool MeetsCriteria(IEnumerable<DraftCard> draftPool) =>
            AllCriteria.ToList().TrueForAll(criterion => criterion(draftPool));

        public int GetScore(IEnumerable<DraftCard> draftPool) =>
            MeetsCriteria(draftPool) ? Reward : -1;
    }
}
