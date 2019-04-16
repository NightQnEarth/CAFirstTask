using System;
using System.IO;
using NUnit.Framework;

namespace CAFirstTask.Tests
{
    [TestFixture]
    public class Tests
    {
        private const string ExpectedNegativeResult = "N";

        private readonly BreadthFirstSearch finder = new BreadthFirstSearch();

        private string GetActualResult(string inputLines)
        {
            var tempFileName = Path.GetTempFileName();

            using (var writer = new StreamWriter(tempFileName))
                writer.Write(inputLines);

            var actualResult = GetActualResult();

            try
            {
                File.Delete(tempFileName);
            }
            catch (IOException) { }

            return actualResult;

            string GetActualResult()
            {
                using (var reader = new StreamReader(tempFileName))
                {
                    var (labyrinth, start, finish) = DataParser.GetInputData(reader.ReadLine);
                    var resultRoute = finder.GetRoute(labyrinth, start, finish);
                    return DataParser.ResultGenerate(resultRoute);
                }
            }
        }

        [Test]
        public void SampleLabyrinthFromReadme_ReturnExpectedRoute()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "5",
                "1 1 1 1 1",
                "1 0 1 0 1",
                "1 0 0 0 1",
                "1 1 1 1 1",
                "2 2",
                "2 4");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "2 2",
                "3 2",
                "3 3",
                "3 4",
                "2 4");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithWallsAroundStart_ReturnN()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "5",
                "1 1 1 1 1",
                "1 0 1 0 1",
                "1 1 0 0 1",
                "1 1 1 1 1",
                "2 2",
                "2 4");

            Assert.AreEqual(ExpectedNegativeResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithUnreachableFinish_ReturnN()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "6",
                "1 1 1 1 1 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 0 0 1 0 1",
                "1 0 1 0 1 1",
                "1 1 1 1 1 1",
                "2 2",
                "5 4");

            Assert.AreEqual(ExpectedNegativeResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWhereStartAndFinishCoincide_ReturnRoute()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "5",
                "1 1 1 1 1",
                "1 0 1 0 1",
                "1 0 0 0 1",
                "1 1 1 1 1",
                "2 2",
                "2 2");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "2 2");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithLeftOrRightTurnChoiceWhenMoveDown_ReturnRouteThrowLeftWay()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "5",
                "5",
                "1 1 1 1 1",
                "1 0 0 0 1",
                "1 0 1 0 1",
                "1 0 0 0 1",
                "1 1 1 1 1",
                "2 3",
                "4 3");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "2 3",
                "2 2",
                "3 2",
                "4 2",
                "4 3");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithLeftOrRightTurnChoiceWhenMoveUp_ReturnRouteThrowLeftWay()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "5",
                "5",
                "1 1 1 1 1",
                "1 0 0 0 1",
                "1 0 1 0 1",
                "1 0 0 0 1",
                "1 1 1 1 1",
                "4 3",
                "2 3");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "4 3",
                "4 2",
                "3 2",
                "2 2",
                "2 3");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithUpOrDownTurnChoiceWhenMoveRight_ReturnRouteThrowUpWay()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "5",
                "5",
                "1 1 1 1 1",
                "1 0 0 0 1",
                "1 0 1 0 1",
                "1 0 0 0 1",
                "1 1 1 1 1",
                "3 2",
                "3 4");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "3 2",
                "2 2",
                "2 3",
                "2 4",
                "3 4");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithUpOrDownTurnChoiceWhenMoveLeft_ReturnRouteThrowUpWay()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "5",
                "5",
                "1 1 1 1 1",
                "1 0 0 0 1",
                "1 0 1 0 1",
                "1 0 0 0 1",
                "1 1 1 1 1",
                "3 4",
                "3 2");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "3 4",
                "2 4",
                "2 3",
                "2 2",
                "3 2");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithoutInteriorWallsWithFinishAtRightBottomCorner_ReturnRouteAlongLeftBorderAndBottom()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "6",
                "1 1 1 1 1 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 1 1 1 1 1",
                "2 2",
                "5 5");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "2 2",
                "3 2",
                "4 2",
                "5 2",
                "5 3",
                "5 4",
                "5 5");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthWithoutInteriorWallsWithFinishAtTopLeftCorner_ReturnRouteAlongRightBorderAndTop()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "6",
                "1 1 1 1 1 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 0 0 0 0 1",
                "1 1 1 1 1 1",
                "5 5",
                "2 2");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "5 5",
                "4 5",
                "3 5",
                "2 5",
                "2 4",
                "2 3",
                "2 2");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void LabyrinthGenerallyWithoutWallsWithFinishAtRightBottomCorner_ReturnRouteAlongLeftBorderAndBottom()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "6",
                "0 0 0 0 0 0",
                "0 0 0 0 0 0",
                "0 0 0 0 0 0",
                "0 0 0 0 0 0",
                "0 0 0 0 0 0",
                "0 0 0 0 0 0",
                "1 1",
                "6 6");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 1",
                "2 1",
                "3 1",
                "4 1",
                "5 1",
                "6 1",
                "6 2",
                "6 3",
                "6 4",
                "6 5",
                "6 6");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }
    }
}