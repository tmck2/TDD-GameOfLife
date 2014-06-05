using System.Linq;
using System.Text;

namespace Life
{
    public class Game
    {
        private char[,] _position;

        public char this[int col, int row]
        {
            get { return _position[col, row]; }
        }

        public Game(string position)
        {
            Parse(position);
        }

        private void Parse(string position)
        {
            var rows = position.Split('\n');
            Height = rows.Count();
            Width = rows[0].Count();    

            _position = new char[Height, Width];
            for (var col = 0; col < Width; col++)
                for (var row = 0; row < Height; row++)
                    _position[row, col] = rows[row][col];
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var row = 0; row < Height; row++)
            {
                sb.Append("\n");

                for (var col = 0; col < Width; col++)
                {
                    sb.Append(_position[row, col]);
                }
            }
            return sb.ToString().Substring(1);
        }

        public void NextGeneration()
        {
            var newPosition = new char[Height, Width];
            for (var row = 0; row < Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    newPosition[row, col] = _position[row, col];
                    var count = GetLiveNeighborCountFor(row, col);
                    if (_position[row, col] == '.' && count == 3)
                        newPosition[row, col] = '*';
                    if (_position[row, col] == '*' && count < 2)
                        newPosition[row, col] = '.';
                    if (_position[row, col] == '*' && count > 3)
                        newPosition[row, col] = '.';
                }
            }
            _position = newPosition;
        }

        private int GetLiveNeighborCountFor(int row, int col)
        {
            var neighbors = new[]
            {
                IsAlive(row - 1, col - 1), // nw
                IsAlive(row - 1, col), // n
                IsAlive(row - 1, col + 1), // ne
                IsAlive(row, col + 1), // e
                IsAlive(row + 1, col + 1), // se
                IsAlive(row+1,col), // s
                IsAlive(row+1,col-1), // sw 
                IsAlive(row,col-1) // w
            };
            return neighbors.Count(m => m);
        }

        private bool IsAlive(int row, int col)
        {
            if (row < 0) row = Height;
            if (col < 0) col = Height;
            if (row >= Height) row = 0;
            if (col >= Width) col = 0;
            
            return row >= 0 && row < Height
                   && col < Width && col >= 0
                   && _position[row, col] == '*';
        }
    }
}