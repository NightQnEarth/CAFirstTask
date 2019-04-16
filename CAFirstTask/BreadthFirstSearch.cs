using System.Collections.Generic;
using System.Linq;

namespace CAFirstTask
{
    public static class BreadthFirstSearch
    {
        public static IEnumerable<Cell> GetRoute(CellState[,] labyrinth, Cell start, Cell finish)
        {
            if (start.Equals(finish)) return new[] { finish };

            var queue = new Queue<Cell>();
            queue.Enqueue(start);
            var track = new Dictionary<Cell, Cell> { { start, null } };

            while (queue.Count != 0)
            {
                var currentCell = queue.Dequeue();
                foreach (var neighbour in GetAccessNeighbours(currentCell).Where(cell => !track.ContainsKey(cell)))
                {
                    queue.Enqueue(neighbour);
                    track.Add(neighbour, currentCell);
                    if (neighbour.Equals(finish)) return RouteRestore().Reverse();
                }
            }

            return null;

            IEnumerable<Cell> GetAccessNeighbours(Cell cell)
            {
                for (int delta = -1; delta < 7; delta += 2)
                {
                    var currentRow = cell.Row + (delta < 2 ? delta : 0);
                    var currentColumn = cell.Column + (delta > 2 ? delta - 4 : 0);
                    if (currentRow < 0 || currentColumn < 0 ||
                        currentRow >= labyrinth.GetLength(0) ||
                        currentColumn >= labyrinth.GetLength(1) ||
                        labyrinth[currentRow, currentColumn] == CellState.Wall) continue;
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