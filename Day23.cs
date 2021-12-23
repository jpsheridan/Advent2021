using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AdventCode
{

    class Day23:DayClass
    {
        int maxcost = 999999;
        Dictionary<string, int> gridchecked = new Dictionary<string, int>();
        Dictionary<int, int> gridchecked2 = new Dictionary<int, int>();

        struct podpos
        {
            public int x1;
            public int y1;
            public int x2;
            public int y2;
            public string ptype;
            public int xhome;
            public int cost;
            public int energy;
        }
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            string[,] grid = new string[13, 5];
            int y = 0;
            List<podpos> pods = new List<podpos>();
            Dictionary< string, int> cost = GetCost();
            gridchecked.Clear();
            
            while ((ln = sr.ReadLine()) != null)
            {
                for (int x = 0; x < ln.Length; x++)
                {
                    grid[x, y] = ln.Substring(x, 1);
                    if (grid[x,y] == "A" || grid[x,y] == "B" || grid[x,y] == "C"|| grid[x,y] == "D")
                    {
                        podpos pp = new podpos();
                        pp.x1 = x;
                        pp.y1 = y;
                        pp.xhome = (((int)char.Parse(grid[x, y])-64)*2)+1;
                        pp.ptype = grid[x, y];
                        pp.energy = cost[grid[x, y]];
                        pods.Add(pp);
                    }
                }
                y++;
            }
            sr.Close();

            int c = 0;
            ln = GridToLine(grid);
            valid = MovePods(grid, pods, c, ln);

            sw.Stop();

            string ret = "Answer : " + maxcost.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + "_part2.txt");

            string ln = "";
            int valid = 0;
            string[,] grid = new string[13, 7];
            int y = 0;
            List<podpos> pods = new List<podpos>();
            Dictionary<string, int> cost = GetCost();
            gridchecked.Clear();
            maxcost = 999999;

            while ((ln = sr.ReadLine()) != null)
            {
                for (int x = 0; x < ln.Length; x++)
                {
                    grid[x, y] = ln.Substring(x, 1);
                    if (grid[x, y] == "A" || grid[x, y] == "B" || grid[x, y] == "C" || grid[x, y] == "D")
                    {
                        podpos pp = new podpos();
                        pp.x1 = x;
                        pp.y1 = y;
                        pp.xhome = (((int)char.Parse(grid[x, y]) - 64) * 2) + 1;
                        pp.ptype = grid[x, y];
                        pp.energy = cost[grid[x, y]];
                        pods.Add(pp);
                    }
                }
                y++;
            }
            sr.Close();

            int c = 0;
            //valid = MovePods2(grid, pods, c);

            //int stackSize = 1024 * 1024 * 32;


            int risk = 0;

            //System.Threading.Thread th = new System.Threading.Thread(() =>
            //{
            //    //risk = GetMinPath(grid, 0, 0, 0, 5000, visited);

            //
            ln = GridToLine(grid);
            valid = MovePods2(grid, pods, c, ln);
            //}, stackSize);

            //th.Start();
            //th.Join();

            sw.Stop();

            string ret = "Answer : " + maxcost.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        Dictionary<string, int> GetCost()
        {
            Dictionary<string, int> c = new Dictionary<string, int>();

            c["A"] = 1;
            c["B"] = 10;
            c["C"] = 100;
            c["D"] = 1000;

            return c;

        }

        int MovePods(string[,] grid, List<podpos> pods, int curcost, string linemap)
        {
            if (curcost > maxcost)
            {
                return maxcost;
            }
            int thismax = maxcost;
            List<podpos> moves = FindAllMoves(grid, pods);
            if (moves.Count > 0)
            {
                foreach (podpos m in moves)
                {
                    string[,] newgrid = grid.Clone() as string[,];
                    newgrid[m.x2, m.y2] = grid[m.x1, m.y1];
                    newgrid[m.x1, m.y1] = ".";
                    int thiscost = curcost + m.cost;
                    int totcost = 0;
                    
                  
                    if (Finished(newgrid))
                    {
                        totcost = thiscost;
                        if (totcost < maxcost)
                        {
                            maxcost = totcost;
                            return maxcost;
                        }
                    }
                    else
                    {
                        //   string ln = GridToLine(newgrid);

                        StringBuilder sb = new StringBuilder(linemap);

                        int p1 = (m.y1 - 1) * 12 + m.x1 - 1;
                        int p2 = (m.y2 - 1) * 12 + m.x2 - 1; ;
                        char t = sb[p1];
                        sb[p1] = sb[p2];
                        sb[p2] = t;
                        string ln = sb.ToString();


                        if (gridchecked.ContainsKey(ln))
                        {
                            if (gridchecked[ln] > thiscost)
                            {
                                gridchecked[ln] = thiscost;
                                List<podpos> newpods = NewList(pods, m);

                                totcost = MovePods(newgrid, newpods, thiscost, ln);
                            }
                        }
                        else
                        {
                            gridchecked[ln] = thiscost;
                            List<podpos> newpods = NewList(pods, m);

                            totcost = MovePods(newgrid, newpods, thiscost, ln);
                        }
                    }


                    if (totcost > 0 && totcost < thismax)
                    {
                        thismax = totcost;
                    }
                }
            }

            return thismax;
        }

        int MovePods2(string[,] grid, List<podpos> pods, int curcost, string linemap)
        {
            if (curcost > maxcost )
            {
                return maxcost;
            }
            int thismax = maxcost;
            List<podpos> moves = FindAllMoves2(grid, pods);
            
            if (moves.Count > 0)
            {
                foreach (podpos m in moves)
                {
                    //string[,] newgrid = CopyGrid(grid);
                    string[,] newgrid = grid.Clone() as string[,];
                    newgrid[m.x2, m.y2] = grid[m.x1, m.y1];
                    newgrid[m.x1, m.y1] = ".";
                    int thiscost = curcost + m.cost;
                    int totcost = 0;
                    


                    if (Finished2(newgrid))
                    {
                        totcost = thiscost;
                        if (totcost < maxcost)
                        {

                            maxcost = totcost;
                           // Debug.WriteLine(maxcost.ToString());
                            return maxcost;
                        }
                    }
                    else
                    {

                        //string ln = GridToLine(newgrid);

                        StringBuilder sb = new StringBuilder(linemap);

                        int p1 = (m.y1 - 1) * 12 + m.x1 - 1;
                        int p2 = (m.y2 - 1) * 12 + m.x2 - 1; ;
                        char t = sb[p1];
                        sb[p1] = sb[p2];
                        sb[p2] = t;
                        string ln = sb.ToString();

                        int gridcost = 0;

                        //if (gridchecked.ContainsKey(ln))
                        if (gridchecked.TryGetValue(ln, out gridcost))
                        {
                            //if (gridchecked[ln] > thiscost)
                            if (gridcost > thiscost)
                            {
                                gridchecked[ln] = thiscost;
                                List<podpos> newpods = NewList(pods, m);

                                totcost = MovePods2(newgrid, newpods, thiscost,ln);
                            }
                        }
                        else
                        {
                            gridchecked[ln] = thiscost;
                            List<podpos> newpods = NewList(pods, m);

                            totcost = MovePods2(newgrid, newpods, thiscost, ln);
                        }

                    }


                    if (totcost > 0 && totcost < thismax)
                    {
                        thismax = totcost;
                    }
                }
            }

            return thismax;
        }

        List<podpos> NewList(List<podpos> pods, podpos p)
        {
            List<podpos> nl = new List<podpos>(pods);

            int i = pods.FindIndex(x => x.x1 == p.x1 && x.y1==p.y1);
            //Array.FindIndex(pods, x=>x.x1=1 && y.y1==2)

            podpos newp = pods[i];
            newp.x1 = p.x2;
            newp.y1 = p.y2;
            nl[i] = newp;
                        
            return nl;
        }
        

        List<podpos> NewList2(List<podpos> pods, podpos p)
        {
            List<podpos> nl = new List<podpos>();
            for (int i = 0; i < pods.Count; i++)
            {
                podpos newp = pods[i];
                if ((newp.x1 == p.x1) && (newp.y1 == p.y1))
                {
                    newp.x1 = p.x2;
                    newp.y1 = p.y2;
                }
                nl.Add(newp);
            }
            return nl;
        }

        bool Finished(string[,] grid)
        {
            bool fin = false;

            //if (grid[3,2] == "A" && grid[3,3] == "A")
            //{
            //    if (grid[5, 2] == "B" && grid[5, 3] == "B")
            //    {
            //        if (grid[7, 2] == "C" && grid[7, 3] == "C")
            //        {
            //            if (grid[9, 2] == "D" && grid[9, 3] == "D")
            //            {
            //                fin = true;
            //            }
            //        }
            //    }
            //}

            if (grid[3, 2] == "A")
            {
                if (grid[5, 2] == "B" )
                {
                    if (grid[7, 2] == "C" )
                    {
                        if (grid[9, 2] == "D" )
                        {
                            fin = true;
                        }
                    }
                }
            }

            return fin;
        }

        bool Finished2(string[,] grid)
        {
            bool fin = false;

            //if (grid[3, 2] == "A" && grid[3, 3] == "A" && grid[3, 4] == "A" && grid[3, 5] == "A")
            //{
            //    if (grid[5, 2] == "B" && grid[5, 3] == "B" && grid[5, 4] == "B" && grid[5, 5] == "B")
            //    {
            //        if (grid[7, 2] == "C" && grid[7, 3] == "C" && grid[7, 4] == "C" && grid[7, 5] == "C")
            //        {
            //            if (grid[9, 2] == "D" && grid[9, 3] == "D" && grid[9, 4] == "D" && grid[9, 5] == "D")
            //            {
            //                fin = true;
            //            }
            //        }
            //    }
            //}

            if (grid[3, 2] == "A" )
            {
                if (grid[5, 2] == "B" )
                {
                    if (grid[7, 2] == "C" )
                    {
                        if (grid[9, 2] == "D" )
                        {
                            fin = true;
                        }
                    }
                }
            }

            return fin;
        }

        List<podpos> FindAllMoves(string[,] grid, List<podpos> pods)
        {
            List<podpos> moves = new List<podpos>();

            foreach (podpos p in pods)
            {
                if (p.y1 == 1)
                {
                    if (HallClear(grid, p))
                    {
                        podpos newp = new podpos();
                        newp.x1 = p.x1;
                        newp.y1 = p.y1;
                        newp.x2 = p.xhome;
                        newp.cost = Math.Abs(newp.x2 - newp.x1) * p.energy;

                        if (grid[p.xhome, 3] == ".")
                        {
                            newp.cost += 2 * p.energy;
                            newp.y2 = 3;
                            moves.Add(newp);
                        }
                        else if (grid[p.xhome, 2] == "." && grid[p.xhome,3] == p.ptype)
                        {
                            newp.cost += p.energy;
                            newp.y2 = 2;
                            moves.Add(newp);
                        }
                        
                    }
                }
                else
                {
                    if (ClearToLeave(grid, p))
                    {
                            for (int x = p.x1+1; x < 13; x++)
                            {
                                if (grid[x,1]== ".")
                                {
                                    podpos newp = new podpos();
                                    newp.x1 = p.x1;
                                    newp.y1 = p.y1;
                                    newp.cost = p.energy * (p.y1 - 1);
                                    newp.cost += p.energy * Math.Abs(x - newp.x1);

                                    if (x==p.xhome)
                                    {
                                        newp.x2 = p.xhome;
                                        if (grid[p.xhome, 3] == ".")
                                        {
                                            newp.cost += 2 * p.energy;
                                            newp.y2 = 3;
                                            moves.Add(newp);
                                        }
                                        else if (grid[p.xhome, 2] == "." && grid[p.xhome, 3] == p.ptype)
                                        {
                                            newp.cost += p.energy;
                                            newp.y2 = 2;
                                            moves.Add(newp);
                                        }

                                    }
                                    else if (x!=3 && x!=5 && x!=7 && x!=9)
                                    {
                                        //newp.cost += p.energy * Math.Abs(x - p.x1);
                                        newp.x2 = x;
                                        newp.y2 = 1;
                                        moves.Add(newp);
                                    }
                                    
                                    
                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int x = p.x1 - 1; x >= 1; x--)
                            {
                                if (grid[x, 1] == ".")
                                {
                                    podpos newp = new podpos();
                                    newp.x1 = p.x1;
                                    newp.y1 = p.y1;
                                    newp.cost = p.energy * (p.y1 - 1);
                                    if (x == p.xhome)
                                    {

                                        newp.x2 = p.xhome;
                                        if (grid[p.xhome, 3] == ".")
                                        {
                                            newp.cost += 2 * p.energy;
                                            newp.y2 = 3;
                                            moves.Add(newp);
                                        }
                                        else if (grid[p.xhome, 2] == "." && grid[p.xhome, 3] == p.ptype)
                                        {
                                            newp.cost += p.energy;
                                            newp.y2 = 2;
                                            moves.Add(newp);
                                        }

                                    }
                                    else if (x != 3 && x != 5 && x != 7 && x != 9)
                                    {
                                        newp.cost += p.energy * Math.Abs(x - p.x1);
                                        newp.x2 = x;
                                        newp.y2 = 1;
                                        moves.Add(newp);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                    }
                    
                }
                

            }
            

            return moves;
        }

        bool HallClear(string[,] grid, podpos p)
        {
            bool ok = false;
            int x1 = 0;
            int x2 = 0;

            if (grid[p.xhome, 2] == ".")   // || grid[p.xhome, 2] == p.ptype
            {
                if (grid[p.xhome, 3] == "." || grid[p.xhome, 3] == p.ptype)
                {
                    ok = true;
                }
            }
            if (ok)
            {
                if (p.x1 > p.xhome)
                {
                    x1 = p.xhome;
                    x2 = p.x1-1;
                }
                else
                {
                    x1 = p.x1+1;
                    x2 = p.xhome;
                }
            
                for (int i = x1; i <= x2; i++)
                {
                    if (grid[i, 1] != ".")
                    {
                        ok = false;
                        break;
                    }
                }
            }


            return ok;
        }


        int HallClear2(string[,] grid, podpos p)
        {
            bool ok = false;
            int x1 = 0;
            int x2 = 0;
            if (p.x1 > p.xhome)
            {
                x1 = p.xhome;
                x2 = p.x1 - 1;
            }
            else
            {
                x1 = p.x1 + 1;
                x2 = p.xhome;
            }
            int pos = 0;
            if (grid[p.xhome, 5] == "." || grid[p.xhome, 5] == p.ptype)
            {
                if (grid[p.xhome, 4] == "." || grid[p.xhome, 4] == p.ptype)
                {
                    if (grid[p.xhome, 3] == "." || grid[p.xhome, 3] == p.ptype)
                    {
                        if (grid[p.xhome, 2] == ".")
                        {
                            ok = true;
                        }
                    }
                }
            }

            if (ok)
            {
                ok = false;
                for (int y=5; y>=2; y--)
                {
                    if (grid[p.xhome, y] == ".")
                    {
                        pos = y;
                        break;
                    }
                    
                }
                for (int i = x1; i <= x2; i++)
                {
                    if (grid[i, 1] != ".")
                    {
                        pos = 0;
                        break;
                    }
                }
            }


            return pos;
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

        }

        string GridToLine(string[,] grid)
        {
            string ln = "";
            for (int y = 1; y < grid.GetLength(1); y++)
            {
                for (int x = 1; x < grid.GetLength(0); x++)
                {
                    ln += grid[x, y];
                }
            }
            return ln;
        }


        string ListToKey(List<podpos> poslist)
        {
            string key = "";
            foreach (podpos p in poslist)
            {
                key += p.x1.ToString() + "," + p.y1.ToString() + "|";
            }
            
            return key;
        }

        List<podpos> FindAllMoves2(string[,] grid, List<podpos> pods)
        {
            List<podpos> moves = new List<podpos>();

            foreach (podpos p in pods)
            {
                if (p.y1 == 1)
                {
                    int ypos = HallClear2(grid, p);

                    if (ypos > 1)
                    {
                        podpos newp = new podpos();
                        newp.x1 = p.x1;
                        newp.y1 = p.y1;
                        newp.x2 = p.xhome;
                        newp.y2 = ypos;
                        newp.cost = (ypos - 1) * p.energy;
                        newp.cost += Math.Abs(newp.x2 - newp.x1) * p.energy;
                        moves.Add(newp);
                    }
      
                }
                else
                {
                    if (ClearToLeave2(grid, p))
                    {
                            for (int x = p.x1 + 1; x < 13; x++)
                            {
                                if (grid[x, 1] == ".")
                                {
                                    podpos newp = new podpos();
                                    newp.x1 = p.x1;
                                    newp.y1 = p.y1;
                                    newp.cost = p.energy * (p.y1 - 1);
                                    newp.cost += p.energy * Math.Abs(x - newp.x1);

                                    if (x == p.xhome)
                                    {
                                        newp.x2 = p.xhome;
                                        int hp = Homepos(grid, p);
                                        if (hp > 0)
                                        {
                                            newp.cost += (hp-1) * p.energy;
                                            newp.y2 = hp;
                                            moves.Add(newp);
                                        }
                                        
                                    }
                                    else if (x != 3 && x != 5 && x != 7 && x != 9)
                                    {
                                        //newp.cost += p.energy * Math.Abs(x - p.x1);
                                        newp.x2 = x;
                                        newp.y2 = 1;
                                        moves.Add(newp);
                                    }


                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int x = p.x1 - 1; x >= 1; x--)
                            {
                                if (grid[x, 1] == ".")
                                {
                                    podpos newp = new podpos();
                                    newp.x1 = p.x1;
                                    newp.y1 = p.y1;
                                    newp.cost = p.energy * (p.y1 - 1);
                                    newp.cost += p.energy * Math.Abs(x - newp.x1);

                                    if (x == p.xhome)
                                    {
                                        newp.x2 = p.xhome;
                                        int hp = Homepos(grid, p);
                                        if (hp > 0)
                                        {
                                            newp.cost += (hp - 1) * p.energy;
                                            newp.y2 = hp;
                                            moves.Add(newp);
                                        }

                                    }
                                    else if (x != 3 && x != 5 && x != 7 && x != 9)
                                    {
                                        //newp.cost += p.energy * Math.Abs(x - p.x1);
                                        newp.x2 = x;
                                        newp.y2 = 1;
                                        moves.Add(newp);
                                    }


                                }
                                else
                                {
                                    break;
                                }
                            }
                    }
                   
                }


            }


            return moves;
        }

        int Homepos(string[,] grid, podpos p)
        {
            bool ok = false;
            int pos = 0;
            for (int y = 5; y >= 2; y--)
            {
                if (grid[p.xhome, y] == ".")
                {
                    pos = y;
                }
                else if (grid[p.xhome,y] != p.ptype)
                {
                    break;
                }
            }

            return pos;

        }

        bool ClearToLeave2(string[,] grid, podpos p)
        {
            bool ok = true;
            if (p.x1 == p.xhome)
            {
                ok = false;
                // only leave if there's something different lower down
                for (int j = p.y1+1; j < 6; j++)
                {
                    if (grid[p.x1, j] != p.ptype)
                    {
                        ok = true;
                    }
                }
            }
            
            if (ok)
            {
                ok = false;

                for (int j = 2; j < 6; j++)
                {
                    if (p.y1 == j)
                    {
                        ok = true;
                    }
                    if (grid[p.x1, j] != ".")
                    {
                        break;
                    }
                }
            }
            

            return ok;
            
        }

        bool ClearToLeave(string[,] grid, podpos p)
        {
            bool ok = true;
            if (p.x1 == p.xhome)
            {
                ok = false;
                // only leave if there's something different lower down
                if (p.y1 == 2)
                { 
                    if (grid[p.x1, 3] != p.ptype)
                    {
                        ok = true;
                    }
                }
            }

            if (ok)
            {
                ok = false;

                for (int j = 2; j < 4; j++)
                {
                    if (p.y1 == j)
                    {
                        ok = true;
                    }
                    if (grid[p.x1, j] != ".")
                    {
                        break;
                    }
                }
            }


            return ok;

        }


    }
}
