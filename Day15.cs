using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + (Distance);
        public Tile Parent { get; set; }

        //The distance is essentially the estimated distance, ignoring walls to our target. 
        //So how many tiles left and right, up and down, ignoring walls, to get there. 
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = (Math.Abs(targetX - X) + Math.Abs(targetY - Y));
        }
    }

    public class Day15:DayClass
    {
        Dictionary<string, int> riskfromhere = new Dictionary<string, int>();
        int[,] tots = new int[500, 500];
        int risk2 = 0;
        int maxrisk = 6908;
        long pc = 0;
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int valid = 0;
            List<string> r = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                r.Add(ln);
            }
            int[,] grid = ListToGrid(r);
            string visited = "";
            //int risk = GetMinPath(grid,0,0,0, 13000, visited);
            //int risk = GetMinPath2(grid, 0, 0, "");

            int stackSize = 1024 * 1024 * 32;


            int risk = 0;

            //System.Threading.Thread th = new System.Threading.Thread(() =>
            //{
            //    //risk = GetMinPath(grid, 0, 0, 0, 5000, visited);
            //    risk = GetMinPath3(grid, 0, 0,  0);
            //}, stackSize);

            //th.Start();
            //th.Join();

           // risk = WalkGrid4(grid);

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + risk.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
         //   return "";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            
            List<string> r = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                r.Add(ln);
            }
            int[,] grid = ListToGrid(r);

            int[,] megagrid = gridtomegagrid(grid);

            maxrisk = 6910;
            riskfromhere.Clear();
            int ytot = 0;

            for (int y = 0; y < megagrid.GetLength(1); y++)
            {
                ytot += megagrid[0, y];
                int xtot = ytot;
                for (int x = 0; x < megagrid.GetLength(0); x++)
                {
                    xtot += megagrid[x, y];
                    //string pos = x.ToString() + "," + y.ToString() ;
                    //riskfromhere[pos] = ytot + xtot;
                    tots[x, y] = ytot + xtot;
                }
            }
            //maxrisk = riskfromhere["499,499"];
            maxrisk = tots[499,499];

            int stackSize = 1024 * 1024 * 32;


            int risk = 0;

            //System.Threading.Thread th = new System.Threading.Thread(() =>
            //{
            //    //risk = GetMinPath(grid, 0, 0, 0, 5000, visited);
                risk = GetMinPath4(megagrid, 0, 0, 0);
            //}, stackSize);

            //th.Start();
            //th.Join();

            //risk = WalkGrid4(megagrid);

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + risk.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        int[,] ListToGrid(List<string> rows)
        {
            int maxy = rows[0].Length;
            int maxx = rows.Count;
            int[,] grid = new int[maxx, maxy];

            for (int y = 0; y < maxy; y++)
            {
                for (int x = 0; x < maxx; x++)
                {
                    grid[x, y] = int.Parse(rows[y].Substring(x, 1));
                }
            }


            return grid;
        }

        int[,] gridtomegagrid(int[,] grid)
        {
            int maxy = grid.GetLength(1);
            int maxx = grid.GetLength(0);
            int[,] newgrid = new int[maxx*5, maxy*5];


            
            for (int b = 0; b < 5; b++)
            {
                for (int a = 0; a < 5; a++)
                {
                    for (int y = 0; y < maxy; y++)
                    {
                        for (int x = 0; x < maxx; x++)
                        {
                            int newval = grid[x, y];
                            newval += a + b;
                            
                            if (newval >= 10)
                            {
                                newval -= 9;
                            }
                            newgrid[x + (a * maxx), y + (b * maxy)] = newval;
                        }
                    }
                }
            }

            return newgrid;
        }



        int GetMinPath(int[,] grid, int curx, int cury, int currisk, int maxrisk, string visited)
        {
            int maxy = grid.GetLength(1)-1;
            int maxx = grid.GetLength(0)-1;
            int newx = 0;
            int newy = 0;
            int newrisk = maxrisk;
            int thisrisk = 0;
            string pos = "";
            string newvisited = "";

            //if (visited == "|0,1||0,2||0,3||1,3||2,3||3,3||4,3||5,3||6,3|")


            if (curx > 0)
            {
                newx=curx-1;
                newy = cury;

                thisrisk = currisk + grid[newx, newy];
                if (thisrisk < maxrisk)
                {

                    pos = "|" + newx.ToString() + "," + newy.ToString() + "|";
                    if (!visited.Contains(pos))
                    {
                        newvisited = visited + pos;
                        newrisk = GetMinPath(grid, newx, newy, thisrisk, maxrisk, newvisited);
                        if (newrisk < maxrisk)
                        {
                            maxrisk = newrisk;
                        }
                    }
                }
            }
            if (curx < maxx)
            {
                newx = curx + 1;
                newy = cury;
                if (newx == maxx && newy == maxy)
                {
                    maxrisk = currisk + grid[newx, newy];
                    return maxrisk;
                }
                else
                {
                    thisrisk = currisk + grid[newx, newy];
                    if (thisrisk < maxrisk)
                    {
                        

                        pos = "|" + newx.ToString() + "," + newy.ToString() + "|";
                        if (!visited.Contains(pos))
                        {
                            newvisited = visited + pos;
                            int r = GetMinPath(grid, newx, newy, thisrisk, maxrisk, newvisited);
                            if (r < newrisk)
                            {
                                newrisk = r;
                            }
                            if (newrisk < maxrisk)
                            {
                                maxrisk = newrisk;
                            }

                        }
                     
                    }
                }
            }

            if (cury > 0)
            {
                newx = curx;
                newy = cury - 1;
                thisrisk = currisk + grid[newx, newy];
                if (thisrisk < maxrisk)
                {


                    pos = "|" + newx.ToString() + "," + newy.ToString() + "|";
                    if (!visited.Contains(pos))
                    {
                        newvisited = visited + pos;
                        int r = GetMinPath(grid, newx, newy, thisrisk, maxrisk, newvisited);
                        if (r < newrisk)
                        {
                            newrisk = r;
                        }
                        if (newrisk < maxrisk)
                        {
                            maxrisk = newrisk;
                        }
                    }
                }
                
            }
            if (cury < maxy)
            {
                newx = curx;
                newy = cury + 1;
                thisrisk = currisk + grid[newx, newy];
                if (thisrisk < maxrisk)
                {


                    if (newx == maxx && newy == maxy)
                    {
                        maxrisk = thisrisk;
                        //if (visited.StartsWith("|0,1|"))
                        //{
                        //    Debug.WriteLine(maxrisk.ToString() + " - " + visited);
                        //}

                        return thisrisk;
                    }
                    else
                    {
                        pos = "|" + newx.ToString() + "," + newy.ToString() + "|";
                        if (!visited.Contains(pos))
                        {
      
                            newvisited = visited + pos;
                            int r = GetMinPath(grid, newx, newy, thisrisk, maxrisk, newvisited);
                            if (r < newrisk)
                            {
                                newrisk = r;
                            }
                            if (newrisk < maxrisk)
                            {
                                maxrisk = newrisk;
                            }
                        }
                    }
                }
            }
            
            return Math.Min(newrisk, maxrisk);
        }



        int GetMinPath2(int[,] grid, int curx, int cury, string thispath)
        {
            int maxy = grid.GetLength(1) - 1;
            int maxx = grid.GetLength(0) - 1;
            int newx = 0;
            int newy = 0;
            int newrisk = int.MaxValue;

            string pos = "";

            pos = "|" + curx.ToString() + "," + cury.ToString() + "|";

    
            if (riskfromhere.ContainsKey(pos))
            {
                return grid[curx,cury] +  riskfromhere[pos];
            }
            if (thispath.Contains(pos))
            {
                return 999999;
            }
            if (curx==maxx && cury==maxy)
            {

                return grid[curx, cury];
            }

            string newpath = thispath+ pos;

            int r = 0;
            
            if (curx > 0)
            {
                
                newrisk =   GetMinPath2(grid, curx - 1, cury, newpath);
            }


            
            if (curx < maxx)
            {
                r =  GetMinPath2(grid, curx + 1, cury, newpath);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }
            if (cury > 0)
            {
                r = GetMinPath2(grid, curx, cury-1, newpath);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }

            if (pos == "|8,7|")
            {
                int xyz = 0;
            }

        
            if (cury < maxy)
            {
                r =  GetMinPath2(grid, curx, cury+1, newpath);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }

            if (pos == "|6,3|")
            {
                int xyz = 0;
            }

            //if (riskfromhere.ContainsKey(pos))
            //{
            //    if (newrisk < riskfromhere[pos])
            //    {
            //        riskfromhere[pos] = newrisk; 
            //    }
            //}
            //else
            //{
            //    riskfromhere[pos] = newrisk;
            //}

            riskfromhere[pos] = newrisk;


            return grid[curx,cury] + newrisk;

        }

        int GetMinPath3(int[,] grid, int curx, int cury, int currisk)
        {

            int maxy = 499; // grid.GetLength(1) - 1;
            int maxx = 499; // grid.GetLength(0) - 1;

            int newrisk = int.MaxValue;

            string pos = "";


            //pos = "|" + curx.ToString() + "," + cury.ToString() + "|";
            pos = curx.ToString() + "," + cury.ToString();

            int thisrisk = grid[curx, cury] + currisk;

            int v = 0;
            if (riskfromhere.TryGetValue(pos, out v))
            {

                if (thisrisk >= v)
                {
                    return 999999;
                }
                else
                {
                   // Debug.WriteLine("adding: " + pos);
                    riskfromhere[pos] = thisrisk;
                }
            }
            else
            {
                riskfromhere[pos] = thisrisk;
            }
            
            if (thisrisk > maxrisk || thisrisk > (((curx+cury) * 9)+9))
            {
                return maxrisk;
            }

            if (curx == maxx && cury == maxy)
            {
                maxrisk = thisrisk;
               // if (thisrisk < 3000)
                {
                    Debug.WriteLine(thisrisk.ToString());
                }
                
                return thisrisk;
            }

            //string newpath = thispath + pos;

            int r = 0;

            if (curx > 0)
            {
                newrisk = GetMinPath3(grid, curx-1, cury,  thisrisk);
            }



            if (curx < maxx)
            {
                r = GetMinPath3(grid, curx+1, cury,  thisrisk);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }
            if (cury > 0)
            {
                r = GetMinPath3(grid, curx, cury-1, thisrisk);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }

            if (cury < maxy)
            {
                r = GetMinPath3(grid, curx, cury+1, thisrisk);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }


            return  newrisk;

        }


        int WalkGrid4(int[,] grid)
        {
            var start = new Tile();
            start.Y = 0;
            start.X = 0;

            var finish = new Tile();
            finish.Y = grid.GetLength(1)-1;
            finish.X = grid.GetLength(0)-1;

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    //Console.Log(We are at the destination!);
                    //We can actually loop through the parents of each tile to find our exact path which we will show shortly. 
                    return checkTile.Cost;
                }

                visitedTiles.Add(checkTile);
                //Debug.WriteLine("x:" + checkTile.X.ToString() + ", Y: " + checkTile.Y.ToString());
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(grid, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > walkableTile.CostDistance)
                        //if (existingTile.Cost > checkTile.Cost)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            return 0;
        }


        private static List<Tile> GetWalkableTiles(int[,] grid, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = grid.GetLength(0) - 1;
            var maxY = grid.GetLength(1) - 1;


            List<Tile> retTiles = possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .ToList();

            for (int i = 0; i < retTiles.Count; i++)
            {
                retTiles[i].Cost = currentTile.Cost + grid[retTiles[i].X, retTiles[i].Y];
            }
            return retTiles;
        }

        int GetMinPath4(int[,] grid, int curx, int cury, int currisk)
        {
            pc++;
            int maxy = 499; // grid.GetLength(1) - 1;
            int maxx = 499; // grid.GetLength(0) - 1;

            int newrisk = int.MaxValue;

            string pos = "";


            //pos = "|" + curx.ToString() + "," + cury.ToString() + "|";
            pos = curx.ToString() + "," + cury.ToString();

            int thisrisk = grid[curx, cury] + currisk;

            //int v = 0;
            //if (tots[curx, cury] == 0)
            //{
                //tots[curx, cury] = thisrisk;
            //}
            //else
            //{ 
                if (thisrisk >= tots[curx,cury])
                {
                    return 999999;
                }
                else
                {
                    // Debug.WriteLine("adding: " + pos);
                    tots[curx, cury]  = thisrisk;
                }
            //}

            if (thisrisk >= maxrisk) // || thisrisk > (((curx + cury) * 9) + 9))
            {
                return maxrisk;
            }

            if (curx == maxx && cury == maxy)
            {
                maxrisk = thisrisk;
                // if (thisrisk < 3000)
                {
                    Debug.WriteLine(thisrisk.ToString() + " - " + pc.ToString()) ;
                }

                return thisrisk;
            }

            //string newpath = thispath + pos;

            int r = 0;

            if (curx > 0)
            {
                newrisk = GetMinPath4(grid, curx - 1, cury, thisrisk);
            }
            if (curx < maxx)
            {
                r = GetMinPath4(grid, curx + 1, cury, thisrisk);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }
            if (cury > 0)
            {
                r = GetMinPath4(grid, curx, cury - 1, thisrisk);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }

            if (cury < maxy)
            {
                r = GetMinPath4(grid, curx, cury + 1, thisrisk);
                if (r < newrisk)
                {
                    newrisk = r;
                }
            }


            return newrisk;

        }


    }
}
