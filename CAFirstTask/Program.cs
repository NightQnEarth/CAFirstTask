using System;
// ReSharper disable PossibleNullReferenceException

namespace CAFirstTask
{    
    class Program
    {
        public static void Main(string[] args)
        {
            var rowCount = int.Parse(Console.ReadLine().Trim());
            var columnCount = int.Parse(Console.ReadLine().Trim());
            var matrix = new int[rowCount,columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                var currentRow = Console.ReadLine().Split();
                for (int column = 0; column < columnCount; column++)
                    matrix[row, column] = int.Parse(currentRow[column]);
            }
 
            var startAsArray = Console.ReadLine().Split();
            var start = (row: int.Parse(startAsArray[0]), column: int.Parse(startAsArray[1]));
            var finishAsArray = Console.ReadLine().Split();
            var finish = (row : int.Parse(finishAsArray[0]), column : int.Parse(finishAsArray[1]));
        }
    }
}