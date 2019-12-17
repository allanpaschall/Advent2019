using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019
{
    public class d12Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Energy
        {
            get
            {
                return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
            }
        }
        public static d12Point operator +(d12Point a, d12Point b)
        {
            return new d12Point()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
                Z = a.Z + b.Z
            };
        }
        public static int operator *(d12Point a, d12Point b)
        {
            return a.Energy * b.Energy;
        }
        public override string ToString()
        {
            return "" + X + ", " + Y + ", " + Z;
        }
    }
    class Day12
    {
        public void Run()
        {
            var begin = DateTime.Now;
            var data = Data;
            d12Point[] Velocities =
            {
                new d12Point() { X = 0, Y = 0, Z = 0 },
                new d12Point() { X = 0, Y = 0, Z = 0 },
                new d12Point() { X = 0, Y = 0, Z = 0 },
                new d12Point() { X = 0, Y = 0, Z = 0 }
            };
            List<d12Point> points = new List<d12Point>();

            for (int j = 0; j < 1000; j++)
            {
                var gravity = DetermineGravity(data, Velocities);
                for (int i = 0; i < Velocities.Length; i++)
                {
                    Velocities[i] = Velocities[i] + gravity[i];
                    data[i] = data[i] + Velocities[i];
                }
            }

            int Energy = 0;
            for (int i = 0; i < data.Length; i++)
            {
                Energy += data[i] * Velocities[i];
            }
            Console.WriteLine("Day 12,P1:" + Energy + ", completed in " + (System.DateTime.Now - begin).TotalMilliseconds + " milliseconds");
            begin = DateTime.Now;
            for (int j = 0; j < 300000; j++)
            {
                var gravity = DetermineGravity(data, Velocities);
                for (int i = 0; i < Velocities.Length; i++)
                {
                    Velocities[i] = Velocities[i] + gravity[i];
                    data[i] = data[i] + Velocities[i];
                }
                points.Add(data[0]);

            }
            int maxX = 0;
            int maxY = 0;
            int maxZ = 0;
            for (int k = 700; k < 299960; k++)
            {
                for (int j = k + 1; j < 299960; j++)
                {
                    if (maxX != 0 && maxY != 0 && maxZ != 0)
                    {
                        k = int.MaxValue - 1;
                        j = int.MaxValue - 1;
                        break;
                    }
                    if (maxX == 0 && points[j].X == points[k].X &&
                        points[j + 1].X == points[k + 1].X &&
                        points[j + 2].X == points[k + 2].X &&
                        points[j + 3].X == points[k + 3].X &&
                        points[j + 4].X == points[k + 4].X &&
                        points[j + 5].X == points[k + 5].X &&
                        points[j + 6].X == points[k + 6].X &&
                        points[j + 7].X == points[k + 7].X &&
                        points[j + 8].X == points[k + 8].X &&
                        points[j + 9].X == points[k + 9].X &&
                        points[j + 10].X == points[k + 10].X &&
                        points[j + 11].X == points[k + 11].X &&
                        points[j + 12].X == points[k + 12].X &&
                        points[j + 13].X == points[k + 13].X &&
                        points[j + 14].X == points[k + 14].X &&
                        points[j + 15].X == points[k + 15].X &&
                        points[j + 16].X == points[k + 16].X &&
                        points[j + 17].X == points[k + 17].X &&
                        points[j + 18].X == points[k + 18].X &&
                        points[j + 19].X == points[k + 19].X &&
                        points[j + 20].X == points[k + 20].X &&
                        points[j + 21].X == points[k + 21].X &&
                        points[j + 22].X == points[k + 22].X &&
                        points[j + 23].X == points[k + 23].X &&
                        points[j + 24].X == points[k + 24].X &&
                        points[j + 25].X == points[k + 25].X &&
                        points[j + 26].X == points[k + 26].X &&
                        points[j + 27].X == points[k + 27].X &&
                        points[j + 28].X == points[k + 28].X &&
                        points[j + 29].X == points[k + 29].X &&
                        points[j + 30].X == points[k + 30].X &&
                        points[j + 31].X == points[k + 31].X &&
                        points[j + 32].X == points[k + 32].X &&
                        points[j + 33].X == points[k + 33].X &&
                        points[j + 34].X == points[k + 34].X &&
                        points[j + 35].X == points[k + 35].X &&
                        points[j + 36].X == points[k + 36].X &&
                        points[j + 37].X == points[k + 37].X &&
                        points[j + 38].X == points[k + 38].X &&
                        points[j + 39].X == points[k + 39].X &&
                        points[j + 40].X == points[k + 40].X)
                    {
                        bool good = true;
                        for (int l = 0; l < (j - k); l++)
                        {
                            if (points[j + l].X != points[k + l].X)
                            {
                                //bad
                                good = false;
                                l = (j - k);
                            }
                            if ((j + l + 1) >= points.Count || (k + l + 1) >= points.Count)
                            {
                                l = (j - k);
                            }
                        }
                        if (good && (j - k) > 1000)
                        {
                            maxX = (j - k);
                        }
                        //Console.WriteLine("X repeats at " + (j-k));
                    }
                    if (maxY == 0 && points[j].Y == points[k].Y &&
                        points[j + 1].Y == points[k + 1].Y &&
                        points[j + 2].Y == points[k + 2].Y &&
                        points[j + 3].Y == points[k + 3].Y &&
                        points[j + 4].Y == points[k + 4].Y &&
                        points[j + 5].Y == points[k + 5].Y &&
                        points[j + 6].Y == points[k + 6].Y &&
                        points[j + 7].Y == points[k + 7].Y &&
                        points[j + 8].Y == points[k + 8].Y &&
                        points[j + 9].Y == points[k + 9].Y &&
                        points[j + 10].Y == points[k + 10].Y &&
                        points[j + 11].Y == points[k + 11].Y &&
                        points[j + 12].Y == points[k + 12].Y &&
                        points[j + 13].Y == points[k + 13].Y &&
                        points[j + 14].Y == points[k + 14].Y &&
                        points[j + 15].Y == points[k + 15].Y &&
                        points[j + 16].Y == points[k + 16].Y &&
                        points[j + 17].Y == points[k + 17].Y &&
                        points[j + 18].Y == points[k + 18].Y &&
                        points[j + 19].Y == points[k + 19].Y &&
                        points[j + 20].Y == points[k + 20].Y &&
                        points[j + 21].Y == points[k + 21].Y &&
                        points[j + 22].Y == points[k + 22].Y &&
                        points[j + 23].Y == points[k + 23].Y &&
                        points[j + 24].Y == points[k + 24].Y &&
                        points[j + 25].Y == points[k + 25].Y &&
                        points[j + 26].Y == points[k + 26].Y &&
                        points[j + 27].Y == points[k + 27].Y &&
                        points[j + 28].Y == points[k + 28].Y &&
                        points[j + 29].Y == points[k + 29].Y &&
                        points[j + 30].Y == points[k + 30].Y &&
                        points[j + 31].Y == points[k + 31].Y &&
                        points[j + 32].Y == points[k + 32].Y &&
                        points[j + 33].Y == points[k + 33].Y &&
                        points[j + 34].Y == points[k + 34].Y &&
                        points[j + 35].Y == points[k + 35].Y &&
                        points[j + 36].Y == points[k + 36].Y &&
                        points[j + 37].Y == points[k + 37].Y &&
                        points[j + 38].Y == points[k + 38].Y &&
                        points[j + 39].Y == points[k + 39].Y &&
                        points[j + 40].Y == points[k + 40].Y)
                    {
                        bool good = true;
                        for (int l = 0; l < (j - k); l++)
                        {
                            if (points[j + l].Y != points[k + l].Y)
                            {
                                //bad
                                good = false;
                                l = (j - k);
                            }
                            if ((j + l + 1) >= points.Count || (k + l + 1) >= points.Count)
                            {
                                l = (j - k);
                            }
                        }
                        if (good && (j - k) > 1000)
                        {
                            maxY = (j - k);
                        }
                        //Console.WriteLine("Y repeats at " + (j - k));
                    }
                    if (maxZ == 0 && points[j].Z == points[k].Z &&
                        points[j + 1].Z == points[k + 1].Z &&
                        points[j + 2].Z == points[k + 2].Z &&
                        points[j + 3].Z == points[k + 3].Z &&
                        points[j + 4].Z == points[k + 4].Z &&
                        points[j + 5].Z == points[k + 5].Z &&
                        points[j + 6].Z == points[k + 6].Z &&
                        points[j + 7].Z == points[k + 7].Z &&
                        points[j + 8].Z == points[k + 8].Z &&
                        points[j + 9].Z == points[k + 9].Z &&
                        points[j + 10].Z == points[k + 10].Z &&
                        points[j + 11].Z == points[k + 11].Z &&
                        points[j + 12].Z == points[k + 12].Z &&
                        points[j + 13].Z == points[k + 13].Z &&
                        points[j + 14].Z == points[k + 14].Z &&
                        points[j + 15].Z == points[k + 15].Z &&
                        points[j + 16].Z == points[k + 16].Z &&
                        points[j + 17].Z == points[k + 17].Z &&
                        points[j + 18].Z == points[k + 18].Z &&
                        points[j + 19].Z == points[k + 19].Z &&
                        points[j + 20].Z == points[k + 20].Z &&
                        points[j + 21].Z == points[k + 21].Z &&
                        points[j + 22].Z == points[k + 22].Z &&
                        points[j + 23].Z == points[k + 23].Z &&
                        points[j + 24].Z == points[k + 24].Z &&
                        points[j + 25].Z == points[k + 25].Z &&
                        points[j + 26].Z == points[k + 26].Z &&
                        points[j + 27].Z == points[k + 27].Z &&
                        points[j + 28].Z == points[k + 28].Z &&
                        points[j + 29].Z == points[k + 29].Z &&
                        points[j + 30].Z == points[k + 30].Z &&
                        points[j + 31].Z == points[k + 31].Z &&
                        points[j + 32].Z == points[k + 32].Z &&
                        points[j + 33].Z == points[k + 33].Z &&
                        points[j + 34].Z == points[k + 34].Z &&
                        points[j + 35].Z == points[k + 35].Z &&
                        points[j + 36].Z == points[k + 36].Z &&
                        points[j + 37].Z == points[k + 37].Z &&
                        points[j + 38].Z == points[k + 38].Z &&
                        points[j + 39].Z == points[k + 39].Z &&
                        points[j + 40].Z == points[k + 40].Z)
                    {
                        bool good = true;
                        for (int l = 0; l < (j - k); l++)
                        {
                            if (points[j + l].Z != points[k + l].Z)
                            {
                                //bad
                                good = false;
                                l = (j - k);
                            }
                            if ((j + l + 1) >= points.Count || (k + l + 1) >= points.Count)
                            {
                                l = (j - k);
                            }
                        }
                        if (good && (j - k) > 1000)
                        {

                            maxZ = (j - k);
                        }
                        //Console.WriteLine("Z repeats at " + (j - k));
                    }
                }

            }
            decimal answer = ((decimal)maxX * (decimal)maxY * (decimal)maxZ) / 4M;
            Console.WriteLine("Day 12,P2:" + answer + ", completed in " + (System.DateTime.Now - begin).TotalMilliseconds + " milliseconds");// + " LCM(" + "X:" + maxX + ", Y:" + maxY + ", Z:" + maxZ + ")");

        }

        public d12Point[] DetermineGravity(d12Point[] Moons, d12Point[] Velocities)
        {
            List<d12Point> v = new List<d12Point>();
            for (int i = 0; i < Moons.Length; i++)
            {
                v.Add(/*Velocities[i] + */new d12Point()
                {
                    X = (from m in Moons where m.X > Moons[i].X select 1).Sum() + (from m in Moons where m.X < Moons[i].X select -1).Sum(),
                    Y = (from m in Moons where m.Y > Moons[i].Y select 1).Sum() + (from m in Moons where m.Y < Moons[i].Y select -1).Sum(),
                    Z = (from m in Moons where m.Z > Moons[i].Z select 1).Sum() + (from m in Moons where m.Z < Moons[i].Z select -1).Sum()
                });
            }

            return v.ToArray();
        }

        public d12Point[] TestData = {
            new d12Point() { X = -8, Y = -10, Z = 0 },
            new d12Point() { X = 5, Y = 5, Z = 10 },
            new d12Point() { X = 2, Y = -7, Z = 3 },
            new d12Point() { X = 9, Y = -8, Z = -3 }
            //X is  apart (diff)
            //Y is 28 apart (diff - 2 times 2?)
            //Z is 44 apart
        };


        public d12Point[] Data = {
            new d12Point() { X = 7, Y = 10, Z = 17 },
            new d12Point() { X = -2, Y = 7, Z = 0 },
            new d12Point() { X = 12, Y = 5, Z = 12 },
            new d12Point() { X = 5, Y = -8, Z = 6 }
        };

        public d12Point[] DataP1 = {
            new d12Point() { X = 7, Y = 10, Z = 17 },
            new d12Point() { X = -2, Y = 7, Z = 0 },
            new d12Point() { X = 12, Y = 5, Z = 12 },
            new d12Point() { X = 5, Y = -8, Z = 6 }
        };

        //            @"<x=-1, y=0, z=2>
        //< x = 2, y = -10, z = -7 >
        //< x = 4, y = -8, z = 8 >
        //< x = 3, y = 5, z = -1 > ";
    }
}