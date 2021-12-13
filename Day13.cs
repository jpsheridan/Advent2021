using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day13 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            string[,] gridtemp = new string[1500, 1500];
            int maxx = 0;
            int maxy = 0;
            List<string> folds = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                if (ln == "")
                {

                }
                else if (ln.StartsWith("fold"))
                {
                    string[] f = ln.Split(' ');
                    folds.Add(f[2]);
                }
                else
                {
                    string[] pt = ln.Split(',');
                    int x1 = int.Parse(pt[0]);
                    int y1 = int.Parse(pt[1]);
                    gridtemp[x1, y1] = "#";
                    if (x1 > maxx)
                    {
                        maxx = x1;
                    }
                    if (y1 > maxy)
                    {
                        maxy = y1;
                    }
                }
            }
            string[,] grid = new string[maxx + 1, maxy + 1];
            for (int y = 0; y <= maxy; y++)
            {
                for (int x = 0; x <= maxx; x++)
                {
                    if (gridtemp[x,y] == "#")
                    {
                        grid[x, y] = gridtemp[x, y];
                    }
                    else
                    {
                        grid[x, y] = ".";
                    }

                    
                }
            }

            grid = FoldGrid(grid, folds[0]);

            valid = CountDots(grid);

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
            string[,] gridtemp = new string[1500, 1500];
            int maxx = 0;
            int maxy = 0;
            List<string> folds = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                if (ln == "")
                {

                }
                else if (ln.StartsWith("fold"))
                {
                    string[] f = ln.Split(' ');
                    folds.Add(f[2]);
                }
                else
                {
                    string[] pt = ln.Split(',');
                    int x1 = int.Parse(pt[0]);
                    int y1 = int.Parse(pt[1]);
                    gridtemp[x1, y1] = "#";
                    if (x1 > maxx)
                    {
                        maxx = x1;
                    }
                    if (y1 > maxy)
                    {
                        maxy = y1;
                    }
                }
            }
            string[,] grid = new string[maxx + 1, maxy + 1];
            for (int y = 0; y <= maxy; y++)
            {
                for (int x = 0; x <= maxx; x++)
                {
                    if (gridtemp[x, y] == "#")
                    {
                        grid[x, y] = gridtemp[x, y];
                    }
                    else
                    {
                        grid[x, y] = ".";
                    }


                }
            }

            foreach (string f in folds)
            {
                grid = FoldGrid(grid, f);
            }
            DrawGrid(grid);

            //valid = CountDots(grid);

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int CountDots(string[,] grid)
        {
            int dots = 0;

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == "#")
                    {
                        dots++;
                    }
                }
            }


            return dots;
        }

        string[,] FoldGrid(string[,] grid, string folds)
        {
            int maxx = grid.GetLength(0);
            int maxy = grid.GetLength(1);
            string[,] newgrid;

            string[] f = folds.Split('=');
            
            if (f[0] == "y")
            {

                int newmaxy = int.Parse(f[1]);

                newgrid = new string[maxx, newmaxy];
                

                for (int y = 0; y < newmaxy; y++)
                {
                    for (int x = 0; x < maxx; x++)
                    {
                        newgrid[x, y] = grid[x, y];
                    }
                }

                int y1 = newmaxy - 1;
                for (int y = newmaxy + 1; y < maxy; y++)
                {
                    for (int x = 0; x < maxx; x++)
                    {
                        if (grid[x,y]=="#")
                        {
                            newgrid[x, y1] = grid[x, y];
                        }
                        
                    }
                    y1--;
                }

            }
            else
            {
                int newmaxx =int.Parse(f[1]);


                newgrid = new string[newmaxx , maxy];


                for (int y = 0; y < maxy; y++)
                {
                    for (int x = 0; x < newmaxx; x++)
                    {
                        newgrid[x, y] = grid[x, y];
                    }
                }

                
                for (int y = 0; y < maxy; y++)
                {
                    int x1 = newmaxx - 1;
                    for (int x = newmaxx+1; x < maxx; x++)
                    {
                        if (grid[x, y] == "#")
                        {
                            newgrid[x1, y] = grid[x, y];
                        }
                        x1--;
                    }
                    
                }
            }

            return newgrid;
        }


        void DrawGrid(string[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string ln = "";
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    ln += grid[x, y];
                }
                Debug.WriteLine(ln);
            }

            Debug.WriteLine("");
        }
    }
}
