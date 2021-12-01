using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            for (int i = 1; i <= 25; i++)
            {
                comboBox1.Items.Add("Day " + i.ToString() + " - Part 1");
                comboBox1.Items.Add("Day " + i.ToString() + " - Part 2");

            }
            int d = DateTime.Today.Day;
            comboBox1.SelectedIndex = (d-1)*2;
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DayClass day = new DayClass();

            string ret = "";
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                case 1:
                    day = new Day01();
                    break;
                case 2:
                case 3:
                    day = new Day02();
                    break;
                case 4:
                case 5:
                    day = new Day03();
                    break;
                case 6:
                case 7:
                    day = new Day04();
                    break;
                case 8:
                case 9:
                    day = new Day05();
                    break;
                case 10:
                case 11:
                    day = new Day06();
                    break;
                case 12:
                case 13:
                    day = new Day07();
                    break;
                case 14:
                case 15:
                    day = new Day08();
                    break;
                case 16:
                case 17:
                    day = new Day09();
                    break;
                case 18:
                case 19:
                    day = new Day10();
                    break;
                case 20:
                case 21:
                    day = new Day11();
                    break;
                case 22:
                case 23:
                    day = new Day12();
                    break;
                case 24:
                case 25:
                    day = new Day13();
                    break;
                case 26:
                case 27:
                    day = new Day14();
                    break;
                case 28:
                case 29:
                    day = new Day15();
                    break;
                case 30:
                case 31:
                    day = new Day16();
                    break;
                case 32:
                case 33:
                    day = new Day17();
                    break;
                case 34:
                case 35:
                    day = new Day18();
                    break;
                case 36:
                case 37:
                    day = new Day19();
                    break;
                case 38:
                case 39:
                    day = new Day20();
                    break;
                case 40:
                case 41:
                    day = new Day21();
                    break;
                case 42:
                case 43:
                    day = new Day22();
                    break;
                case 44:
                case 45:
                    day = new Day23();
                    break;
                case 46:
                case 47:
                    day = new Day24();
                    break;
                case 48:
                case 49:
                    day = new Day25();
                    break;
            }

            string cr = Environment.NewLine;
            //if (comboBox1.SelectedIndex%2==0)
            //{
                ret = "Part1:" + cr + day.Part1();
            //}
            //else
            //{
                ret += cr+cr+ "Part 2: " + cr + day.Part2();
            //}

            textBox1.Text = ret;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
