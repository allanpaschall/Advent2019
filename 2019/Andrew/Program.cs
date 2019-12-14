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
            new Day10().Run();
            new Day11().Run();
            new Day12().Run();
            new Day13().Run(false);
            new Day14().Run();
            Console.WriteLine("\r\nCompleted!");
            //Console.ReadLine();
        }
        /*Day 02,P1:4023471
Day 02,P2:8051
Day 03,P1:1431
Day 03,P2:48012
Day 04,P1:1048
Day 04,P2:677
Day 05,P1:15259545
Day 05,P2:7616021
Day 06,P1:358244
Day 06,P2:517
Day 07,P1:437860
Day 07,P2:49810599
Day 08,P1:1320
Day 08,P2:RCYKR
Day 09,P1:3906448201
Day 09,P2:59785
Day 10,P1:309
Day 10,P2:416
Day 11,P1:1909
Day 11,P2:JUFEKHPH
Day 12,P1:9958
Day 12,P2:318382803780324 LCM(X:28482, Y:231614, Z:193052)
*/
    }
}
