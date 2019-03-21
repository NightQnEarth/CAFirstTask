namespace CAFirstTask
{
    public class Cell
    {
        public readonly int Row;
        public readonly int Column;

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString() => $"{Row + 1} {Column + 1}";

        public override bool Equals(object obj) =>
            obj is Cell cell && cell.Row == Row && cell.Column == Column;

        public override int GetHashCode() => unchecked((Row * 397) ^ Column);
    }
}