using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AdventCode
{
    class Day21:DayClass
    {
        struct gamestruct
        {
            int p1pos;
            int p2pos;
            int p1score;
            int p2score;
        }

        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            
            
            int p1pos = 7;
            int p2pos = 1;

            //p1pos = 4;
            //p2pos = 8;

            long p1 = 0;
            long p2 = 0;
            int die = 1;
            
            int dist = 0;
            long rolls = 0;
            long plose = 0;
        
            while (p1 < 1000 && p2 < 1000)
            {
                rolls += 3;
                dist = GetMove(ref die);
                p1pos += dist;
                while (p1pos > 10)
                {
                    plose = p2;
                    p1pos -= 10;
                }
                p1 += p1pos;
                if (p1 >= 1000)
                {
                    plose = p2;
                    break;
                    
                }


                dist = GetMove(ref die);
                rolls += 3;
                p2pos += dist;
                while (p2pos > 10)
                {
                    p2pos -= 10;
                }
                p2 += p2pos;
                if (p2 >= 1000)
                {
                    plose = p1;
                    break;
                }


            }

            long ans = rolls * plose;


            string ret = "Answer : " + ans.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int[] p1pos = new int[10];
            int[] p2pos = new int[10];
            

            p1pos[4] = 1;
            p2pos[8] = 1;

            int[] p1score = new int[10];
            int[] p2score = new int[10];

            int die = 1;

            int dist = 0;
            long rolls = 0;
            long plose = 0;


            //List<Tuple<long, long>> scores = new List<Tuple<long, long>>();
            long[,,,] g = new long[11, 11, 21, 21];
            long[,,,] g2 = new long[11, 11, 21, 21];

            g[7, 1, 0, 0] = 1;
            long winp1 = 0;
            long winp2 = 0;
            int turns = 0;
            //while (p1 < 1000 && p2 < 1000)
            bool played = true;

            while (played)
            {
                turns++;
                played = false;
                for (int p1 = 1; p1 < 11; p1++)
                {
                    for (int p2 = 1; p2 < 11; p2++)
                    {

                        for (int s1 = 0; s1 < 21; s1++)
                        {
                            
                            for (int s2 = 0; s2 < 21; s2++)
                            {
                                if (g[p1, p2, s1, s2] > 0)
                                {
                                    played = true;
                                    // 3 universes here
                                    int pos = 0;
                                    for (int k = 3; k < 10; k++)
                                    {
                                        long mult = 1;
                                        switch (k)
                                        {
                                            case 4:
                                                mult = 3;
                                                break;
                                            case 5:
                                                mult = 6;
                                                break;
                                            case 6:
                                                mult = 7;
                                                break;
                                            case 7:
                                                mult = 6;
                                                break;
                                            case 8:
                                                mult = 3;
                                                break;
                                        }

                                        pos =p1 + k;
                                        //pos = (((p1 + k)-1)%10)+1;
                                        if (pos > 10)
                                        {
                                            pos -= 10;
                                        }
                                        if (s1 + pos > 20)
                                        {
                                            winp1 += mult * g[p1, p2, s1, s2];
                                        }
                                        else
                                        {
                                            g2[pos, p2, s1 + pos, s2] += mult * g[p1, p2, s1, s2];
                                        }
                                    }
                                    
                                }
                            }
                        }
                       
                    }
                }


                for (int p1 = 1; p1 < 11; p1++)
                {
                    for (int p2 = 1; p2 < 11; p2++)
                    {
                        for (int s1 = 0; s1 < 21; s1++)
                        {
                            for (int s2 = 0; s2 < 21; s2++)
                            {

                                g[p1, p2, s1, s2] = g2[p1, p2, s1, s2];
                                g2[p1, p2, s1, s2] = 0;

                            }
                        }

                    }
                }




                for (int p1 = 1; p1 < 11; p1++)
                {
                    for (int p2 = 1; p2 < 11; p2++)
                    {
                        for (int s1 = 0; s1 < 21; s1++)
                        {
                            for (int s2 = 0; s2 < 21; s2++)
                            {
                                if (g[p1, p2, s1, s2] > 0)
                                {
                                    played = true;
                                    // 3 universes here
                                    int pos = 0;
                                    for (int k = 3; k < 10; k++)
                                    {
                                        pos = p2 + k;
                                        long mult = 1;
                                        switch (k)
                                        {
                                            case 4:
                                                mult = 3;
                                                break;
                                            case 5:
                                                mult = 6;
                                                break;
                                            case 6:
                                                mult = 7;
                                                break;
                                            case 7:
                                                mult = 6;
                                                break;
                                            case 8:
                                                mult = 3;
                                                break;
                                        }
                                        if (pos > 10)
                                        {
                                            pos -= 10;
                                        }
                                        if (s2 + pos > 20)
                                        {
                                            winp2 += mult *  g[p1, p2, s1, s2];
                                        }
                                        else
                                        {
                                            g2[p1, pos, s1, s2+pos] += mult * g[p1, p2, s1, s2];
                                        }
                                    }

                                }
                            }
                        }

                    }
                }

                for (int p1 = 1; p1 < 11; p1++)
                {
                    for (int p2 = 1; p2 < 11; p2++)
                    {
                        for (int s1 = 0; s1 < 21; s1++)
                        {
                            for (int s2 = 0; s2 < 21; s2++)
                            {

                                g[p1, p2, s1, s2] = g2[p1, p2, s1, s2];
                                g2[p1, p2, s1, s2] = 0;

                            }
                        }

                    }
                }


            }


            sw.Stop();

            string ret = "Answer : " + Math.Max(winp1, winp2);
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
}

        int GetMove(ref int die)
        {
            int move = 0;
            for (int i = 0; i < 3; i ++)
            {
                move += die;
                die++;
                if (die > 100)
                {
                    die = 1;
                }
            }
            return move;
        }
    }

}

 