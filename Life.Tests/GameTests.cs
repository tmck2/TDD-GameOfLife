using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Life.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void ParsedPositionHasCorrectDimensions()
        {
            var game = new Game(
                "........\n" +
                "....*...\n" +
                "...**...\n" +
                "........");
            Assert.That(game.Width, Is.EqualTo(8));
            Assert.That(game.Height, Is.EqualTo(4));
        }

        [Test, Sequential]
        public void ParsedPositionPrintsCorrectly(
            [Values(
                "........\n" +
                "....*...\n" +
                "...**...\n" +
                "........",

                "........\n" +
                "....*...\n" +
                "....*...\n" +
                "......**"
                )] string position)
        {
            var game = new Game(position);
            Assert.That(game.ToString(), Is.EqualTo(position));
        }

        [Test] // 1
        public void DeathFromUnderpopulation()
        {
            const string input = "........\n" +
                                 "........\n" +
                                 "...**...\n" +
                                 "........";
            const string output = "........\n" +
                                  "........\n" +
                                  "........\n" +
                                  "........";
            var game = new Game(input);
            game.NextGeneration();
            Assert.That(game.ToString(), Is.EqualTo(output));
        }

        [Test] // 2
        public void DeathBecauseOfOvercrowding()
        {
            const string input = "***.....\n" +
                                 "**......\n" +
                                 "*.......\n" +
                                 "........";
            var game = new Game(input);
            game.NextGeneration();
            Assert.That(game[1, 0], Is.EqualTo('.'));
        }

        [Test, Sequential] //3
        public void LivesOnToNextGeneration([Values(
            "........\n" +
            "....*...\n" +
            "...**...\n" +
            "........",

            "........\n" +
            "....*...\n" +
            "...***..\n" +
            "........"
            )] string input)
        {
            var game = new Game(input);
            game.NextGeneration();
            Assert.That(game[1, 4], Is.EqualTo('*'));
        }

        [Test] //4
        public void BirthBecauseOfNeighbors()
        {
            const string input = "........\n" +
                                 "....*...\n" +
                                 "...**...\n" +
                                 "........";
            const string output = "........\n" +
                                  "...**...\n" +
                                  "...**...\n" +
                                  "........";
            var game = new Game(input);
            game.NextGeneration();
            Assert.That(game.ToString(), Is.EqualTo(output));
        }
    }
}