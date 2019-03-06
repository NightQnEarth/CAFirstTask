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

        public override string ToString() => $"{Row} {Column}";

        public override bool Equals(object obj) =>
            !(obj is Cell) &&
            obj == null ||
            Row == ((Cell)obj).Row &&
            Column == ((Cell)obj).Column;

        public override int GetHashCode() => 1039 * Row + Column;
    }
}