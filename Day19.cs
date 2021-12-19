using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventCode
{
    class Day19 : DayClass
    {
        public struct coords
        {
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
            public int origx { get; set; }
            public int origy { get; set; }
            public int origz { get; set; }
            public int rotation { get; set; }

        }
        public bool gotit = false;
        public int mx = 0;
        public int my = 0;
        public int mz = 0;
        public List<coords> offsets = new List<coords>();

        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            int maxlen = 5000;
            coords[] crds = new coords[maxlen];


            List<coords[]> scans = new List<coords[]>();
            List<string> allcoords = new List<string>();

            int rows = 0;
            int zrows = 0;
            while ((ln = sr.ReadLine()) != null)
            {
                if (ln != "")
                {
                    if (ln.Contains("scanner"))
                    {
                        if (rows > 0)
                        {
                            if (zrows == 0)
                            {
                                zrows = rows;
                            }
                            scans.Add(crds);
                            rows = 0;
                            crds = new coords[maxlen];

                        }
                        
                    }
                    else
                    {
                        string[] c = ln.Split(',');
                        crds[rows].x = int.Parse(c[0]);
                        crds[rows].y = int.Parse(c[1]);
                        crds[rows].z = int.Parse(c[2]);

                        crds[rows].origx = int.Parse(c[0]);
                        crds[rows].origy = int.Parse(c[1]);
                        crds[rows].origz = int.Parse(c[2]);
                        rows++;
                    }
                }

            }

            scans.Add(crds);

            for (int i = 0; i < scans[0].Length; i++)
            {
                if (scans[0][i].x == 0 && scans[0][i].y == 0 && scans[0][i].z == 0)
                {
                    break;
                }
                string thispt = scans[0][i].x.ToString() + "," + scans[0][i].y.ToString() + "," + scans[0][i].z.ToString();
                if (!allcoords.Contains(thispt))
                {
                    allcoords.Add(thispt);
                }
            }


            bool removed = true;
            

            while (scans.Count > 1 && removed)
            {
                int zz = 0;
                removed = false;

                for (int y = scans.Count - 1; y > 0; y--)
                {
                    int comp1 = 0;
                    int comp2 = y;

                    for (int i = 0; i < 25; i++)
                    {
                        comparescansx(scans[comp1], scans[comp2]);
                        if (gotit)
                        {
                            coords offs = new coords();
                            offs.x = mx;
                            offs.y = my;
                            offs.z = mz;
                            offsets.Add(offs);

                            for (int k = 0; k < scans[y].Length; k++)
                            {
                                if (scans[y][k].x == 0 && scans[y][k].y == 0 && scans[y][k].z == 0)
                                {
                                    break;
                                }
                                scans[y][k].x += offs.x;
                                scans[y][k].y += offs.y;
                                scans[y][k].z += offs.z;

                                
                                string thispt = scans[y][k].x.ToString() + "," + scans[y][k].y.ToString() + "," + scans[y][k].z.ToString();
                                
                                if (!allcoords.Contains(thispt))
                                {
                                    allcoords.Add(thispt);
                                    coords nc = new coords();
                                    nc.x = scans[y][k].x;
                                    nc.y = scans[y][k].y;
                                    nc.z = scans[y][k].z;
                                    scans[0][zrows] = nc;
                                    zrows++;
                                }

                            }
                            removed = true;
                            scans.RemoveAt(y);
                            gotit = false;

                            break;
                        }
                        gotit = false;
                        scans[comp2] = RotateScanner(scans[comp2]);
                        if (removed)
                        {
                            break;
                        }
                    }

                }
            }


            //Dictionary<int, List<int>> paths = new Dictionary<int, List<int>>();
            //Dictionary<string, coords> offsets = new Dictionary<string, coords>();
            //for (int x = 0; x < scans.Count; x++)
            //{
            //    for (int y = 0; y < scans.Count; y++)
            //    {
            //        if (x != y)
            //        {
            //            int comp1 = x;
            //            int comp2 = y;
            //            if (x == 35 && y == 19)
            //            {
            //                int zzz = 0;
            //            }
            //            for (int i = 0; i < 25; i++)
            //            {
            //                comparescansx(scans[comp1], scans[comp2]);
            //                if (x == 35 && y == 19)
            //                {
            //                    Debug.WriteLine(i.ToString() + "," + scans[comp2][24].x.ToString() + "," + scans[comp2][24].y.ToString() + scans[comp2][24].z.ToString());
            //                }
            //                if (gotit)
            //                {
            //                    Debug.WriteLine("matched: " + comp1.ToString() + "-" + comp2.ToString());
            //                    //if (x==19 && y==35)
            //                    //{
            //                    //    for (int z = 0; z < scans[comp2].Length; z++)
            //                    //    {
            //                    //        if (scans[comp2][z].x==0)
            //                    //        {
            //                    //            break;
            //                    //        }
            //                    //        Debug.WriteLine((scans[comp2][z].x - mx).ToString() + "," + (scans[comp2][z].y - my).ToString() + " - " + (scans[comp2][z].z - mz).ToString());
            //                    //    }

            //                    //}
            //                    coords offs = new coords();
            //                    offs.x = mx;
            //                    offs.y = my;
            //                    offs.z = mz;
            //                    offsets[comp1.ToString() + "," + comp2.ToString()] = offs;

            //                    if (!paths.ContainsKey(comp1))
            //                    {
            //                        paths[comp1] = new List<int>();
            //                        paths[comp1].Add(comp2);
            //                    }
            //                    else
            //                    {
            //                        paths[comp1].Add(comp2);

            //                    }

            //                    gotit = false;

            //                    break;
            //                }
            //                gotit = false;
            //                scans[comp2] = RotateScanner(scans[comp2]);
            //            }
            //            resetscan(scans[comp2]);
            //        }
            //    }
            //}

            sw.Stop();

            sr.Close();

            string ret = "Answer : " + allcoords.Count.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        void resetscan(coords[] scans)
        {
            for (int i = 0; i < scans.Length; i++)
            {
                if (scans[i].x == 0 && scans[i].y == 0 && scans[i].z == 0)
                {
                    break;
                }
                    scans[i].x = scans[i].origx;
                scans[i].y = scans[i].origy;
                scans[i].z = scans[i].origz;
            }
            
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int maxoff = 0;
            for (int i = 0; i < offsets.Count-1; i++)
            {
                for (int j= i; j < offsets.Count; j++)
                {
                    int thisoff = Math.Abs(offsets[i].x - offsets[j].x);
                    thisoff += Math.Abs(offsets[i].y - offsets[j].y);
                    thisoff += Math.Abs(offsets[i].z - offsets[j].z);

                    if (thisoff > maxoff)
                    {
                        maxoff = thisoff;
                    }
                }
            }

            string ret = "Answer : " + maxoff.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        List<int> GetMinPath(Dictionary<int, List<int>> paths, int pnum, List<int> curpath)
        {

            List<int> newpath = new List<int>();
            List<int> temppath = new List<int>();

            foreach (int p in paths[pnum])
            {
                if (p == 0)
                {
                    newpath = new List<int>(curpath);
                    newpath.Add(0) ;

                    break;
                }
                else
                {
                    if (!curpath.Contains(p))
                    {
                        List<int> thispath = new List<int>(curpath);

                        thispath.Add(p);

                        List<int> mp = GetMinPath(paths, p, thispath);

                        
                        if (newpath.Count == 0)
                        {
                            newpath = new List<int>(mp);
                        }
                        else
                        {
                            if (mp.Count < newpath.Count && mp.Contains(0))
                            {
                                newpath = new List<int>(mp);
                            }
                        }
                    }
                }

            }
            

            return newpath;
        }

        void comparescansx(coords[] scan1, coords[] scan2)
        {
            Dictionary<int, int> diffs = new Dictionary<int, int>();
            for (int i = 0; i < scan1.Length; i++)
            {
                if (scan1[i].x==0 && scan1[i].y==0 && scan1[i].z==0)
                {
                    break;
                }
                for (int j = 0; j < scan2.Length; j++)
                {
                    if (scan2[j].x == 0 && scan2[j].y == 0 && scan2[j].z == 0)
                    {
                        break;
                    }
                    int diff = scan1[i].x - scan2[j].x;
                    
                    if (diffs.ContainsKey(diff))
                    {
                        diffs[diff]++;
                        if (diffs[diff]==12)
                        {
                            comparescansy(scan1, scan2, diff);
                        }
                    }
                    else
                    {
                        diffs[diff] = 1;
                    }
                }
            }

        }

        void comparescansy(coords[] scan1, coords[] scan2, int x)
        {
            Dictionary<int, int> diffs = new Dictionary<int, int>();
            for (int i = 0; i < scan1.Length; i++)
            {
                if (scan1[i].x == 0 && scan1[i].y == 0 && scan1[i].z == 0)
                {
                    break;
                }
                for (int j = 0; j < scan2.Length; j++)
                {
                    if (scan2[j].x == 0 && scan2[j].y == 0 && scan2[j].z == 0)
                    {
                    break;
                }
                int diffx = scan1[i].x - scan2[j].x;
                    if (diffx == x)
                    {
                        int diffy = scan1[i].y - scan2[j].y;
                        if (diffs.ContainsKey(diffy))
                        {
                            diffs[diffy]++;
                            if (diffs[diffy] == 12)
                            {
                                comparescansz(scan1, scan2, x, diffy);
                            }
                        }
                        else
                        {
                            diffs[diffy] = 1;
                        }
                    }
                }
            }

        }

        void comparescansz(coords[] scan1, coords[] scan2, int x, int y)
        {
            gotit = false;
            Dictionary<int, int> diffs = new Dictionary<int, int>();
            for (int i = 0; i < scan1.Length; i++)
            {
                if (scan1[i].x == 0 && scan1[i].y == 0 && scan1[i].z == 0)
                {
                    break;
                }
            for (int j = 0; j < scan2.Length; j++)
            {
                    if (scan2[j].x == 0 && scan2[j].y == 0 && scan2[j].z == 0)
                    {
                    break;
                }

                    int diffx = scan1[i].x - scan2[j].x;
                    int diffy = scan1[i].y - scan2[j].y;
                    if (diffx == x && diffy == y)
                    {
                        int diffz = scan1[i].z - scan2[j].z;
                        if (diffs.ContainsKey(diffz))
                        {
                            diffs[diffz]++;
                            if (diffs[diffz] == 12)
                            {
                                //Debug.WriteLine("Got one: " + x.ToString() + "," + y.ToString() + "," + diffz.ToString());
                                gotit = true;
                                mx = x;
                                my = y;
                                mz = diffz;
        
                               }
                        }
                        else
                        {
                            diffs[diffz] = 1;
                        }
                    }
                }
            }

        }


        coords[] RotateScanner(coords[] inpscan)
        {
            for (int i = 0; i < inpscan.Length;i++)
            {
                if (inpscan[i].x == 0 && inpscan[i].y==0 && inpscan[i].z==0)
                {
                    break;
                }
                inpscan[i] = Rotate(inpscan[i]); 
            }
            return inpscan;
        }
        coords Rotate(coords inp)
        {
                if (inp.rotation == 0)
                {
                    inp.origx = inp.x;
                    inp.origy = inp.y;
                    inp.origz = inp.z;

                }

                inp.rotation++;

                if (inp.rotation == 24)
                {
                    inp.rotation = 0;
                    inp.x = inp.origx;
                    inp.y = inp.origy;
                    inp.z = inp.origz;
                    return inp;
                }

                switch (inp.rotation)
                {
                    case 0:
                        inp.x = inp.origx;
                        inp.y = inp.origy;
                        inp.z = inp.origz;
                        break;
                    case 1:
                        inp.x = -1 * inp.origz;
                        inp.z = inp.origx;
                        break;
                    case 2:
                        inp.x = -1 * inp.origx;
                        inp.z = -1 * inp.origz;
                        break;
                    case 3:
                        inp.x = inp.origz;
                        inp.z = -1 * inp.origx;
                        break;

                    case 4:
                        inp.x = -1 * inp.origx;
                        inp.y = -1 * inp.origy;
                        inp.z = inp.origz;
                        break;
                    case 5:
                        inp.x = -1 * inp.origz;
                        inp.z = -1 * inp.origx;
                        break;
                    case 6:
                        inp.x = inp.origx;
                        inp.z = -1 * inp.origz;
                        break;
                    case 7:
                        inp.x = inp.origz;
                        inp.z = inp.origx;
                        break;



                    case 8:
                        inp.x = -1 * inp.origy;
                        inp.y = inp.origx;
                        inp.z = inp.origz;
                        break;

                    case 9:
                        inp.x = -1 * inp.origz;
                        inp.z = -1 * inp.origy;
                        break;
                    case 10:
                        inp.x = inp.origy;
                        inp.z = -1 * inp.origz;
                        break;
                    case 11:
                        inp.x = inp.origz;
                        inp.z = inp.origy;
                        break;



                    case 12:
                        inp.x = inp.origy;
                        inp.y = -1 * inp.origx;
                        inp.z = inp.origz;
                        break;

                    case 13:
                        inp.x = -1 * inp.origz;
                        inp.z = inp.origy;
                        break;
                    case 14:
                        inp.x = -1 * inp.origy;
                        inp.z = -1 * inp.origz;
                        break;
                    case 15:
                        inp.x = inp.origz;
                        inp.z = -1 * inp.origy;
                        break;



                    case 16:
                        inp.x = inp.origx;
                        inp.y = -1 * inp.origz;
                        inp.z = inp.origy;
                        break;

                    case 17:
                        inp.x = -1 * inp.origy;
                        inp.z = inp.origx;
                        break;
                    case 18:
                        inp.x = -1 * inp.origx;
                        inp.z = -1 * inp.origy;
                        break;
                    case 19:
                        inp.x = inp.origy;
                        inp.z = -1 * inp.origx;
                        break;



                    case 20:
                        inp.x = -1 * inp.origx;
                        inp.y = inp.origz;
                        inp.z = inp.origy;
                        break;

                    case 21:
                        inp.x = inp.origy;
                        inp.z = inp.origx;
                        break;
                    case 22:
                        inp.x = inp.origx;
                        inp.z = -1 * inp.origy;
                        break;
                    case 23:
                        inp.x = -1 * inp.origy;
                        inp.z = -1 * inp.origx;
                        break;

                }
            

            return inp;
        }

    }
}

