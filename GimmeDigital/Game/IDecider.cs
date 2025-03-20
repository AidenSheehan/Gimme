using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital.Game
{
    public interface IDecider
    {
        public void ChooseStartingGoalsBasic(IEnumerable<GoalCard> possibleGoals, List<GoalCard> personalGoals);
        public void ChooseStartingGoalsSnake(IEnumerable<GoalCard> goals);

        public void PlayTurn(PlayerPerspective pespective);
    }
}