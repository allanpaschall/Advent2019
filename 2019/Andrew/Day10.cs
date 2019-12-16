using System;
using System.Collections.Generic;
using System.Linq;
namespace AoC2019
{
    public class LoS
    {
        public int X { get; set; }
        public int Y { get; set; }
        public decimal Clockwise
        {
            get
            {
                if (Y >= 0 && X < 0)//RIGHT
                {
                    //Quadrant 2
                    return (decimal)Y / (decimal)-X;
                }
                if (Y>=0 && X>0)//RIGHT
                {
                    //Quadrant 1
                    return 200 - (decimal)Y / (decimal)X;
                }
                if (Y>0 && X==0)//RIGHT
                {
                    return 100;
                }
                if (Y <= 0 && X > 0)
                {
                    //Quadrant 4
                    return 300 + (decimal)-Y / (decimal)X;
                }
                if (Y <= 0 && X < 0)
                {
                    //Quadrant 4
                    return 500 - (decimal)-Y / (decimal)-X;
                }
                if (Y < 0 && X == 0)
                {
                    return 400;
                }
                return 999;
            }
        }
    }
    public class Day10
    {
        public Day10()
        {
        }
        public void Run()
        {
            var d = Data;
            var lines = d.Replace("\r\n", "\n").Split('\n');
            char[][] llines = new char[lines.Length][];
            for (int i = 0; i < llines.Length; i++)
            {
                llines[i] = new char[lines[i].Length];
                for (int j = 0; j < llines[i].Length; j++)
                {
                    llines[i][j] = lines[i][j];
                }
            }
            var widths = lines[0].Length;
            //I need all non reducable fractions 0 to 48.
            //primes: 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47
            List<LoS> los = new List<LoS>
            {
                new LoS { X = 0, Y = 1 },
                new LoS { X = 1, Y = 0 },
                new LoS { X = 0, Y = -1 },
                new LoS { X = -1, Y = 0 },
                new LoS { X = 1, Y = 1 },
                new LoS { X = 1, Y = -1 },
                new LoS { X = -1, Y = -1 },
                new LoS { X = -1, Y = 1 }
            };
            for (int p = 2; p < widths; p++)
            {
                for (int i = 1; i < p; i++)
                {
                    bool candidate = true;
                    for (int j = 0; j < Primes.Length; j++)
                    {
                        if (i < Primes[j]) break;
                        if (i%Primes[j]==0 && p%Primes[j]==0)
                        {
                            candidate = false;//the numeral is evenly divisible by a prime.
                            break;
                        }
                    }
                    if (candidate)
                    {
                        los.Add(new LoS { X = i, Y = p });
                        los.Add(new LoS { X = p, Y = i });
                        los.Add(new LoS { X = -i, Y = p });
                        los.Add(new LoS { X = p, Y = -i });
                        los.Add(new LoS { X = i, Y = -p });
                        los.Add(new LoS { X = -p, Y = i });
                        los.Add(new LoS { X = -i, Y = -p });
                        los.Add(new LoS { X = -p, Y = -i });
                        //los.Add()
                    }
                }
            }
            //foreach (var item in (from l in los orderby l.Clockwise select l))
            {
                //Console.WriteLine("X:" + item.Y + ", Y:" + item.X);
            }
            //now that the "Knight like" paths have been determined, iterate until we fall off a clif or hit an asteroid.
            //do this for every asteroid.
            Dictionary<int, LoS> Found = new Dictionary<int, LoS>();
            for (int x = 0; x < lines.Length; x++)
            {
                for (int y = 0; y < lines[x].Length; y++)
                {
                    if (lines[x][y]=='#')
                    {
                        //we have to do a test with every possible iteration...
                        int count = 0;
                        foreach (var item in los)
                        {
                            for (int m = 1; m < 48; m++)
                            {
                                if (x + (item.X * m) < 0 || x + (item.X * m) >= lines.Length)
                                {
                                    m = 48;
                                    continue;
                                }
                                if (y + (item.Y * m) < 0 || y + (item.Y * m) >= lines[0].Length)
                                {
                                    m = 48;
                                    continue;
                                }
                                if (lines[x + (item.X * m)][y + (item.Y * m)] == '#')
                                {
                                    count++;
                                    m = 48;
                                }
                            }
                        }
                        //Found.Add(count);
                        Found[count] = new LoS { X = x, Y = y };
                    }
                }
            }
            var ff = (from f in Found orderby f.Key descending select f).First();
            Console.WriteLine("Day 10,P1:" + (from f in Found orderby f.Key descending select f).First().Key);
            //Console.WriteLine("X:" + ff.Value.Y + " Y:" + ff.Value.X);
            int destroyed = 0;
            do
            {
                foreach (var item in (from l in los orderby l.Clockwise select l))
                {
                    for (int m = 1; m < 48; m++)
                    {
                        if (ff.Value.X + (item.X * m) < 0 || ff.Value.X + (item.X * m) >= llines.Length)
                        {
                            m = 48;
                            continue;
                        }
                        if (ff.Value.Y + (item.Y * m) < 0 || ff.Value.Y + (item.Y * m) >= llines[0].Length)
                        {
                            m = 48;
                            continue;
                        }
                        if (llines[ff.Value.X + (item.X * m)][ff.Value.Y + (item.Y * m)] == '#')
                        {
                            llines[ff.Value.X + (item.X * m)][ff.Value.Y + (item.Y * m)] = '.';
                            destroyed++;
                            if (destroyed==200)
                            {
                                Console.WriteLine("Day 10,P2:" + ((ff.Value.Y + (item.Y * m))*100 + (ff.Value.X + (item.X * m))));
                            }
                            m = 48;
                        }
                    }

                }
            } while (destroyed < 200);

        }



