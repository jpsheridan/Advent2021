using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day03:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            int[] c = new int[12];
            int rows = 0;
            while ((ln = sr.ReadLine()) != null)
            {
                rows++;
                for (int i = 0; i < ln.Length; i++)
                {
                    c[i] += int.Parse(ln.Substring(i, 1));
                }
            }

            string eps = "";
            string gam = "";
            int max = rows / 2;
            for (int i = 0; i<12; i++)
            {
                if (c[i]> max)
                {
                    gam += "1";
                    eps += "0";
                }
                else
                {
                    gam += "0";
                    eps += "1";
                }
            }

            int g = Convert.ToInt32(gam, 2);
            int e = Convert.ToInt32(eps, 2);

            valid = g * e;
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + valid.ToString();
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
            int strlen = 12;
            int[] c = new int[strlen];
            int rows = 0;
            List<string> diag = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                rows++;
                for (int i = 0; i < ln.Length; i++)
                {
                    c[i] += int.Parse(ln.Substring(i, 1));
                }
                diag.Add(ln);

            }

            string gam = matchnums("gam", diag);
            string eps = matchnums("eps", diag);

            int g = Convert.ToInt32(gam, 2);
            int e = Convert.ToInt32(eps, 2);

            valid = g * e;
        
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        string matchnums (string match, List<string> nums)
        {
            List<string> matched = new List<string>(nums);
            List<string> temp0 = new List<string>();
            List<string> temp1 = new List<string>();

            
            for (int i = 0; i < nums[0].Length; i++)
            {
                int max = (int) Math.Ceiling((double)matched.Count / 2);

                int ones = 0;
                for (int j = 0; j < matched.Count; j++)
                {
                    if (matched[j].Substring(i,1)== "1")
                    {
                        temp1.Add(matched[j]);
                        ones++;
                    }
                    else
                    {
                        temp0.Add(matched[j]);
                    }
                }
                                
                if (ones >= max)
                {
                    if (match == "gam")
                        matched = new List<string>(temp1);
                    else
                        matched = new List<string>(temp0);
                }
                else
                {
                    if (match == "gam")
                        matched = new List<string>(temp0);
                    else
                        matched = new List<string>(temp1);
                    
                }

                temp0.Clear();
                temp1.Clear();
                ones = 0;

                if (matched.Count==1)
                {
                    break;
                }

            }

            return matched[0];
        }
    }
}
