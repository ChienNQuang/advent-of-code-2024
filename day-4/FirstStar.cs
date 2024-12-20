namespace day_4;

public class FirstStar
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
    }
    public static int Run()
    {
        var input  = File.ReadAllLines("./input.txt");

        string[][] matrix = new string[input[0].Length][];
        var total = 0;

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
            }
        }
        
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                var curr = matrix[i][j];
                if (curr == "X")
                {
                    total += Check(i, j);
                }
            }
        }

        int Check(int i, int j)
        {
            var localTotal = 0;

            if (CheckDirection(i, j, "M", ["A", "S"], Direction.Up)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.Down)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.Left)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.Right)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.UpLeft)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.UpRight)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.DownLeft)) localTotal++;
            if (CheckDirection(i, j, "M", ["A", "S"], Direction.DownRight)) localTotal++;

            return localTotal;
        }

        bool CheckDirection(int i, int j, string currChar, string[] leftChars, Direction direction)
        {
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
                case Direction.UpLeft:
                    newI -= 1;
                    newJ -= 1;
                    break;
                case Direction.UpRight:
                    newI -= 1;
                    newJ += 1;
                    break;
                case Direction.DownLeft:
                    newI += 1;
                    newJ -= 1;
                    break;
                case Direction.DownRight:
                    newI += 1;
                    newJ += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (ValidI(newI) && ValidJ(newJ) && matrix[newI][newJ].Equals(currChar))
            {
                if (leftChars.Length == 0)
                {
                    return true;
                }

                return CheckDirection(newI, newJ, leftChars[0], leftChars[1..], direction);
            }

            return false;
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