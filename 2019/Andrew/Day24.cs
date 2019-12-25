using System;
using System.Collections.Generic;
using System.Linq;
namespace AoC2019
{
    public class Day24
    {
        public Day24()
        {
        }
        public void Run()
        {
            /*HashSet<string> previous = new HashSet<string>();
            string result = Data;
            while (true)
            {
                result = Mutate(result);
                if (previous.Contains(result))
                {
                    Console.WriteLine(result);
                    long answer = 0;
                    int power = 0;
                    foreach (var character in result)
                    {
                        if (character == '#')
                        {
                            answer += (long)Math.Pow(2,power);
                        }
                        power++;
                    }
                    Console.WriteLine(answer);
                    break;
                }
                else
                {
                    previous.Add(result);
                }
            }
            //Console.WriteLine(Mutate(testData));*/

            Dictionary<int, string> MutationLevelsBefore = new Dictionary<int, string>();
            Dictionary<int, string> MutationLevelsAfter = new Dictionary<int, string>();
            for (int i = -200; i <= 200; i++)
            {
                MutationLevelsBefore[i] = ".".PadLeft(25, '.');
                if (i == 0)
                {
                    MutationLevelsBefore[i] = Data.Replace("\r","").Replace("\n","");
                }
            }

            for (int repeat = 0; repeat < 200; repeat++)
            {
                for (int i = 0; i <= 200; i++)
                {
                    MutationLevelsAfter[i] = Mutate(MutationLevelsBefore, MutationLevelsBefore[i], i);
                }
                for (int i = -1; i >= -200; i--)
                {
                    MutationLevelsAfter[i] = Mutate(MutationLevelsBefore, MutationLevelsBefore[i], i);
                }
                MutationLevelsBefore = new Dictionary<int, string>(MutationLevelsAfter);
            }
            int bugCount = 0;
            foreach (var entry in MutationLevelsAfter)
            {
                foreach (var character in entry.Value)
                {
                    if (character == '#')
                        bugCount++;
                }
            }
 

            Console.WriteLine(bugCount);


        }

        public void PrettyPrint(string grid)
        {
            Console.WriteLine(grid.Substring(0, 5));
            Console.WriteLine(grid.Substring(5, 5));
            Console.WriteLine(grid.Substring(10, 5));
            Console.WriteLine(grid.Substring(15, 5));
            Console.WriteLine(grid.Substring(20, 5));
            Console.WriteLine();
        }

        public string Mutate(Dictionary<int,string> dict_before, string before,int level)
        {
            before = before.Replace("\r", "").Replace("\n", "");
            string after = "";
            for (int i = 0; i < 25; i++)
            {
                int above = (i - 5 < 0) ? -1 : (before[i - 5] == '#' ? 1 : 0);
                int left = ((i % 5) == 0) ? -1 : (before[i - 1] == '#' ? 1 : 0);
                int right = ((i % 5) == 4) ? -1 : (before[i + 1] == '#' ? 1 : 0);
                int below = (i >= 20) ? -1 : (before[i + 5] == '#' ? 1 : 0);
                int count = 0;//(above == '#' ? 1 : 0) + (left == '#' ? 1 : 0) + (right == '#' ? 1 : 0) + (below == '#' ? 1 : 0);

                if (above==-1)
                {
                    above = 0;
                    if (dict_before.ContainsKey(level-1) && dict_before[level-1][7]=='#')
                    {
                        count++;
                    }
                    //level+1, i7
                }
                if (below==-1)
                {
                    below = 0;
                    if (dict_before.ContainsKey(level - 1) && dict_before[level - 1][17] == '#')
                    {
                        count++;
                    }
                    //level+1,i17
                }
                if (left==-1)
                {
                    left = 0;
                    if (dict_before.ContainsKey(level - 1) && dict_before[level - 1][11] == '#')
                    {
                        count++;
                    }
                    //level+1,i11
                }
                if (right==-1)
                {
                    right = 0;
                    if (dict_before.ContainsKey(level - 1) && dict_before[level - 1][13] == '#')
                    {
                        count++;
                    }
                    //level_1,i13
                }
                if (i==12)
                {
                    after += "?";
                    continue;//there is no 13...
                }
                else if (i==7)
                {
                    if (dict_before.ContainsKey(level+1))
                    {
                        string lowerlevel = dict_before[level + 1];
                        //int count2 = lowerlevel.Substring(0, 5).Count('#');
                        int count2 = 0;
                        for (int j = 0; j < 5; j++)
                        {
                            count2 += lowerlevel[j] == '#' ? 1 : 0;
                        }
                        count += count2;
                    }
                    //we need to get counts for level-1, i0 thru 4
                }
                else if (i==11)
                {
                    //we need to get counts for level-1, i0,5,10,15,20
                    if (dict_before.ContainsKey(level + 1))
                    {
                        string lowerlevel = dict_before[level + 1];
                        //int count2 = lowerlevel.Substring(0, 5).Count('#');
                        int count2 = 0;
                        for (int j = 0; j < 25; j+=5)
                        {
                            count2 += lowerlevel[j] == '#' ? 1 : 0;
                        }
                        count += count2;
                    }

                }
                else if (i==13)
                {
                    //we need to get counts for level-1,i4,9,14,19,24
                    if (dict_before.ContainsKey(level + 1))
                    {
                        string lowerlevel = dict_before[level + 1];
                        //int count2 = lowerlevel.Substring(0, 5).Count('#');
                        int count2 = 0;
                        for (int j = 4; j < 25; j+=5)
                        {
                            count2 += lowerlevel[j] == '#' ? 1 : 0;
                        }
                        count += count2;
                    }

                }
                else if (i==17)
                {
                    //we need to get counts for level-1,i20 thru 24
                    if (dict_before.ContainsKey(level + 1))
                    {
                        string lowerlevel = dict_before[level + 1];
                        //int count2 = lowerlevel.Substring(0, 5).Count('#');
                        int count2 = 0;
                        for (int j = 20; j < 25; j++)
                        {
                            count2 += lowerlevel[j] == '#' ? 1 : 0;
                        }
                        count += count2;
                    }

                }
                count = count + above + below + left + right;
                //compute count...

                if (before[i] == '#' && count != 1)
                {
                    after += '.';
                }
                else if (before[i] == '.' && (count == 1 || count == 2))
                {
                    after += '#';
                }
                else
                {
                    after += before[i];
                }
            }
            return after;
        }
        public string testData = @"....#
#..#.
#..##
..#..
#....";

        public string Data = @".#.##
...#.
....#
.#...
..#..
";
    }
}
