using System;
using System.Collections.Generic;
using System.Linq;
namespace AoC2019
{
    public class Day7
    {
        public Day7()
        {
        }
        public void Run()
        {
            //Part1();
            Part2();
        }
        public void Part1()
        {
            Dictionary<int, int[]> attempts = new Dictionary<int, int[]>();
            for (int a = 0; a <= 4; a++)
            {
                for (int b = 0; b <= 4; b++)
                {
                    for (int c = 0; c <= 4; c++)
                    {
                        for (int d = 0; d <= 4; d++)
                        {
                            for (int e = 0; e <= 4; e++)
                            {
                                if (a==b || a==c || a==d || a==e || b==c || b==d || b==e || c==d || c==e || d==e)
                                {
                                    continue;
                                }
                                var data = (int[])Data.Clone();
                                var output = 0;
                                output = IntcodeComputer(data, new int[] { a, output });
                                output = IntcodeComputer(data, new int[] { b, output });
                                output = IntcodeComputer(data, new int[] { c, output });
                                output = IntcodeComputer(data, new int[] { d, output });
                                output = IntcodeComputer(data, new int[] { e, output });
                                if (!attempts.ContainsKey(output))
                                {
                                    attempts[output] = new int[] { a, b, c, d, e };
                                }
                            }
                        }
                    }
                }
            }
            var best = (from att in attempts orderby att.Key descending select att).First();
            Console.WriteLine("Best:" + best.Key + "," +  best.Value[0] + best.Value[1] + best.Value[2] + best.Value[3] + best.Value[4]);
        }
        public void Part2()
        {
            Dictionary<int, int[]> attempts = new Dictionary<int, int[]>();
            for (int a = 5; a <= 9; a++)
            {
                for (int b = 5; b <= 9; b++)
                {
                    for (int c = 5; c <= 9; c++)
                    {
                        for (int d = 5; d <= 9; d++)
                        {
                            for (int e = 5; e <= 9; e++)
                            {
                                if (a == b || a == c || a == d || a == e || b == c || b == d || b == e || c == d || c == e || d == e)
                                {
                                    continue;
                                }
                                var output = 0;
                                Pointers = new int[] { 0, 0, 0, 0, 0 };
                                var nonZeroOutput = 0;
                                var dataA = (int[])Data.Clone();
                                var dataB = (int[])Data.Clone();
                                var dataC = (int[])Data.Clone();
                                var dataD = (int[])Data.Clone();
                                var dataE = (int[])Data.Clone();
                                int[][] Inputs = { dataA, dataB, dataC, dataD, dataE };
                                do
                                {
                                    output = IntcodeComputerRecursion(Inputs, Pointers, new int[] { a, b, c, d, e, output }, 0);
                                    if (output != 0) nonZeroOutput = output;
                                    //output = IntcodeComputer(dataA, new int[] { 9, output });
                                    //output = IntcodeComputer(dataB, new int[] { 8, output });
                                    //output = IntcodeComputer(dataC, new int[] { 7, output });
                                    //output = IntcodeComputer(dataD, new int[] { 6, output });
                                    //output = IntcodeComputer(dataE, new int[] { 5, output });
                                    //Console.WriteLine("Output: " + output);
                                } while (output != 0);
                                if (!attempts.ContainsKey(nonZeroOutput))
                                {
                                    attempts[nonZeroOutput] = new int[] { a, b, c, d, e };
                                }
                            }
                        }
                    }
                }
            }
            var best = (from att in attempts orderby att.Key descending select att).First();
            Console.WriteLine("Best:" + best.Key + "," + best.Value[0] + best.Value[1] + best.Value[2] + best.Value[3] + best.Value[4]);


        }


        public static int Fetch(int mode, int[] Input, int i)
        {
            return (mode % 10 == 1 ? Input[i] : Input[Input[i]]);
        }

        int[] Pointers = { 0, 0, 0, 0, 0 };

