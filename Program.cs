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

            // Action<int, int, int> action1 = (a, b, c) => File.AppendAllText("./data/out.csv", $"A: {a}, B: {b}, C: {c}, A+B: {a + b}\n");
            // foreach (var a in dataSet)
            //     action1(a.A, a.B, a.C);


            var fnDict2 = new Dictionary<string, Action<string, string, DataStructure>>()
            {
                {"sum", (colOne, colTwo, colData) => {
                    if (colOne == "A" && colTwo == "B")
                    File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, D(sum): {colData.A + colData.B}\n");
                    else if (colOne == "A" && colTwo == "C")
                    File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, D(sum): {colData.A + colData.C}\n");
                    else if (colOne == "B" && colTwo == "C")
                    File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, D(sum): {colData.B + colData.C}\n");
                        }
                    },

                {"product", (colOne, colTwo, colData) => {
                    if (colOne == "A" && colTwo == "B")
                    File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, E(product): {colData.A * colData.B}\n");
                    else if (colOne == "A" && colTwo == "C")
                    File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, E(product): {colData.A * colData.C}\n");
                    else if (colOne == "B" && colTwo == "C")
                    File.AppendAllText("./data/out.csv", $"A: {colData.A}, B: {colData.B}, C: {colData.C}, E(product): {colData.B * colData.C}\n");
                        }
                    },
            };

            var fnDict = new Dictionary<string, Action<int, int, int>>()
            {
                {"sum", (A, B, C) => File.AppendAllText("./data/out.csv", $"A: {A}, B: {B}, C: {C}, D(sum): {A + B}\n")},
                {"product", (A, B, C) => File.AppendAllText("./data/out.csv", $"A: {A}, B: {B}, C: {C}, E(product): {A * B}\n")},
            };

            foreach (var a in dataSet)
                Console.WriteLine(a);
            //UI
            Console.WriteLine("Here's your data, you can sum or multiply the columns. Type \"sum\" to add 2 columns, type \"product\" to multiply 2 columns.");
            var input = Console.ReadLine();
            Console.WriteLine("Enter two columns: ");
            var input1 = Console.ReadLine();
            var input2 = Console.ReadLine();
            // var input = "sum";
            foreach (var a in dataSet)
                fnDict2[input](input1, input2, a);

        }
    }
}
