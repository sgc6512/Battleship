namespace Battleship.Test
{
    public class BattleShipTest
    {
        [Fact]
        public void ValidShipAndHitCoords()
        {
            // Arrange
            List<string> ships = new List<string> { "3:2,3:5" };
            List<string> guesses = new List<string> { "3:2", "3:3", "3:4", "3:5" };
            bool[,] board = Game.CreateBoard(ships);

            // Assert
            Game.Play(ships, guesses, board).Should().Be(1);
        }

        [Fact]
        public void InvalidShipAndHitCoords()
        {
            // Arrange
            List<string> ships = new List<string> { "3:2,3:5" };
            List<string> guesses = new List<string> { "3:2", "3:3", "3:4" };
            bool[,] board = Game.CreateBoard(ships);

            // Assert
            Game.Play(ships, guesses, board).Should().Be(0);
        }

        [Fact]
        public void MultipleShipsSank()
        {
            // Arrange
            List<string> ships = new List<string> { "3:2,3:5", "7:9,8:9" };
            List<string> guesses = new List<string> { "3:2", "3:3", "3:4", "3:5", "7:9", "8:9" };
            bool[,] board = Game.CreateBoard(ships);

            // Assert
            Game.Play(ships, guesses, board).Should().Be(2);
        }

        [Fact]
        public void MissedHit()
        {
            // Arrange
            List<string> ships = new List<string> { "3:2,3:5", "7:9,8:9" };
            bool[,] board = Game.CreateBoard(ships);

            // Act
            string sunk = Game.IsShipSunk(board, ships, 5, 5);

            // Assert
            sunk.Should().Be("Not Sunk");
        }

        [Fact]
        public void HitShipNotSunk()
        {
            // Arrange
            List<string> ships = new List<string> { "3:2,3:5", "7:9,8:9" };
            bool[,] board = Game.CreateBoard(ships);

            // Act
            // Act as though we hit these squares on the game board
            board[3, 2] = false;
            string sunk = Game.IsShipSunk(board, ships, 3, 2);

            // Assert
            sunk.Should().Be("Not Sunk");
        }

        [Fact]
        public void SunkShip()
        {
            // Arrange
            List<string> ships = new List<string> { "5:5,5:6"};
            bool[,] board = Game.CreateBoard(ships);

            // Act

            // Act as though we hit these squares on the game board
            board[5, 5] = false;
            board[5, 6] = false;

            string sunk = Game.IsShipSunk(board, ships, 5, 6);

            // Assert
            sunk.Should().Be("5:5,5:6");
        }
    }
}