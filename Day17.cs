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
    public class Day17:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            //target area: x = 192..251, y = -89..- 59
            //target area: x=20..30, y=-10..-5

            //int xmin = 20;
            //int xmax = 30;
            //int ymin = -10;
            //int ymax = -5;

            int xmin = 192;
            int xmax = 251;
            int ymin = -89;
            int ymax = -59;

            valid = FireIt(xmin, xmax, ymin, ymax);
            
            sw.Stop();

            //sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            //while ((ln = sr.ReadLine()) != null)
            {

            }
            sw.Stop();

            //r.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        int FireIt(int minx, int maxx, int miny, int maxy)
        {
            int maxh = 0;
            int hit = 0;
            for (int x = 1; x < maxx+1; x++)
            {
                for (int y = miny-1; y < 500; y++)
                {
                    int x1 = x;
                    int y1 = y;
                    int xpos = 0;
                    int ypos = 0;
                    int thismax = 0;
                    
                    while (xpos <= maxx && ypos >= miny)
                    {
                        xpos += x1;
                        ypos += y1;
                        if (ypos > thismax)
                        {
                            thismax = ypos;
                        }
                        if (x1 > 0)
                        {
                            x1--;
                        }
                        
                        y1--;

                        if (x == 6 && y == 0)
                        {
                            Debug.WriteLine(xpos.ToString() + " - " + ypos.ToString());
                        }

                        if (xpos >= minx && xpos <= maxx && ypos >= miny && ypos <= maxy)
                        {
                        
                            hit++;
                            
                            //if (thismax > maxh)
                            //{
                            //    maxh = thismax;
                               // Debug.WriteLine(x.ToString() + "- " + y.ToString() + " - " + maxh.ToString());
                            //}
                            break;
                        }
                    }
                }
            }


            return hit;
        }

    }
}
