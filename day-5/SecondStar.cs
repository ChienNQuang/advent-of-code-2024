namespace day_5;

public class SecondStar
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

        List<List<int>> incorrectUpdates = [];
        foreach (var update in updates)
        {
            List<int> noAfter = [];
            foreach (var page in update)
            {
                if (noAfter.Contains(page))
                {
                    incorrectUpdates.Add(update.ToList());
                    break;
                }
                if (dictionary.TryGetValue(page, out var result))
                {
                    noAfter.AddRange(result);
                }
            }
        }

        foreach (var incorrectUpdate in incorrectUpdates)
        {
            for (int i = 0; i < incorrectUpdate.Count - 1; i++)
            {
                for (int j = i + 1; j < incorrectUpdate.Count; j++)
                {
                    if (Compare(incorrectUpdate[i], incorrectUpdate[j]) == -1)
                    {
                        (incorrectUpdate[i], incorrectUpdate[j]) = (incorrectUpdate[j], incorrectUpdate[i]);
                    }
                }
            }
            
            var index = (incorrectUpdate.Count - 1) / 2;
            total += incorrectUpdate.ElementAt(index);
        }

        int Compare(int x, int y)
        {
            if (dictionary.TryGetValue(x, out var yNumbers) && yNumbers.Contains(y))
            {
                return -1;
            }

            if (dictionary.TryGetValue(y, out var xNumbers) && xNumbers.Contains(x))
            {
                return 1;
            }

            return 0;
        }

        return total;
    }
}