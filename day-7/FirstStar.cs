namespace day_7;

public class FirstStar
{
    public static long Run()
    {
        var lines  = File.ReadAllLines("./input2.txt");

        long total = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(": ");
            var value = long.Parse(parts[0]); 
            var numbers = parts[1].Split(" ").Select(long.Parse);

            var combinations = Calculate(numbers.ToList());
            if (combinations.Contains(value))
            {
                total += value;
            }

            List<long> Calculate(List<long> nums)
            {
                if (nums.Count == 1)
                {
                    return [nums[0]];
                }
                var plus = nums[0] + nums[1];
                var multiply = nums[0] * nums[1];
                var plusCombinations = Calculate([plus, ..nums[2..]]);
                var multiplyCombinations = Calculate([multiply, ..nums[2..]]);
                return [..plusCombinations, ..multiplyCombinations];
            }
        }


        return total;
    }
}