        public int[] Primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };

        public string TestData = @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

        public string Data = @"#.#................#..............#......#......
.......##..#..#....#.#.....##...#.........#.#...
.#...............#....#.##......................
......#..####.........#....#.......#..#.....#...
.....#............#......#................#.#...
....##...#.#.#.#.............#..#.#.......#.....
..#.#.........#....#..#.#.........####..........
....#...#.#...####..#..#..#.....#...............
.............#......#..........#...........#....
......#.#.........#...............#.............
..#......#..#.....##...##.....#....#.#......#...
...#.......##.........#.#..#......#........#.#..
#.............#..........#....#.#.....#.........
#......#.#................#.......#..#.#........
#..#.#.....#.....###..#.................#..#....
...............................#..........#.....
###.#.....#.....#.............#.......#....#....
.#.....#.........#.....#....#...................
........#....................#..#...............
.....#...#.##......#............#......#.....#..
..#..#..............#..#..#.##........#.........
..#.#...#.......#....##...#........#...#.#....#.
.....#.#..####...........#.##....#....#......#..
.....#..#..##...............................#...
.#....#..#......#.#............#........##...#..
.......#.....................#..#....#.....#....
#......#..###...........#.#....#......#.........
..............#..#.#...#.......#..#.#...#......#
.......#...........#.....#...#.............#.#..
..##..##.............#........#........#........
......#.............##..#.........#...#.#.#.....
#........#.........#...#.....#................#.
...#.#...........#.....#.........#......##......
..#..#...........#..........#...................
.........#..#.......................#.#.........
......#.#.#.....#...........#...............#...
......#.##...........#....#............#........
#...........##.#.#........##...........##.......
......#....#..#.......#.....#.#.......#.##......
.#....#......#..............#.......#...........
......##.#..........#..................#........
......##.##...#..#........#............#........
..#.....#.................###...#.....###.#..#..
....##...............#....#..................#..
.....#................#.#.#.......#..........#..
#........................#.##..........#....##..
.#.........#.#.#...#...#....#........#..#.......
...#..#.#......................#...............#";
    }
}
