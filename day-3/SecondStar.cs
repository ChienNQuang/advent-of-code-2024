namespace day_3;

public class SecondStar
{
    public static int Run()
    {
        var input = File.ReadAllText("./input.txt");

        var total = 0;

        var ok = true;
        while (true)
        {
            var index = input.IndexOf("mul(", StringComparison.Ordinal);
            var doIndex = input.IndexOf("do()", StringComparison.Ordinal);
            var dontIndex = input.IndexOf("don't()", StringComparison.Ordinal);
            var min = Math.Min(doIndex, Math.Min(dontIndex, index));
            if (index * doIndex * dontIndex == -1 || index == -1)
            {
                break;
            }

            if (min == -1)
            {
                List<int> nums = [index];
                if (doIndex != -1) nums.Add(doIndex);
                if (dontIndex != -1) nums.Add(dontIndex);
                min = nums.Min();
            }

            if (min == doIndex)
            {
                input = input[(min + 4)..];
                ok = true;
            }

            if (min == dontIndex)
            {
                input = input[(min + 7)..];
                ok = false;
            }

            if (min != index) continue;
            
            if (ok)
            {

                var endIndex = input.IndexOf(')', index + 4);
                var str = input.Substring(index + 4, endIndex - index - 4);
                var nums = str.Split(',');

                if (nums.Length == 2 && int.TryParse(nums[0], out var x) && int.TryParse(nums[1], out var y))
                {
                    total += x * y;
                    input = input[(endIndex)..];
                }
                else
                {
                    input = input[(index + 4)..];
                }
            }
            else
            {
                input = input[(index + 4)..];
            }

        }

        return total;
    }
}