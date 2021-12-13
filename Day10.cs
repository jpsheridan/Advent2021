using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day10 : DayClass
    {
        Dictionary<int, long> joltcount = new Dictionary<int, long>();
        public override string Part1()
        {
            
    //): 3 points.
    //]: 57 points.
    //}: 1197 points.
    //>: 25137 points.

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            int synval = 0;

            while ((ln = sr.ReadLine()) != null)
            {
                int thisval = ParseChunk(ln);
                
                synval += thisval;
            }
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + synval.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            //): 3 points.
            //]: 57 points.
            //}: 1197 points.
            //>: 25137 points.

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");
            List<long> scores = new List<long>();
            string ln = "";
            
            while ((ln = sr.ReadLine()) != null)
            {
                long thisval = ParseChunkComplete(ln);
                
                if (thisval > 0)
                {
                    scores.Add(thisval);
                }
                
                
            }
            sw.Stop();
            sr.Close();

            scores.Sort();
            long synval = scores[(scores.Count ) / 2];

            string ret = "Answer : " + synval.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int ParseChunk(string inp)
        {
            //): 3 points.
            //]: 57 points.
            //}: 1197 points.
            //>: 25137 points.
            int endlen = inp.Length-1;
            int startlen = inp.Length;

            while (startlen > endlen && endlen > 0)
            {
                startlen = inp.Length;
                inp = inp.Replace("<>", "").Replace("{}", "").Replace("[]", "").Replace("()", "");
                endlen = inp.Length;
            }
            
            if (endlen == 0)
            {
                return 0;
            }
            else
            {
                for (int i = 1; i < inp.Length; i++)
                {
                    switch (inp.Substring(i, 1))
                    {
                        case "}":
                            return 1197;
                        case ")":
                            return 3;
                        case "]":
                            return 57;
                        case ">":
                            return 25137;
                    }
                }
            }
            return 0;
        }


        long ParseChunkComplete(string inp)
        {
            //): 3 points.
            //]: 57 points.
            //}: 1197 points.
            //>: 25137 points.
            int endlen = inp.Length - 1;
            int startlen = inp.Length;

            while (startlen > endlen && endlen > 0)
            {
                startlen = inp.Length;
                inp = inp.Replace("<>", "").Replace("{}", "").Replace("[]", "").Replace("()", "");
                endlen = inp.Length;
            }

            if (endlen == 0)
            {
                return 0;
            }
            else
            {
                string lastc = inp.Substring(0, 1);
                for (int i = 1; i < inp.Length; i++)
                {
                    switch (inp.Substring(i, 1))
                    {
                        case "}":
                        case ")":
                        case "]":
                        case ">":
                            return 0;
                    }
                }
            }

            long repval = 0;
            // chunk incomplete
            for (int i = inp.Length-1; i >=0; i--)
            {
                switch (inp.Substring(i, 1))
                {
                    case "{":
                        repval *= 5;
                        repval += 3;
                        break;
                    case "(":
                        repval *= 5;
                        repval += 1;
                        break;
                    case "[":
                        repval *= 5;
                        repval += 2;
                        break;
                    case "<":
                        repval *= 5;
                        repval += 4;
                        break;
                }
            }
            return repval;

        }
    }
}
