using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC1.Days;

public class Day3
{
    public static void Execute()
    {
        string filePath = @"C:\Users\Lucas\Desktop\AoC\AoC1\Input\Day3.txt";
        string pattern = @"mul\(\d{1,3},\d{1,3}\)|don't\(\)|do\(\)"; // Example: Find SSN patterns like 123-45-6789

        Regex regex = new Regex(pattern);
        int total = 0;

        bool doMath = true;
        foreach (string line in File.ReadLines(filePath))
        {
            // Check for matches in the current line
            MatchCollection matches = regex.Matches(line);

        
            foreach (Match match in matches)
            {
                Console.WriteLine($"Found match: {match.Value}");


                string pattern2 = @"mul\((\d+),(\d+)\)";
                Match match2 = Regex.Match(match.Value, pattern2);

                if (match2.Success)
                {
                    // Extract the numbers as strings and parse them as integers
                    int num1 = int.Parse(match2.Groups[1].Value);
                    int num2 = int.Parse(match2.Groups[2].Value);

                    // Perform the multiplication
                    int result = num1 * num2;

                    if(doMath)
                        total += result;

                    Console.WriteLine($"The result of multiplying {num1} and {num2} is: {result}");
                }
                else
                {

                    if(match.Value.Contains("don't"))
                    {
                        doMath = false;
                    }
                    else if (match.Value.Contains("do"))
                    {
                        doMath = true;
                    }

                    Console.WriteLine("Invalid input format.");
                }

            }

            
        }

        Console.WriteLine(total);
    }
}
 