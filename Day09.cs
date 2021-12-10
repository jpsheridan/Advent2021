using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day09:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            //100,100 for main
            //10,5 for sample
            int maxx = 100;
            int maxy = 100;

            int[,] grid = new int[maxx, maxy];
            int y = 0;
            while ((ln = sr.ReadLine()) != null)
            {
                for (int x = 0; x < ln.Length; x++)
                {
                    grid[x, y] = int.Parse(ln.Substring(x, 1));
                }
                y++;
            }

            int ans = SumLows(grid);
            sw.Stop();

            sr.Close();

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
            //100,100 for main
            //10,5 for sample
            int maxx = 100;
            int maxy = 100;

            int[,] grid = new int[maxx, maxy];
            int y = 0;
            while ((ln = sr.ReadLine()) != null)
            {
                for (int x = 0; x < ln.Length; x++)
                {
                    grid[x, y] = int.Parse(ln.Substring(x, 1));
                }
                y++;
            }

            int ans = FindBasins(grid);
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + ans.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int SumLows(int[,] grid)
        {
            int maxx = grid.GetLength(0);
            int maxy = grid.GetLength(1);
            int sumlows = 0;
            for (int x = 0; x < maxx; x++)
            {
                for (int y = 0; y < maxy; y++)
                {
                    bool islow = true;
                    if (y > 0)
                    {
                        if(grid[x,y] >= grid[x,y-1])
                        {
                            islow = false;
                        }
                    }
                    if (y < maxy-1)
                    {
                        if (grid[x, y] >= grid[x, y + 1])
                        {
                            islow = false;
                        }
                    }
                    if (x > 0)
                    {
                        if (grid[x, y] >= grid[x-1, y])
                        {
                            islow = false;
                        }
                    }
                    if (x < maxx - 1)
                    {
                        if (grid[x, y] >= grid[x+1, y])
                        {
                            islow = false;
                        }
                    }

                    if (islow)
                    {
                        sumlows += grid[x, y]+1;
                    }
                }
                
            }
            return sumlows;
        }

        int FindBasins(int[,] grid)
        {
            int maxx = grid.GetLength(0);
            int maxy = grid.GetLength(1);
            int sumlows = 0;
            List<int> basinsize = new List<int>();

            for (int x = 0; x < maxx; x++)
            {
                for (int y = 0; y < maxy; y++)
                {
                    bool islow = true;
                    if (y > 0)
                    {
                        if (grid[x, y] >= grid[x, y - 1])
                        {
                            islow = false;
                        }
                    }
                    if (y < maxy - 1)
                    {
                        if (grid[x, y] >= grid[x, y + 1])
                        {
                            islow = false;
                        }
                    }
                    if (x > 0)
                    {
                        if (grid[x, y] >= grid[x - 1, y])
                        {
                            islow = false;
                        }
                    }
                    if (x < maxx - 1)
                    {
                        if (grid[x, y] >= grid[x + 1, y])
                        {
                            islow = false;
                        }
                    }

                    if (islow)
                    {
                        int bs = GetBasinSize(grid, x, y);
                        basinsize.Add(bs);
                    }
                }

            }
            basinsize.Sort();
            int l = basinsize.Count - 1;
            int multbasins = basinsize[l] * basinsize[l - 1] * basinsize[l - 2];
            return multbasins;
        }

        int GetBasinSize(int[,] grid, int startx, int starty)
        {
            int maxx = grid.GetLength(0);
            int maxy = grid.GetLength(1);
            int[,] grid2 = grid.Clone() as int[,];
            int b = 0;
            bool marked = false;

            for (int x = startx + 1; x < maxx; x++)
            {
                if ((grid[x, starty] != 9) && (grid[x, starty] > grid[x - 1, starty]))
                {
                    grid2[x, starty] = -1;
                }
                else
                {
                    break;
                }
            }

            for (int x = startx - 1; x >= 0; x--)
            {
                if ((grid[x, starty] != 9) && (grid[x, starty] > grid[x + 1, starty]))
                {
                    grid2[x, starty] = -1;
                }
                else
                {
                    break;
                }
            }

            for (int y = starty + 1; y < maxy; y++)
            {
                if ((grid[startx, y] != 9) && (grid[startx, y] > grid[startx, y - 1]))
                {
                    grid2[startx, y] = -1;
                }
                else
                {
                    break;
                }
            }

            for (int y = starty - 1; y >= 0; y--)
            {
                if ((grid[startx, y] != 9) && (grid[startx, y] > grid[startx, y + 1]))
                {
                    grid2[startx, y] = -1;
                }
                else
                {
                    break;
                }
            }

            marked = true;
            while (marked)
            {
                marked = false;
                for (int x = 0; x < maxx; x++)
                {
                    for (int y = 0; y < maxy; y++)
                    {
                        if (grid2[x, y] == -1)
                        {
                            bool ret = markgrid2(grid, grid2, x, y, maxx, maxy);
                            if (ret)
                            {
                                marked = true;
                            }
                        }
                    }
                }
            }

            b = 1;
            for (int x = 0; x < maxx; x++)
            {
                for (int y = 0; y < maxy; y++)
                {
                    if (grid2[x, y] == -1)
                    {
                        b++;
                    }
                }
            }



            return b;

        }

        bool markgrid2(int[,] grid, int[,] grid2,  int startx, int starty, int maxx, int maxy)
        {

            bool marked = false;
            for (int x = startx + 1; x < maxx; x++)
            {
                if ((grid[x, starty] != 9) && (grid[x, starty] > grid[x - 1, starty]))
                {
                    if (grid2[x,starty]!=-1)
                    {
                        grid2[x, starty] = -1;
                        marked = true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int x = startx - 1; x >= 0; x--)
            {
                if ((grid[x, starty] != 9) && (grid[x, starty] > grid[x + 1, starty]))
                {
                    if (grid2[x, starty] != -1)
                    {
                        grid2[x, starty] = -1;
                        marked = true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int y = starty + 1; y < maxy; y++)
            {
                if ((grid[startx, y] != 9) && (grid[startx, y] > grid[startx, y - 1]))
                {
                    if (grid2[startx, y] != -1)
                    {
                        grid2[startx, y] = -1;
                        marked = true;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int y = starty - 1; y > 0; y--)
            {
                if ((grid[startx, y] != 9) && (grid[startx, y] > grid[startx, y + 1]))
                {
                    if (grid2[startx, y] != -1)
                    {
                        grid2[startx, y] = -1;
                        marked = true;
                    }
                }
                else
                {
                    break;
                }
            }
            return marked;
        }

        void DrawGrid(int[,] thisboard)
        {
            for (int y = 0; y < 5; y++)
            {
                string ln = "";
                for (int x = 0; x < 10; x++)
                {
                    if (thisboard[x, y] == -1)
                    {
                        ln += "X";
                    }
                    else
                    {
                        ln += thisboard[x, y].ToString("0");
                    }

                }
                Debug.WriteLine(ln);
            }

            Debug.WriteLine("");
        }
    }
}
