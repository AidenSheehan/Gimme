using GimmeDigital.GoalCards;

namespace GimmeDigital.Game
{
    public class Game
    {
        private const int NUM_ROUNDS = 12;
        private Deck<DraftCard> _draftDeck;
        private Deck<GoalCard> _goalDeck;

        private List<Player> _players;
        private List<DraftCard> _draftZone;
        private List<GoalCard> _groupGoals;

        private IScoringStrategy _scoringStrategy;

        public int RoundNumber { get; private set; }

        public Game(
            List<Player> players,
            ISetupStrategy setupStrategy,
            IScoringStrategy scoringStrategy
        )
        {
            _draftDeck = DraftDeckFactory.CreateDeck();
            _goalDeck = GoalDeckFactory.CreateDeck();
            _players = players;

            setupStrategy.Setup(
                players,
                out _draftZone,
                out _groupGoals,
                ref _draftDeck,
                ref _goalDeck
            );
            RoundNumber = 0;
            _scoringStrategy = scoringStrategy;
        }

        public void PerformGame()
        {
            for (int i = 0; i < NUM_ROUNDS; i++)
            {
                PerformRound();
            }
            var finalScores = CalculateFinalScores();
            var winners = DetermineWinners(finalScores);
        }

        private void PerformRound()
        {
            RoundNumber++;
            Console.WriteLine("Round: " + RoundNumber);
            _players.ForEach(PerformPlayerTurn);
        }

        private void PerformPlayerTurn(Player player)
        {
            PlayerPerspective playerPespective = CreatePlayerPerspective(player);
            Console.WriteLine(player.Name);
            player.PlayTurn(playerPespective);
        }

        private Dictionary<Player, int> CalculateFinalScores()
        {
            Dictionary<Player, int> finalScores = [];
            foreach (Player player in _players)
            {
                int finalScore = player.CalculateScore(_scoringStrategy, _groupGoals, true);
                finalScores.Add(player, finalScore);
            }
            return finalScores;
        }

        private IEnumerable<Player> DetermineWinners(Dictionary<Player, int> finalScores)
        {
            //TODO: Should we be using a strategy here for different win types?
            var winners = _players.Where(p => finalScores[p] == finalScores.Values.Max());
            return winners;
        }

        private PlayerPerspective CreatePlayerPerspective(Player player)
        {
            GameState gameState = new([.. _groupGoals], [.. _draftZone], NUM_ROUNDS - RoundNumber);
            PlayerState playerState = player.CreatePlayerState();
            IEnumerable<Player> opponents = _players.Where(p => !p.Equals(player));
            IEnumerable<PlayerState> opponentStates = opponents.Select(o => o.CreatePlayerState());
            return new PlayerPerspective(playerState, opponentStates, gameState);
        }
    }
}
