namespace GimmeDigital.Game
{
    public interface ISetupStrategy
    {
        public void Setup(
            in List<Player> players,
            out List<DraftCard> draftZone,
            out List<GoalCard> groupGoalZone,
            ref Deck<DraftCard> draftDeck,
            ref Deck<GoalCard> goalDeck
        );
    }

    public class StandardSetup : ISetupStrategy
    {

        public void Setup(in List<Player> players, out List<DraftCard> draftZone, out List<GoalCard> groupGoalZone, ref Deck<DraftCard> draftDeck, ref Deck<GoalCard> goalDeck)
        {
            groupGoalZone = goalDeck.DrawCards(2, true).ToList();
            draftZone = draftDeck.DrawCards(3, true).ToList();
            List<GoalCard> leftoverGoals = [];
            foreach (Player player in players)
            {
                var startingGoals = goalDeck.DrawCards(3, false);
                player.ChooseStartingGoals(startingGoals);
                // Set aside leftover goals
                leftoverGoals.AddRange(startingGoals.ToList());
            }
            // Put leftover goals back in deck
            // TODO: Determine if we should be providing a seed?
            leftoverGoals.Shuffle(new Random());
            goalDeck.AddCards(leftoverGoals);
        }
    }

    public class TournamentSetup : ISetupStrategy
    {
        public void Setup(in List<Player> players, out List<DraftCard> draftZone, out List<GoalCard> groupGoalZone, ref Deck<DraftCard> draftDeck, ref Deck<GoalCard> goalDeck)
        {
            throw new NotImplementedException();
        }
    }
}
