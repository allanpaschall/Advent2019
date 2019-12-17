using System;
using System.Linq;
using System.Collections.Generic;
namespace AoC2019
{
    public static class StoreAside
    {
        public static Dictionary<string,long> Storage { get; set; }
        public static void Store(string Name, long QTY)
        {
            if (!Storage.ContainsKey(Name))
            {
                Storage[Name] = 0;
            }
            Storage[Name] += QTY;
        }
        public static long Take(string Name, long QTY)
        {
            if (!Storage.ContainsKey(Name))
            {
                Storage[Name] = 0;
            }
            if (Storage[Name]==0)
            {
                return 0;
            }
            if (Storage[Name] >= QTY)
            {
                Storage[Name] -= QTY;
                return QTY;
            }
            else
            {
                var temp = Storage[Name];
                Storage[Name] = 0;
                return temp;
            }
        }
    }
    public class Chemical
    {
        public string Name { get; set; }
        public long EndQTY { get; set; }
        public List<Reaction> ToProduce { get; private set; }
        public Chemical()
        {
            ToProduce = new List<Reaction>();
        }
        public long Produce(long NeededQTY)
        {
            //NeededQTY will come in as....7 for example, but EndQTY may be 10
            if (EndQTY != -1)
            {
                //determine multiplier. until NeededQTY <= EndQty, increment multiplier...
                NeededQTY -= StoreAside.Take(Name, NeededQTY);
                if (NeededQTY==0)
                {
                    return 0;//it has already been produced...don't create more waste...
                }
                long multiplier = (long)Math.Ceiling((decimal)NeededQTY / (decimal)EndQTY);
                long ORENeeded = 0;
                foreach (var r in ToProduce)
                {
                    ORENeeded += r.Chemical.Produce(r.NeededQty * multiplier);
                }
                StoreAside.Store(Name, (EndQTY*multiplier) - NeededQTY);
                return ORENeeded;
            }
            StoreAside.Take(Name, NeededQTY);
            return NeededQTY;//In this case, it is a root element...ORE
        }
    }
    public class Reaction
    {
        public Chemical Chemical { get; set; }
        public long NeededQty { get; set; }
    }
    public class Day14
    {
        public Day14()
        {
        }
        public void Run()
        {
            var begin = DateTime.Now;
            StoreAside.Storage = new Dictionary<string, long>();
            Dictionary<string,Chemical> chemicals = new Dictionary<string,Chemical>();
            List<Reaction> reactions = new List<Reaction>();
            var data = Data;
            var textReactions = data.Split('\n');
            foreach (var r in textReactions)
            {
                var elements = r.Trim().Replace(" => ", "~").Replace(", ", "~").Split('~');
                //if (chemicals.ContainsKey())
                foreach (var element in elements)
                {
                    string name = element.Trim().Split(' ')[1];
                    if (!chemicals.ContainsKey(name))
                    {
                        chemicals[name] = new Chemical() { Name = name, EndQTY = -1 };
                    }
                }
                chemicals[elements.Last().Split(' ')[1]].EndQTY = long.Parse(elements.Last().Split(' ')[0]);
                var chemical = chemicals[elements.Last().Split(' ')[1]];
                foreach (var element in elements.Reverse().Skip(1))
                {
                    string name = element.Trim().Split(' ')[1];
                    chemical.ToProduce.Add(new Reaction()
                    {
                        Chemical = chemicals[name],
                        NeededQty = long.Parse(element.Trim().Split(' ')[0])
                    });
                }
            }
            StoreAside.Storage["ORE"] = 1000000000000;
            //                          1000000000000
            var result = chemicals["FUEL"].Produce(1);
            Console.WriteLine("Day 14,P1:" + result + ", completed in " + (System.DateTime.Now - begin).TotalMilliseconds + " milliseconds");
            begin = DateTime.Now;
            int test = 1 ;
            bool wasPriorTestTooHigh;
            do
            {
                test *= 10;
                StoreAside.Storage = new Dictionary<string, long>();
                StoreAside.Storage["ORE"] = 1000000000000;
                chemicals["FUEL"].Produce(test);
                wasPriorTestTooHigh = (StoreAside.Storage["ORE"] > 0);
            } while (wasPriorTestTooHigh);
            int resolution = test / 10;
            wasPriorTestTooHigh = !(StoreAside.Storage["ORE"] > 0);
            do
            {
                StoreAside.Storage = new Dictionary<string, long>();
                StoreAside.Storage["ORE"] = 1000000000000;
                if (wasPriorTestTooHigh)
                {
                    //the test was too high, meaning we should decrement test by resolution until it is too low,
                    //then when too low, capture in "increase" resolution by /10.
                    test -= resolution;
                }
                else
                {
                    test += resolution;
                    resolution = resolution / 10;
                    test -= resolution;
                }
                chemicals["FUEL"].Produce(test);
                wasPriorTestTooHigh = !(StoreAside.Storage["ORE"] > 0);
            }
            while (resolution != 0);
            Console.WriteLine("Day 14,P2:" + (test-1) + ", completed in " + (System.DateTime.Now - begin).TotalMilliseconds + " milliseconds");


        }
        public string TestData1 = @"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL";
        public string TestData2 = @"9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL";
        public string TestData3 = @"157 ORE => 5 NZVS
165 ORE => 6 DCFZ
44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
179 ORE => 7 PSHF
177 ORE => 5 HKGWZ
7 DCFZ, 7 PSHF => 2 XJWVT
165 ORE => 2 GPVTF
3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT";

