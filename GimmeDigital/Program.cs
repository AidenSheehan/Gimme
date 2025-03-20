using System.Diagnostics;
using GimmeDigital.Game;


List<Player> players = [];
for (int i = 0; i < 3; i++)
{
    string name = "Player " + (char)('A' + i);
    players.Add(new Player(name, new HumanDecider()));
}
Game game = new(players, new StandardSetup(), new StandardScoringStrategy());
game.PerformGame();