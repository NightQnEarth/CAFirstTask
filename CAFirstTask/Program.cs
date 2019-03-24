using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CAFirstTask
{
    public static class Program
    {
        public static void Main()
        {
            var finder = new BreadthFirstSearch();
            var (labyrinth, start, finish) = GetInputData(Console.ReadLine);
            var resultRoute = finder.GetRoute(labyrinth, start, finish);

            Console.WriteLine(ResultGenerate(resultRoute));
        }

        public static string ResultGenerate(IEnumerable<Cell> route) =>
            route == null ? "N" : string.Join(Environment.NewLine, "Y", string.Join(Environment.NewLine, route));

        public static (CellState[,] labyrinth, Cell start, Cell finish) GetInputData(Func<string> lineReader)
        {
            var rowCount = int.Parse(lineReader().Trim());
            var columnCount = int.Parse(lineReader().Trim());
            var labyrinth = new CellState[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                var currentRow = ReadLineToArray();
                for (int column = 0; column < columnCount; column++)
                    labyrinth[row, column] = int.Parse(currentRow[column]) == 0 ? CellState.Free : CellState.Wall;
            }

            var startAsArray = ReadLineToArray();
            var start = new Cell(int.Parse(startAsArray[0]) - 1, int.Parse(startAsArray[1]) - 1);
            var finishAsArray = ReadLineToArray();
            var finish = new Cell(int.Parse(finishAsArray[0]) - 1, int.Parse(finishAsArray[1]) - 1);

            return (labyrinth, start, finish);

            string[] ReadLineToArray() => Regex.Split(lineReader(), @"\W+")
                                               .Where(str => str.Length > 0)
                                               .ToArray();
        }
    }
}