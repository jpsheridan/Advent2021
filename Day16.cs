using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Numerics;

namespace AdventCode
{
    class Day16 : DayClass
    {
        public override string Part1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            long valid = 0;

            ln = sr.ReadLine();
            //ln = "D2FE28";
            sr.Close();
            //ln = "8A004A801A8002F478";
            //ln = "620080001611562C8802118E34";
            //ln = "C0015000016115A2E0802F182340";
            //ln = "A0016C880162017C3686B18A3D4780";
            string bin = HexToBin(ln);
            int pl = 0;
            List<long> nums = new List<long>();
            long decbin = DecodeBin(bin,0, ref pl, nums);
            
                       


            sw.Stop();

            

            string ret = "Answer : " + decbin.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;
        }

        public override string Part2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StreamReader sr = new StreamReader("c:\\temp\\advent_2021\\advent_2021_" + this.GetType().Name + ".txt");

            string ln = "";
            long valid = 0;

            ln = sr.ReadLine();
            //ln = "D2FE28";
            sr.Close();
            //ln = "8A004A801A8002F478";
            //ln = "620080001611562C8802118E34";
            //ln = "C0015000016115A2E0802F182340";
            //ln = "A0016C880162017C3686B18A3D4780";
            //ln = "C200B40A82";
            //ln = "04005AC33890";
            //ln = "880086C3E88112";
            //ln = "D8005AC2A8F0";
            //ln = "9C0141080250320F1802104A08";
            //ln = "9C0141080250320F1802104A08";
            string bin = HexToBin(ln);
            int pl = 0;
            List<long> nums = new List<long>();
            long decbin = DecodeBin2(bin, 1, ref pl, nums);


            sw.Stop();



