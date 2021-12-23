using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
namespace AdventCode
{
    class Day22 : DayClass
    {
        //List<string> rounds = new List<string>();
        struct cuboid
        {
            public bool onoff;
            public long x1;
            public long x2;
            public long y1;
            public long y2;
            public long z1;
            public long z2;

        }


        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            long valid = 0;
            List<string> inst = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                inst.Add(ln);
            }

            bool[,,] cube = ProcessList(inst);
            valid = CountCubes(cube);
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
            
            List<string> inst = new List<string>();
            while ((ln = sr.ReadLine()) != null)
            {
                inst.Add(ln);
            }

            long s = 0;
            s = ProcessList2(inst);
            //s=ProcessList3(inst);
            //valid = CountCubes(cube);
            sw.Stop();

            sr.Close();

            string ret = "Answer : " + s.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;


        }

        bool[,,] ProcessList(List<string> inst)
        {
            bool[,,] cube = new bool[101, 101, 101];
            
            foreach (string i in inst)
            {
                int p1 = i.IndexOf("x=");
                int p2 = i.IndexOf("y=");
                int p3 = i.IndexOf("z=");

                string o = i.Substring(0, p1).Trim();

                string xs = i.Substring(p1 + 2, p2 - p1 - 3);
                string ys = i.Substring(p2 + 2, p3 - p2 - 3);
                string zs = i.Substring(p3 + 2);

                long x1 = 0, x2 = 0, y1 = 0, y2 = 0, z1 = 0, z2 = 0;
                
                GetNums(xs, ref x1, ref x2);
                GetNums(ys, ref y1, ref y2);
                GetNums(zs, ref z1, ref z2);

                    
                bool onoff = false;
                
                if (o=="on")
                {
                    onoff = true;
                }
                for (long z = z1; z <= z2; z++)
                {
                    for (long y = y1; y <= y2; y++)
                    {
                        for (long x = x1; x <= x2; x++)
                        {
                            cube[x, y, z] = onoff;
                        }
                    }
                }

            }
            return cube;
        }

        long  ProcessList2(List<string> inst)
        {
            //bool[,,] cube = new bool[101, 101, 101];
            
            long sumcubes = 0;
            List<cuboid> cubes = new List<cuboid>();
            
            foreach (string i in inst)
            {
                int p1 = i.IndexOf("x=");
                int p2 = i.IndexOf("y=");
                int p3 = i.IndexOf("z=");

                string o = i.Substring(0, p1).Trim();

                string xs = i.Substring(p1 + 2, p2 - p1 - 3);
                string ys = i.Substring(p2 + 2, p3 - p2 - 3);
                string zs = i.Substring(p3 + 2);

                long x1 = 0, x2 = 0, y1 = 0, y2 = 0, z1 = 0, z2 = 0;
                
                GetNums2(xs, ref x1, ref x2);
                GetNums2(ys, ref y1, ref y2);
                GetNums2(zs, ref z1, ref z2);

                bool onoff = false;

                if (o == "on")
                {
                    onoff = true;
                }
                cuboid thiscube = new cuboid();
                thiscube.onoff = onoff;
                thiscube.x1 = x1;
                thiscube.x2 = x2;
                thiscube.y1 = y1;
                thiscube.y2 = y2;
                thiscube.z1 = z1;
                thiscube.z2 = z2;

                cubes.Add(thiscube);
            }

            List<cuboid> cubesproc = new List<cuboid>();
            cubesproc.Add(cubes[0]);
            for (int i = 1; i < cubes.Count; i++)
            {
                cuboid thiscube = cubes[i];
                if (thiscube.x2 > 50000)
                {
                    int abc = 0;
                }
                List<cuboid> newcubes = new List<cuboid>();
                //bool haveintersect = false;
                foreach(cuboid c in cubesproc)
                {
                    List<cuboid> intersect = Findintersect(c, thiscube);

                    
                    //newcubes.Add(c);
                  
                    newcubes.AddRange(intersect);
                    
                }

                if (thiscube.onoff)
                {
                    newcubes.Add(thiscube);
                }

                //newcubes.Add(thiscube);
                cubesproc = new List<cuboid>(newcubes);
                
            }


            foreach (cuboid c in cubesproc)
            {
                long v = ((c.x2-c.x1)+1) * ((c.y2-c.y1)+1) * ((c.z2-c.z1)+1);
                

                //if (c.onoff)
                //{
                    sumcubes += v;
                    //Debug.WriteLine(v.ToString() + " PLUS " + sumcubes.ToString());
                //}
                //else
                //{
                  //  sumcubes -= v;
                    //Debug.WriteLine(v.ToString() + " MINUS " + sumcubes.ToString());
                //}
                
            }

            return sumcubes;
        }

        List<cuboid> Findintersect(cuboid c1, cuboid c2)
        {
            
            List<cuboid> cl = new List<cuboid>();

            cuboid intersect = new cuboid();

            intersect.x1 = Math.Max(c1.x1, c2.x1);
            intersect.x2 = Math.Min(c1.x2, c2.x2);
            intersect.y1 = Math.Max(c1.y1, c2.y1);
            intersect.y2 = Math.Min(c1.y2, c2.y2);
            intersect.z1 = Math.Max(c1.z1, c2.z1);
            intersect.z2 = Math.Min(c1.z2, c2.z2);
            intersect.onoff = c1.onoff;

            if (ValidCube(intersect))
            {
                if (c1.onoff)
                {
                    if (intersect.x1 > c1.x1)
                    {
                        cuboid c = GetCuboid(c1.onoff, c1.x1, intersect.x1 - 1, c1.y1, c1.y2, c1.z1, c1.z2);
                        cl.Add(c);
                    }
                    if (intersect.x2 < c1.x2)
                    {
                        cuboid c = GetCuboid(c1.onoff, intersect.x2 + 1, c1.x2, c1.y1, c1.y2, c1.z1, c1.z2);
                        cl.Add(c);
                    }
                    if (intersect.y1 > c1.y1)
                    {
                        cuboid c = GetCuboid(c1.onoff, intersect.x1, intersect.x2, c1.y1, intersect.y1 - 1, c1.z1, c1.z2);
                        cl.Add(c);
                    }
                    if (intersect.y2 < c1.y2)
                    {
                        cuboid c = GetCuboid(c1.onoff, intersect.x1, intersect.x2, intersect.y2 + 1, c1.y2, c1.z1, c1.z2);
                        cl.Add(c);
                    }
                    if (intersect.z1 > c1.z1)
                    {
                        cuboid c = GetCuboid(c1.onoff, intersect.x1, intersect.x2, intersect.y1, intersect.y2, c1.z1, intersect.z1 - 1);
                        cl.Add(c);
                    }
                    if (intersect.z2 < c1.z2)
                    {
                        cuboid c = GetCuboid(c1.onoff, intersect.x1, intersect.x2, intersect.y1, intersect.y2, intersect.z2 + 1, c1.z2);
                        cl.Add(c);
                    }
                }
            }
            else
            {
                cl.Add(c1);
            }

            

            

            return cl;
        }

        cuboid GetCuboid(bool onoff, long x1, long x2, long y1, long y2, long z1, long z2)
        {
            cuboid c = new cuboid();
            c.onoff = onoff;
            c.x1 = x1;
            c.x2 = x2;
            c.y1 = y1;
            c.y2 = y2;
            c.z1 = z1;
            c.z2 = z2;

            return c;

        }

        bool ValidCube(cuboid cube)
        {
            if (cube.x1 <= cube.x2 && cube.y1 <= cube.y2 && cube.z1 <= cube.z2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void GetNums(string nums, ref long n1, ref long n2)
        {

            int p1 = nums.IndexOf(".");

            n1 = long.Parse(nums.Substring(0, p1));
            n2 = long.Parse(nums.Substring(p1 + 2));

            
            if (n1 < -50 )
            {
                if (n2 > -50)
                {
                    n1 = -50;
                }
                else
                {
                    n1 = 51;
                    n2 = 50;
                }
            }
            
            if (n1 > 50 )
            {
                n2 = 51;
                n1 = 52;
            }

            if (n2 < -50)
            {
                n1 = n2;
            }
            if (n2 > 50 && n1 <= 50)
            {
                n2 = 50;
            }
            n1 += 50;
            n2 += 50;
        }

        void GetNums2(string nums, ref long n1, ref long n2)
        {

            int p1 = nums.IndexOf(".");

            n1 = long.Parse(nums.Substring(0, p1));
            n2 = long.Parse(nums.Substring(p1 + 2));

        }


        long CountCubes(bool[,,] cube)
        {
            long c = 0;
            for (long z=0; z < 101; z++)
            {
                for (long y = 0; y < 101; y++)
                {
                    for (long x = 0; x < 101; x++)
                    {
                        if (cube[x,y,z])
                        {
                            c++;
                        }
                    }
                }
            }
            return c;
        }


        long ProcessList3(List<string> input)
        {
            List<cuboid> cuboids = new List<cuboid>();
            List<cuboid> cuboids2 = new List<cuboid>();
            Regex regex = new Regex(@"^(on|off) x=(-?\d+|-)\.\.(-?\d+|-),y=(-?\d+|-)\.\.(-?\d+|-),z=(-?\d+|-)\.\.(-?\d+|-)$");

            foreach (string line in input)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    bool setOn = match.Groups[1].Value == "on";
                    (int x1, int x2, int y1, int y2, int z1, int z2) = (
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[4].Value),
                        int.Parse(match.Groups[5].Value),
                        int.Parse(match.Groups[6].Value),
                        int.Parse(match.Groups[7].Value)
                    );
                    cuboid newcuboid = GetCuboid(setOn, x1, x2, y1, y2, z1, z2);

                    for (int i = 0; i < cuboids.Count; i++)
                    {
                        //cuboids2.AddRange(Abjunction(cuboids[i], newcuboid));
                        List<cuboid> intersection = Findintersect(cuboids[i], newcuboid);
                        cuboids2.AddRange(intersection);
                    }
                    if (setOn)
                        cuboids2.Add(newcuboid);

                    (cuboids, cuboids2) = (cuboids2, cuboids);
                    cuboids2.Clear();
                }
            }

            long sumcubes = 0;
            foreach (cuboid c in cuboids)
            {
                long v = ((c.x2 - c.x1) + 1) * ((c.y2 - c.y1) + 1) * ((c.z2 - c.z1) + 1);

                sumcubes += v;
            }
            return sumcubes;
        }
    
    }
}

