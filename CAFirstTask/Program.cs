using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace CAFirstTask
{
    struct Cell
    {
        public readonly int Row;
        public readonly int Column;

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString() => $"({Row}, {Column})";
    }

    static class Program
    {        
        public static void Main(string[] args)
        {
            var (start, finish, matrix) = GetInputData();
            var routeLength = GetRouteLength(start, finish, matrix);
            
            Console.WriteLine(routeLength == null ? "NO" : $"YES\n{routeLength}");
        }

        private static int? GetRouteLength(Cell start, Cell finish, bool[,] matrix)
        {
            var visited = new List<Cell>();
            var queue = new Queue<Cell>();
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count != 0)
            {
                var currentCell = queue.Dequeue();
                foreach (var neighbour in GetNeighbours(currentCell).Where(IsAccessibleCell))
                {
                    queue.Enqueue(neighbour);
                    visited.Add(neighbour);
                    if (neighbour.Equals(finish)) return visited.Count;
                }
            }

            return null;
            
            bool IsAccessibleCell(Cell cell) => matrix[cell.Row, cell.Column] && !visited.Contains(cell);

            IEnumerable<Cell> GetNeighbours(Cell cell) =>
                Enumerable.Range(-1, 3)
                          .SelectMany(deltaRow => Enumerable.Range(-1, 3)
                                                            .Where(deltaColumn =>
                                                                       Math.Abs(deltaRow) + Math.Abs(deltaColumn) == 1)
                                                            .Select(deltaColumn =>
                                                                        new Cell(cell.Row + deltaRow, 
                                                                                 cell.Column + deltaColumn)));
        }

        private static (Cell start, Cell finish, bool[,] matrix) GetInputData()
        {   
            var rowCount = int.Parse(Console.ReadLine().Trim());
            var columnCount = int.Parse(Console.ReadLine().Trim());
            var matrix = new bool[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                var currentRow = ReadLineToArray();
                for (int column = 0; column < columnCount; column++)
                    matrix[row, column] = int.Parse(currentRow[column].Trim()) == 0;
            }

            var startAsArray = ReadLineToArray();
            var start = new Cell(int.Parse(startAsArray[0]) - 1, int.Parse(startAsArray[1]) - 1);
            var finishAsArray = ReadLineToArray();
            var finish = new Cell(int.Parse(finishAsArray[0]) - 1, int.Parse(finishAsArray[1]) - 1);

            return (start, finish, matrix);

            string[] ReadLineToArray() => Regex.Split(Console.ReadLine(), @"\W+")
                                               .Where(str => str.Length > 0)
                                               .ToArray();
        }
    }
}