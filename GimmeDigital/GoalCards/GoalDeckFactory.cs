using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital.GoalCards
{
    public abstract class GoalDeckFactory : IDeckFactory<GoalCard>
    {
        public static Deck<GoalCard> CreateDeck()
        {
            List<GoalCard> cardsToAdd = [];
            AddGoals(cardsToAdd);
            return new Deck<GoalCard>(cardsToAdd);
        }

        private record GoalDefinition(string Desc, string Criteria, string Points);

        private static void AddGoals(List<GoalCard> cardsToAdd)
        {
            using StreamReader reader = new("Goals.txt");
            while (!reader.EndOfStream)
            {
                var definition = ReadNextGoal(reader);
                GoalCard card = CreateGoalCardFromDefinition(definition);
                cardsToAdd.Add(card);
            }
        }

        private static GoalDefinition ReadNextGoal(StreamReader reader)
        {
            string? desc = reader.ReadLine();
            string? criteria = reader.ReadLine();
            string? points = reader.ReadLine();
            if (!reader.EndOfStream)
            {
                string? discard = reader.ReadLine();
            }
            if (
                string.IsNullOrEmpty(desc)
                || string.IsNullOrEmpty(criteria)
                || string.IsNullOrEmpty(points)
            )
            {
                throw new ArgumentException("Invalid entry in goal file");
            }
            else
            {
                return new GoalDefinition(desc, criteria, points);
            }
        }

        private static GoalCard CreateGoalCardFromDefinition(GoalDefinition definition)
        {
            int points = int.Parse(definition.Points);
            List<string> criteriaStrings = [.. definition.Criteria.Split("&")];
            var criteria = CriteriaFactory.CreateCriteriaFromStrings(criteriaStrings);
            return new GoalCard(criteria, definition.Desc, points);
        }
    }
}
