using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using MyExt;

namespace Data_Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataJson = File.ReadAllText("./data/generic.json");
            var dataSet = JsonSerializer.Deserialize<List<DataStructure>>(dataJson);

            var fnDict = new Dictionary<string, Action<string, string, DataStructure>>()
            {
                {"sum", (colOne, colTwo, colData) => File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, D(sum): {Convert.ToInt16(typeof(DataStructure).GetProperty(colOne).GetValue(colData)) + Convert.ToInt16(typeof(DataStructure).GetProperty(colTwo).GetValue(colData))}\n")

            },

               {"product", (colOne, colTwo, colData) => File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, E(product): {Convert.ToInt16(typeof(DataStructure).GetProperty(colOne).GetValue(colData)) * Convert.ToInt16(typeof(DataStructure).GetProperty(colTwo).GetValue(colData))}\n")

                },
            };

            //UI
            foreach (var a in dataSet)
                Console.WriteLine(a);
            Console.WriteLine("Here's your data, you can type \"sum\" to add 2 columns, \"product\" to multiply 2 columns, or \"mutate\" to mutate a column.");

            var input = Console.ReadLine();

            if (input == "sum" || input == "product")
            {
                Console.WriteLine("Enter two columns: ");
                var input1 = Console.ReadLine();
                var input2 = Console.ReadLine();

                foreach (var a in dataSet)
                    fnDict[input](input1, input2, a);
                Console.WriteLine("Done! Results are written to ./data/out.json");
            }

            else //mutation
            {
                Console.WriteLine("Enter a column to mutate on: ");
                var mutateCol = Console.ReadLine();

                foreach (var col in dataSet)
                {
                    if (mutateCol == "A")
                        File.AppendAllText("./data/out.csv", $"A: {col.A.Mutate()}, B: {col.B}, C: {col.C}\n");
                    else if (mutateCol == "B")
                        File.AppendAllText("./data/out.csv", $"A: {col.A}, B: {col.B.Mutate()}, C: {col.C}\n");
                    else if (mutateCol == "C")
                        File.AppendAllText("./data/out.csv", $"A: {col.A}, B: {col.B}, C: {col.C.Mutate()}\n");
                }
                Console.WriteLine("Done! Results are written to ./data/out.json");
            }


        }
    }
}
