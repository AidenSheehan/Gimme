using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital.Game
{
    public class Player(string name, IDecider decider)
    {
        public string Name { get; init; } = name;
        private IDecider Decider { get; set; } = decider;

        private List<DraftCard> DraftedCards { get; set; } = [];
        private List<GoalCard> PersonalGoals { get; set; } = [];

        public void AddGoal(GoalCard goalCard, bool reveal)
        {
            PersonalGoals.Add(goalCard);
            if (reveal)
            {
                goalCard.Reveal();
            }
        }

        public void AddDraftedCard(DraftCard draftCard)
        {
            DraftedCards.Add(draftCard);
        }

        public void RemoveAllCards()
        {
            PersonalGoals = [];
            DraftedCards = [];
        }

        public int CalculateScore(
            IScoringStrategy scoringStrategy,
            List<GoalCard> groupGoals,
            bool includeHidden
        )
        {
            return scoringStrategy.CalculateScore(
                PersonalGoals,
                groupGoals,
                DraftedCards,
                includeHidden
            );
        }

        public void ChooseStartingGoals(IEnumerable<GoalCard> possibleGoals) =>
            Decider.ChooseStartingGoalsBasic(possibleGoals, PersonalGoals);

        public void PlayTurn(PlayerPerspective pespective) => Decider.PlayTurn(pespective);

        public PlayerState CreatePlayerState() => new(PersonalGoals, DraftedCards);
    }
}
