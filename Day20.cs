using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AdventCode
{
    class Day20:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            ln = sr.ReadLine();

            string[] alg = new string[ln.Length];

            for (int i = 0; i < ln.Length; i++)
            {
                alg[i] = ln.Substring(i, 1);
            }    

            ln = sr.ReadLine();
            string[,] grid = new string[500, 500];
            BlankGrid(grid, ".");

            int row = 100;
            int col = 100;

            while ((ln = sr.ReadLine()) != null)
            {
                for (int i = 0; i < ln.Length; i++)
                {
                    grid[col + i, row] = ln.Substring(i, 1);
                }
                row++;
            }
            sr.Close();


            //DrawGrid(grid);
            // part1 = do this 2 times.


            for (int i = 1; i < 51; i++)
            {
                if (i%2 == 0)
                {
                    grid = EnhanceImage(alg, grid, 100 - i, ".");
                }
                else
                {
                    grid = EnhanceImage(alg, grid, 100 - i, "#");
                }
            }
                
            

            
            valid = CountPixels(grid, 50, 200);
            //DrawGrid(grid);


            sw.Stop();

            

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

            while ((ln = sr.ReadLine()) != null)
            {

            }
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        string[,] EnhanceImage(string[] alg, string[,] grid, int start, string blankchar)
        {
            string[,] outg = new string[grid.GetLength(1), grid.GetLength(0)];
            BlankGrid(outg, blankchar);

            int glen = 100 + (2 * (100 - start));

            for (int y = start; y < grid.GetLength(1)- glen; y++)
            {
                for (int x = start; x < grid.GetLength(0)- glen; x++)
                {
                    string num = "";
                    for (int j = y - 1; j < y + 2; j++)
                    {
                        for (int i = x - 1; i < x + 2; i++)
                        {
                            if (grid[i,j]== "#")
                            {
                                num += "1";
                            }
                            else
                            {
                                num += "0";
                            }
                            
                        }
                    }
                    
                    if (num == "000100010")
                    {
                        int xyz = 0;
                    }
                    int bnum = Convert.ToInt32(num, 2);
                    outg[x, y] = alg[bnum];
                }

            }

            return outg;
        }
        int CountPixels(string[,] grid, int start, int len)
        {
            int c = 0;
            //grid.GetLength(1)
            for (int y = start; y < start+ len; y++)
            {
                for (int x= start; x < start + len; x++)
                {
                    if (grid[x,y]=="#")
                    {
                        c++;
                    }
                }
            }
            return c;
        }

        void BlankGrid(string[,] grid, string blankchar)
        {
            int c = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    grid[x, y] = blankchar;
                }
            }
        }

        void DrawGrid(string[,] grid)
        {
            int c = 0;
            for (int y = 95; y < 210; y++)
            {
                string thisln = "";
                for (int x = 95; x < 210; x++)
                {
                    thisln += grid[x, y];
                }
                Debug.WriteLine(thisln);
            }
        }
    }
}
