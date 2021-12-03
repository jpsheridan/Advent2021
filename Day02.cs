using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day02:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            long x = 0;
            long y = 0;
            while ((ln=sr.ReadLine())!=null)
            {
                string[] move = ln.Split(' ');
                long diff = int.Parse(move[1]);
                switch (move[0])
                {
                    case "up":
                        y -= diff;
                        break;
                    case "down":
                        y += diff;
                        break;
                    case "forward":
                        x += diff;
                        break;
                }
            }
            sw.Stop();

            sr.Close();
            long ans = x * y;
            string ret = "Answer : " + ans.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            long x = 0;
            long y = 0;
            long aim = 0;

            while ((ln = sr.ReadLine()) != null)
            {
                string[] move = ln.Split(' ');
                long diff = int.Parse(move[1]);
                switch (move[0])
                {
                    case "up":
                        aim -= diff;
                        break;
                    case "down":
                        aim += diff;
                        break;
                    case "forward":
                        x += diff;
                        y += aim * diff;
                        break;
                }
            }
            sw.Stop();

            sr.Close();
            long ans = x * y;
            string ret = "Answer : " + ans.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }
    

    }
}
