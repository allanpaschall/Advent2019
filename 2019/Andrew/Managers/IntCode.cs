using System;
using System.Collections;
using System.Collections.Generic;
namespace AoC2019
{
    public delegate long GetInput(int ThreadID);
    public class IntCode
    {
        public Queue<long> Output { get; private set; }
        public Queue<long> Input { get; private set; }
        private int IP { get; set; }
        private int RB { get; set; }
        public int ThreadID { get; set; }
        private GetInput inputCallback;

        public long[] IntCodeInstructions { get; set; }
        public IntCode(long[] I)
        {
            //IntCodeInstructions = (int[])I.Clone();
            IntCodeInstructions = new long[1024 * 512];
            for (int i = 0; i < I.Length; i++)
            {
                IntCodeInstructions[i] = I[i];
            }
            Output = new Queue<long>();
            Input = new Queue<long>();
        }
        public IntCode(long[] I, GetInput inputCallback, int ThreadID = -1)
        {
            this.ThreadID = ThreadID;
            this.inputCallback = inputCallback;
            //IntCodeInstructions = (int[])I.Clone();
            IntCodeInstructions = new long[1024 * 512];
            for (int i = 0; i < I.Length; i++)
            {
                IntCodeInstructions[i] = I[i];
            }
            Output = new Queue<long>();
            Input = new Queue<long>();
        }
        public long Fetch(int mode, int i)
        {
            return ((mode % 10 == 2 ? IntCodeInstructions[(int)(RB + IntCodeInstructions[i])] : (mode % 10 == 1 ? IntCodeInstructions[i] : IntCodeInstructions[(int)IntCodeInstructions[i]])));
        }

        public int GetAddress(int mode, int i)
        {
            return (int)((mode % 10 == 2 ? RB : 0) + IntCodeInstructions[i]);
        }

        public bool Run(RunningMode runningMode = RunningMode.OutputUnattached)
        {
            int timer = 0;
            if (runningMode == RunningMode.OutputUnattached)
                IP = 0;
            for (; IP < IntCodeInstructions.Length; IP++)
            {
                timer++;
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
                        if (inputCallback!=null)
                        {
                            IntCodeInstructions[GetAddress(mode, IP + 1)] = inputCallback(ThreadID);
                        }
                        else
                        {
                            IntCodeInstructions[GetAddress(mode, IP + 1)] = Input.Dequeue();
                        }
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
                if (timer>=25 && runningMode== RunningMode.Timed)
                {
                    IP += 1;
                    return false;
                }
            }
            return true;
        }
    }

    public enum RunningMode
    {
        OutputUnattached,
        OutputAttached,
        Timed
    }


    /*var cp = new System.CodeDom.Compiler.CompilerParameters {
  ReferencedAssemblies.Add(filesystemLocation), // like /R: option on csc.exe
  GenerateInMemory = true,    // you will get a System.Reflection.Assembly back
  GenerateExecutable = false, // Dll
  IncludeDebugInformation = false,
  CompilerOptions = ""
};

var csharp = new Microsoft.CSharp.CSharpCodeProvider();

// this actually runs csc.exe:
System.CodeDom.Compiler.CompilerResults cr = 
      csharp.CompileAssemblyFromSource(cp, LiteralSource);


// cr.Output contains the output from the command

if (cr.Errors.Count != 0)
{
    // handle errors
}

System.Reflection.Assembly a = cr.CompiledAssembly;

// party on the type here, either via reflection...
System.Type t = a.GetType("TheDynamicallyGeneratedType");

// or via a wellknown interface*/
}
