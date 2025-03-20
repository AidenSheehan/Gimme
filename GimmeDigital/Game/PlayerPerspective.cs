using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital.Game
{
    public record PlayerPerspective(
        PlayerState PlayerState,
        IEnumerable<PlayerState> OpponentStates,
        GameState GameState
    );

    public record PlayerState(List<GoalCard> PersonalGoals, List<DraftCard> DraftedCards);

    public record GameState(
        List<GoalCard> GroupGoals,
        List<DraftCard> DraftableCards,
        int RemainingRounds
    );
}
