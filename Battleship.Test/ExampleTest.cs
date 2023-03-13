namespace Battleship.Test
{
    public class ExampleTest
    {
        [Fact]
        public void TestPlay()
        {
            List<string> ships = new List<string> { "3:2,3:5" };
            List<string> guesses = new List<string> { "7:0", "3:3" };
            bool[,] board = Game.CreateBoard(ships);
            Game.Play(ships, guesses, board).Should().Be(0);
        }
    }
}