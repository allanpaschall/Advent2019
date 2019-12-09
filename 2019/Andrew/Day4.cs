using System;
namespace AoC2019
{
    public class Day4
    {
        public Day4()
        {
        }
        public void Run(int start, int end)
        {
            int p1 = 0;
            int p2 = 0;
            for (int i = start; i < end; i++)
            {
                string I = i.ToString();
                bool isDouble = (I[0] == I[1] || I[1] == I[2] || I[2]==I[3] || I[3] == I[4] || I[4] == I[5]);
                bool isExactlyDouble = (
                    (I[0] == I[1] &&  I[2]!=I[1])
                    || (I[1] == I[2] && I[0]!=I[1] && I[2]!=I[3])
                    || (I[2] == I[3] && I[1]!=I[2] && I[3]!=I[4])
                    || (I[3] == I[4] && I[2]!=I[3] && I[4]!=I[5])
                    || (I[4] == I[5] && I[3]!=I[4]));
                bool isIncreasing = I[0] <= I[1] && I[1] <= I[2] && I[2] <= I[3] && I[3] <= I[4] && I[4] <= I[5];
                if (isDouble && isIncreasing)
                {
                    p1++;
                }
                if (isDouble && isIncreasing && isExactlyDouble)
                {
                    p2++;
                }
            }
            Console.WriteLine("Day 04,P1:" + p1);
            Console.WriteLine("Day 04,P2:" + p2);
        }
    }
}
