using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital.Game
{
    public interface IScoringStrategy
    {
        public int CalculateScore(
            List<GoalCard> personalGoals,
            List<GoalCard> groupGoals,
            List<DraftCard> draftedCards,
            bool includeHidden
        );
    }

    public class StandardScoringStrategy : IScoringStrategy
    {
        public int CalculateScore(List<GoalCard> personalGoals, List<GoalCard> groupGoals, List<DraftCard> draftedCards, bool includeHidden)
        {
            int score = 0;

            // Add points for personal goals
            // Always include revealed peronal goals
            var selectedPersonalGoals = personalGoals.Where(c => c.IsRevealed);
            // Only include unrevealed personal goals if specified
            if (includeHidden)
            {
                selectedPersonalGoals = selectedPersonalGoals.Union(personalGoals.Where(c => !c.IsRevealed));
            }

            // Add points for completed goals, lose 1 point for each incomplete
            foreach (GoalCard goalCard in selectedPersonalGoals) {
                if (goalCard.MeetsCriteria(draftedCards)) {
                    score += goalCard.Reward;
                } else {
                    score -= 1;
                }
            }

            // Add points for completed group goals
            score += groupGoals.Where(c => c.MeetsCriteria(draftedCards)).Sum(c => c.Reward);
            // Add points for star cards
            score += draftedCards.Count(c => c.SymbolPicker.Symbol == Symbol.Star);
            return score;
        }
    }
}
