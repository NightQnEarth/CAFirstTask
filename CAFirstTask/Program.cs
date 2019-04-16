using System;

namespace CAFirstTask
{
    public static class Program
    {
        public static void Main()
        {
            var (labyrinth, start, finish) = DataParser.GetInputData(Console.ReadLine);
            var resultRoute = BreadthFirstSearch.GetRoute(labyrinth, start, finish);

            Console.WriteLine(DataParser.ResultGenerate(resultRoute));
        }
    }
}