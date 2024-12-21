namespace day_6;

public class SecondStar
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
        var input = File.ReadAllLines("./input2.txt");

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
        
        var initI = currentI;
        var initJ = currentJ;

        var maxI = matrix.Length;
        var maxJ = matrix[0].Length;

        List<(int i, int j, Direction d)> list = [];
        var currentDirection = Direction.Up;
        while (!IsEdge(currentI, currentJ))
        {
            // (int i, int j, Direction d) last = list.LastOrDefault();
            // if (last is not { i: 0, j: 0 } && last.i == currentI && last.j == currentJ)
            // {
            //     list.Remove(last);
            // }

            list.Add((currentI, currentJ, currentDirection));
            (currentI, currentJ, currentDirection) = Move(currentI, currentJ, currentDirection);
        }
        list.Add((currentI, currentJ, currentDirection));

        list = list.Distinct().ToList();

        List<(int i, int j, Direction d)> firstList = [list[0]];
        
        for (int k = 1; k < list.Count; k++)
        {
            var (i, j, direction) = list[k];
            var changeDirection = direction switch
            {
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                Direction.Right => Direction.Down,
                Direction.Up => Direction.Right,
            };
            var nextPosition = Move(i, j, direction);
            if (firstList.Any(x => x.i == nextPosition.i && x.j == nextPosition.j))
            {
                firstList.Add(list[k]);
                continue;
            }
            if (nextPosition.i == initI && nextPosition.j == initJ)
            {
                firstList.Add(list[k]);
                continue;
            }
            var temp = matrix[nextPosition.i][nextPosition.j];
            matrix[nextPosition.i][nextPosition.j] = "#";
            var result = CheckLoop([..firstList], i, j, changeDirection);
            
            if (result)
            {
                total++;
            }
            firstList.Add(list[k]);
            matrix[nextPosition.i][nextPosition.j] = temp;
        }

        bool CheckLoop(List<(int i, int j, Direction d)> previousMoves, int i, int j, Direction direction)
        {
            var isLoop = false;
            while (!IsEdge(i, j))
            {
                if (previousMoves.Any(x => x.i == i && x.j == j && direction == x.d))
                {
                    isLoop = true;
                    break;
                }
                previousMoves.Add((i, j, direction));
                (i, j, direction) = Move(i, j, direction);
            }

            return isLoop;
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

        (int i, int j, Direction direction) Move(int i, int j, Direction direction)
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
                    direction = newDirection;
                }

                if (token.Equals(".") || token.Equals("^"))
                {
                    i = newI;
                    j = newJ;
                }
            }

            return (i, j, direction);
        }
    }
}