            string ret = "Answer : " + decbin.ToString();
            ret += Environment.NewLine + "Time: " + sw.ElapsedMilliseconds.ToString();
            return ret;

        }

        String HexToBin(string inp)
        {
            string sout = "";

            for (int i = 0; i < inp.Length; i++)
            {
                switch (inp.Substring(i,1))
                {
                    case "0":
                        sout += "0000";
                        break;
                    case "1":
                        sout += "0001";
                        break;
                    case "2":
                        sout+= "0010";
                        break;
                    case "3":
                        sout += "0011";
                        break;
                    case "4":
                        sout += "0100";
                        break;
                    case "5":
                        sout += "0101";
                        break;
                    case "6":
                        sout += "0110";
                        break;
                    case "7":
                        sout += "0111";
                        break;
                    case "8":
                        sout += "1000";
                        break;
                    case "9":
                        sout += "1001";
                        break;
                    case "A":
                        sout += "1010";
                        break;
                    case "B":
                        sout += "1011";
                        break;
                    case "C":
                        sout += "1100";
                        break;
                    case "D":
                        sout += "1101";
                        break;
                    case "E":
                        sout += "1110";
                        break;
                    case "F":
                        sout += "1111";
                        break;
                }
                
            }
            return sout;
        }

        long DecodeBin(string bin, long numpacks, ref int retpacketlen, List<long> nums)
        {
            
            long sumver = 0;
            int pos = 0;

            long iloop = numpacks;
            if (numpacks <= 0)
            {
                iloop = 1;
            }

            int packetlen = 0;

            for (long p = 0; p < iloop; p++)
            {
                string ver = bin.Substring(pos, 3);
                long iver = Convert.ToInt64(ver, 2);
                sumver += iver;

                if (p==1)
                {
                    long abc = 0;
                }
                pos += 3;
                string type = bin.Substring(pos, 3);
                long itype = Convert.ToInt64(type, 2);
                
                pos += 3;

                bool end = false;

                if (itype == 4)
                {
                    end = false;
                    string thisnum = "";
                    while (!end)
                    {
                        if (bin.Substring(pos, 1) == "0")
                        {
                            // last bit of data;
                            end = true;
                        }
                        pos++;
                        thisnum += bin.Substring(pos, 4);
                        //nums.Add(Convert.ToInt64(s,2));

                        pos += 4;
                    }
                    nums.Add(Convert.ToInt64(thisnum, 2));
                }
                else
                {
                    List<long> thisnums = new List<long>();

                    string mode = bin.Substring(pos, 1);
                    if (mode == "0")
                    {
                        // next 15 bits are the length of sub packets
                        pos++;
                        int subpacklen = Convert.ToInt32(bin.Substring(pos, 15), 2);

                        pos += 15;
                        sumver += DecodeBin(bin.Substring(pos, subpacklen), -1, ref packetlen, thisnums);
                        pos += subpacklen;

                    }
                    else
                    {
                        // next 11 bits are the # of sub packets
                        pos++;
                        long subpacks = Convert.ToInt64(bin.Substring(pos, 11), 2);
                        pos+=11;
                       
                        sumver += DecodeBin(bin.Substring(pos), subpacks, ref packetlen, thisnums );
                        
                        pos += packetlen;
                    }

                    switch (itype)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                    }
                }
            }
            if (numpacks < 0)
            {
                string b2 = bin.Substring(pos);
                if (b2 != "")
                {
                    List<long> nums3 = new List<long>();
                    sumver += DecodeBin(b2, -1, ref packetlen, nums3);
                }
            }
            retpacketlen = pos;

            return sumver;
        }


        long DecodeBin2(string bin, long numpacks, ref int retpacketlen, List<long> nums)
        {
            int pos = 0;
            long ret = 0;

            long iloop = numpacks;
            if (numpacks <= 0)
            {
                iloop = 1;
            }


            for (long p = 0; p < iloop; p++)
            {
                string ver = bin.Substring(pos, 3);
                long iver = Convert.ToInt64(ver, 2);
                
                pos += 3;
                string type = bin.Substring(pos, 3);
                int itype = Convert.ToInt32(type, 2);
                pos += 3;
                //DebugType(itype);

                bool end = false;
                

                if (itype == 4)
                {
                    end = false;
                    string thisnum = "";
                    while (!end)
                    {
                        if (bin.Substring(pos, 1) == "0")
                        {
                            // last bit of data;
                            end = true;
                        }
                        pos++;
                        thisnum += bin.Substring(pos, 4);
                        //nums.Add(Convert.ToInt64(s,2));

                        pos += 4;
                    }
                    nums.Add(Convert.ToInt64(thisnum, 2));
                }
                else
                {
                    List<long> thisnums = new List<long>();

                    string mode = bin.Substring(pos, 1);
                    if (mode == "0")
                    {
                        // next 15 bits are the length of sub packets
                        pos++;
                        int subpacklen = Convert.ToInt32(bin.Substring(pos, 15), 2);

                        pos += 15;
                        
                        string subp = bin.Substring(pos, subpacklen);
                        Debug.WriteLine(bin.Length.ToString() + " - " + subpacklen.ToString());
                        pos += subpacklen;
                        while (subp != "")
                        {
                            int packetlen = 0;
                            List<long> subnums = new List<long>();
                            
                            ret = DecodeBin2(subp, 1, ref packetlen, subnums);
                         
                            subp = subp.Substring(packetlen);
                            thisnums.AddRange(subnums);
                        }
              
                        //Debug.WriteLine("itype: " + itype.ToString() + " - " + thisnums.Count.ToString());

                    }
                    else
                    {
                        // next 11 bits are the # of sub packets
                        pos++;
                        long subpacks = Convert.ToInt64(bin.Substring(pos, 11), 2);
                        pos += 11;
                        int packetlen = 0;
                        //Debug.WriteLine("itype: " + itype.ToString() + ", subs:" + subpacks.ToString() );

                        List<long> subnums = new List<long>();
                        ret = DecodeBin2(bin.Substring(pos), subpacks, ref packetlen, subnums);
                        thisnums.AddRange(subnums);
                        pos += packetlen;
                    }
                    long m1 = 0;
                    ret = 0;
                    switch (itype)
                    {
                        case 0:
                            
                            foreach (long n in thisnums)
                            {
                                ret += n;
                            }
                            break;
                        case 1:
                            ret = 1;
                            foreach (long n in thisnums) 
                            {
                                ret *= n;
                            }
                            break;
                        case 2:
                            m1 = thisnums[0];
                            for (int j = 1; j < thisnums.Count;j++)
                            {
                                m1 = Math.Min(m1, thisnums[j]);
                            }
                            ret = m1;
                            
                            break;
                        case 3:
                            m1 = thisnums[0];
                            for (int  j = 1; j < thisnums.Count; j++)
                            {
                                m1 = Math.Max(m1, thisnums[j]);
                            }
                            ret = m1;
                            break;
                        case 5:
                            if (thisnums[0] > thisnums[1])
                                ret = 1;
                            
                            break;
                        case 6:
                            if (thisnums[0] < thisnums[1])
                                ret = 1;

                            break;
                        case 7:
                            if (thisnums[0] == thisnums[1])
                                ret = 1;

                            break;
                    }
                    nums.Add(ret);
                    
                }
            }


            retpacketlen = pos;
                  

            return ret;
        }

        void DebugType(long itype)
        {
            switch (itype)
            {
                case 0:
                    Debug.WriteLine("sum");
                    break;
                case 1:
                    Debug.WriteLine("mult");
                    break;
                case 2:
                    Debug.WriteLine("min");
                    break;
                case 3:
                    Debug.WriteLine("max");
                    
                    break;
                case 5:
                    Debug.WriteLine("gt");
                    break;
                case 6:
                    Debug.WriteLine("lt");
                    break;
                case 7:
                    Debug.WriteLine("eq");

                    break;
            }
            
        }
    }
}

