using System;
using System.Collections.Generic;
using System.Linq;
namespace AoC2019
{
    public class Day16
    {
        public Day16()
        {
        }
        public void Run()
        {
            var begin = System.DateTime.Now;
            int[] iPhase = RunDay(Data, false,0);
            var timeSpent = (System.DateTime.Now - begin).TotalMilliseconds;
            Console.WriteLine("Day 16,P1:" + iPhase[0] + iPhase[1] + iPhase[2] + iPhase[3] + iPhase[4] + iPhase[5] + iPhase[6] + iPhase[7] + ", completed in " + timeSpent + " milliseconds");
            begin = System.DateTime.Now;
            var offset = int.Parse(Data.Substring(0, 7));
            iPhase = RunDay(Data, true, int.Parse(Data.Substring(0, 7)));
            timeSpent = (System.DateTime.Now - begin).TotalMilliseconds;
            Console.WriteLine("Day 16,P2:" + iPhase[offset] + iPhase[offset + 1] + iPhase[offset + 2] + iPhase[offset + 3] + iPhase[offset + 4] + iPhase[offset + 5] + iPhase[offset + 6] + iPhase[offset + 7] + ", completed in " + timeSpent + " milliseconds");


        }

        private static int[] RunDay(string data, bool stretchData, int offset)
        {
            int[] oPatternFast = { 0, 1, 0, -1 };
            int[] iPhase;
            if (!stretchData)
                iPhase = (from iP in data.ToCharArray() select int.Parse("" + iP)).ToArray();
            else
            {
                //offset must be over 50% of length....
                iPhase = new int[data.Length * 10000];
                for (int i = offset; i < iPhase.Length; i++)
                {
                    iPhase[i] = int.Parse("" + data[i % data.Length]);
                }
            }
            int[] oPhase = new int[iPhase.Length];
            int iPhaseLength = iPhase.Length;
            int iPhaseLengthHalved = iPhaseLength / 2;
            for (int phase = 0; phase < 100; phase++)
            {
                for (int i = offset; i < iPhaseLengthHalved; i++)
                {
                    long output = 0;
                    for (int j = i; j < iPhaseLength; j++)
                    {
                        var oPatternCheck = oPatternFast[((j + 1) / (i + 1)) % 4];
                        if (oPatternCheck == 1)
                        {
                            output += iPhase[j];
                        }
                        else if (oPatternCheck == -1)
                        {
                            output -= iPhase[j];
                        }
                    }
                    if (output < 0)
                        output = -output;
                    oPhase[i] = (int)(output % 10);
                }
                int outputHalved = 0;
                for (int i = 0; i < iPhaseLengthHalved; i++)
                {
                    outputHalved += iPhase[iPhaseLengthHalved + i];
                }
                for (int i = 0; i < iPhaseLengthHalved; i++)
                {
                    oPhase[iPhaseLengthHalved + i] = outputHalved % 10;
                    outputHalved -= iPhase[iPhaseLengthHalved + i];
                }
                var temp = iPhase;
                iPhase = oPhase;
                oPhase = temp;
            }

            return iPhase;
        }

        public string TestData = "12345678";

        public string TestData2 = "03036732577212944063491565474664";

        public string Data = "59701570675760924481468012067902478812377492613954566256863227624090726538076719031827110112218902371664052147572491882858008242937287936208269895771400070570886260309124375604950837194732536769939795802273457678297869571088110112173204448277260003249726055332223066278888042125728226850149451127485319630564652511440121971260468295567189053611247498748385879836383604705613231419155730182489686270150648290445732753180836698378460890488307204523285294726263982377557287840630275524509386476100231552157293345748976554661695889479304833182708265881051804444659263862174484931386853109358406272868928125418931982642538301207634051202072657901464169114";
    }
}
