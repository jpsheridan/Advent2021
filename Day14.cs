using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace AdventCode
{
    
    public class Day14:DayClass
    {

        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            string chain = sr.ReadLine();
            sr.ReadLine();
            Dictionary<string, string> conv = new Dictionary<string, string>();
            while ((ln = sr.ReadLine()) != null)
            {
                string[] e = ln.Replace(" -> ", "|").Split('|');

                conv[e[0]] = e[1];

            }


            for (int i = 0; i < 10; i++)
            {
                chain = DoTurn(chain, conv);
                //Debug.WriteLine(chain);
            }

            valid = CountElements(chain);
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
            long valid = 0;
            string chain = sr.ReadLine();
            sr.ReadLine();
            Dictionary<string, string> conv = new Dictionary<string, string>();

            Dictionary<string, long> pairs  = new Dictionary<string, long>();
            Dictionary<string, long> elementcount = new Dictionary<string, long>();
            while ((ln = sr.ReadLine()) != null)
            {
                string[] e = ln.Replace(" -> ", "|").Split('|');

                conv[e[0]] = e[1];
                pairs[e[0]] = 0;

                if (!elementcount.ContainsKey(e[1]))
                {
                    elementcount[e[1]] = 0;
                }

            }
            

            for (int i = 0; i < chain.Length-1; i++)
            {
                if (pairs.ContainsKey(chain.Substring(i,2)))
                {
                    pairs[chain.Substring(i, 2)]++;
                }
                else
                {
                    pairs[chain.Substring(i, 2)] = 1;
                }

                if (elementcount.ContainsKey(chain.Substring(i,1)))
                {
                    elementcount[chain.Substring(i, 1)]++;
                }
                else
                {
                    elementcount[chain.Substring(i, 1)] = 1;
                }

            }

            elementcount[chain.Substring(chain.Length - 1, 1)]++;



            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> newpairs = new Dictionary<string, long>(pairs);

                foreach (KeyValuePair<string, long> c in pairs)
                {
                    if (c.Value > 0)
                    {
                        string p1 = c.Key.Substring(0, 1) + conv[c.Key];
                        string p2 = conv[c.Key] + c.Key.Substring(1, 1);

                        elementcount[conv[c.Key]] += pairs[c.Key];
                        
                        newpairs[c.Key] -= pairs[c.Key];
                        newpairs[p1] += pairs[c.Key];
                        newpairs[p2] += pairs[c.Key];

                    }
                }
                pairs = new Dictionary<string, long>(newpairs);
            }

            foreach (KeyValuePair<string, long> c in elementcount)
            {
                Debug.WriteLine(c.Key + " - " + c.Value);
            }

            long min = long.MaxValue;
            long max = long.MinValue;

            foreach (KeyValuePair<string, long> c in elementcount)
            {
                // Debug.WriteLine(c.Key + " - " + c.Value);
                if (c.Value < min)
                {
                    min = c.Value;
                }
                if (c.Value > max)
                {
                    max = c.Value;
                }
            }

            sw.Stop();
            sr.Close();

            string ret = "Answer : " +  (max-min).ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        string DoTurn(string chain, Dictionary<string, string> conv)
        {
            string newchain = "";

            newchain = chain.Substring(0, 1);
            for (int i = 0; i < chain.Length-1; i++)
            {
                string thispair = chain.Substring(i, 2);

                newchain += conv[thispair] + thispair.Substring(1); 

                 
            }
            return newchain;
        }

        int CountElements(string chain)
        {
            Dictionary<string, int> chaincount = new Dictionary<string, int>();
            for (int i =0; i < chain.Length; i++)
            {
                if (chaincount.ContainsKey(chain.Substring(i, 1)))
                {
                    chaincount[chain.Substring(i, 1)]++;
                }
                else
                {
                    chaincount[chain.Substring(i, 1)] = 1;
                }
                
            }
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (KeyValuePair<string, int> c in chaincount)
            {
               // Debug.WriteLine(c.Key + " - " + c.Value);
                if (c.Value < min)
                {
                    min = c.Value;
                }
                if (c.Value > max)
                {
                    max = c.Value;
                }
            }

            return (max-min);
        }
    }
}
