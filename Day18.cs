using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day18 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            string sn = sr.ReadLine();
            while ((ln = sr.ReadLine()) != null)
            {
                sn = AddNum(sn, ln);
                sn = SplitExplode(sn);
            }
            sw.Stop();

            sr.Close();
            valid = GetSum(sn);

            

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
            
            List<string> snailnums = new List<string>();
            string sn = "";

            while ((ln = sr.ReadLine()) != null)
            {
                snailnums.Add(ln);
            }
            sw.Stop();
            int maxsum = 0;

            for (int i = 0; i < snailnums.Count-1; i++)
            {
                //causDebug.WriteLine(i.ToString() + " - " + maxsum.ToString());
                for (int j=i+1; j < snailnums.Count; j++)
                {
                    sn = AddNum(snailnums[i], snailnums[j]);
                    sn = SplitExplode(sn);
                    int sum = GetSum(sn);
                    if (sum > maxsum)
                    {
                        maxsum = sum;
                    }

                    sn = AddNum(snailnums[j], snailnums[i]);
                    sn = SplitExplode(sn);
                    sum = GetSum(sn);
                    if (sum > maxsum)
                    {
                        maxsum = sum;
                    }
                }
            }


            sr.Close();
            
            valid = GetSum(sn);



            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int GetSum(string sn)
        {
            while (sn.Contains("["))
            {
                int lastleft= 0;
                for (int i = 0; i < sn.Length; i++)
                {
                    if (sn.Substring(i,1)=="[")
                    {
                        lastleft = i;
                    }
                    if (sn.Substring(i,1)=="]")
                    {

                        string ts = sn.Substring(lastleft + 1, i - lastleft - 1);
                        string[] nums = ts.Split(',');
                        int n1 = int.Parse(nums[0]);
                        int n2 = int.Parse(nums[1]);

                        int n3 = n1 * 3 + n2 * 2;
                        StringBuilder sb = new StringBuilder(sn);
                        sn = sn.Replace("[" + ts + "]", n3.ToString());
                        break;
                    }
                }
            }
            return int.Parse(sn);
        }

        string AddNum(string n1, string n2)
        {
            string n3 = "[" + n1 + "," + n2 + "]";
            return n3;
        }

        string SplitExplode(string sn)
        {
            bool changed = true;
            string thisnum = "";
            while (changed)
            {
                //Debug.WriteLine(sn);
                changed = false;
                int lastleft = 0;
                int leftcount = 0;
                int numstart = 0;
                int numend = 0;
                thisnum = "";
                string savenum = "";
                int thisnumstart = 0;
                bool havenum = false;
                
                for (int i = 0; i < sn.Length; i++)
                {
                    if (sn.Substring(i, 1) == "[")
                    {
                        thisnum = "";
                        lastleft = i;
                        leftcount++;
                    }
                    else if (sn.Substring(i, 1) == "]")
                    {
                        
                        if (leftcount > 4) // explode
                        {
                            
                            string ts = sn.Substring(lastleft + 1, i - lastleft - 1);
                            string[] nums = ts.Split(',');
                            int n1 = int.Parse(nums[0]);
                            int n2 = int.Parse(nums[1]);

                            string snleft = sn.Substring(0, lastleft);
                            string snright = sn.Substring(i + 1);

                            snleft = AddLast(snleft, n1);
                            snright = AddFirst(snright, n2);

                            sn = snleft + "0" + snright;
                            changed = true;
                            break;
                        }
                        leftcount--;
                        if (thisnum != "" & !havenum)
                        {
                            if (int.Parse(thisnum) > 9)
                            {
                                havenum = true;
                                numend = i;
                                savenum = thisnum;
                                numstart = thisnumstart;
                            }
                            
                        }
                        thisnum = "";
                        thisnumstart = 0;


                    }
                    else if (sn.Substring(i,1) == ",")
                    {
                        if (thisnum != "")
                        {
                            if (int.Parse(thisnum) > 9)
                            {
                                havenum = true;
                                numend = i;
                                savenum = thisnum;
                                numstart = thisnumstart;
                            }
                        }
                        thisnum = "";
                        thisnumstart = 0;


                    }
                    else
                    {
                        if (!havenum)
                        {
                            if (thisnumstart == 0)
                            {
                                thisnumstart = i;
                            }
                            
                            thisnum += sn.Substring(i, 1);
                        }
                        
                    }
                  
                }
                if (havenum && !changed)
                {
                    string newstr = GetSplitStr(int.Parse(savenum));
                    sn = sn.Substring(0, numstart) + newstr + sn.Substring(numend); 
                    changed = true;
                    havenum = false;
                }
            }
            //Debug.WriteLine(sn);
            return sn;
        }

        string GetSplitStr(int num)
        {
            int rep1 = num / 2;
            int rep2 = (num / 2);
            if (num%2==1)
            {
                rep2++;
            }

            return "[" + rep1 + "," + rep2 + "]";

        }

        string AddFirst(string str, int num)
        {
            string ret = str;
            string thisnum = "";
            int start = 0;
            for (int i = 0; i < str.Length; i++)
            {
                string c= ret.Substring(i, 1);
                if (c=="["  || c=="]" || c== ",")
                {
                    if (thisnum != "")
                    {
                        int n = int.Parse(thisnum);
                        n += num;
                        ret = ret.Substring(0, start) + n.ToString() + ret.Substring(i);
                        break;
                    }
                    start = 0;
                }
                else
                {
                    if (start==0)
                    {
                        start = i;
                    }
                    
                    thisnum += c;
                }
            }

            return ret;
        }

        string AddLast(string str, int num)
        {
            string ret = str;
            string thisnum = "";
            int end = 0;
            for (int i = str.Length -1; i > 0 ; i--)
            {
                
                string c = ret.Substring(i, 1);
                if (c == "[" || c == "]" || c == ",")
                {
                    if (thisnum != "")
                    {
                        int n = int.Parse(thisnum);
                        n += num;
                        ret = ret.Substring(0, i+1) + n.ToString() + ret.Substring(end+1);
                        break;
                    }
                }
                else
                {
                    if (end ==0)
                    {
                        end = i;
                    }
                    
                    thisnum = c + thisnum;
                }
            }

            return ret;
        }
    }
}
