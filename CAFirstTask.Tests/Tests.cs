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

            using (StreamReader reader = new StreamReader(tempFileName))
            {
                var (start, finish, matrix) = Program.GetInputData(reader.ReadLine);
                var resultChain = _finder.GetRoute(start, finish, matrix);
                var actualResult = Program.ResultGenerate(resultChain);
                return expectedResult.Equals(actualResult);
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
        public void StartFinishCoincideTest()
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
    }
}