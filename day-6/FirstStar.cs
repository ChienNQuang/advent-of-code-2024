namespace day_6;

public class FirstStar
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
    public static int Run()
    {
        var input  = File.ReadAllLines("./input.txt");

        string[][] matrix = new string[input[0].Length][];
        var total = 0;

        var currentI = 0;
        var currentJ = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if (matrix[i] is null)
            {
                matrix[i] = new string[line.Length];
            }
            for (int j = 0; j < line.Length; j++)
            {
                matrix[i][j] = line[j].ToString();
                if (line[j].ToString().Equals("^"))
                {
                    currentI = i;
                    currentJ = j;
                }
            }
        }

        var maxI = matrix.Length;
        var maxJ = matrix[0].Length;

        List<string> list = [];
        while (!IsEdge(currentI, currentJ))
        {
            (currentI, currentJ) = Move(currentI, currentJ, Direction.Up);
        }

        total = list.Distinct().Count();

        (int i, int j) Move(int i, int j, Direction direction)
        {
            list.Add($"{i}__{j}");
            int newI = i;
            int newJ = j;

            switch (direction)
            {
                case Direction.Up:
                    newI -= 1;
                    break;
                case Direction.Down:
                    newI += 1;
                    break;
                case Direction.Left:
                    newJ -= 1;
                    break;
                case Direction.Right:
                    newJ += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (ValidI(newI) && ValidJ(newJ))
            {
                var token = matrix[newI][newJ];
                if (token.Equals("#"))
                {
                    var newDirection = direction switch
                    {
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Up,
                        Direction.Right => Direction.Down,
                        Direction.Up => Direction.Right,
                        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                    };
                    return Move(i, j, newDirection);
                }

                if (token.Equals(".") || token.Equals("^"))
                {
                    return Move(newI, newJ, direction);
                }

            }
            
            return (i, j);
        }

        bool IsEdge(int i, int j)
        {
            return i == 0 || i == maxI - 1 || j == 0 || j == maxJ - 1;
        }
        
        bool ValidI(int val)
        {
            return val >= 0 && val < input.Length;
        }

        bool ValidJ(int val)
        {
            return val >= 0 && val < input[0].Length;
        }

        return total;
    }
}