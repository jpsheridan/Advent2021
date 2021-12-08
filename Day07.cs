using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day07:DayClass
    {
        object o = new object();
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");


            string[] crabs = sr.ReadLine().Split(',');
            int[] crabint = new int[crabs.Length];

            int maxcrab = 0;
            for (int i = 0; i < crabs.Length; i++)
            {
                crabint[i] = int.Parse(crabs[i]);

                if (crabint[i]> maxcrab)
                {
                    maxcrab = crabint[i];
                }
            }

    
            int minmove = int.MaxValue;
            for (int i = 0; i < maxcrab; i++)
            {
                int f = SumFuel(crabint, i);
                if (f < minmove)
                {
                    minmove = f;
                }
            }

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + minmove.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString() + "ms";
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");
            
            string[] crabs = sr.ReadLine().Split(',');
            int[] crabint = new int[crabs.Length];

            int maxcrab = 0;
            for (int i = 0; i < crabs.Length; i++)
            {
                crabint[i] = int.Parse(crabs[i]);
                
                if (crabint[i] > maxcrab)
                {
                    maxcrab = crabint[i];
                }
            }

            int minmove = int.MaxValue;

            for (int i = 0; i < maxcrab; i++)
            {
                int f = SumFuel2(crabint, i);
                if (f < minmove)
                {
                    minmove = f;
                }
            }


            sw.Stop();

            sr.Close();

            string ret = "Answer : " + minmove.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString() + "ms";
            return ret;


        }

        int SumFuel(int[] crabs, int pos )
        {
            int sumfuel = 0;
            for (int i = 0; i < crabs.Length; i++)
            {
                sumfuel += Math.Abs(crabs[i] - pos);
            }

            return sumfuel ;
        }

        int SumFuel2(int[] crabs, int pos)
        {
            int sumfuel = 0;
            for (int i = 0; i < crabs.Length; i++)
            {
                int dist = Math.Abs(crabs[i] - pos);
                sumfuel += (dist * (dist + 1)) / 2;
            }

            return sumfuel;
        }
    }
}
