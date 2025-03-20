using System.Diagnostics;
using GimmeDigital.UI;

namespace GimmeDigital.Game
{
    public class HumanDecider : IDecider
    {

        private PlayerDecisionView view = new();

        public void ChooseStartingGoalsBasic(IEnumerable<GoalCard> possibleGoals, List<GoalCard> personalGoals)
        {
            
            personalGoals.AddRange(possibleGoals.Take(2));
        }

        public void ChooseStartingGoalsSnake(IEnumerable<GoalCard> goals)
        {
            throw new NotImplementedException();
        }

        public void PlayTurn(PlayerPerspective perspective)
        {
            Console.WriteLine("T");
            //throw new NotImplementedException();
        }
    }
}