        public string TestData4 = @"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX";

        public string Data = @"1 JKXFH => 8 KTRZ
11 TQGT, 9 NGFV, 4 QZBXB => 8 MPGLV
8 NPDPH, 1 WMXZJ => 7 VCNSK
1 MPGLV, 6 CWHX => 5 GDRZ
16 JDFQZ => 2 CJTB
1 GQNQF, 4 JDFQZ => 5 WJKDC
2 TXBS, 4 SMGQW, 7 CJTB, 3 NTBQ, 13 CWHX, 25 FLPFX => 1 FUEL
3 WMXZJ, 14 CJTB => 5 FLPFX
7 HDCTQ, 1 MPGLV, 2 VFVC => 1 GSVSD
1 WJKDC => 2 NZSQR
1 RVKLC, 5 CMJSL, 16 DQTHS, 31 VCNSK, 1 RKBMX, 1 GDRZ => 8 SMGQW
2 JDFQZ, 2 LGKHR, 2 NZSQR => 9 TSWN
34 LPXW => 8 PWJFD
2 HDCTQ, 2 VKWN => 8 ZVBRF
2 XCTF => 3 QZBXB
12 NGFV, 3 HTRWR => 5 HDCTQ
1 TSWN, 2 WRSD, 1 ZVBRF, 1 KFRX, 5 BPVMR, 2 CLBG, 22 NPSLQ, 9 GSVSD => 5 NTBQ
10 TSWN => 9 VFVC
141 ORE => 6 MKJDZ
4 NPSLQ, 43 VCNSK, 4 PSJL, 14 KTRZ, 3 KWCDP, 3 HKBS, 11 WRSD, 3 MXWHS => 8 TXBS
8 VCNSK, 1 HDCTQ => 7 MXWHS
3 JDFQZ, 2 GQNQF => 4 XJSQW
18 NGFV, 4 GSWT => 5 KFRX
2 CZSJ => 7 GMTW
5 PHKL, 5 VCNSK, 25 GSVSD => 8 FRWC
30 FRWC, 17 GKDK, 8 NPSLQ => 3 CLBG
8 MXWHS, 3 SCKB, 2 NPSLQ => 1 JKXFH
1 XJSQW, 7 QZBXB => 1 LGKHR
115 ORE => 6 GQNQF
12 HTRWR, 24 HDCTQ => 1 RKBMX
1 DQTHS, 6 XDFWD, 1 MXWHS => 8 VKWN
129 ORE => 3 XCTF
6 GQNQF, 7 WJKDC => 5 PHKL
3 NZSQR => 2 LPXW
2 FLPFX, 1 MKLP, 4 XDFWD => 8 NPSLQ
4 DQTHS, 1 VKWN => 1 BPVMR
7 GMTW => 1 TXMVX
152 ORE => 8 JDFQZ
21 LGKHR => 9 NPDPH
5 CJTB, 1 QZBXB, 3 KFRX => 1 GTPB
1 MXWHS => 3 CWHX
3 PHKL => 1 NGFV
1 WMXZJ => 7 XDFWD
3 TSWN, 1 VKWN => 8 GKDK
1 ZVBRF, 16 PWJFD => 8 CMJSL
3 VCNSK, 7 GDRZ => 4 HKBS
20 XJSQW, 6 HTRWR, 7 CJTB => 5 WMXZJ
12 ZVBRF, 10 FRWC, 12 TSWN => 4 WRSD
16 HDCTQ, 3 GTPB, 10 NGFV => 4 KWCDP
3 TXMVX, 1 NPDPH => 8 HTRWR
9 NPDPH, 6 LPXW => 8 GSWT
4 MKLP => 1 TQGT
34 GTPB => 3 RVKLC
25 VFVC, 5 RVKLC => 8 DQTHS
7 KWCDP => 3 SCKB
6 LGKHR => 8 MKLP
39 MKJDZ => 9 CZSJ
2 TSWN, 1 WMXZJ => 3 PSJL";
    }
}
