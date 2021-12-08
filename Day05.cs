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
    public class Day05 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            int[,] grid = new int[1000, 1000];

            while ((ln = sr.ReadLine()) != null)
            {
                ln = ln.Replace(" -> ", ",");
                string[] c = ln.Split(',');


                if (c[0] == c[2] || c[1] == c[3])
                {
                    //Debug.WriteLine(ln);
                    grid = DrawLine(grid, c);
                }
            }
            sw.Stop();


            sr.Close();
            //DrawGrid(grid);
            valid = FindSpots(grid);

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
            int[,] grid = new int[1000, 1000];

            while ((ln = sr.ReadLine()) != null)
            {
                ln = ln.Replace(" -> ", ",");
                string[] c = ln.Split(',');


                //if (c[0] == c[2] || c[1] == c[3])
                //{
                grid = DrawLine(grid, c);
                //Debug.WriteLine(ln);
                //DrawGrid(grid);
                //}
            }
            sw.Stop();


            sr.Close();
            DrawGrid(grid);
            valid = FindSpots(grid);

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int[,] DrawLine(int[,] grid, string[] coords)
        {
            
            if (coords[0]== coords[2])
            {
                int min = Math.Min(int.Parse(coords[1]), int.Parse(coords[3]));
                int max = Math.Max(int.Parse(coords[1]), int.Parse(coords[3]));
                int x = int.Parse(coords[0]);
                for (int y = min; y <= max; y++)
                {
                    grid[x, y]++;
                }
            }
            else if(coords[1] == coords[3])
            {

                int min = Math.Min(int.Parse(coords[0]), int.Parse(coords[2]));
                int max = Math.Max(int.Parse(coords[0]), int.Parse(coords[2]));
                int y = int.Parse(coords[1]);
                for (int x = min; x <= max; x++)
                {

                    grid[x, y]++;
                }
            }
            else
            {
                int startx = int.Parse(coords[0]);
                int starty = int.Parse(coords[1]);
                int endx = int.Parse(coords[2]);
                int endy = int.Parse(coords[3]);

                
                
                if (startx < endx)
                {
                    int y = starty;

                    for (int x = startx; x <= endx; x++)
                    {

                        
                        grid[x, y]++;
                        if (starty < endy)
                        {
                            y++;

                        }
                        else
                        {
                            y--;
                        }
                    }
                }
                else
                {
                    int y = endy;
                    for (int x = endx; x <= startx; x++)
                    {
                        grid[x, y]++;
                        if (endy < starty)
                        {
                            y++;

                        }
                        else
                        {
                            y--;
                        }
                    }
                }

                
            }
            return grid;
        }

        int FindSpots(int[,] grid)
        {
            int c = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (grid[x,y] > 1)
                    {
                        c++;
                    }
                }
            }
            return c;
        }

        void DrawGrid(int[,] thisboard)
        {
            for (int y = 0; y < 10; y++)
            {
                string ln = "";
                for (int x = 0; x < 10; x++)
                {
                    ln += thisboard[x, y].ToString("0") ;
                    
                }
                Debug.WriteLine(ln);
            }

            Debug.WriteLine("");
        }

    }
}
