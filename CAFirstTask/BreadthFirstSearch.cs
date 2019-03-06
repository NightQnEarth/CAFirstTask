using System.Collections.Generic;
using System.Linq;

namespace CAFirstTask
{
    public class BreadthFirstSearch
    {
        public IEnumerable<Cell> GetRoute(Cell start, Cell finish, bool[,] matrix)
        {
            if (start.Equals(finish)) return new[] {finish};
            
            var queue = new Queue<Cell>();
            queue.Enqueue(start);
            var visited = new HashSet<Cell>{start};
            var parent = new Dictionary<Cell, Cell>{ {start, null} };
            
            while (queue.Count != 0)
            {
                var currentCell = queue.Dequeue();
                foreach (var neighbour in GetNeighbours(currentCell).Where(IsAccessibleCell))
                {
                    queue.Enqueue(neighbour);
                    visited.Add(neighbour);
                    parent.Add(neighbour, currentCell);
                    if (neighbour.Equals(finish)) return RouteRestore().Reverse();
                }
            }

            return null;
            
            bool IsAccessibleCell(Cell cell) => matrix[cell.Row, cell.Column] && !visited.Contains(cell);

            IEnumerable<Cell> GetNeighbours(Cell cell)
            {
                for (int delta = -1; delta < 7; delta += 2)
                {
                    var currentRow = cell.Row + (delta < 2 ? delta : 0);
                    var currentColumn = cell.Column + (delta > 2 ? delta - 4 : 0);
                    if (currentRow < 0 || currentRow >= matrix.GetLength(0) || 
                        currentColumn < 0 || currentColumn >= matrix.GetLength(1)) continue;
                    yield return new Cell(currentRow, currentColumn);
                }
            }
            
            IEnumerable<Cell> RouteRestore()
            {
                var currentPoint = finish;
                yield return currentPoint;
                while (parent[currentPoint] != null)
                {
                    yield return parent[currentPoint];
                    currentPoint = parent[currentPoint];
                }
            }
        }
    }
}