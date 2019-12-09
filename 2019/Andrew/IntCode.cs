using System;
using System.Collections;
using System.Collections.Generic;
namespace AoC2019
{
    public class IntCode
    {
        public Queue<decimal> Output { get; private set; }
        public Queue<decimal> Input { get; private set; }
        private int IP { get; set; }
        private int RB { get; set; }

        public decimal[] IntCodeInstructions { get; set; }
        public IntCode(decimal[] I)
        {
            //IntCodeInstructions = (int[])I.Clone();
            IntCodeInstructions = new decimal[1024 * 512];
            for (int i = 0; i < I.Length; i++)
            {
                IntCodeInstructions[i] = I[i];
            }
            Output = new Queue<decimal>();
            Input = new Queue<decimal>();
        }
        public decimal Fetch(int mode, int i)
        {
            return ((mode % 10 == 2 ? IntCodeInstructions[(int)(RB + IntCodeInstructions[i])] : (mode % 10 == 1 ? IntCodeInstructions[i] : IntCodeInstructions[(int)IntCodeInstructions[i]])));
        }

        public int GetAddress(int mode, int i)
        {
            return (int)((mode % 10 == 2 ? RB : 0) + IntCodeInstructions[i]);
        }

        public bool Run(RunningMode runningMode = RunningMode.OutputUnattached)
        {
            if (runningMode == RunningMode.OutputUnattached)
                IP = 0;
            for (; IP < IntCodeInstructions.Length; IP++)
            {
                int mode = (int)IntCodeInstructions[IP] / 100;
                switch (IntCodeInstructions[IP] % 100)
                {
                    case 1://add
                        IntCodeInstructions[GetAddress(mode / 100,IP + 3)] = Fetch(mode, IP + 1) + Fetch(mode / 10, IP + 2);
                        IP += 3;
                        break;
                    case 2://multiply
                        IntCodeInstructions[GetAddress(mode / 100, IP + 3)] = Fetch(mode, IP + 1) * Fetch(mode / 10, IP + 2);
                        IP += 3;
                        break;
                    case 3://input
                        IntCodeInstructions[GetAddress(mode, IP + 1)] = Input.Dequeue();
                        IP += 1;
                        break;
                    case 4://output
                        Output.Enqueue(Fetch(mode, IP + 1));
                        IP += 1;
                        if (runningMode== RunningMode.OutputAttached)
                        {
                            IP += 1;
                            return false;
                        }
                        break;
                    case 5://jump if true
                        IP = Fetch(mode, IP + 1) != 0 ? (int)Fetch(mode / 10, IP + 2) - 1 : IP + 2;
                        break;
                    case 6://jump if false
                        IP = Fetch(mode, IP + 1) == 0 ? (int)Fetch(mode / 10, IP + 2) - 1 : IP + 2;
                        break;
                    case 7://less than
                        IntCodeInstructions[GetAddress(mode / 100, IP + 3)] = Fetch(mode, IP + 1) < Fetch(mode / 10, IP + 2) ? 1 : 0;
                        IP += 3;
                        break;
                    case 8://equals
                        IntCodeInstructions[GetAddress(mode / 100, IP + 3)] = Fetch(mode, IP + 1) == Fetch(mode / 10, IP + 2) ? 1 : 0;
                        IP += 3;
                        break;
                    case 9:
                        RB += (int)Fetch(mode, IP + 1);
                        IP += 1;
                        break;
                    case 99://exit
                        return true;
                }
            }
            return true;
        }
    }

    public enum RunningMode
    {
        OutputUnattached,
        OutputAttached
    }
}
