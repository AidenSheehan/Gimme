using GimmeDigital.Game;

namespace GimmeDigital.UI
{
    public class PlayerDecisionView : IDecisionRequester
    {
        public IEnumerable<GoalCard> RequestStartingGoalDecision(IEnumerable<GoalCard> options)
        {
            var e = options.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                i++;
                Console.WriteLine($"Option {i}");
                var goalCard = e.Current;
                Console.WriteLine(goalCard.GoalText);
                Console.WriteLine($"({goalCard.Reward})");
            }

            Console.WriteLine("Please enter the number of each card you wish to keep");
            var response = Console.ReadLine();
            if (response == null)
                throw new NullReferenceException();

            e.Reset();
            i = 0;
            List<GoalCard> selectedGoals = [];
            while (e.MoveNext())
            {
                i++;
                if (response.Contains(i.ToString()))
                {
                    selectedGoals.Add(e.Current);
                }
            }
            return selectedGoals;
        }

        public DraftCard RequestDraftDecision(IEnumerable<DraftCard> options)
        {
            throw new NotImplementedException();
        }
    }
}
