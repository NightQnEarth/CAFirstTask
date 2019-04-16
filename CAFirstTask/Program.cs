using System;

namespace CAFirstTask
{
    public static class Program
    {
        public static void Main()
        {
            var finder = new BreadthFirstSearch();
            var (labyrinth, start, finish) = DataParser.GetInputData(Console.ReadLine);
            var resultRoute = finder.GetRoute(labyrinth, start, finish);

            Console.WriteLine(DataParser.ResultGenerate(resultRoute));
        }
    }
}