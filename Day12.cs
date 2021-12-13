using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day12:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + "_sample.txt");

            string ln = "";

            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            while ((ln = sr.ReadLine()) != null)
            {
                string[] m = ln.Split('-');
                if (!map.ContainsKey(m[0]))
                {
                    map[m[0]] = new List<string>();
                    map[m[0]].Add(m[1]);
                }
                else if (!map[m[0]].Contains(m[1]))
                {
                    map[m[0]].Add(m[1]);
                }

                if (!map.ContainsKey(m[1]))
                {
                    map[m[1]] = new List<string>();
                    map[m[1]].Add(m[0]);
                }
                else if (!map[m[1]].Contains(m[0]))
                {
                    map[m[1]].Add(m[0]);
                }

            }


            int paths = 0;
            
            foreach (string p in map["start"])
            {
                string newwalk = "|start|" + p + "|";
                paths+= WalkMap(map, newwalk, p);
            }

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + paths.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";

            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            while ((ln = sr.ReadLine()) != null)
            {
                string[] m = ln.Split('-');
                if (!map.ContainsKey(m[0]))
                {
                    map[m[0]] = new List<string>();
                    map[m[0]].Add(m[1]);
                }
                else if (!map[m[0]].Contains(m[1]))
                {
                    map[m[0]].Add(m[1]);
                }

                if (!map.ContainsKey(m[1]))
                {
                    map[m[1]] = new List<string>();
                    map[m[1]].Add(m[0]);
                }
                else if (!map[m[1]].Contains(m[0]))
                {
                    map[m[1]].Add(m[0]);
                }

            }
            
            int paths = 0;

            foreach (string p in map["start"])
            {
                string newwalk = "|start|" + p + "|";
                paths += WalkMap2(map, newwalk, p);
            }

            sw.Stop();
            sr.Close();

            string ret = "Answer : " + paths.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        int WalkMap(Dictionary<string, List<string>> map, string thiswalk, string curpos)
        {
            int paths = 0;
            
            foreach (string p in map[curpos])
            {
                
                if (thiswalk.Contains("|" + p + "|" ) && (p.ToLower() == p))
                {
                    // can't walk it again 
                }
                else
                {
                    string newwalk = thiswalk + p + "|";
                    
                    if (p == "end")
                    {
                        paths++;
                    }
                    else
                    {
                        paths += WalkMap(map, newwalk, p);
                    }
                    
                }
                
            }

            return paths;
        }

        int WalkMap2(Dictionary<string, List<string>> map, string thiswalk, string curpos)
        {
            int paths = 0;
            
            foreach (string p in map[curpos])
            {

                if (thiswalk.Contains("|" + p + "|") && (p.ToLower() == p))
                {
                    if ((p!="start") && (!thiswalk.StartsWith("_")))
                    {
                        string newwalk = thiswalk + p + "|";
                        newwalk = "_" + newwalk;
                        paths += WalkMap2(map, newwalk, p);
                    }
                }
                else
                {
                    string newwalk = thiswalk + p + "|";

                    if (p == "end")
                    {
                        {
                            paths++;
                        }
                    }
                    else
                    {
                        paths += WalkMap2(map, newwalk, p);
                    }

                }

            }

            return paths;
        }

        int WalkMapOpt(Dictionary<string, List<string>> map, string thiswalk, List<string> thiswalklist, string curpos)
        {
            int paths = 0;
            
            foreach (string p in map[curpos])
            {

                if (thiswalk.Contains("|" + p + "|") && (p.ToLower() == p))
                {
                    // can't walk it again 
                    if ((p != "start") && (!thiswalk.StartsWith("_")))
                    {
                        string newwalk = thiswalk + p + "|";
                        newwalk = "_" + newwalk;
                        paths += WalkMapOpt(map, newwalk, thiswalklist, p);
                    }
                }
                else
                {
                    string newwalk = thiswalk + p + "|";

                    if (p == "end")
                    {
                        //if (!Walked.Contains(newwalk))
                        //{
                          //  Walked.Add(newwalk);
                            paths++;
                        //}
                    }
                    else
                    {
                        paths += WalkMapOpt(map, newwalk, thiswalklist, p);
                    }

                }

            }

            return paths;
        }

    }
}
