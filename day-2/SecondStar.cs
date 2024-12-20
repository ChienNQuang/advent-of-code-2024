namespace day_2;

public class SecondStar
{
    public static int Run()
    {
        var lines  = File.ReadAllLines("./input.txt");

        var total = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var levels = lines[i].Split(" ").Select(int.Parse).ToArray();
            var inc = false;
            var safe = true;
            var hasReset = false;
            for (int j = 0; j < levels.Length - 1; j++)
            {
                var currentLevel = levels[j];
                var nextLevel = levels[j + 1];
                if (j == 0)
                {
                    inc = currentLevel < nextLevel;
                }
                else if (j == 1 && hasReset)
                {
                    inc = currentLevel < nextLevel;
                }
                else
                {
                    if (currentLevel < nextLevel != inc)
                    {
                        if (!hasReset)
                        {
                            hasReset = true;
                            continue;
                        }

                        safe = false;
                        break;
                    }
                }
                
                var distance = Math.Abs(currentLevel - nextLevel);
                if (distance is < 1 or > 3)
                {
                    if (!hasReset)
                    {
                        hasReset = true;
                        continue;
                    }
                    
                    safe = false;
                    break;
                }
            }

            if (safe) total++;
        }

        return total;
    }
}