        public static int IntcodeComputerRecursion(int[][] Inputs, int[] Pointers, int[] signals, int depth)
        {
            var Input = Inputs[depth];
            int output = 0;
            bool initialSignalUsed = Pointers[depth]!=0;
            for (int i = Pointers[depth]; i < Input.Length; i++)
            {
                int mode = Input[i] / 100;
                switch (Input[i] % 100)
                {
                    case 1://add
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) + Fetch(mode / 10, Input, i + 2);
                        i += 3;
                        break;
                    case 2://multiply
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) * Fetch(mode / 10, Input, i + 2);
                        i += 3;
                        break;
                    case 3://input
                        int signal;
                        if (!initialSignalUsed)
                        {
                            signal = signals[depth];
                            initialSignalUsed = true;
                        }
                        else
                        {
                            signal = signals[5];
                        }
                        Input[Input[i + 1]] = signal;//console in
                        i += 1;
                        break;
                    case 4://output
                        Console.WriteLine("Output, Depth " + depth + ":" + Fetch(mode, Input, i + 1));
                        output = Fetch(mode, Input, i + 1);
                        Pointers[depth] = i + 1;
                        if (depth!=4)
                        {
                            signals[5] = output;
                            return IntcodeComputerRecursion(Inputs, Pointers, signals, depth + 1);
                        }
                        else
                        {
                            return output;
                        }
                        i += 1;
                        break;
                    case 5://jump if true
                        i += 2;
                        if (Fetch(mode, Input, i - 1) != 0)
                        {
                            i = Fetch(mode / 10, Input, i) - 1;
                        }
                        break;
                    case 6://jump if false
                        i += 2;
                        if (Fetch(mode, Input, i - 1) == 0)
                        {
                            i = Fetch(mode / 10, Input, i) - 1;
                        }
                        break;
                    case 7://less than
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) < Fetch(mode / 10, Input, i + 2) ? 1 : 0;
                        i += 3;
                        break;
                    case 8://equals
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) == Fetch(mode / 10, Input, i + 2) ? 1 : 0;
                        i += 3;
                        break;
                    case 99://exit
                        return output;

                }
            }
            return Input[0];
        }

        public static int IntcodeComputer(int[] Input, int[] op3)
        {
            int output = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                int mode = Input[i] / 100;
                switch (Input[i] % 100)
                {
                    case 1://add
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) + Fetch(mode / 10, Input, i + 2);
                        i += 3;
                        break;
                    case 2://multiply
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) * Fetch(mode / 10, Input, i + 2);
                        i += 3;
                        break;
                    case 3://input
                        Input[Input[i + 1]] = op3.First();//console in
                        op3 = op3.Skip(1).ToArray();
                        i += 1;
                        break;
                    case 4://output
                        //Console.WriteLine("Output:" + Fetch(mode, Input, i + 1));
                        output = Fetch(mode, Input, i + 1);
                        i += 1;
                        break;
                    case 5://jump if true
                        i += 2;
                        if (Fetch(mode, Input, i - 1) != 0)
                        {
                            i = Fetch(mode / 10, Input, i) - 1;
                        }
                        break;
                    case 6://jump if false
                        i += 2;
                        if (Fetch(mode, Input, i - 1) == 0)
                        {
                            i = Fetch(mode / 10, Input, i) - 1;
                        }
                        break;
                    case 7://less than
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) < Fetch(mode / 10, Input, i + 2) ? 1 : 0;
                        i += 3;
                        break;
                    case 8://equals
                        Input[Input[i + 3]] = Fetch(mode, Input, i + 1) == Fetch(mode / 10, Input, i + 2) ? 1 : 0;
                        i += 3;
                        break;
                    case 99://exit
                        return output;

                }
            }
            return Input[0];
        }

        public int[] TestData = { 3,23,3,24,1002,24,10,24,1002,23,-1,23,
101,5,23,23,1,24,23,23,4,23,99,0,0 };

        public int[] TestDataPart2 = {3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };

        public int[] Data = { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 46, 55, 72, 85, 110, 191, 272, 353, 434, 99999, 3, 9, 1002, 9, 5, 9, 1001, 9, 2, 9, 102, 3, 9, 9, 101, 2, 9, 9, 102, 4, 9, 9, 4, 9, 99, 3, 9, 102, 5, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 101, 2, 9, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1002, 9, 4, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 3, 9, 101, 5, 9, 9, 1002, 9, 3, 9, 101, 3, 9, 9, 1002, 9, 5, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99 };
    }
}
