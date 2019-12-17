using System;
using System.Collections.Generic;
namespace AoC2019
{
    public class d15Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Type { get; set; }
        //This makes it so that a check of Point==Point is really comparing X and Y, ignoring MDistance and most importantly, Step.
        public override bool Equals(object obj)
        {
            var o2 = obj as Point;
            if (o2.X == X && o2.Y == Y)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ("X:" + X + "Y:" + Y).GetHashCode();
        }
    }
    public class Day15
    {
        public Day15()
        {
        }
        long x = 21;
        long y = 21;
        long IntendedMove = 0;
        public void Run()
        {
            var begin = DateTime.Now;
            int len = InputCommands.Length;
            Dictionary<int, d15Point> grid = new Dictionary<int, d15Point>();

            //Draw Grid. Use the data directly from the intcode...
            //0X
            //X1
            //X is determined by if the item is greater than 27 or not...
            //Which X to draw is determined by Y...if odd, -1. if even, do nothing.

            for (int xx = 0; xx < 39; xx++)
            {
                for (int yy = 0; yy < 20; yy++)
                {
                    //252 + (y*39) + x
                    var result = ((Data[252 + ((yy) * 39) + xx] > 27) ? 0 : 1);
                    grid[((int)(yy + 1) * 2 * 100) + (xx%2==1 ? -100 : 0) + (int)xx + 1] = new d15Point { Type = (int)result + 1 };
                }
            }

            //This duplicated function 124 to 179
            //draws the 1s and 0s in the schematic above, along with the walls...
            for (int xx = 0; xx <= 40; xx++)
            {
                for (int yy = 0; yy <= 40; yy++)
                {
                    if (xx % 2 == 1 && yy % 2 == 1)
                    {
                        grid[((int)yy * 100) + (int)xx] = new d15Point { Type = (int)2 };
                    }
                    else if (xx % 2 == 0 && yy % 2 == 0)
                    {
                        grid[((int)yy * 100) + (int)xx] = new d15Point { Type = (int)1 };
                    }
                    else if (xx == 0 || yy == 0 || xx == 40 || yy == 40)
                    {
                        grid[((int)yy * 100) + (int)xx] = new d15Point { Type = (int)1 };
                    }
                }
            }
            //The point where the oxygen is...
            grid[3335] = new d15Point { Type = 3 };


            var i = new IntCode(Data, ProvideInput);
            do
            {
                var finishMode = i.Run(RunningMode.OutputAttached);
                var result = i.Output.Dequeue();
                if (result == 0)
                {
                    var tempX = x;
                    var tempY = y;
                    switch (IntendedMove)
                    {
                        case 1://North
                            tempY -= 1;
                            break;
                        case 2:
                            tempY += 1;
                            break;
                        case 3:
                            tempX -= 1;
                            break;
                        case 4:
                            tempX += 1;
                            break;
                    }
                    grid[((int)tempY * 100) + (int)tempX] = new d15Point { X = (int)tempX, Y = (int)tempY, Type = (int)result + 1 };
                }
                else
                {
                    switch (IntendedMove)
                    {
                        case 1://North
                            y -= 1;
                            break;
                        case 2:
                            y += 1;
                            break;
                        case 3:
                            x -= 1;
                            break;
                        case 4:
                            x += 1;
                            break;
                    }
                    if (result == 2)
                    {
                        Console.WriteLine("Day 15,P1:" + (len - InputCommands.Length) + ", completed in " + (System.DateTime.Now - begin).TotalMilliseconds + " milliseconds");
                        break;
                    }
                    grid[((int)y * 100) + (int)x] = new d15Point { X = (int)x, Y = (int)y, Type = (int)result + 1 };
                }
            } while (true);


            //This is Part 2...it walks the grid until all '2' and '3'. Temporarily uses '4' to not poison the timeline.
            begin = DateTime.Now;
            int count = 0;
            for (int j = 0; j < 5000; j++)
            {
                for (int xx = 0; xx < 40; xx++)
                {
                    for (int yy = 0; yy < 40; yy++)
                    {
                        if (grid[yy * 100 + xx].Type == 2)
                        {
                            //check for a 3 in any of the quadrants around it...
                            if (yy != 0 && grid[(yy - 1) * 100 + xx].Type == 3)
                            {
                                grid[yy * 100 + xx].Type = 4;
                            }
                            if (xx != 0 && grid[(yy) * 100 + xx - 1].Type == 3)
                            {
                                grid[yy * 100 + xx].Type = 4;
                            }
                            if (grid[(yy + 1) * 100 + xx].Type == 3)
                            {
                                grid[yy * 100 + xx].Type = 4;
                            }
                            if (grid[(yy) * 100 + xx + 1].Type == 3)
                            {
                                grid[yy * 100 + xx].Type = 4;
                            }
                        }
                    }
                }
                bool done = true;
                for (int xx = 0; xx < 40; xx++)
                {
                    for (int yy = 0; yy < 40; yy++)
                    {
                        if (grid[yy * 100 + xx].Type == 4)
                        {
                            grid[yy * 100 + xx].Type = 3;
                        }
                        if (grid[yy * 100 + xx].Type == 2)
                        {
                            done = false;
                        }
                    }
                }
                count++;
                if (done) break;
                //DrawScreen(grid);
                //System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine("Day 15,P2:" + count + ", completed in " + (System.DateTime.Now - begin).TotalMilliseconds + " milliseconds");

        }
        public long ProvideInput()
        {
            if (InputCommands.Length != 0)
            {
                char command = InputCommands[0];
                IntendedMove = command - 48;
                InputCommands = InputCommands.Substring(1);
                return command - 48;

            }
            else
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                    IntendedMove = 1;
                else if (key.Key == ConsoleKey.DownArrow)
                    IntendedMove = 2;
                else if (key.Key == ConsoleKey.LeftArrow)
                    IntendedMove = 3;
                else if (key.Key == ConsoleKey.RightArrow)
                    IntendedMove = 4;
                return IntendedMove;

            }
            //return 0;
        }
        string InputCommands = "223322444444444422332244222233113311333333223311333311331144442244111133331111333333224444223322222244224422444422333333223322444411442244114444444411442222441144224444444411111111113322331133222222442244111133";

        private void DrawScreen(Dictionary<int, d15Point> screen)
        {
            for (int yy = 0; yy <= 40; yy++)
            {
                for (int xx = 0; xx <= 40; xx++)
                {
                    long ttype = screen.ContainsKey(((int)yy * 100) + (int)xx) ? screen[((int)yy * 100) + (int)xx].Type : 0;
                    char tchar = ' ';
                    switch (ttype)
                    {
                        case 0:
                            tchar = ' ';
                            break;
                        case 1:
                            tchar = '#';
                            break;
                        case 2:
                            tchar = '.';
                            break;
                        case 3:
                            tchar = 'o';
                            break;
                    }
                    if (xx == x && yy == y)
                    {
                        tchar = 'x';
                    }
                    Console.Write(tchar);
                }
                Console.WriteLine();
            }
            //Console.WriteLine("Score: " + score);
        }


        public long[] Data = {
            3,1033,
            1008,1033,1,1032,
            1005,1032,31,
            1008,1033,2,1032,
            1005,1032,58,
            1008,1033,3,1032,
            1005,1032,81,
            1008,1033,4,1032,
            1005,1032,104,
            99,
            //North
            101,0,1034,1039,
            101,0,1036,1041,
            1001,1035,-1,1040,
            1008,1038,0,1043,//if 0, 1. if 1, 0
            102,-1,1043,1032,
            1,1037,1032,1042,
            1105,1,124,
            //South
            102,1,1034,1039,
            101,0,1036,1041,
            1001,1035,1,1040,
            1008,1038,0,1043,//if 0, 1. if 1, 0
            1,1037,1038,1042,
            1105,1,124,
            //East
            1001,1034,-1,1039,
            1008,1036,0,1041,//if 0, 1. if 1, 0
            1002,1035,1,1040,
            1002,1038,1,1043,
            1001,1037,0,1042,
            1105,1,124,
            //West
            1001,1034,1,1039,
            1008,1036,0,1041,//if 0, 1. if 1, 0
            1002,1035,1,1040,
            101,0,1038,1043,
            101,0,1037,1042,
            //Function 124
            1006,1039,217,//217 is some bail instruction
            1006,1040,217,
            1008,1039,40,1032,//134
            1005,1032,217,
            1008,1040,40,1032,
            1005,1032,217,//144
            1008,1039,35,1032,
            1006,1032,165,
            1008,1040,33,1032,//155
            1006,1032,165,
            1102,2,1,1044,
            1106,0,224,//Jump to224
            2,1041,1043,1032,//165: Jump here is not at 35,33 (where the oxygen is)
            1006,1032,179,
            1101,1,0,1044,//at this instruction we are outputting an open spot if 1041 and 1043 are 1.
            1106,0,224,
            1,1041,1043,1032,//179: 1 or 0. 1 if either are 1...0 if both are 0.
            1006,1032,217,
            1,1042,1043,1032,//To be at this point, either 1041 or 1043 are 1.
            1001,1032,-1,1032,
            1002,1032,39,1032,//197
            1,1032,1039,1032,//201
            101,-1,1032,1032,
            101,252,1032,211,//this instruction modifies the next.
            1007,0,27,1044,
            1105,1,224,
            1101,0,0,1044,//217: write a 0. designed to be a perm wall
            1105,1,224,
            1006,1044,247,
            101,0,1039,1034,//224: this block moves memory from 1039-1043 to 1034-1038
            1002,1040,1,1035,
            101,0,1041,1036,
            1001,1043,0,1038,
            101,0,1042,1037,
            4,1044,
            1106,0,0,
            8,86,20,11,8,18,84,20,96,25,15,28,96,20,74,24,7,5,77,6,77,6,23,74,3,23,93,21,72,23,1,57,87,10,17,9,23,48,16,
            9,32,11,62,73,5,70,2,10,77,23,16,76,24,28,13,46,92,26,15,10,87,13,28,54,10,50,4,16,47,75,24,55,4,99,92,17,66,24,
            7,13,33,43,21,65,24,4,74,40,8,28,25,5,72,25,5,54,19,72,6,44,49,3,65,11,24,85,39,11,5,77,15,6,65,12,66,66,14,
            8,88,81,2,8,99,7,54,70,2,97,69,9,17,51,47,1,56,88,81,41,10,98,16,23,35,24,82,24,5,99,39,67,8,14,46,56,5,8,
            59,9,53,9,21,95,6,95,7,12,85,26,79,82,19,21,62,99,5,13,81,19,31,15,29,67,45,22,75,84,14,25,83,33,97,4,85,15,17,25,21,51,55,11,76,32,15,43,60,13,13,11,65,65,16,9,96,26,17,10,94,23,12,37,16,49,2,81,17,11,20,17,16,37,87,16,12,96,23,10,68,22,75,34,4,22,14,34,14,62,8,34,12,72,7,40,5,54,10,89,7,96,1,14,72,7,11,60,93,68,51,21,86,25,34,26,20,38,7,21,94,78,10,8,46,4,81,12,84,30,11,9,48,12,83,73,42,83,26,26,40,22,91,6,38,99,2,40,24,93,10,22,84,22,19,94,8,6,42,33,11,15,31,66,33,2,65,39,67,26,5,67,19,86,1,12,20,28,54,80,84,3,17,32,26,51,8,6,20,67,15,54,30,5,31,97,9,10,29,18,45,8,23,69,18,61,11,4,73,5,46,13,96,16,80,66,17,1,11,50,37,4,34,94,15,32,77,5,93,69,12,66,6,24,18,84,26,42,5,78,74,22,82,15,23,60,11,64,61,59,48,11,99,49,3,68,2,16,14,99,7,94,9,22,75,20,30,21,17,91,20,41,21,26,42,44,19,18,85,17,96,21,2,88,62,69,8,39,3,11,62,12,25,29,69,79,52,56,6,52,22,78,42,8,18,22,59,91,13,94,89,10,16,73,11,17,80,81,26,36,26,55,16,13,30,6,6,43,1,43,83,21,69,11,42,8,77,21,31,25,24,99,26,56,85,15,74,1,88,13,3,18,42,14,54,13,6,91,49,7,36,42,2,8,67,55,14,35,5,33,6,96,24,94,24,59,46,18,4,61,95,2,33,33,2,31,24,97,1,91,15,52,15,53,44,10,20,47,93,8,1,48,80,22,80,23,15,92,18,18,59,19,69,17,8,55,38,26,9,68,23,85,2,12,23,77,4,21,16,6,90,45,17,61,16,28,22,24,58,30,26,2,85,1,53,29,18,37,30,38,4,12,92,60,19,13,56,19,85,7,66,19,73,39,9,90,81,3,8,9,72,25,37,24,5,96,25,13,81,92,34,19,95,3,26,36,25,25,25,15,95,6,35,43,92,10,79,70,8,30,18,96,75,1,5,76,17,86,3,46,22,11,50,96,1,56,43,2,23,53,7,71,20,61,73,34,31,57,24,69,4,24,6,25,98,50,21,63,12,97,11,9,72,19,40,21,7,2,18,77,83,16,1,82,24,25,57,72,25,9,15,76,21,14,71,16,94,7,64,21,69,87,18,65,1,21,20,61,91,10,86,7,55,36,1,40,99,39,8,41,5,92,76,33,20,40,15,81,76,48,5,35,64,59,6,30,13,52,19,84,21,58,1,89,29,53,10,76,22,33,26,65,3,96,
            0,//1032. Jump If Counter
            0,//1033 Input
            21,//1034 Pre X
            21,//1035 Pre Y
            1,//1036//moved to 1041??
            10,//1037//Y / 2
            1,//1038//moved to 1043??
            0,//1039 Post X
            0,//1040 Post Y
            0,//1041//moved from 1036??
            0,//1042//Y / 2. But since he doesn't have a divide function, it is just independently kept...
            0,//1043//moved from 1038??
            0};//1044//0 or 1 or 2. 2 is at 35, 33. We start at 21, 21
    }
}
