using System;
using System.Collections.Generic;
using System.Linq;

namespace CAFirstTask
{
    public class BreadthFirstSearch
    {
        public List<Cell> GetRoute(Cell start, Cell finish, bool[,] matrix)
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
                    if (neighbour.Equals(finish)) return visited;
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
    }
}