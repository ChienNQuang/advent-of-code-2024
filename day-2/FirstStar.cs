namespace day_2;

public class FirstStar
{
    public static int Run()
    {
        var lines  = File.ReadAllLines("./input.txt");

        var total = 0;
        foreach (var t in lines)
        {
            var levels = t.Split(" ").Select(int.Parse).ToArray();
            var prevLevel = levels[0];
            var inc = prevLevel < levels[1];
            var safe = true;
            for (int j = 1; j < levels.Length; j++)
            {
                var num = levels[j];
                if (prevLevel < num != inc)
                {
                    safe = false;
                    break;
                }
                
                var distance = Math.Abs(num - prevLevel);
                if (distance is < 1 or > 3)
                {
                    safe = false;
                    break;
                }
                
                prevLevel = num;
            }

            if (safe) total++;
        }

        return total;
    }
}