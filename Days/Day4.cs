using System;
using System.Formats.Asn1;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;

namespace AoC1.Days;

public class Day4
{
    public static void Execute()
    {
           string filePath = @"C:\Users\Lucas\Desktop\AoC\AoC1\Input\Day4.txt";  // Replace with the actual file path

        // Read the content from the file
         string[] lines;
        try
        {
            lines = File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
            return;
        }

        // Define the dimensions of the 2D array (14 rows and 140 columns)
        int rows = 140;
        int cols = 140;

        // Check if the number of lines is correct (should be 14)
        if (lines.Length != rows)
        {
            Console.WriteLine("The number of lines in the file does not match the expected number of rows.");
            return;
        }

        // Create the 2D array
        char[,] array = new char[rows, cols];

        // Fill the array with characters from the lines
        for (int i = 0; i < rows; i++)
        {
            string line = lines[i].Trim(); // Trim removes any leading/trailing whitespace like \r\n
            if (line.Length != cols)
            {
                Console.WriteLine($"Line {i + 1} does not have the expected number of columns.");
                return;
            }

            for (int j = 0; j < cols; j++)
            {
                array[i, j] = line[j];
            }
        }

        Console.WriteLine(array[138,139]);

        // Define the value to search for (e.g. 'X')
        char searchValue = 'X';

        // List to store the indexes of the value 'X'
        List<(int, int)> Xindexes = new List<(int x, int y)>();

        // Loop through the 2D array to find all occurrences of the search value
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array[i, j] == searchValue)
                {
                    Xindexes.Add((i, j)); // Add the indexes (row, column) to the list
                }
            }
        }


        int answer = 0;
        Console.WriteLine(Xindexes.Count);

        // Loop through all Xs
        foreach(var Xindex in Xindexes)
        {
            if(Xindex == (0,10))
            {
                Console.WriteLine(array[Xindex.Item1, Xindex.Item2]);
            }

            // Get neighboorCoords and that are M
            var MCoords = CheckLetters(array, getNeighborCoords(Xindex), 'M');

            Console.WriteLine(MCoords.Count);

            // need to check Ms for A neighbors as same slope as X-M
            foreach(var MCoord in MCoords)
            {
                // Find slope
                var thisSlope = getSlope(Xindex, MCoord);

                var ACoords = CheckLetters(array, getNeighborCoords(MCoord), 'A');
    
                foreach(var ACoord in ACoords)
                {
                    // Find slope
                    var thisnextSlope = getSlope(MCoord, ACoord);

                    if(thisSlope == thisnextSlope)
                    {
                        var SCoords = CheckLetters(array, getNeighborCoords(ACoord), 'S');


                            foreach(var SCoord in SCoords)
                            {
                                // Find slope
                                var thisnextnextSlope = getSlope(ACoord, SCoord);

                                if(thisnextSlope == thisnextnextSlope)
                                {
                                    answer++;
                                    
                                }
                                    
                        
                            }
                    }
                        
            
                }

            }

        }


        //Part 1
        Console.WriteLine("Part 1:");
        Console.WriteLine(answer);

        //
        //Part 2
        //
        Console.WriteLine("Part 2:");

        int part2Results = 0;


        // Define the value to search for (e.g. 'X')
        char Part2searchValue = 'A';

        // List to store the indexes of the value 'X'
        List<(int, int)> Aindexes = new List<(int x, int y)>();

        // Loop through the 2D array to find all occurrences of the search value
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array[i, j] == Part2searchValue)
                {
                    Aindexes.Add((i, j)); // Add the indexes (row, column) to the list
                }
            }
        }

        foreach(var aindex in Aindexes)
        {
            try
            {
            if(array[aindex.Item1-1,aindex.Item2-1] == 'M' && array[aindex.Item1-1,aindex.Item2+1] == 'S' && array[aindex.Item1+1,aindex.Item2-1] == 'M' && array[aindex.Item1+1,aindex.Item2+1] == 'S')
            {
                part2Results++;
            }
            else if(array[aindex.Item1-1,aindex.Item2-1] == 'S' && array[aindex.Item1-1,aindex.Item2+1] == 'M' && array[aindex.Item1+1,aindex.Item2-1] == 'S' && array[aindex.Item1+1,aindex.Item2+1] == 'M')
            {
                part2Results++;
            }
            else if(array[aindex.Item1-1,aindex.Item2-1] == 'M' && array[aindex.Item1-1,aindex.Item2+1] == 'M' && array[aindex.Item1+1,aindex.Item2-1] == 'S' && array[aindex.Item1+1,aindex.Item2+1] == 'S')
            {
                part2Results++;
            }
            else if(array[aindex.Item1-1,aindex.Item2-1] == 'S' && array[aindex.Item1-1,aindex.Item2+1] == 'S' && array[aindex.Item1+1,aindex.Item2-1] == 'M' && array[aindex.Item1+1,aindex.Item2+1] == 'M')
            {
                part2Results++;
            }
            }
            catch (Exception ex)
            {

            }


        }

        Console.WriteLine(part2Results);

    }


    private static (int rise, int run) getSlope((int x, int y) xCoord, (int x, int y) yCoord)
    {
        // Calculate the rise (y2 - y1)
            int rise = yCoord.y - xCoord.y;

            // Calculate the run (x2 - x1)
            int run = yCoord.x - xCoord.x;

            // Handle the case of vertical line (run = 0)
            if (run == 0)
            {
                // Infinite slope (vertical line)
                rise = 1; // You can use any non-zero value to represent the infinite slope
                run = 0;  // Representing vertical line
            }

            return (rise, run);
    }

    private static List<(int,int)> CheckLetters(char[,] array, List<(int x,int y)> coords, Char Char)
    {
        var results = new List<(int x,int y)> ();

        foreach(var coord in coords)
        {
            if(array[coord.x, coord.y] == Char)
            {
                results.Add(coord);
            }
        }

        return results;
    }

    private static List<(int,int)> getNeighborCoords((int x,int y) coords)
    {
        var results = new List<(int x,int y)> ();

        //both +1
        results.Add((coords.x + 1, coords.y +1));
        //both -1
        results.Add((coords.x - 1, coords.y - 1));
        //left +1 right -1
        results.Add((coords.x + 1, coords.y - 1));
        //left -1 right +1
        results.Add((coords.x - 1, coords.y + 1));
        //left +1
        results.Add((coords.x + 1, coords.y));
        //left -1
        results.Add((coords.x - 1, coords.y));
        //right +1
        results.Add((coords.x, coords.y + 1));
        //right -1
        results.Add((coords.x, coords.y - 1));



        return results.Where(e => e.x >= 0 && e.y >= 0 && e.x < 140 && e.y < 140).ToList();

    }
}
