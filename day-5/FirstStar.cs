namespace day_5;

public class FirstStar
{
    public static int Run()
    {
        var input  = File.ReadAllText("./input.txt");

        var total = 0;
        
        var firstHalf = input.Split("\n\n")[0];
        var secondHalf = input.Split("\n\n")[1];

        var orderingRules = firstHalf
            .Split("\n")
            .Select(x => x
                .Split("|")
                .Select(int.Parse));

        Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();

        foreach (var rule in orderingRules)
        {
            var prevPage = rule.ElementAt(0);
            var laterPage = rule.ElementAt(1);
            dictionary.TryGetValue(laterPage, out var numbers);
            if (numbers is null)
            {
                dictionary.Add(laterPage, [prevPage]);
            }
            else
            {
                dictionary[laterPage] = [..numbers, prevPage];
            }
        }

        var updates = secondHalf
            .Split("\n")
            .Select(x => x
                .Split(",")
                .Select(int.Parse));

        foreach (var update in updates)
        {
            bool rightOrdered = true;
            List<int> noAfter = [];
            foreach (var page in update)
            {
                if (noAfter.Contains(page))
                {
                    rightOrdered = false;
                    break;
                }
                if (dictionary.TryGetValue(page, out var result))
                {
                    noAfter.AddRange(result);
                }
            }

            if (rightOrdered)
            {
                var index = (update.Count() - 1) / 2;
                total += update.ElementAt(index);
            }
        }

        return total;
    }
}