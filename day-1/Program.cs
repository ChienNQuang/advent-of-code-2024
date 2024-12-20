// See https://aka.ms/new-console-template for more information
Console.WriteLine(SecondStar());
int FirstStar()
{
    var lines  = File.ReadAllLines("./input.txt");

    int[] list1 = new int[1000];
    int[] list2 = new int[1000];

    for (int i = 0; i < 1000; i++)
    {
        var nums = lines[i].Split("   ").Select(int.Parse).ToArray();
        list1[i] = nums[0];
        list2[i] = nums[1];
    }

    list1 = list1.Order().ToArray();
    list2 = list2.Order().ToArray();

    var total = 0;

    for (int i = 0; i < 1000; i++)
    {
        total += Math.Abs(list1[i] - list2[i]);
    }

    return total;
}

int SecondStar()
{
    var lines  = File.ReadAllLines("./input.txt");

    int[] list1 = new int[1000];
    int[] list2 = new int[1000];

    for (int i = 0; i < 1000; i++)
    {
        var nums = lines[i].Split("   ").Select(int.Parse).ToArray();
        list1[i] = nums[0];
        list2[i] = nums[1];
    }

    list1 = list1.Order().ToArray();
    list2 = list2.Order().ToArray();

    var total = 0;

    var secondIndex = 0;
    for (int i = 0; i < 1000; i++)
    {
        var num1 = list1[i];
        var times = 0;
        if (secondIndex == 1000)
        {
            break;
        }
        while (true)
        {
            if (secondIndex == 1000)
            {
                break;
            }
            var num2 = list2[secondIndex];
            if (num1 == num2)
            {
                times++;
                secondIndex++;
            } else if (num1 < num2)
            {
                break;
            }
            else
            {
                secondIndex++;
            }
        }
        total += times * num1;
    }

    return total;
}