using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day08 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int c = 0;
            
            while ((ln = sr.ReadLine()) != null)
            {
                string[] inout = ln.Replace(" | ", "|").Split('|');
                string[] outs = inout[1].Split(' ');

                foreach (string o in outs)
                {
                    if (o.Length == 2 || o.Length == 4 || o.Length == 3 || o.Length == 7)
                    {
                        c++;   
                    }
                }
            }
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + c.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + "_sample.txt");

            string ln = "";
            int c = 0;

            int tot = 0;
            
            while ((ln = sr.ReadLine()) != null)
            {
                string[] inout = ln.Replace(" | ", "|").Split('|');
                string[] ins = inout[0].Split(' ');
                string[] outs = inout[1].Split(' ');

                Dictionary<string, int> thisdig = DecodeNum(ins);
                string n = "";
                foreach (string o in outs)
                {
                    string osort = String.Concat(o.OrderBy(ch => ch));
                    n += thisdig[osort];
                }
                tot += int.Parse(n);
                Debug.WriteLine(n);

            }
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + tot.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

      
        Dictionary<string, int> DecodeNum(string[] inpnums)
        {
            Dictionary<string, int> dig = new Dictionary<string, int>();
            Dictionary<int, string> dig2 = new Dictionary<int, string>();
            List<string> tofind = new List<string>();
            for (int i = 0; i < inpnums.Length; i++)
            {
                inpnums[i]= String.Concat(inpnums[i].OrderBy(ch => ch));
                tofind.Add(inpnums[i]);
            }

            // find the 2,3,4,7 digit numbers

            foreach (string i in inpnums)
            {
                if (i.Length == 2)
                {
                    dig[i] = 1;
                    dig2[1] = i;
                    tofind.Remove(i);
                }
                if (i.Length == 4)
                {
                    dig[i] = 4;
                    dig2[4] = i;
                    tofind.Remove(i);
                }
                    
                if (i.Length == 3)
                {
                    dig[i] = 7;
                    dig2[7] = i;
                    tofind.Remove(i);
                }
                if (i.Length == 7)
                {
                    dig[i] = 8;
                    dig2[8] = i;
                    tofind.Remove(i);
                }
            }


            
            //2,3,5 have 5 digits but only 5 has bd
            //0,6,9 have 6 digits, only 0 doesn't have bd.
            string f = dig2[4].Replace(dig2[1].Substring(0,1), "");
            f = f.Replace(dig2[1].Substring(1, 1), "");

            string f1 = f.Substring(0, 1);
            string f2 = f.Substring(1, 1);

            for (int z = tofind.Count - 1; z >= 0; z--)
            {
                string tf = tofind[z];
                if (tf.Length==5)
                {
                    if (tf.Contains(f1) && tf.Contains(f2))
                    {
                        tofind.Remove(tf);
                        dig[tf] = 5;
                        dig2[5] = tf;
                    }
                    else if (tf.Length == 6)
                    {
                        if (!(tf.Contains(f1) && tf.Contains(f2)))
                        {
                            tofind.Remove(tf);
                            dig[tf] = 0;
                            dig2[0] = tf;
                        }
                    }
                }
            }


            f1 = dig2[1].Substring(0, 1);
            f2 = dig2[1].Substring(1, 1);
            // now 2 5 digit nums left.
            // if it contains "1" then = 3 else 2
            // 2 6 digit nums
            // if it contains "1" then 9 else 6

            for (int z = tofind.Count-1; z >=0;z--)
            {
                string tf = tofind[z];
                if (tf.Length == 5)
                {
                    if (tf.Contains(f1) && tf.Contains(f2))
                    {
                        dig[tf] = 3;
                        dig2[3] = tf;
                    }
                    else
                    {
                        tofind.Remove(tf);
                        dig[tf] = 2;
                        dig2[2] = tf;
                    }
                }
                else if (tf.Length == 6)
                {
                    if (tf.Contains(f1) && tf.Contains(f2))
                    {
                        tofind.Remove(tf);
                        dig[tf] = 9;
                        dig2[9] = tf;
                    }
                    else
                    {
                        tofind.Remove(tf);
                        dig[tf] = 6;
                        dig2[6] = tf;
                    }
                }
            }

            
    


            return dig;
        }

    }
}
