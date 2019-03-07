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
            var track = new Dictionary<Cell, Cell>{ {start, null} };
            
            while (queue.Count != 0)
            {
                var currentCell = queue.Dequeue();
                foreach (var neighbour in GetNeighbours(currentCell).Where(IsAccessibleNotVisitedCell))
                {
                    queue.Enqueue(neighbour);
                    track.Add(neighbour, currentCell);
                    if (neighbour.Equals(finish)) return RouteRestore().Reverse();
                }
            }

            return null;
            
            bool IsAccessibleNotVisitedCell(Cell cell) => matrix[cell.Row, cell.Column] && !track.ContainsKey(cell);

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
                while (track[currentPoint] != null)
                {
                    yield return track[currentPoint];
                    currentPoint = track[currentPoint];
                }
            }
        }
    }
}