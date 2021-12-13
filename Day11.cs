using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day11:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            int[,] grid = new int[10, 10];
            

            for (int y = 0; y < 10; y++)
            {
                ln = sr.ReadLine();
                for (int x =0; x < 10; x++)
                {
                    grid[x, y] = int.Parse(ln.Substring(x, 1));
                }
            }

            int flashes = 0;
            for (int t = 0; t < 100; t++)
            {
                flashes += Octo(grid);
            }

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + flashes.ToString();
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
            int[,] grid = new int[10, 10];


            for (int y = 0; y < 10; y++)
            {
                ln = sr.ReadLine();
                for (int x = 0; x < 10; x++)
                {
                    grid[x, y] = int.Parse(ln.Substring(x, 1));
                }
            }

            int flashes = 0;
            int t = 0;
            while (true)
            {
                t++;
                flashes = Octo(grid);
                if (flashes == 100)
                {
                    break;
                }
            }

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + t.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        int Octo(int[,] grid)
        {
            int flashes = 0;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    grid[x, y] += 1;
                }
            }
            bool flashed = true;
            while (flashed)
            {
                flashed = false;
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if (grid[x, y] >= 10)
                        {
                            grid[x, y] = 0;
                            flashes++;
                            flashed = true;
                            for (int y1 = Math.Max(y - 1, 0); y1 < Math.Min(y + 2, 10); y1++)
                            {
                                for (int x1 = Math.Max(x - 1, 0); x1 < Math.Min(x + 2, 10); x1++)
                                {
                                    if (!(x1 == x && y1 == y) && grid[x1, y1] != 0)
                                    {
                                        grid[x1, y1]++;
                                    }
                                }
                            }

                        }
                    }
                }
            }


            return flashes;
        }

        void DrawGrid(int[,] grid)
        {
            for (int y = 0; y < 10; y++)
            {
                string ln = "";
                for (int x = 0; x < 10; x++)
                {
                    
                    ln += grid[x, y].ToString("0");

                }
                Debug.WriteLine(ln);
            }

            Debug.WriteLine("");
        }

    }
}
