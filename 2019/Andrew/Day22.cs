using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
namespace AoC2019
{
    public class Day22
    {
        public Day22()
        {
        }
        public void Run()
        {
            DateTime begin = DateTime.Now;
            string[] lines = Data
                .Replace("deal with increment","di")
                .Replace("deal into new stack", "ns")
                .Replace("\r\n","\n")
                .Split('\n');
            Console.WriteLine("Day 22,P1:" + FindPosition(lines, 2019, 10007) + ", completed in " + (DateTime.Now - begin).TotalMilliseconds + " milliseconds");
            //Console.WriteLine(FindCard(lines, 4649, 10007, 1) + " ");
            begin = DateTime.Now;
            Console.WriteLine("Day 22,P2:" + FindCard(lines, 2020, 119315717514047, 101741582076661) + ", completed in " + (DateTime.Now - begin).TotalMilliseconds + " milliseconds");

        }

        public int FindPosition(string[] lines, int cardNumber, int numberCards)
        {
            foreach (var line in lines)
            {
                var param = line.Split(' ');
                int iParam = 0;
                if (param.Length==2)
                {
                    iParam = int.Parse(param[1]);
                }
                switch (param[0])
                {
                    case "ns":
                        cardNumber = numberCards - (cardNumber+1);
                        //card is numberCards-cardNumber position now...
                        break;
                    case "cut":
                        cardNumber = ((cardNumber - iParam) + numberCards) % numberCards;
                        //card is shifted << on positive, >> on negative.
                        //cardNumber-param
                        break;
                    case "di":
                        cardNumber = (cardNumber * iParam) % numberCards;
                        //card is now (cardNumber*param)%numberCards now...
                        break;
                }
            }
            return cardNumber;
        }

        public BigInteger FindCard(string[] lines, int cardNumber, long numberCards, long times)
        {
            /*
             * Big Number Theory on this one. So let me explain what's going on here:
             * offset is offset from 0. If you ""Cut 4"" then you are really adjusting offset.
             * * * If you reverse or "ns" then you also adjust offset.
             *
             * multiplier is distance to next like number. so in the beginning 0 and 1 are together,
             * * * And thus multiplier is 1. If you reverse, multiplier is gone negative.
             * * * If you deal increment of "DI" then your multiplier will change dramatically.
             * * * This by far was the hardest to understand, and then when given the hint of "modInv"
             * * * I still couldn't fucking do it. Nobody but 0.01% of programmers also know crypto theory.
             * * * I had to make my own. I used
             * https://stackoverflow.com/questions/7483706/c-sharp-modinverse-function
             * * * To make it.
             * */

            BigInteger multiplier = 1;
            BigInteger offset = 0;
            foreach (var line in lines)
            {
                var param = line.Split(' ');
                int iParam = 0;
                if (param.Length == 2)
                {
                    iParam = int.Parse(param[1]);
                }
                switch (param[0])
                {
                    case "ns":
                        multiplier = -multiplier % numberCards;
                        offset = (offset + multiplier) % numberCards;
                        break;
                    case "cut":
                        offset = (offset + iParam * multiplier) % numberCards;
                        break;
                    case "di":
                        multiplier = (multiplier * ModInv(iParam, numberCards)) % numberCards;
                        break;
                }
            }

            BigInteger increment = BigInteger.ModPow(multiplier, times, numberCards);

            return ((offset * (1 - increment) *
                ModInv((1 - multiplier) % numberCards, numberCards) % numberCards) + (increment * cardNumber)) % numberCards;
        }

        internal static BigInteger ModInv(BigInteger a, BigInteger n)
        {
            return BigInteger.ModPow(a, n - 2, n);
        }

        public static string TestData1 = @"deal with increment 3";

        public static string TestData2 = @"cut 6
deal with increment 7
deal into new stack";

        public static string Data = @"deal with increment 10
cut -5908
deal with increment 75
cut 8705
deal with increment 49
cut -1609
deal with increment 69
cut 7797
deal into new stack
cut -6211
deal with increment 10
cut 6188
deal with increment 57
cut -1659
deal with increment 16
cut -5930
deal into new stack
cut -2758
deal with increment 33
cut -3275
deal with increment 39
cut -1301
deal with increment 50
cut 7837
deal with increment 74
cut 1200
deal with increment 23
deal into new stack
cut -9922
deal with increment 65
cut 4896
deal with increment 61
deal into new stack
cut 5945
deal with increment 9
deal into new stack
deal with increment 2
cut -8205
deal with increment 75
cut -4063
deal with increment 40
deal into new stack
cut -7366
deal with increment 51
cut 7213
deal into new stack
cut 4763
deal with increment 43
cut 3963
deal with increment 50
cut -8856
deal with increment 43
cut 8604
deal with increment 72
cut -7026
deal into new stack
deal with increment 25
cut 7843
deal with increment 71
cut -1272
deal with increment 64
cut 7770
deal with increment 18
cut -5278
deal with increment 67
deal into new stack
deal with increment 18
deal into new stack
cut 2216
deal with increment 42
cut 3206
deal with increment 14
deal into new stack
cut -6559
deal into new stack
deal with increment 12
deal into new stack
deal with increment 75
deal into new stack
deal with increment 41
cut 7378
deal with increment 44
cut 774
deal with increment 60
cut 7357
deal with increment 41
cut 479
deal with increment 40
cut 5146
deal with increment 13
cut 2017
deal into new stack
deal with increment 35
cut 9218
deal into new stack
deal with increment 22
cut -2462
deal with increment 23
cut -1820
deal with increment 69";
    }
}
