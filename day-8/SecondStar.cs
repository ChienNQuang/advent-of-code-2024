namespace day_8;

public class SecondStar
{
    public static long Run()
    {
        var lines  = File.ReadAllLines("./input2.txt");

        string[][] matrix = new string[lines[0].Length][];
        var total = 0;

        var coordinateDictionary = new Dictionary<string, List<(int, int)>>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (matrix[i] is null)
            {
                matrix[i] = new string[line.Length];
            }
            for (int j = 0; j < line.Length; j++)
            {
                matrix[i][j] = line[j].ToString();

                if (matrix[i][j].Equals(".")) continue;
                
                if (!coordinateDictionary.ContainsKey(matrix[i][j]))
                {
                    coordinateDictionary.Add(matrix[i][j], []);
                }
                coordinateDictionary[matrix[i][j]].Add((i, j));
            }
        }

        var frequencyCoordinates = coordinateDictionary.Values.SelectMany(x => x).ToList();
        
        var list = new List<(int, int)>();

        foreach (var frequency in coordinateDictionary.Keys)
        {
            var coordinates = coordinateDictionary[frequency];
            for (int i = 0; i < coordinates.Count - 1; i++)
            {
                for (int j = i + 1; j < coordinates.Count; j++)
                {
                    var first = coordinates[i];
                    var second = coordinates[j];
                    var antinodes = GetAntinodeCoordinates(first.Item1, first.Item2, second.Item1, second.Item2);
                    antinodes.ForEach(x =>
                    {
                        if (!frequencyCoordinates.Contains(x))
                        {
                            list.Add(x);
                        } ;
                    });
                }
            }
        }

        list = list.Distinct().ToList();
        
        total = list.Count + frequencyCoordinates.Count;

        List<(int i, int j)> GetAntinodeCoordinates(int iA, int jA, int iB, int jB)
        {
            var leftAntinodes = new List<(int i, int j)>();
            var rightAntinodes = new List<(int i, int j)>();
            var index = 1;
            while (true)
            {
                var leftAntinodeI = iA + (iA - iB) * index;
                var leftAntinodeJ = jA + (jA - jB) * index;
                if (ValidI(leftAntinodeI) && ValidJ(leftAntinodeJ))
                {
                    leftAntinodes.Add((leftAntinodeI, leftAntinodeJ));
                    index++;
                }
                else
                {
                    break;
                }
            }

            index = 1;
            
            while (true)
            {
                var rightAntinodeI = iB + (iB - iA) * index;
                var rightAntinodeJ = jB + (jB - jA) * index;
                if (ValidI(rightAntinodeI) && ValidJ(rightAntinodeJ))
                {
                    leftAntinodes.Add((rightAntinodeI, rightAntinodeJ));
                    index++;
                }
                else
                {
                    break;
                }
            }
            return [..leftAntinodes, ..rightAntinodes];
        } 
        
        bool ValidI(int val)
        {
            return val >= 0 && val < lines.Length;
        }

        bool ValidJ(int val)
        {
            return val >= 0 && val < lines[0].Length;
        }
        
        
        return total;
    }
}