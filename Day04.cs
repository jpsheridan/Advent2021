using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public class Day04:DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;

            string[] nums = sr.ReadLine().Split(',');
            List<int[,]> boards = new List<int[,]>();
            List<List<int>> boardmap = new List<List<int>>();
            int board = 0;
            
            while ((ln = sr.ReadLine()) != null)
            {

                board++;
                int[,] thisboard = new int[5, 5];
                List<int> thismap = new List<int>();
                string[] thisline = new string[5];
            
                for (int row = 0; row < 5; row++)
                {
                    ln = sr.ReadLine();
                                        
                    for (int col = 0; col < 5; col++)
                    {
                        int thisnum = int.Parse(ln.Substring(col * 3, 2));
                        thisboard[col, row] = thisnum;
                        thismap.Add(thisnum);
                    }
                }
                boardmap.Add(thismap);
                boards.Add(thisboard);
            }

            valid = PlayBingo(boards, boardmap, nums);

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

            string[] nums = sr.ReadLine().Split(',');
            List<int[,]> boards = new List<int[,]>();
            List<List<int>> boardmap = new List<List<int>>();
            int board = 0;

            while ((ln = sr.ReadLine()) != null)
            {

                board++;
                int[,] thisboard = new int[5, 5];
                List<int> thismap = new List<int>();
                string[] thisline = new string[5];

                for (int row = 0; row < 5; row++)
                {
                    ln = sr.ReadLine();

                    for (int col = 0; col < 5; col++)
                    {
                        int thisnum = int.Parse(ln.Substring(col * 3, 2));
                        thisboard[col, row] = thisnum;
                        thismap.Add(thisnum);
                    }
                }
                boardmap.Add(thismap);
                boards.Add(thisboard);
            }

            valid = PlayBingoLose(boards, boardmap, nums);

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + valid.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int PlayBingo(List<int[,]> boards, List<List<int>> boardmap, string[] nums)
        {

            foreach (string n in nums)
            {
                int thisnum = int.Parse(n);

                for (int i = 0; i < boardmap.Count; i++)
                {
                    if (boardmap[i].Contains(thisnum))
                    {
                        int pos = boardmap[i].IndexOf(thisnum);
                        int x = (pos % 5);
                        int y = pos /5;

                        //Debug.WriteLine("Board " + i.ToString() + " - " + thisnum);
                        if (boards[i][x,y]==thisnum)
                        {
                            boards[i][x, y] = -1;
                            //DrawGrid(boards[i]);
                        }

                        int boardsum = winningboard(boards[i]);
                        if (boardsum > 0)
                        {
                            return thisnum * boardsum;
                        }
                    }
                }
            }

            return 0;
        }

        int winningboard(int[,] thisboard)
        {

            bool win = true;
            for (int x = 0; x < 5; x++)
            {
                win = true;
                for (int y = 0; y < 5; y++)
                {
                    if (thisboard[x, y] != -1)
                    {
                        win = false;
                        break;
                    }
                }
                if (win)
                {
                    break;
                }
            }

            if (!win)
            {
                win = true;
                for (int x = 0; x < 5; x++)
                {
                    win = true;
                    for (int y = 0; y < 5; y++)
                    {
                        if (thisboard[y,x] != -1)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        break;
                    }
                }
            }

            int thissum = 0;

            if (win)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (thisboard[y, x] != -1)
                        {
                            thissum += thisboard[y, x];


                        }
                    }
                }
            }
            return thissum;
        }

        void DrawGrid(int[,] thisboard)
        {
            for (int y = 0; y < 5; y++) 
            {
                string ln = "";
                for (int x = 0; x < 5; x++) 
                {
                    if (thisboard[x,y] == -1)
                    {
                        ln += " X ";
                    }
                    else
                    {
                        ln += thisboard[x, y].ToString("00") + " ";
                    }
                    
                }
                Debug.WriteLine(ln);
            }

            Debug.WriteLine("");
        }


        int PlayBingoLose(List<int[,]> boards, List<List<int>> boardmap, string[] nums)
        {

            int lastwinsum = 0;
            List<int[,]> origboards = new List<int[,]>(boards);


            foreach (string n in nums)
            {
                int thisnum = int.Parse(n);
                

                for (int i = boardmap.Count-1; i > 0; i--)
                {
                    if (boardmap[i].Contains(thisnum))
                    {
                        int pos = boardmap[i].IndexOf(thisnum);
                        int x = (pos % 5);
                        int y = pos / 5;

                        if (boards[i][x, y] == thisnum)
                        {
                            boards[i][x, y] = -1;
                        }

                        int boardsum = winningboard(boards[i]);
                        if (boardsum > 0)
                        {
                            lastwinsum = thisnum * boardsum;
                            boardmap.RemoveAt(i);
                            boards.RemoveAt(i);
                        }
                    }
                }
                
            }

            

            return lastwinsum;
        }

    }
}
