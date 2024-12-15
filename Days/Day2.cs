using System;

namespace AoC1.Days;

public static class Day2
{
     public static void Execute()
    {
        string filePath = @"C:\Users\Lucas\Desktop\AoC\AoC1\Input\Day2.txt"; // Path to your file

        // List to store rows of integers
        var rows = new List<List<int>>();

        // Read the file line by line
        foreach (var line in File.ReadLines(filePath))
        {
            // Split each line by spaces, convert each part to an integer, and add to a list
            var row = new List<int>();
            foreach (var value in line.Split(' '))
            {
                row.Add(int.Parse(value));  // Parse the string value into an integer and add to the list
            }
            rows.Add(row);  // Add the row to the list of rows
        }

        // Print each row
        foreach (var row in rows)
        {
            Console.WriteLine(string.Join(" ", row));
        }

        var goodLists = new List<int>();

        for(int j = 0; j < rows.Count; j++)
        {

            var list = rows[j];

            if(checkList(list)){
                goodLists.Add(j);
            }
            else
            {
                if(redeemList(list))
                {
                    goodLists.Add(j);
                }
            }
             
        }

        Console.WriteLine(goodLists.Count); //680

    }

        public static bool redeemList(List<int> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                var testList = list.Where((item, index) => index != i).ToList();

                if(checkList(testList))
                {
                    return true;
                }

            }
            return false;
        }


        public static bool checkList(List<int> list)
        {
            // lets look at first pair of levels in list to determine increasing or decreasing
                int levelTruth = list[0] - list[1];

                Console.Write(levelTruth);
                
                //Determine increasing or decreasing or dead
                if(levelTruth <= 3 && levelTruth > 0)
                {
                    //decreasing
                    Console.WriteLine("decreasing");

                    for(int i = 2; i < list.Count;i++)
                    {
                        int quant = list[i-1] - list[i];

                     if(quant <= 3 && quant > 0)
                        {
                         if(i == list.Count-1){
                            return true;
                         }
                        }
                     else
                        {
                            return false;
                        }
                    }
                }
                else if(levelTruth > -4 && levelTruth < 0)
                {
                    //increasing
                    Console.WriteLine("increasing");

                    for(int i = 2; i < list.Count;i++)
                    {
                        int quant = list[i-1] - list[i];

                     if(quant > -4 && quant < 0)
                        {
                         if(i == list.Count-1){
                            return true;
                         }
                        }
                     else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed");
                    return false;
                }

                return false;
        }   
     
}
