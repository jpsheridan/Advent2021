using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day06:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            long[] fish = new long[9];

            string ln = "";
            int valid = 0;
            string[] inp = sr.ReadLine().Split(',');
            
            for (int i = 0; i < inp.Length; i++)
            {
                int age = int.Parse(inp[i]);
                fish[age]++;
            }

            for (int j = 0; j < 80; j++)
            {
                long newf = fish[0];
                for (int i = 0; i < 8; i++)
                {
                    fish[i] = fish[i + 1];
                }
                fish[6] += newf;
                fish[8] = newf;
            }
            sw.Stop();

            sr.Close();
            long fishcount = 0;
            for (int i = 0; i < 9; i++)
            {
                fishcount += fish[i];
            }

            string ret = "Answer : " + fishcount.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            long[] fish = new long[9];

            string ln = "";
            int valid = 0;
            string[] inp = sr.ReadLine().Split(',');

            for (int i = 0; i < inp.Length; i++)
            {
                int age = int.Parse(inp[i]);
                fish[age]++;
            }

            for (int j = 0; j < 256; j++)
            {
                long newf = fish[0];
                for (int i = 0; i < 8; i++)
                {
                    fish[i] = fish[i + 1];
                }
                fish[6] += newf;
                fish[8] = newf;
            }
            sw.Stop();

            sr.Close();
            long fishcount = 0;
            for (int i = 0; i < 9; i++)
            {
                fishcount += fish[i];
            }

            string ret = "Answer : " + fishcount.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }
    }
}
