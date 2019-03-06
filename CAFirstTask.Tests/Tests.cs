using System;
using System.IO;
using NUnit.Framework;

namespace CAFirstTask.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly BreadthFirstSearch _finder = new BreadthFirstSearch();

        private bool CheckCorrect(string inputLines, string expectedResult)
        {
            var tempFileName = Path.GetTempFileName();
            
            using (StreamWriter writer = new StreamWriter(tempFileName)) 
                writer.Write(inputLines);

            var actualResult = GetActualResult();

            try
            {
                File.Delete(tempFileName);
            }
            catch (Exception) { }
            
            return expectedResult.Equals(actualResult);

            string GetActualResult()
            {
                using (StreamReader reader = new StreamReader(tempFileName))
                {
                    var (start, finish, matrix) = Program.GetInputData(reader.ReadLine);
                    var resultChain = _finder.GetRoute(start, finish, matrix);
                    return Program.ResultGenerate(resultChain);
                }
            }
        }

        [Test]
        public void DescriptionFromTaskTest()
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
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void NoWayTest()
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
            
            var expectedResult = string.Join(
                Environment.NewLine,
                "N");
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void StartAndFinishCoincideTest()
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
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void LeftOrRightTurnWhenMoveDownTest()
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
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void LeftOrRightTurnWhenMoveUpTest()
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
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void UpOrDownTurnWhenMoveRightTest()
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
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
        
        [Test]
        public void UpOrDownTurnWhenMoveLeftTest()
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
            
            Assert.IsTrue(CheckCorrect(inputLines, expectedResult));
        }
    }
}