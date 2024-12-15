// Day1.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AoC1.Days
{
public static class Day1
{
    public static void Execute()
    {
        string filePath = @"C:\Users\Lucas\Desktop\AoC\AoC1\Input\Day1.csv"; // Path to your CSV file

        // Dictionary to hold column data
        Dictionary<int, List<int>> columns = new Dictionary<int, List<int>>();

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            // Split the line into values (assuming comma as delimiter)
            string[] values = line.Split(',');

            for (int i = 0; i < values.Length; i++)
            {
                if (!columns.ContainsKey(i))
                {
                    columns[i] = new List<int>();
                }

                columns[i].Add(int.Parse(values[i]));
            }
        }

        var list1Ordered = columns[0].OrderBy(e => e).ToList();
        var list2Ordered = columns[1].OrderBy(e => e).ToList();

        int totalDistance = 0;

        for (int i = 0; i < list1Ordered.Count; i++)
        {
            Console.WriteLine($"{list1Ordered[i]} - {list2Ordered[i]} = {Math.Abs(list1Ordered[i] - list2Ordered[i])}");
            totalDistance += Math.Abs(list1Ordered[i] - list2Ordered[i]);
        }

        Console.WriteLine("Total Distance: " + totalDistance);

        Console.WriteLine("Part 2");

        var counts = list2Ordered.GroupBy(e => e)
                                 .ToDictionary(e => e.Key, g => g.Count());

        var results = list1Ordered.Join(
            counts,
            e => e,
            d => d.Key,
            (e, d) => new {
                number = e,
                count = d.Value
            });

        int score = results.Sum(e => e.number * e.count);

        Console.WriteLine("Score: " + score);
    }
}

}
