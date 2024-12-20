namespace day_3;

public class FirstStar
{
    public static int Run()
    {
        var input  = File.ReadAllText("./input.txt");

        var total = 0;

        while (true)
        {
            var index = input.IndexOf("mul(", StringComparison.Ordinal);

            if (index != -1)
            {
                var endIndex = input.IndexOf(')', index + 4);
                var str = input.Substring(index + 4, endIndex - index - 4);
                var nums = str.Split(',');

                if (nums.Length == 2 && int.TryParse(nums[0], out var x) && int.TryParse(nums[1], out var y))
                {
                    total += x * y;
                    input = input[(endIndex+1)..];
                }
                else
                {
                    input = input[(index + 5)..];
                }
            }
            else
            {
                break;
            }
        }

        return total;
    }
}