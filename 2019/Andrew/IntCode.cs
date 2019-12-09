using System;
using System.Collections;
using System.Collections.Generic;
namespace AoC2019
{
    public class IntCode
    {
        public Queue<int> Output { get; private set; }
        public Queue<int> Input { get; private set; }
        private int IP { get; set; }

        public int[] IntCodeInstructions { get; set; }
        public IntCode(int[] I)
        {
            IntCodeInstructions = (int[])I.Clone();
            Output = new Queue<int>();
            Input = new Queue<int>();
        }
        public int Fetch(int mode, int i)
        {
            return (mode % 10 == 1 ? IntCodeInstructions[i] : IntCodeInstructions[IntCodeInstructions[i]]);
        }

        public bool Run(RunningMode runningMode = RunningMode.OutputUnattached)
        {
            if (runningMode == RunningMode.OutputUnattached)
                IP = 0;
            for (; IP < IntCodeInstructions.Length; IP++)
            {
                int mode = IntCodeInstructions[IP] / 100;
                switch (IntCodeInstructions[IP] % 100)
                {
                    case 1://add
                        IntCodeInstructions[IntCodeInstructions[IP + 3]] = Fetch(mode, IP + 1) + Fetch(mode / 10, IP + 2);
                        IP += 3;
                        break;
                    case 2://multiply
                        IntCodeInstructions[IntCodeInstructions[IP + 3]] = Fetch(mode, IP + 1) * Fetch(mode / 10, IP + 2);
                        IP += 3;
                        break;
                    case 3://input
                        IntCodeInstructions[IntCodeInstructions[IP + 1]] = Input.Dequeue();
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
                        IP = Fetch(mode, IP + 1) != 0 ? Fetch(mode / 10, IP + 2) - 1 : IP + 2;
                        break;
                    case 6://jump if false
                        IP = Fetch(mode, IP + 1) == 0 ? Fetch(mode / 10, IP + 2) - 1 : IP + 2;
                        break;
                    case 7://less than
                        IntCodeInstructions[IntCodeInstructions[IP + 3]] = Fetch(mode, IP + 1) < Fetch(mode / 10, IP + 2) ? 1 : 0;
                        IP += 3;
                        break;
                    case 8://equals
                        IntCodeInstructions[IntCodeInstructions[IP + 3]] = Fetch(mode, IP + 1) == Fetch(mode / 10, IP + 2) ? 1 : 0;
                        IP += 3;
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
