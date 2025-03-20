using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    // A player has an associated decision requester
    public interface IDecisionRequester
    {
        public IEnumerable<GoalCard> RequestStartingGoalDecision(IEnumerable<GoalCard> options);
        public DraftCard RequestDraftDecision(IEnumerable<DraftCard> options);
    }
}