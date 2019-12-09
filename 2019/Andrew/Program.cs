using System;

namespace AoC2019
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CharacterOCR.CharacterHeight = 6;
            CharacterOCR.CharacterWidth = 5;
            new Day2().Run();
            new Day3().Run();
            new Day4().Run(246515,739105);
            new Day5().Run();
            new Day6().Run();
            new Day7().Run();
            new Day8().Run(5);
            new Day9().Run();
            Console.WriteLine("\r\nCompleted!");
            //Console.ReadLine();
        }

    }
}
