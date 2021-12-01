using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventCode
{
    class AdventProblems
    {
        public string day1_p1()
        {
            //            ---Day 1: The Tyranny of the Rocket Equation ---

            //Santa has become stranded at the edge of the Solar System while delivering presents to other planets!To accurately calculate his position in space, safely align his warp drive, and return to Earth in time to save Christmas, he needs you to bring him measurements from fifty stars.

            //Collect stars by solving puzzles.Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star.Good luck!

            //The Elves quickly load you into a spacecraft and prepare to launch.

            //At the first Go / No Go poll, every Elf is Go until the Fuel Counter - Upper.They haven't determined the amount of fuel required yet.

            //Fuel required to launch a given module is based on its mass. Specifically, to find the fuel required for a module, take its mass, divide by three, round down, and subtract 2.

            //For example:

            //    For a mass of 12, divide by 3 and round down to get 4, then subtract 2 to get 2.
            //    For a mass of 14, dividing by 3 and rounding down still yields 4, so the fuel required is also 2.
            //    For a mass of 1969, the fuel required is 654.
            //    For a mass of 100756, the fuel required is 33583.

            //The Fuel Counter - Upper needs to know the total fuel requirement.To find it, individually calculate the fuel needed for the mass of each module(your puzzle input), then add together all the fuel values.

            // What is the sum of the fuel requirements for all of the modules on your spacecraft ?

            StreamReader sr = new StreamReader("c:\\temp\\advent_fuel.txt");
            string l = "";
            long tot = 0;
            while ((l = sr.ReadLine()) != null)
            {
                long f = long.Parse(l);
                f /= 3;
                f -= 2;
                tot += f;
            }
            sr.Close();

            return tot.ToString();

        }

        public string day1_p2()
        {
            //            ---Part Two-- -

            // During the second Go / No Go poll, the Elf in charge of the Rocket Equation Double-Checker stops the launch sequence. 
            // Apparently, you forgot to include additional fuel for the fuel you just added.


            //Fuel itself requires fuel just like a module - take its mass, divide by three, round down, and subtract 2.
            // However, that fuel also requires fuel, and that fuel requires fuel, and so on.
            // Any mass that would require negative fuel should instead be treated as if it requires zero fuel; 
            // the remaining mass, if any, is instead handled by wishing really hard, which has no mass and is outside the scope of this calculation.

            // So, for each module mass, calculate its fuel and add it to the total.Then, treat the fuel amount you just calculated as the input mass and repeat the process, 
            // continuing until a fuel requirement is zero or negative.

            // For example:

            // A module of mass 14 requires 2 fuel.This fuel requires no further fuel(2 divided by 3 and rounded down is 0, which would call for a negative fuel), 
            // so the total fuel required is still just 2.

            //    At first, a module of mass 1969 requires 654 fuel.Then, this fuel requires 216 more fuel(654 / 3 - 2). 216 then requires 70 more fuel, 
            // which requires 21 fuel, which requires 5 fuel, which requires no further fuel.So, the total fuel required for a module of mass 1969 is 654 + 216 + 70 + 21 + 5 = 966.

            // The fuel required by a module of mass 100756 and its fuel is: 33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2 = 50346.

            //What is the sum of the fuel requirements for all of the modules on your spacecraft when also taking into account the mass of the added fuel ? 
            // (Calculate the fuel requirements for each module separately, then add them all up at the end.)

            StreamReader sr = new StreamReader("c:\\temp\\advent_fuel.txt");
            string l = "";
            long tot = 0;
            while ((l = sr.ReadLine()) != null)
            {
                long f = long.Parse(l);
                while (f > 0)
                {
                    f /= 3;
                    f -= 2;

                    if (f <= 0)
                    {
                        break;
                    }
                    else
                    {
                        tot += f;
                    }

                }


            }

            sr.Close();
            return tot.ToString();

        }

        public string day2_p1()
        {
            //---Day 2: 1202 Program Alarm ---

            //On the way to your gravity assist around the Moon, your ship computer beeps angrily about a "1202 program alarm".
            // On the radio, an Elf is already explaining how to handle the situation: "Don't worry, that's perfectly norma--" The ship computer bursts into flames.

            // You notify the Elves that the computer's magic smoke seems to have escaped.
            // "That computer ran Intcode programs like the gravity assist program it was working on; surely there are enough spare parts up there to build a new Intcode computer!"

            // An Intcode program is a list of integers separated by commas(like 1, 0, 0, 3, 99).
            // To run one, start by looking at the first integer (called position 0). 
            // Here, you will find an opcode - either 1, 2, or 99.
            // The opcode indicates what to do; for example, 
            // 99 means that the program is finished and should immediately halt.Encountering an unknown opcode means something went wrong.

            //  Opcode 1 adds together numbers read from two positions and stores the result in a third position.
            // The three integers immediately after the opcode tell you these three positions - 
            // the first two indicate the positions from which you should read the input values, and the third indicates the position at which the output should be stored.

            // For example, if your Intcode computer encounters 1,10,20,30, it should read the values at positions 10 and 20, 
            // add those values, and then overwrite the value at position 30 with their sum.

            // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. 
            // Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.

            // Once you're done processing an opcode, move to the next one by stepping forward 4 positions.

            // For example, suppose you have the following program:

            //            1,9,10,3,2,3,11,0,99,30,40,50

            //  For the purposes of illustration, here is the same program split into multiple lines:

            // 1,9,10,3,
            // 2,3,11,0,
            // 99,
            // 30,40,50

            //The first four integers, 1,9,10,3, are at positions 0, 1, 2, and 3.Together, they represent the first opcode(1, addition), 
            // the positions of the two inputs(9 and 10), and the position of the output(3). 
            // To handle this opcode, you first need to get the values at the input positions: position 9 contains 30, 
            // and position 10 contains 40. Add these numbers together to get 70. 
            // Then, store this value at the output position; here, the output position(3) is at position 3, so it overwrites itself. Afterward, the program looks like this:

            // 1,9,10,70,
            // 2,3,11,0,
            // 99,
            // 30,40,50

            // Step forward 4 positions to reach the next opcode, 2.
            // This opcode works just like the previous, but it multiplies instead of adding.
            //  The inputs are at positions 3 and 11; these positions contain 70 and 50 respectively.Multiplying these produces 3500; this is stored at position 0:

            //3500,9,10,70,
            //2,3,11,0,
            //99,
            //30,40,50

            //Stepping forward 4 more positions arrives at opcode 99, halting the program.

            //Here are the initial and final states of a few more small programs:

            //            1,0,0,0,99 becomes 2,0,0,0,99(1 + 1 = 2).
            //    2,3,0,3,99 becomes 2,3,0,6,99(3 * 2 = 6).
            //    2,4,4,5,99,0 becomes 2,4,4,5,99,9801(99 * 99 = 9801).
            //    1,1,1,4,99,5,6,0,99 becomes 30,1,1,4,2,5,6,0,99.

            // Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) 
            // to the "1202 program alarm" state it had just before the last computer caught fire. 
            // To do this, before running the program, replace position 1 with the value 12 and replace position 2 with the value 2.
            // What value is left at position 0 after the program halts?

            // ans: 2782414
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day2_input.txt");

            string ln = sr.ReadLine();

            sr.Close();


            int[] inp = adv.GetComputerInput(ln);


            int res = 0;

            inp[1] = 12;
            inp[2] = 2;

            res = adv.Compute(inp);

            return "done: " + res.ToString();
        }

        public string day2_p2()
        {
            //            ---Part Two-- -

            //"Good, the new computer seems to be working correctly! 
            // Keep it nearby during this mission - you'll probably use it again. 
            // Real Intcode computers support many more features than your new one, but we'll let you know what they are as you need them."

            // "However, your current priority should be to complete your gravity assist around the Moon. 
            // For this mission to succeed, we should settle on some terminology for the parts you've already built."

            // Intcode programs are given as a list of integers; these values are used as the initial state for the computer's memory. 
            // When you run an Intcode program, make sure to start by initializing memory to the program's values.
            // A position in memory is called an address(for example, the first value in memory is at "address 0").

            // Opcodes(like 1, 2, or 99) mark the beginning of an instruction. 
            // The values used immediately after an opcode, if any, are called the instruction's parameters. 
            // For example, in the instruction 1,2,3,4, 1 is the opcode; 2, 3, and 4 are the parameters. 
            // The instruction 99 contains only an opcode and has no parameters.

            // The address of the current instruction is called the instruction pointer; it starts at 0.
            // After an instruction finishes, the instruction pointer increases by the number of values in the instruction; 
            // until you add more instructions to the computer, this is always 4(1 opcode + 3 parameters) for the add and multiply instructions. 
            // (The halt instruction would increase the instruction pointer by 1, but it halts the program instead.)


            // "With terminology out of the way, we're ready to proceed. 
            // To complete the gravity assist, you need to determine what pair of inputs produces the output 19690720."


            // The inputs should still be provided to the program by replacing the values at addresses 1 and 2, just like before.
            // In this program, the value placed in address 1 is called the noun, and the value placed in address 2 is called the verb.
            // Each of the two input values will be between 0 and 99, inclusive.


            // Once the program has halted, its output is available at address 0, also just like before.
            // Each time you try a pair of inputs, make sure you first reset the computer's memory to the values in the program (your puzzle input)
            // - in other words, don't reuse memory from a previous attempt.

            //  Find the input noun and verb that cause the program to produce the output 19690720.
            // What is 100 * noun + verb ? (For example, if noun = 12 and verb = 2, the answer would be 1202.)
            StreamReader sr = new StreamReader("c:\\temp\\advent_day2_input.txt");
            AdventShared adv = new AdventShared();

            string ln = sr.ReadLine();

            sr.Close();


            int[] inp = adv.GetComputerInput(ln);

            int res = 0;

            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100; j++)
                {
                    int[] inprun = (int[])inp.Clone();
                    inprun[1] = i;
                    inprun[2] = j;

                    res = adv.Compute(inprun);

                    if (res == 19690720)
                    {
                        return "found it: " + ((i * 100) + j).ToString();
                    }
                    //Debug.WriteLine(i.ToString() + " - " + j.ToString() + " - " + res.ToString());
                }
            }

            return "failed";

        }

        public string day3_p1()
        {
            // ---Day 3: Crossed Wires ---

            //  The gravity assist was successful, and you're well on your way to the Venus refuelling station. 
            // During the rush back on Earth, the fuel management system wasn't completely installed, so that's next on the priority list.

            // Opening the front panel reveals a jumble of wires.
            // Specifically, two wires are connected to a central port and extend outward on a grid. 
            // You trace the path each wire takes as it leaves the central port, one wire per line of text(your puzzle input).

            // The wires twist and turn, but the two wires occasionally cross paths.
            // To fix the circuit, you need to find the intersection point closest to the central port.
            // Because the wires are on a grid, use the Manhattan distance for this measurement.
            // While the wires do technically cross right at the central port where they both start, 
            // this point does not count, nor does a wire count as crossing with itself.

            // For example, if the first wire's path is R8,U5,L5,D3, then starting from the central port (o), it goes right 8, up 5, left 5, and finally down 3:

            // ...........
            // ...........
            // ...........
            // ....+ ----+.
            // ....|....|.
            // ....|....|.
            // ....|....|.
            // .........|.
            // .o------ - +.
            // ...........

            // Then, if the second wire's path is U7,R6,D4,L4, it goes up 7, right 6, down 4, and left 4:

            // ...........
            // .+ -----+...
            // .|.....|...
            // .|..+ --X - +.
            // .|..|..|.|.
            // .|.- X-- +.|.
            // .|..|....|.
            // .|.......|.
            // .o------ - +.
            // ...........

            // These wires cross at two locations(marked X), but the lower - left one is closer to the central port: its distance is 3 + 3 = 6.

            // Here are a few more examples:

            // R75,D30,R83,U83,L12,D49,R71,U7,L72
            // U62, R66, U55, R34, D71, R55, D58, R83 = distance 159
            // R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
            // U98, R91, D20, R16, D67, R40, U7, R15, U6, R7 = distance 135

            // What is the Manhattan distance from the central port to the closest intersection ?

            //js: we'll call the centre 5000,5000

            StreamReader sr = new StreamReader("c:\\temp\\advent_day3_input.txt");
            AdventShared adv = new AdventShared();
            int max = 22000;
            int[,] grid = new int[max, max];

            string p1 = sr.ReadLine();
            string p2 = sr.ReadLine();

            string[] path1 = p1.Split(',');
            string[] path2 = p2.Split(',');

            sr.Close();

            grid = adv.WorkGrid(path1, grid, 1, max);
            grid = adv.WorkGrid(path2, grid, 2, max);
            int min = 100000;
            for (int x = 0; x <= max - 1; x++)
            {
                for (int y = 0; y <= max - 1; y++)
                {
                    int tot = 0;
                    if (grid[x, y] == 3)
                    {
                        tot = Math.Abs(x - (max / 2)) + Math.Abs(y - (max / 2));
                    }
                    if (tot < min && tot > 0)
                    {
                        min = tot;
                    }
                }
            }
            return "Done - " + min.ToString();
        }

        public string day3_p2()
        {
            //---Part Two-- -

            // It turns out that this circuit is very timing - sensitive; you actually need to minimize the signal delay.

            //  To do this, calculate the number of steps each wire takes to reach each intersection; 
            //  choose the intersection where the sum of both wires' steps is lowest. 
            //  If a wire visits a position on the grid multiple times, 
            //  use the steps value from the first time it visits that position when calculating the total value of a specific intersection.

            //  The number of steps a wire takes is the total number of grid squares the wire has entered to get to that location, 
            //  including the intersection being considered.Again consider the example from above:

            //...........
            //.+-----+...
            //.|.....|...
            //.|..+ --X - +.
            //.|..|..|.|.
            //.|.- X-- +.|.
            //.|..|....|.
            //.|.......|.
            //.o------ - +.
            //...........

            // In the above example, the intersection closest to the central port is reached after 8 + 5 + 5 + 2 = 20 steps by the 
            // first wire and 7 + 6 + 4 + 3 = 20 steps by the second wire for a total of 20 + 20 = 40 steps.

            //  However, the top - right intersection is better: the first wire takes only 8 + 5 + 2 = 15 
            // and the second wire takes only 7 + 6 + 2 = 15, a total of 15 + 15 = 30 steps.


            //  Here are the best steps for the extra examples from above:


            //R75, D30, R83, U83, L12, D49, R71, U7, L72

            //    U62, R66, U55, R34, D71, R55, D58, R83 = 610 steps

            //    R98, U47, R26, D63, R33, U87, L62, D20, R33, U53, R51

            //    U98, R91, D20, R16, D67, R40, U7, R15, U6, R7 = 410 steps


            //What is the fewest combined steps the wires must take to reach an intersection ?
            StreamReader sr = new StreamReader("c:\\temp\\advent_day3_input.txt");
            AdventShared adv = new AdventShared();
            int max = 22000;
            string[,] gridsteps = new string[max, max];
            //int[,] steps = new int[max, max];
            string p1 = sr.ReadLine();
            string p2 = sr.ReadLine();

            string[] path1 = p1.Split(',');
            string[] path2 = p2.Split(',');

            sr.Close();

            gridsteps = adv.WorkGridStepCount(path1, gridsteps, "B", max);
            gridsteps = adv.WorkGridStepCount(path2, gridsteps, "R", max);
            int min = 100000;
            for (int x = 0; x <= max - 1; x++)
            {
                for (int y = 0; y <= max - 1; y++)
                {
                    int tot = 0;
                    if (!(gridsteps[x, y] == null) && gridsteps[x, y].StartsWith("RB"))
                    {
                        tot = int.Parse(gridsteps[x, y].Substring(2));
                    }
                    if (tot < min && tot > 0)
                    {
                        min = tot;
                    }
                }
            }
            return "Done - " + min.ToString();
        }

        public string day3_p2a()
        {
            StreamReader sr = new StreamReader("c:\\dev\\advent_d3_input.txt");
            AdventShared adv = new AdventShared();

            Dictionary<Tuple<int, int>, int> Grid1 = new Dictionary<Tuple<int, int>, int>();
            Dictionary<Tuple<int, int>, int> Grid2 = new Dictionary<Tuple<int, int>, int>();

            int max = 22000;
            string[,] gridsteps = new string[max, max];
            //int[,] steps = new int[max, max];
            string p1 = sr.ReadLine();
            string p2 = sr.ReadLine();

            string[] path1 = p1.Split(',');
            string[] path2 = p2.Split(',');

            sr.Close();

            Grid1 = adv.WorkGridStepCountList(path1);
            Grid2 = adv.WorkGridStepCountList(path2);
            int min = 100000;
            int tot = 0;
            foreach (KeyValuePair<Tuple<int, int>, int> k in Grid1)
            {
                if (Grid2.ContainsKey(k.Key))
                {
                    tot = k.Value + Grid2[k.Key];

                    if (tot < min && tot > 0)
                    {
                        min = tot;
                    }
                }
            }

            return "Done - " + min.ToString();
        }


        public string day4_p1()
        {
            // ---Day 4: Secure Container ---
            // You arrive at the Venus fuel depot only to discover it's protected by a password. 
            // The Elves had written the password on a sticky note, but someone threw it out.

            // However, they do remember a few key facts about the password:

            // It is a six - digit number.
            // The value is within the range given in your puzzle input.

            // Two adjacent digits are the same(like 22 in 122345).
            // Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).

            // Other than the range rule, the following are true:

            // 111111 meets these criteria(double 11, never decreases).
            // 223450 does not meet these criteria(decreasing pair of digits 50).
            // 123789 does not meet these criteria(no double).

            // How many different passwords within the range given in your puzzle input meet these criteria?

            // Your puzzle input is 245318-765747.

            AdventShared adv = new AdventShared();
            int min = 245318;
            int max = 765747;

            int c = adv.PasswordCount1(min, max);
            return c.ToString();


        }

        public string day4_p2()
        {

            //---Part Two-- -

            //An Elf just remembered one more important detail: the two adjacent matching digits are not part of a larger group of matching digits.

            //Given this additional criterion, but still ignoring the range rule, the following are now true:

            //    112233 meets these criteria because the digits never decrease and all repeated digits are exactly two digits long.
            //    123444 no longer meets the criteria(the repeated 44 is part of a larger group of 444).
            //    111122 meets the criteria(even though 1 is repeated more than twice, it still contains a double 22).

            //How many different passwords within the range given in your puzzle input meet all of the criteria?

            AdventShared adv = new AdventShared();
            int min = 245318;
            int max = 765747;

            int c = adv.PasswordCount2(min, max);
            return c.ToString();

        }

        public string day5_p1()
        {
            // ---Day 5: Sunny with a Chance of Asteroids ---

            // You're starting to sweat as the ship makes its way toward Mercury. 
            // The Elves suggest that you get the air conditioner working by upgrading your ship computer to support the Thermal Environment Supervision Terminal.

            // The Thermal Environment Supervision Terminal(TEST) starts by running a diagnostic program(your puzzle input). 
            // The TEST diagnostic program will run on your existing Intcode computer after a few modifications:

            // First, you'll need to add two new instructions:

            // Opcode 3 takes a single integer as input and saves it to the address given by its only parameter.
            // For example, the instruction 3,50 would take an input value and store it at address 50.
            // Opcode 4 outputs the value of its only parameter.
            // For example, the instruction 4,50 would output the value at address 50.

            // Programs that use these instructions will come with documentation that explains what should be connected to the input and output. The program 3,0,4,0,99 outputs whatever it gets as input, then halts.

            // Second, you'll need to add support for parameter modes:

            // Each parameter of an instruction is handled based on its parameter mode. 
            // Right now, your ship computer already understands parameter mode 0, position mode, 
            // which causes the parameter to be interpreted as a position - if the parameter is 50, 
            // its value is the value stored at address 50 in memory.Until now, all parameters have been in position mode.

            // Now, your ship computer will also need to handle parameters in mode 1, 
            // immediate mode. In immediate mode, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50.

            // Parameter modes are stored in the same value as the instruction's opcode. 
            // The opcode is a two-digit number based only on the ones and tens digit of the value, that is, 
            // the opcode is the rightmost two digits of the first value in an instruction. 
            // Parameter modes are single digits, one per parameter, 
            // read right-to-left from the opcode: the first parameter's mode is in the hundreds digit, 
            // the second parameter's mode is in the thousands digit, the third parameter's mode is in the ten-thousands digit, and so on.
            // Any missing modes are 0.

            // For example, consider the program 1002,4,3,4,33.

            // The first instruction, 1002,4,3,4, is a multiply instruction - the rightmost two digits of the first value, 02, 
            // indicate opcode 2, multiplication.
            // Then, going right to left, the parameter modes are 0(hundreds digit), 1(thousands digit), 
            // and 0(ten - thousands digit, not present and therefore zero):

            //ABCDE
            // 1002

            //DE - two - digit opcode,      02 == opcode 2
            // C - mode of 1st parameter,  0 == position mode
            // B - mode of 2nd parameter,  1 == immediate mode
            // A - mode of 3rd parameter,  0 == position mode,
            //                                  omitted due to being a leading zero

            // This instruction multiplies its first two parameters. 
            // The first parameter, 4 in position mode, works like it did before - 
            // its value is the value stored at address 4(33).The second parameter, 
            // 3 in immediate mode, simply has value 3.The result of this operation, 33 * 3 = 99, 
            // is written according to the third parameter, 4 in position mode, which also works like it did before -99 is written to address 4.

            // Parameters that an instruction writes to will never be in immediate mode.

            // Finally, some notes:

            // It is important to remember that the instruction pointer should increase by the number of values in
            // the instruction after the instruction finishes. Because of the new instructions, this amount is no longer always 4.
            // Integers can be negative: 1101,100,-1,4,0 is a valid program(find 100 + -1, store the result in position 4).

            // The TEST diagnostic program will start by requesting from the user the ID of the system to
            // test by running an input instruction - provide it 1, the ID for the ship's air conditioner unit.

            // It will then perform a series of diagnostic tests confirming that various parts of the Intcode computer, 
            // like parameter modes, function correctly.For each test, it will run an output instruction indicating 
            // how far the result of the test was from the expected value, where 0 means the test was successful.
            // Non - zero outputs mean that a function is not working correctly; 
            // check the instructions that were run before the output instruction to see which one failed.

            // Finally, the program will output a diagnostic code and immediately halt. 
            // This final output isn't an error; an output followed immediately by a halt 
            // means the program finished. If all outputs were zero except the diagnostic code, the diagnostic program ran successfully.

            // After providing 1 to the only input instruction and passing all the tests, what diagnostic code does the program produce?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day5_input.txt");

            string ln = sr.ReadLine();

            sr.Close();


            int[] inp = adv.GetComputerInput(ln);


            int res = 0;

            int startcode = 1;
            res = adv.Compute5(inp, startcode);


            return "done: " + res.ToString();

        }

        public string day5_p2()
        {

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day5_input.txt");

            string ln = sr.ReadLine();

            sr.Close();


            int[] inp = adv.GetComputerInput(ln);


            int res = 0;

            int startcode = 5;
            res = adv.Compute5a(inp, startcode);


            return "done: " + res.ToString();

        }

        public string day6_p1()
        {
            // ---Day 6: Universal Orbit Map-- -

            // You've landed at the Universal Orbit Map facility on Mercury.
            // Because navigation in space often involves transferring between orbits, 
            // the orbit maps here are useful for finding efficient routes between, for example, you and Santa.
            // You download a map of the local orbits (your puzzle input).

            // Except for the universal Center of Mass(COM), every object in space is in orbit around exactly one other object.An orbit looks roughly like this:

            //                  \
            //                   \
            //                    |
            //                    |
            //AAA-- > o            o < --BBB
            //                    |
            //                    |
            //                   /
            //                  /

            // In this diagram, the object BBB is in orbit around AAA.
            // The path that BBB takes around AAA(drawn with lines) is only partly shown.
            // In the map data, this orbital relationship is written AAA) BBB, which means "BBB is in orbit around AAA".

            // Before you use your map data to plot a course, you need to make sure it wasn't corrupted during the download. 
            // To verify maps, the Universal Orbit Map facility uses orbit count checksums - the total number of direct orbits (like the one shown above) and indirect orbits.

            // Whenever A orbits B and B orbits C, then A indirectly orbits C. 
            // This chain can be any number of objects long: if A orbits B, B orbits C, and C orbits D, then A indirectly orbits D.

            // For example, suppose you have the following map:

            //COM)B
            //B)C
            //C)D
            //D)E
            //E)F
            //B)G
            //G)H
            //D)I
            //E)J
            //J)K
            //K)L

            // Visually, the above map of orbits looks like this:

            //        G - H       J - K - L
            //       /           /
            //COM - B - C - D - E - F
            //               \
            //                I

            //  In this visual representation, when two objects are connected by a line, the one on the right directly orbits the one on the left.

            // Here, we can count the total number of orbits as follows:

            // D directly orbits C and indirectly orbits B and COM, a total of 3 orbits.
            // L directly orbits K and indirectly orbits J, E, D, C, B, and COM, a total of 7 orbits.
            // COM orbits nothing.

            // The total number of direct and indirect orbits in this example is 42.

            // What is the total number of direct and indirect orbits in your map data ?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day6_input.txt");

            List<string> l = new List<string>();
            string ln = "";
            while ((ln = sr.ReadLine()) != null)
            {
                l.Add(ln);
            }
            sr.Close();

            //int Direct = l.Count();

            int orbits = adv.ProcessList(l);

            return "Orbits: " + orbits.ToString();


        }

        public string day6_p2()
        {
            // ---Part Two-- -
            // Now, you just need to figure out how many orbital transfers you(YOU) need to take to get to Santa(SAN).

            // You start at the object YOU are orbiting; your destination is the object SAN is orbiting.
            // An orbital transfer lets you move from any object to an object orbiting or orbited by that object.

            // For example, suppose you have the following map:

            //COM)B
            //B)C
            //C)D
            //D)E
            //E)F
            //B)G
            //G)H
            //D)I
            //E)J
            //J)K
            //K)L
            //K)YOU
            //I)SAN

            // Visually, the above map of orbits looks like this:

            //                          YOU
            //                         /
            //        G - H       J - K - L
            //       /           /
            //COM - B - C - D - E - F
            //               \
            //                I - SAN

            // In this example, YOU are in orbit around K, and SAN is in orbit around I.
            // To move from K to I, a minimum of 4 orbital transfers are required:

            // K to J
            // J to E
            // E to D
            // D to I

            // Afterward, the map of orbits looks like this:

            //         G - H       J - K - L
            //        /           /
            // COM - B - C - D - E - F
            //                \
            //                 I - SAN
            //                  \
            //                   YOU

            // What is the minimum number of orbital transfers required to move from the object 
            // YOU are orbiting to the object SAN is orbiting ? (Between the objects they are orbiting -not between YOU and SAN.)

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day6_input.txt");

            List<string> l = new List<string>();

            List<string> lsan = new List<string>();
            List<string> lyou = new List<string>();
            string ln = "";
            while ((ln = sr.ReadLine()) != null)
            {
                l.Add(ln);
            }
            sr.Close();

            //int Direct = l.Count();

            lsan = adv.ProcessList2(l, "SAN");
            lyou = adv.ProcessList2(l, "YOU");

            for (int i = 0; i < lsan.Count; i++)
            {
                if (lyou.Contains(lsan[i]))
                {
                    int j = lyou.FindIndex(x => x == lsan[i]);
                    return "Got it: " + lsan[i] + " at " + i.ToString() + " - " + j.ToString();
                }
            }

            return "failed";
            //Debug.WriteLine("Orbits: " + orbits.ToString());
        }

        public string day7_p1()
        {

            //        ---Day 7: Amplification Circuit ---

            // Based on the navigational maps, you're going to need to send more power to your ship's thrusters to reach Santa in time.
            // To do this, you'll need to configure a series of amplifiers already installed on the ship.

            // There are five amplifiers connected in series; each one receives an input signal and produces an output signal.
            // They are connected such that the first amplifier's output leads to the second amplifier's input, 
            // the second amplifier's output leads to the third amplifier's input, and so on. 
            // The first amplifier's input value is 0, and the last amplifier's output leads to your ship's thrusters.

            //    O------ - O  O------ - O  O------ - O  O------ - O  O------ - O
            // 0->| Amp A |->| Amp B |->| Amp C |->| Amp D |->| Amp E |-> (to thrusters)
            //    O------ - O  O------ - O  O------ - O  O------ - O  O------ - O

            // The Elves have sent you some Amplifier Controller Software(your puzzle input), 
            // a program that should run on your existing Intcode computer. Each amplifier will need to run a copy of the program.

            // When a copy of the program starts running on an amplifier,
            // it will first use an input instruction to ask the amplifier for its current phase setting(an integer from 0 to 4).
            // Each phase setting is used exactly once, but the Elves can't remember which amplifier needs which phase setting.

            // The program will then call another input instruction to get the amplifier's input signal, 
            // compute the correct output signal, and supply it back to the amplifier with an output instruction. 
            // (If the amplifier has not yet received an input signal, it waits until one arrives.)

            // Your job is to find the largest output signal that can be sent to the thrusters 
            // by trying every possible combination of phase settings on the amplifiers.
            // Make sure that memory is not shared or reused between copies of the program.

            // For example, suppose you want to try the phase setting sequence 3,1,2,4,0, 
            // which would mean setting amplifier A to phase setting 3, amplifier B to setting 1, C to 2, D to 4, and E to 0.
            // Then, you could determine the output signal that gets sent from amplifier E to the thrusters with the following steps:

            // Start the copy of the amplifier controller software that will run on amplifier A. 
            // At its first input instruction, provide it the amplifier's phase setting, 3. 
            // At its second input instruction, provide it the input signal, 0. 
            // After some calculations, it will use an output instruction to indicate the amplifier's output signal.
            // Start the software for amplifier B. Provide it the phase setting(1) and then 
            // whatever output signal was produced from amplifier A.It will then produce a new output signal destined for amplifier C.

            // Start the software for amplifier C, provide the phase setting(2) and the value from amplifier B, then collect its output signal.

            // Run amplifier D's software, provide the phase setting (4) and input value, and collect its output signal.

            // Run amplifier E's software, provide the phase setting (0) and input value, and collect its output signal.

            // The final output signal from amplifier E would be sent to the thrusters.
            // However, this phase setting sequence may not have been the best one; another sequence might have sent a higher signal to the thrusters.

            // Here are some example programs:
            // Max thruster signal 43210(from phase setting sequence 4, 3, 2, 1, 0):

            // 3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0

            // Max thruster signal 54321(from phase setting sequence 0, 1, 2, 3, 4):

            // 3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0

            // Max thruster signal 65210(from phase setting sequence 1, 0, 4, 3, 2):

            // 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,
            // 1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0

            // Try every combination of phase settings on the amplifiers.What is the highest signal that can be sent to the thrusters?
            // ans = 298586

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day7_input.txt");

            string ln = sr.ReadLine();

            sr.Close();

            //ln = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            int[] inp = adv.GetComputerInput(ln);


            int res = 0;
            int maxres = 0;
            string maxstr = "";
            int[] phases = new int[5] { 4, 3, 2, 1, 0 };

            foreach (var z in phases.ToList().Permute())
            {
                phases = z.ToArray();
                res = adv.Amplify(phases, ln);
                if (res > maxres)
                {
                    maxres = res;
                    maxstr = "MAXRES:" + res.ToString();
                }
            }

            return "done: " + maxstr;

        }

        public string day7_p2()
        {
            // ---Part Two-- -

            // It's no good - in this configuration, the amplifiers can't generate a large enough output signal to produce the thrust you'll need. 
            // The Elves quickly talk you through rewiring the amplifiers into a feedback loop:

            //             O------ - O  O------ - O  O------ - O  O------ - O  O------ - O
            //      0 - +->| Amp A |->| Amp B     |->| Amp C   |->| Amp D    |->| Amp E  | -.
            //          |  O------ - O  O------ - O  O------ - O  O------ - O  O------ - O |
            //          |                                                                  |
            //          '------------------------------------------------------------------+
            //                                                                             |
            //                                                                             v
            //                                                           (to thrusters)

            // Most of the amplifiers are connected as they were before; 
            // amplifier A's output is connected to amplifier B's input, and so on. However, 
            //  the output from amplifier E is now connected into amplifier A's input. 
            // This creates the feedback loop: the signal will be sent through the amplifiers many times.

            // In feedback loop mode, the amplifiers need totally different phase settings: integers from 5 to 9, 
            // again each used exactly once.
            // These settings will cause the Amplifier Controller Software to repeatedly take input and produce output many times before halting.
            // Provide each amplifier its phase setting at its first input instruction; all further input / output instructions are for signals.

            // Don't restart the Amplifier Controller Software on any amplifier during this process. Each one should continue receiving and sending signals until it halts.

            // All signals sent or received in this process will be between pairs of amplifiers except the very first signal and the very last signal.
            // To start the process, a 0 signal is sent to amplifier A's input exactly once.

            // Eventually, the software on the amplifiers will halt after they have processed the final loop.
            // When this happens, the last output signal from amplifier E is sent to the thrusters.
            // Your job is to find the largest output signal that can be sent to the thrusters using the new phase settings and feedback loop arrangement.

            // Here are some example programs:

            // Max thruster signal 139629729(from phase setting sequence 9, 8, 7, 6, 5):

            // 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5

            // Max thruster signal 18216(from phase setting sequence 9, 7, 8, 5, 6):

            // 3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,
            // -5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,
            // 53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10

            // Try every combination of the new phase settings on the amplifier feedback loop. What is the highest signal that can be sent to the thrusters?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day7_input.txt");

            string ln = sr.ReadLine();

            sr.Close();

            //ln = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            //ln = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            int[] inp = adv.GetComputerInput(ln);


            long res = 0;
            long maxres = 0;
            string maxstr = "";
            //int[] phases = new int[5] { 9, 7, 8, 5, 6};
            int[] phases = new int[5] { 9, 8, 7, 6, 5 };

            foreach (var z in phases.ToList().Permute())
            {
                phases = z.ToArray();
                //res = adv.AmplifywithFeedback(phases, ln);
                res = adv.AmplifywithFeedback64(phases, ln);
                if (res > maxres)
                {
                    maxres = res;
                    maxstr = "MAXRES:" + res.ToString();

                }
            }

            return "done: " + maxstr;

        }

        public string day8_p1()
        {
            //  ---Day 8: Space Image Format-- -

            // The Elves' spirits are lifted when they realize you have an opportunity 
            // to reboot one of their Mars rovers, and so they are curious if you would spend a brief sojourn on Mars. 
            // You land your ship near the rover.

            // When you reach the rover, you discover that it's already in the process of rebooting!
            // It's just waiting for someone to enter a BIOS password.
            // The Elf responsible for the rover takes a picture of the password(your puzzle input) and sends it to you via the Digital Sending Network.

            // Unfortunately, images sent via the Digital Sending Network aren't encoded with any normal encoding; 
            // instead, they're encoded in a special Space Image Format.None of the Elves seem to remember why this is the case. 
            // They send you the instructions to decode it.

            // Images are sent as a series of digits that each represent the color of a single pixel.
            // The digits fill each row of the image left - to - right, then move downward to the next row, 
            // filling rows top - to - bottom until every pixel of the image is filled.

            // Each image actually consists of a series of identically - sized layers that are filled in this way.
            // So, the first digit corresponds to the top - left pixel of the first layer, 
            // the second digit corresponds to the pixel to the right of that on the same layer, 
            // and so on until the last digit, which corresponds to the bottom - right pixel of the last layer.

            // For example, given an image 3 pixels wide and 2 pixels tall, the image data 123456789012 corresponds to the following image layers:

            // Layer 1: 123
            //          456

            // Layer 2: 789
            //          012


            // The image you received is 25 pixels wide and 6 pixels tall.

            // To make sure the image wasn't corrupted during transmission, the Elves would like you to find the layer that contains the fewest 0 digits. 
            // On that layer, what is the number of 1 digits multiplied by the number of 2 digits?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day8_input.txt");

            string ln = "";

            ln = sr.ReadLine();
            sr.Close();

            string img = adv.GetImageCRCLayer(ln);
            return "CRC: " + adv.GetImageCRC(img).ToString();

        }

        public string day8_p2()
        {
            // ---Part Two-- -
            // Now you're ready to decode the image. 
            // The image is rendered by stacking the layers and aligning the pixels with the same positions in each layer. 
            // The digits indicate the color of the corresponding pixel: 0 is black, 1 is white, and 2 is transparent.

            // The layers are rendered with the first layer in front and the last layer in back.
            // So, if a given position has a transparent pixel in the first and second layers, 
            // a black pixel in the third layer, and a white pixel in the fourth layer, the final image would have a black pixel at that position.

            // For example, given an image 2 pixels wide and 2 pixels tall, the image data 0222112222120000 corresponds to the following image layers:

            //Layer 1: 02
            //         22

            //Layer 2: 11
            //         22

            //Layer 3: 22
            //         12

            //Layer 4: 00
            //         00

            // Then, the full image can be found by determining the top visible pixel in each position:

            // The top-left pixel is black because the top layer is 0.
            // The top-right pixel is white because the top layer is 2(transparent), but the second layer is 1.
            // The bottom-left pixel is white because the top two layers are 2, but the third layer is 1.
            // The bottom-right pixel is black because the only visible pixel in that position is 0(from layer 4).

            // So, the final image looks like this:

            // 01
            // 10

            // What message is produced after decoding your image ?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day8_input.txt");

            string ln = "";
            ln = sr.ReadLine();
            sr.Close();

            int layer = 0;
            int[,] img = new int[6, 25];
            string[] imgrow = new string[150];

            for (int i = 0; i < ln.Length; i += 150)
            {

                string s = ln.Substring(i, 150);
                Debug.WriteLine(s);
                imgrow[layer] = s;
                layer++;
            }

            string outimg = "";
            string px = " ";
            string pxstr = "2";
            string outstr = "";

            char bl = '\u25A0';
            string bls = bl.ToString();
            for (int i = 0; i < 150; i++)
            {
                px = " ";
                for (int j = 0; j < layer; j++)
                {
                    string thispx = imgrow[j].Substring(i, 1);
                    if (thispx != "2")
                    {
                        if (thispx == "1")
                        {
                            px = "*";   // thispx;
                            px = bls;
                        }
                        else
                        {
                            px = " ";
                        }

                        pxstr = thispx;
                        break;
                    }

                }
                outimg += px;
                outstr += pxstr;
            }

            //Debug.WriteLine("done");
            // Debug.WriteLine(outstr);
            string ret = "";
            for (int i = 0; i <= 5; i++)
            {
                ret += outimg.Substring((i * 25), 25) + Environment.NewLine;

            }
            return ret;


        }

        public string day9_p1()
        {
            // ---Day 9: Sensor Boost ---
            // You've just said goodbye to the rebooted rover and left Mars when you receive a faint distress 
            // signal coming from the asteroid belt. It must be the Ceres monitoring station!

            // In order to lock on to the signal, you'll need to boost your sensors. The Elves send up the latest BOOST program - Basic Operation Of System Test.

            // While BOOST(your puzzle input) is capable of boosting your sensors, for tenuous safety reasons, 
            // it refuses to do so until the computer it runs on passes some checks to demonstrate it is a complete Intcode computer.

            // Your existing Intcode computer is missing one key feature: it needs support for parameters in relative mode.

            // Parameters in mode 2, relative mode, behave very similarly to parameters in position mode: 
            // the parameter is interpreted as a position. Like position mode, parameters in relative mode can be read from or written to.

            // The important difference is that relative mode parameters don't count from address 0. 
            // Instead, they count from a value called the relative base. The relative base starts at 0.

            // The address a relative mode parameter refers to is itself plus the current relative base.
            // When the relative base is 0, relative mode parameters and position mode parameters with the same value refer to the same address.

            // For example, given a relative base of 50, a relative mode parameter of - 7 refers to memory address 50 + -7 = 43.

            // The relative base is modified with the relative base offset instruction:

            // Opcode 9 adjusts the relative base by the value of its only parameter.
            // The relative base increases(or decreases, if the value is negative) by the value of the parameter.

            // For example, if the relative base is 2000, then after the instruction 109,19, 
            // the relative base would be 2019.If the next instruction were 204,-34, then the value at address 1985 would be output.

            // Your Intcode computer will also need a few other capabilities:

            // The computer's available memory should be much larger than the initial program.
            // Memory beyond the initial program starts with the value 0 and can be read or written like any other memory.
            // (It is invalid to try to access memory at a negative address, though.)
            // The computer should have support for large numbers. Some instructions near the beginning of the BOOST program will verify this capability.

            // Here are some example programs that use these features:

            // 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 takes no input and produces a copy of itself as output.
            // 1102, 34915192, 34915192, 7, 4, 7, 99, 0 should output a 16 - digit number.
            // 104, 1125899906842624, 99 should output the large number in the middle.

            // The BOOST program will ask for a single input; 
            // run it in test mode by providing it the value 1.
            // It will perform a series of checks on each opcode, 
            // output any opcodes(and the associated parameter modes) that seem to be functioning incorrectly, 
            // and finally output a BOOST keycode.

            // Once your Intcode computer is fully functional, 
            // the BOOST program should report no malfunctioning opcodes when run in test mode; 
            // it should only output a single value, the BOOST keycode.
            // What BOOST keycode does it produce?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day9_input.txt");

            string ln = sr.ReadLine();
            sr.Close();
            int pos = 0;
            long outp = 0;
            sr.Close();
            //ln = "109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99";
            // ln = "1102, 34915192, 34915192, 7, 4, 7, 99, 0";
            //ln = "104, 1125899906842624, 99";

            long[] inp = adv.GetComputerInput64(ln);

            outp = adv.Compute9a(inp, 1, 1, ref pos, false);

            return outp.ToString();
        }

        public string day9_p2()
        {
            // You now have a complete Intcode computer.

            // Finally, you can lock on to the Ceres distress signal!
            // You just need to boost your sensors using the BOOST program.

            // The program runs in sensor boost mode by providing the input instruction the value 2.
            // Once run, it will boost the sensors automatically, 
            // but it might take a few seconds to complete the operation on slower hardware.
            // In sensor boost mode, the program will output a single value: the coordinates of the distress signal.

            // Run the BOOST program in sensor boost mode.What are the coordinates of the distress signal?

            Stopwatch sw = new Stopwatch();
            sw.Start();

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day9_input.txt");

            string ln = sr.ReadLine();
            sr.Close();
            int pos = 0;
            long outp = 0;
            sr.Close();
            //ln = "109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99";
            // ln = "1102, 34915192, 34915192, 7, 4, 7, 99, 0";
            //ln = "104, 1125899906842624, 99";

            long[] inp = adv.GetComputerInput64(ln);

            outp = adv.Compute9a(inp, 2, 0, ref pos, false);

            string ret = "Answer: " + outp.ToString() + Environment.NewLine;
            ret += "Instruction Count: " + adv.instructionCount.ToString() + Environment.NewLine;
            ret += "Time: " + sw.ElapsedMilliseconds.ToString() + " milliseconds.";

            return ret;
        }

        public string day10_p1()
        {
            // ---Day 10: Monitoring Station ---
            // You fly into the asteroid belt and reach the Ceres monitoring station.The Elves here have an emergency: they're having trouble tracking all of the asteroids and can't be sure they're safe.

            // The Elves would like to build a new monitoring station in a nearby area of space; 
            // they hand you a map of all of the asteroids in that region(your puzzle input).

            // The map indicates whether each position is empty (.) or contains an asteroid(#). 
            // The asteroids are much smaller than they appear on the map, 
            // and every asteroid is exactly in the center of its marked position. 
            // The asteroids can be described with X,Y coordinates where X is the distance from the left edge and Y is the distance 
            // from the top edge (so the top-left corner is 0,0 and the position immediately to its right is 1,0).

            // Your job is to figure out which asteroid would be the best place to build a new monitoring station.
            // A monitoring station can detect any asteroid to which it has direct line of sight 
            // - that is, there cannot be another asteroid exactly between them. 
            // This line of sight can be at any angle, not just lines aligned to the grid or diagonally.
            // The best location is the asteroid that can detect the largest number of other asteroids.

            // For example, consider the following map:

            //.#..#
            //.....
            //#####
            //....#
            //...##

            // The best location for a new monitoring station on this map is the highlighted asteroid at 3, 4
            // because it can detect 8 asteroids, more than any other location. 
            // (The only asteroid it cannot detect is the one at 1, 0; its view of this asteroid is blocked by the asteroid at 2, 2.) 
            // All other asteroids are worse locations; they can detect 7 or fewer other asteroids. Here is the number of other asteroids
            // a monitoring station on each asteroid could detect:

            // .7..7
            // .....
            // 67775
            // ....7
            // ...87

            // Here is an asteroid (#) and some examples of the ways its line of sight might be blocked. 
            // If there were another asteroid at the location of a capital letter, the locations marked 
            // with the corresponding lowercase letter would be blocked and could not be detected:

            //#.........
            //...A......
            //...B..a...
            //.EDCG....a
            //..F.c.b...
            //.....c....
            //..efd.c.gb
            //.......c..
            //....f...c.
            //...e..d..c

            //Here are some larger examples:

            //            Best is 5,8 with 33 other asteroids detected:

            //    ......#.#.
            //    #..#.#....
            //    ..#######.
            //    .#.#.###..
            //    .#..#.....
            //    ..#....#.#
            //    #..#....#.
            //    .##.#..###
            //    ##...#..#.
            //    .#....####

            //    Best is 1,2 with 35 other asteroids detected:

            //    #.#...#.#.
            //    .###....#.
            //    .#....#...
            //    ##.#.#.#.#
            //    ....#.#.#.
            //    .##..###.#
            //    ..#...##..
            //    ..##....##
            //    ......#...
            //    .####.###.

            //    Best is 6,3 with 41 other asteroids detected:

            //    .#..#..###
            //    ####.###.#
            //    ....###.#.
            //    ..###.##.#
            //    ##.##.#.#.
            //    ....###..#
            //    ..#.#..#.#
            //    #..#.#.###
            //    .##...##.#
            //    .....#.#..

            //    Best is 11,13 with 210 other asteroids detected:

            //    .#..##.###...#######
            //    ##.############..##.
            //    .#.######.########.#
            //    .###.#######.####.#.
            //    #####.##.#.##.###.##
            //    ..#####..#.#########
            //    ####################
            //    #.####....###.#.#.##
            //    ##.#################
            //    #####.##.###..####..
            //    ..######..##.#######
            //    ####.##.####...##..#
            //    .#####..#.######.###
            //    ##...#.##########...
            //    #.##########.#######
            //    .####.#.###.###.#.##
            //    ....##.##.###..#####
            //    .#.#.###########.###
            //    #.#.#.#####.####.###
            //    ###.##.####.##.#..##

            // Find the best location for a new monitoring station.How many other asteroids can be detected from that location?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day10_input.txt");

            // ans != 295
            int max = 28;
            bool[,] grid = new bool[max, max];
            //string ln = sr.ReadLine();
            string ln = "";

            int i = 0;
            while ((ln = sr.ReadLine()) != null)
            {
                string outs = "";
                for (int j = 0; j < max; j++)
                {
                    string s = ln.Substring(j, 1);
                    outs += s + ",";
                    if (s == "#")
                    {
                        grid[j, i] = true;
                    }

                }
                Debug.WriteLine(outs);
                i++;
            }


            int maxcount = 0;
            for (int y = 0; y < max; y++)
            {
                for (int z = 0; z < max; z++)
                {
                    if (grid[y, z])
                    {
                        int ac = adv.AsteroidCount(grid, y, z, max);

                        Debug.WriteLine(y.ToString() + " - " + z.ToString() + " - " + ac.ToString());
                        if (ac > maxcount)
                        {
                            maxcount = ac;
                        }
                    }

                }
            }


            sr.Close();
            return maxcount.ToString();

        }
        public string day10_p2()
        {
            // Once you give them the coordinates, the Elves quickly deploy an Instant Monitoring Station to the location 
            // and discover the worst: there are simply too many asteroids.
            // The only solution is complete vaporization by giant laser.
            // Fortunately, in addition to an asteroid scanner, the new monitoring station also comes equipped with a giant rotating 
            // laser perfect for vaporizing asteroids. The laser starts by pointing up and always rotates clockwise, vaporizing any asteroid it hits.

            // If multiple asteroids are exactly in line with the station, the laser only has enough power 
            // to vaporize one of them before continuing its rotation.In other words, 
            // the same asteroids that can be detected can be vaporized, but if vaporizing one asteroid makes another one detectable,
            // the newly - detected asteroid won't be vaporized until the laser has returned to the same position by rotating a full 360 degrees.

            // For example, consider the following map, where the asteroid with the new monitoring station(and laser) is marked X:

            // .#....#####...#..
            // ##...##.#####..##
            // ##...#...#.#####.
            // ..#.....X...###..
            // ..#.#.....#....##

            // The first nine asteroids to get vaporized, in order, would be:

            // .#....###24...#..
            // ##...##.13#67..9#
            // ##...#...5.8####.
            // ..#.....X...###..
            // ..#.#.....#....##

            // Note that some asteroids(the ones behind the asteroids marked 1, 5, and 7) 
            // won't have a chance to be vaporized until the next full rotation. The laser continues rotating; the next nine to be vaporized are:

            //.#....###.....#..
            //##...##...#.....#
            //##...#......1234.
            //..#.....X...5##..
            //..#.9.....8....76

            //The next nine to be vaporized are then:

            // .8....###.....#..
            // 56...9#...#.....#
            // 34...7...........
            // ..2.....X....##..
            // ..1..............

            // Finally, the laser completes its first full rotation(1 through 3), 
            // a second rotation(4 through 8), and vaporizes the last asteroid(9) partway through its third rotation:

            //......234.....6..
            //......1...5.....7
            //.................
            //........X....89..
            //.................

            // In the large example above(the one with the best monitoring station location at 11, 13):

            // The 1st asteroid to be vaporized is at 11,12.
            // The 2nd asteroid to be vaporized is at 12,1.
            // The 3rd asteroid to be vaporized is at 12,2.
            // The 10th asteroid to be vaporized is at 12,8.
            // The 20th asteroid to be vaporized is at 16,0.
            // The 50th asteroid to be vaporized is at 16,9.
            // The 100th asteroid to be vaporized is at 10,16.
            // The 199th asteroid to be vaporized is at 9,6.
            // The 200th asteroid to be vaporized is at 8,2.
            // The 201st asteroid to be vaporized is at 10,9.
            // The 299th and final asteroid to be vaporized is at 11,1.

            // The Elves are placing bets on which will be the 200th asteroid to be vaporized.
            // Win the bet by determining which asteroid that will be; 
            // what do you get if you multiply its X coordinate by 100 and then add its Y coordinate? (For example, 8, 2 becomes 802.)

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day10_input.txt");

            // ans != 295
            int max = 28;
            bool[,] grid = new bool[max, max];
            double[,] atans = new double[max, max];
            //string ln = sr.ReadLine();
            string ln = "";
            int i = 0;
            //int tx = 11;
            //int ty = 13;
            int tx = 22;
            int ty = 19;
            while ((ln = sr.ReadLine()) != null)
            {
                string outs = "";
                for (int j = 0; j < max; j++)
                {
                    string s = ln.Substring(j, 1);
                    outs += s + ",";
                    if (s == "#")
                    {
                        grid[j, i] = true;
                    }

                }
                //Debug.WriteLine(outs);
                i++;
            }

            Dictionary<Tuple<int, int>, double> d1 = new Dictionary<Tuple<int, int>, double>();
            Dictionary<Tuple<int, int>, double> d2 = new Dictionary<Tuple<int, int>, double>();
            Dictionary<Tuple<int, int>, double> d3 = new Dictionary<Tuple<int, int>, double>();
            Dictionary<Tuple<int, int>, double> d4 = new Dictionary<Tuple<int, int>, double>();

            atans = adv.CalcAngles(grid, max, tx, ty, d1, d2, d3, d4);


            d1 = d1.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            d2 = d2.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            d3 = d3.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            d4 = d4.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);




            int ac = adv.AsteroidKill(grid, tx, ty, max, 200, d2, d3, d4, d1);



            return ac.ToString();
        }

        public string day11_p1()
        {
            //    ---Day 11: Space Police ---
            //  On the way to Jupiter, you're pulled over by the Space Police.

            // "Attention, unmarked spacecraft! You are in violation of Space Law! 
            // All spacecraft must have a clearly visible registration identifier! You have 24 hours to comply or be sent to Space Jail!"

            // Not wanting to be sent to Space Jail, you radio back to the Elves on Earth for help.
            // Although it takes almost three hours for their reply signal to reach you,
            // they send instructions for how to power up the emergency hull painting robot and even provide a small Intcode program(your puzzle input) 
            // that will cause it to paint your ship appropriately.

            // There's just one problem: you don't have an emergency hull painting robot.

            // You'll need to build a new emergency hull painting robot. 
            // The robot needs to be able to move around on the grid of square panels on the side of your ship, 
            // detect the color of its current panel, and paint its current panel black or white. (All of the panels are currently black.)

            // The Intcode program will serve as the brain of the robot.The program uses input instructions to access the robot's camera:
            // provide 0 if the robot is over a black panel or 1 if the robot is over a white panel. Then, the program will output two values:

            //  First, it will output a value indicating the color to paint the panel the robot is over: 0 means to paint the panel black, and 1 means to paint the panel white.
            //  Second, it will output a value indicating the direction the robot should turn: 0 means it should turn left 90 degrees, and 1 means it should turn right 90 degrees.

            // After the robot turns, it should always move forward exactly one panel.The robot starts facing up.

            // The robot will continue running for a while like this and halt when it is finished drawing. Do not restart the Intcode computer inside the robot during this process.

            // For example, suppose the robot is about to start running.
            // Drawing black panels as ., white panels as #, and the robot pointing the direction it is facing (< ^ > v), the initial state and region near the robot looks like this:

            // .....
            // .....
            // ..^..
            // .....
            // .....

            // The panel under the robot(not visible here because a ^ is shown instead) is also black, 
            // and so any input instructions at this point should be provided 0.
            // Suppose the robot eventually outputs 1(paint white) and then 0 (turn left). After taking these actions and moving forward one panel, the region now looks like this:

            // .....
            // .....
            // .<#..
            // .....
            // .....

            //Input instructions should still be provided 0.Next, the robot might output 0 (paint black) and then 0(turn left):

            //.....
            //.....
            //..#..
            //.v...
            //.....

            //After more outputs(1, 0, 1, 0):

            //.....
            //.....
            //..^..
            //.##..
            //.....

            //The robot is now back where it started, but because it is now on a white panel, 
            // input instructions should be provided 1.After several more outputs(0, 1, 1, 0, 1, 0), the area looks like this:

            //.....
            //..<#.
            //...#.
            //.##..
            //.....

            // Before you deploy the robot, you should probably have an estimate of the area it will cover: 
            // specifically, you need to know the number of panels it paints at least once, 
            // regardless of color.In the example above, the robot painted 6 panels at least once. 
            // (It painted its starting panel twice, but that panel is still only counted once; it also never painted the panel it ended on.)

            // Build a new emergency hull painting robot and run the Intcode program on it. How many panels does it paint at least once ?

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day11_input.txt");
            string ln = sr.ReadLine();

            long[] inp = adv.GetComputerInput64(ln);

            string ret = adv.PaintGrid(inp, 0);

            //int pos = 0;
            //int max = 10000;
            //string[,] grid = new string[max, max];
            //bool[,] paintgrid = new bool[max, max];
            //int x = max / 2;
            //int y = max / 2;
            //int painted = 0;
            //long outcol = 0;
            //long outmove = 0;
            //string curdir = "^";
            //int restartpos = 0;


            //while (true)
            //{
            //    int register = 0;
            //    if (grid[x,y]=="#")
            //    {
            //        register = 1;
            //    }
            //    outcol = adv.Compute9a(inp, 0, register, ref restartpos, true);
            //    if (restartpos == -1)
            //    {
            //        break;
            //    }
            //    outmove = adv.Compute9a(inp, 0, register, ref restartpos, true);


            //    if (grid[x, y] is null) grid[x, y] = ".";

            //    if (outcol == 1)
            //    {
            //        if (grid[x,y]==".")
            //        {
            //            grid[x, y] = "#";
            //        }
            //    }
            //    else
            //    {
            //        grid[x, y] = ".";
            //    }
            //    if (!paintgrid[x, y])
            //    {
            //        paintgrid[x, y] = true;
            //        painted++;
            //    }

            //    if (outmove == 1)
            //    {
            //        switch (curdir)
            //        {
            //            case "^":
            //                curdir = ">";
            //                break;
            //            case ">":
            //                curdir = "v";
            //                break;
            //            case "v":
            //                curdir = "<";
            //                break;
            //            case "<":
            //                curdir = "^";
            //                break;
            //        }
            //    }
            //    else if (outmove == 0)
            //    {
            //        switch (curdir)
            //        {
            //            case "^":
            //                curdir = "<";
            //                break;
            //            case "<":
            //                curdir = "v";
            //                break;
            //            case "v":
            //                curdir = ">";
            //                break;
            //            case ">":
            //                curdir = "^";
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        Debug.WriteLine("huh?");
            //    }

            //    switch (curdir)
            //    {
            //        case "^":
            //            y--;
            //            break;
            //        case ">":
            //            x++;
            //            break;
            //        case "v":
            //            y++;
            //            break;
            //        case "<":
            //            x--;
            //            break;
            //    }


            //    if (restartpos == -1)
            //    {
            //        break;
            //    }

            //}
            //int doublecount = 0;
            //for (int a = 0; a< max;a++)
            //{
            //    for (int b = 0; b < max; b++)
            //    {
            //        if (paintgrid[a,b])
            //        {
            //            doublecount++;
            //        }
            //    }
            //}
            return ret;
        }
        public string day11_p2()
        {
            // You're not sure what it's trying to paint, but it's definitely not a registration identifier. The Space Police are getting impatient.

            // Checking your external ship cameras again, you notice a white panel marked "emergency hull painting robot starting panel".
            // The rest of the panels are still black, but it looks like the robot was expecting to start on a white panel, not a black one.

            // Based on the Space Law Space Brochure that the Space Police attached to one of your windows, 
            // a valid registration identifier is always eight capital letters.
            // After starting the robot on a single white panel instead, what registration identifier does it paint on your hull?

            // Although it hasn't changed, you can still get your puzzle input.
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day11_input.txt");
            string ln = sr.ReadLine();

            long[] inp = adv.GetComputerInput64(ln);

            string ret = adv.PaintGrid(inp, 1);

            return ret;
        }
        public string day12_p1()
        {
            // ---Day 12: The N-Body Problem-- -
            // The space near Jupiter is not a very safe place; you need to be careful of a big distracting red spot,
            // extreme radiation, and a whole lot of moons swirling around.You decide to start by tracking the four largest moons: Io, Europa, Ganymede, and Callisto.
            // After a brief scan, you calculate the position of each moon(your puzzle input). You just need to simulate their motion so you can avoid them.

            // Each moon has a 3 - dimensional position(x, y, and z) and a 3 - dimensional velocity.
            // The position of each moon is given in your scan; the x, y, and z velocity of each moon starts at 0.

            // Simulate the motion of the moons in time steps. Within each time step, first update the velocity of every moon by applying gravity.
            // Then, once all moons' velocities have been updated, update the position of every moon by applying velocity.
            // Time progresses by one step once all of the positions are updated.

            // To apply gravity, consider every pair of moons.
            // On each axis(x, y, and z), the velocity of each moon changes by exactly +1 or - 1 to pull the moons together.
            // For example, if Ganymede has an x position of 3, and Callisto has a x position of 5, 
            // then Ganymede's x velocity changes by +1 (because 5 > 3) and Callisto's x velocity changes by - 1 (because 3 < 5).
            // However, if the positions on a given axis are the same, the velocity on that axis does not change for that pair of moons.

            // Once all gravity has been applied, apply velocity: simply add the velocity of each moon to its own position.For example, 
            // if Europa has a position of x = 1, y = 2, z = 3 and a velocity of x = -2, y = 0,z = 3, 
            // then its new position would be x = -1, y = 2, z = 6.
            // This process does not modify the velocity of any moon.

            // For example, suppose your scan reveals the following positions:

            // < x = -1, y = 0, z = 2 >
            // < x = 2, y = -10, z = -7 >
            // < x = 4, y = -8, z = 8 >
            // < x = 3, y = 5, z = -1 >

            // Simulating the motion of these moons would produce the following:

            // After 0 steps:
            //pos =< x = -1, y = 0, z = 2 >, vel =< x = 0, y = 0, z = 0 >
            //pos =< x = 2, y = -10, z = -7 >, vel =< x = 0, y = 0, z = 0 >
            //pos =< x = 4, y = -8, z = 8 >, vel =< x = 0, y = 0, z = 0 >
            //pos =< x = 3, y = 5, z = -1 >, vel =< x = 0, y = 0, z = 0 >

            //                                          After 1 step:
            //pos =< x = 2, y = -1, z = 1 >, vel =< x = 3, y = -1, z = -1 >
            //            pos =< x = 3, y = -7, z = -4 >, vel =< x = 1, y = 3, z = 3 >
            //                        pos =< x = 1, y = -7, z = 5 >, vel =< x = -3, y = 1, z = -3 >
            //                                    pos =< x = 2, y = 2, z = 0 >, vel =< x = -1, y = -3, z = 1 >

            //                                                After 2 steps:
            //pos =< x = 5, y = -3, z = -1 >, vel =< x = 3, y = -2, z = -2 >
            //             pos =< x = 1, y = -2, z = 2 >, vel =< x = -2, y = 5, z = 6 >
            //                         pos =< x = 1, y = -4, z = -1 >, vel =< x = 0, y = 3, z = -6 >
            //                                     pos =< x = 1, y = -4, z = 2 >, vel =< x = -1, y = -6, z = 2 >

            //                                                  After 3 steps:
            //pos =< x = 5, y = -6, z = -1 >, vel =< x = 0, y = -3, z = 0 >
            //             pos =< x = 0, y = 0, z = 6 >, vel =< x = -1, y = 2, z = 4 >
            //                        pos =< x = 2, y = 1, z = -5 >, vel =< x = 1, y = 5, z = -4 >
            //                                   pos =< x = 1, y = -8, z = 2 >, vel =< x = 0, y = -4, z = 0 >

            //                                               After 4 steps:
            //pos =< x = 2, y = -8, z = 0 >, vel =< x = -3, y = -2, z = 1 >
            //             pos =< x = 2, y = 1, z = 7 >, vel =< x = 2, y = 1, z = 1 >
            //                       pos =< x = 2, y = 3, z = -6 >, vel =< x = 0, y = 2, z = -1 >
            //                                  pos =< x = 2, y = -9, z = 1 >, vel =< x = 1, y = -1, z = -1 >

            //                                              After 5 steps:
            //pos =< x = -1, y = -9, z = 2 >, vel =< x = -3, y = -1, z = 2 >
            //              pos =< x = 4, y = 1, z = 5 >, vel =< x = 2, y = 0, z = -2 >
            //                        pos =< x = 2, y = 2, z = -4 >, vel =< x = 0, y = -1, z = 2 >
            //                                    pos =< x = 3, y = -7, z = -1 >, vel =< x = 1, y = 2, z = -2 >

            //                                                After 6 steps:
            //pos =< x = -1, y = -7, z = 3 >, vel =< x = 0, y = 2, z = 1 >
            //            pos =< x = 3, y = 0, z = 0 >, vel =< x = -1, y = -1, z = -5 >
            //                        pos =< x = 3, y = -2, z = 1 >, vel =< x = 1, y = -4, z = 5 >
            //                                    pos =< x = 3, y = -4, z = -2 >, vel =< x = 0, y = 3, z = -1 >

            //                                                After 7 steps:
            //pos =< x = 2, y = -2, z = 1 >, vel =< x = 3, y = 5, z = -2 >
            //           pos =< x = 1, y = -4, z = -4 >, vel =< x = -2, y = -4, z = -4 >
            //                         pos =< x = 3, y = -7, z = 5 >, vel =< x = 0, y = -5, z = 4 >
            //                                     pos =< x = 2, y = 0, z = 0 >, vel =< x = -1, y = 4, z = 2 >

            //                                                After 8 steps:
            //pos =< x = 5, y = 2, z = -2 >, vel =< x = 3, y = 4, z = -3 >
            //           pos =< x = 2, y = -7, z = -5 >, vel =< x = 1, y = -3, z = -1 >
            //                        pos =< x = 0, y = -9, z = 6 >, vel =< x = -3, y = -2, z = 1 >
            //                                     pos =< x = 1, y = 1, z = 3 >, vel =< x = -1, y = 1, z = 3 >

            //                                                After 9 steps:
            //pos =< x = 5, y = 3, z = -4 >, vel =< x = 0, y = 1, z = -2 >
            //           pos =< x = 2, y = -9, z = -3 >, vel =< x = 0, y = -2, z = 2 >
            //                        pos =< x = 0, y = -8, z = 4 >, vel =< x = 0, y = 1, z = -2 >
            //                                   pos =< x = 1, y = 1, z = 5 >, vel =< x = 0, y = 0, z = 2 >

            //                                             After 10 steps:
            //pos =< x = 2, y = 1, z = -3 >, vel =< x = -3, y = -2, z = 1 >
            //             pos =< x = 1, y = -8, z = 0 >, vel =< x = -1, y = 1, z = 3 >
            //                         pos =< x = 3, y = -6, z = 1 >, vel =< x = 3, y = 2, z = -3 >
            //                                    pos =< x = 2, y = 0, z = 4 >, vel =< x = 1, y = -1, z = -1 >

            // Then, it might help to calculate the total energy in the system.
            // The total energy for a single moon is its potential energy multiplied by its kinetic energy.
            // A moon's potential energy is the sum of the absolute values of its x, y, and z position coordinates.
            // A moon's kinetic energy is the sum of the absolute values of its velocity coordinates.
            // Below, each line shows the calculations for a moon's potential energy (pot), kinetic energy (kin), and total energy:

            //  Energy after 10 steps:
            //pot: 2 + 1 + 3 = 6; kin: 3 + 2 + 1 = 6; total: 6 * 6 = 36
            //pot: 1 + 8 + 0 = 9; kin: 1 + 1 + 3 = 5; total: 9 * 5 = 45
            //pot: 3 + 6 + 1 = 10; kin: 3 + 2 + 3 = 8; total: 10 * 8 = 80
            //pot: 2 + 0 + 4 = 6; kin: 1 + 1 + 1 = 3; total: 6 * 3 = 18
            //Sum of total energy: 36 + 45 + 80 + 18 = 179

            // In the above example, adding together the total energy for all moons after 10 steps produces the total energy in the system, 179.

            //Here's a second example:

            //< x = -8, y = -10, z = 0 >
            //< x = 5, y = 5, z = 10 >
            //< x = 2, y = -7, z = 3 >
            //< x = 9, y = -8, z = -3 >

            //Every ten steps of simulation for 100 steps produces:

            //After 0 steps:
            //pos =< x = -8, y = -10, z = 0 >, vel =< x = 0, y = 0, z = 0 >
            //pos =< x = 5, y = 5, z = 10 >, vel =< x = 0, y = 0, z = 0 >
            //pos =< x = 2, y = -7, z = 3 >, vel =< x = 0, y = 0, z = 0 >
            //pos =< x = 9, y = -8, z = -3 >, vel =< x = 0, y = 0, z = 0 >

            //After 10 steps:
            //pos =< x = -9, y = -10, z = 1 >, vel =< x = -2, y = -2, z = -1 >
            //pos =< x = 4, y = 10, z = 9 >, vel =< x = -3, y = 7, z = -2 >
            //pos =< x = 8, y = -10, z = -3 >, vel =< x = 5, y = -1, z = -2 >
            //pos =< x = 5, y = -10, z = 3 >, vel =< x = 0, y = -4, z = 5 >

            //After 20 steps:
            //pos =< x = -10, y = 3, z = -4 >, vel =< x = -5, y = 2, z = 0 >
            //pos =< x = 5, y = -25, z = 6 >, vel =< x = 1, y = 1, z = -4 >
            //pos =< x = 13, y = 1, z = 1 >, vel =< x = 5, y = -2, z = 2 >
            //pos =< x = 0, y = 1, z = 7 >, vel =< x = -1, y = -1, z = 2 >

            //After 30 steps:
            //pos =< x = 15, y = -6, z = -9 >, vel =< x = -5, y = 4, z = 0 >
            //pos =< x = -4, y = -11, z = 3 >, vel =< x = -3, y = -10, z = 0 >
            //pos =< x = 0, y = -1, z = 11 >, vel =< x = 7, y = 4, z = 3 >
            //pos =< x = -3, y = -2, z = 5 >, vel =< x = 1, y = 2, z = -3 >

            //After 40 steps:
            //pos =< x = 14, y = -12, z = -4 >, vel =< x = 11, y = 3, z = 0 >
            //pos =< x = -1, y = 18, z = 8 >, vel =< x = -5, y = 2, z = 3 >
            //pos =< x = -5, y = -14, z = 8 >, vel =< x = 1, y = -2, z = 0 >
            //pos =< x = 0, y = -12, z = -2 >, vel =< x = -7, y = -3, z = -3 >

            //After 50 steps:
            //pos =< x = -23, y = 4, z = 1 >, vel =< x = -7, y = -1, z = 2 >
            //pos =< x = 20, y = -31, z = 13 >, vel =< x = 5, y = 3, z = 4 >
            //pos =< x = -4, y = 6, z = 1 >, vel =< x = -1, y = 1, z = -3 >
            //pos =< x = 15, y = 1, z = -5 >, vel =< x = 3, y = -3, z = -3 >

            //After 60 steps:
            //pos =< x = 36, y = -10, z = 6 >, vel =< x = 5, y = 0, z = 3 >
            //pos =< x = -18, y = 10, z = 9 >, vel =< x = -3, y = -7, z = 5 >
            //pos =< x = 8, y = -12, z = -3 >, vel =< x = -2, y = 1, z = -7 >
            //pos =< x = -18, y = -8, z = -2 >, vel =< x = 0, y = 6, z = -1 >

            //After 70 steps:
            //pos =< x = -33, y = -6, z = 5 >, vel =< x = -5, y = -4, z = 7 >
            //pos =< x = 13, y = -9, z = 2 >, vel =< x = -2, y = 11, z = 3 >
            //pos =< x = 11, y = -8, z = 2 >, vel =< x = 8, y = -6, z = -7 >
            //pos =< x = 17, y = 3, z = 1 >, vel =< x = -1, y = -1, z = -3 >

            //After 80 steps:
            //pos =< x = 30, y = -8, z = 3 >, vel =< x = 3, y = 3, z = 0 >
            //pos =< x = -2, y = -4, z = 0 >, vel =< x = 4, y = -13, z = 2 >
            //pos =< x = -18, y = -7, z = 15 >, vel =< x = -8, y = 2, z = -2 >
            //pos =< x = -2, y = -1, z = -8 >, vel =< x = 1, y = 8, z = 0 >

            //After 90 steps:
            //pos =< x = -25, y = -1, z = 4 >, vel =< x = 1, y = -3, z = 4 >
            //pos =< x = 2, y = -9, z = 0 >, vel =< x = -3, y = 13, z = -1 >
            //pos =< x = 32, y = -8, z = 14 >, vel =< x = 5, y = -4, z = 6 >
            //pos =< x = -1, y = -2, z = -8 >, vel =< x = -3, y = -6, z = -9 >

            //After 100 steps:
            //pos =< x = 8, y = -12, z = -9 >, vel =< x = -7, y = 3, z = 0 >
            //pos =< x = 13, y = 16, z = -3 >, vel =< x = 3, y = -11, z = -5 >
            //pos =< x = -29, y = -11, z = -1 >, vel =< x = -3, y = 7, z = 4 >
            //pos =< x = 16, y = -13, z = 23 >, vel =< x = 7, y = 1, z = 1 >

            //Energy after 100 steps:
            //pot:  8 + 12 + 9 = 29; kin: 7 + 3 + 0 = 10; total: 29 * 10 = 290
            //pot: 13 + 16 + 3 = 32; kin: 3 + 11 + 5 = 19; total: 32 * 19 = 608
            //pot: 29 + 11 + 1 = 41; kin: 3 + 7 + 4 = 14; total: 41 * 14 = 574
            //pot: 16 + 13 + 23 = 52; kin: 7 + 1 + 1 = 9; total: 52 * 9 = 468
            //Sum of total energy: 290 + 608 + 574 + 468 = 1940

            //What is the total energy in the system after simulating the moons given in your scan for 1000 steps ?

            // <x = 3, y = 15, z = 8 >
            //< x = 5, y = -1, z = -2 >
            //< x = -10, y = 8, z = 2 >
            //< x = 8, y = 4, z = -5 >
            AdventShared adv = new AdventShared();

            Dictionary<int, Tuple<int, int, int>> moonpos = new Dictionary<int, Tuple<int, int, int>>();
            Dictionary<int, Tuple<int, int, int>> moonvel = new Dictionary<int, Tuple<int, int, int>>();

            Tuple<int, int, int> moon;
            Tuple<int, int, int> vel;


            moon = new Tuple<int, int, int>(3, 15, 8);
            moonpos.Add(0, moon);

            moon = new Tuple<int, int, int>(5, -1, -2);
            moonpos.Add(1, moon);

            moon = new Tuple<int, int, int>(-10, 8, 2);
            moonpos.Add(2, moon);

            moon = new Tuple<int, int, int>(8, 4, -5);
            moonpos.Add(3, moon);

            vel = new Tuple<int, int, int>(0, 0, 0);
            moonvel.Add(0, vel);
            moonvel.Add(1, vel);
            moonvel.Add(2, vel);
            moonvel.Add(3, vel);

            for (int steps = 1; steps <= 1000; steps++)
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int y = x + 1; y < 4; y++)
                    {
                        moonvel[x] = adv.UpdateMoonVelocity(moonpos[x], moonpos[y], moonvel[x]);
                        moonvel[y] = adv.UpdateMoonVelocity(moonpos[y], moonpos[x], moonvel[y]);

                    }
                }
                for (int x = 0; x < 4; x++)
                {
                    moonpos[x] = adv.UpdateMoonPositions(moonpos[x], moonvel[x]);
                }
            }

            int pe = 0;
            int ke = 0;
            int totenergy = 0;

            for (int x = 0; x < 4; x++)
            {
                pe = Math.Abs(moonpos[x].Item1) + Math.Abs(moonpos[x].Item2) + Math.Abs(moonpos[x].Item3);
                ke = Math.Abs(moonvel[x].Item1) + Math.Abs(moonvel[x].Item2) + Math.Abs(moonvel[x].Item3);

                totenergy += pe * ke;
            }

            return (totenergy).ToString();
        }


        public string day12_p2()
        {
            //            ---Part Two-- -

            //All this drifting around in space makes you wonder about the nature of the universe. Does history really repeat itself? You're curious whether the moons will ever return to a previous state.

            //Determine the number of steps that must occur before all of the moons' positions and velocities exactly match a previous point in time.

            //For example, the first example above takes 2772 steps before they exactly match a previous point in time; it eventually returns to the initial state:

            //            After 0 steps:
            //            pos =< x = -1, y = 0, z = 2 >, vel =< x = 0, y = 0, z = 0 >
            //                  pos =< x = 2, y = -10, z = -7 >, vel =< x = 0, y = 0, z = 0 >
            //                          pos =< x = 4, y = -8, z = 8 >, vel =< x = 0, y = 0, z = 0 >
            //                                pos =< x = 3, y = 5, z = -1 >, vel =< x = 0, y = 0, z = 0 >

            //                                      After 2770 steps:
            //            pos =< x = 2, y = -1, z = 1 >, vel =< x = -3, y = 2, z = 2 >
            //                   pos =< x = 3, y = -7, z = -4 >, vel =< x = 2, y = -5, z = -6 >
            //                           pos =< x = 1, y = -7, z = 5 >, vel =< x = 0, y = -3, z = 6 >
            //                                  pos =< x = 2, y = 2, z = 0 >, vel =< x = 1, y = 6, z = -2 >

            //                                       After 2771 steps:
            //            pos =< x = -1, y = 0, z = 2 >, vel =< x = -3, y = 1, z = 1 >
            //                   pos =< x = 2, y = -10, z = -7 >, vel =< x = -1, y = -3, z = -3 >
            //                             pos =< x = 4, y = -8, z = 8 >, vel =< x = 3, y = -1, z = 3 >
            //                                    pos =< x = 3, y = 5, z = -1 >, vel =< x = 1, y = 3, z = -1 >

            //                                          After 2772 steps:
            //            pos =< x = -1, y = 0, z = 2 >, vel =< x = 0, y = 0, z = 0 >
            //                  pos =< x = 2, y = -10, z = -7 >, vel =< x = 0, y = 0, z = 0 >
            //                          pos =< x = 4, y = -8, z = 8 >, vel =< x = 0, y = 0, z = 0 >
            //                                pos =< x = 3, y = 5, z = -1 >, vel =< x = 0, y = 0, z = 0 >

            //                                      Of course, the universe might last for a very long time before repeating.Here's a copy of the second example from above:

            //                                      < x = -8, y = -10, z = 0 >
            //                                      < x = 5, y = 5, z = 10 >
            //                                      < x = 2, y = -7, z = 3 >
            //                                      < x = 9, y = -8, z = -3 >

            //                                      This set of initial positions takes 4686774924 steps before it repeats a previous state!Clearly, you might need to find a more efficient way to simulate the universe.


            //                                      How many steps does it take to reach the first state that exactly matches a previous state ?

            AdventShared adv = new AdventShared();


            int[] startpos = new int[4];

            startpos[0] = 3;
            startpos[1] = 5;
            startpos[2] = -10;
            startpos[3] = 8;
            long x = adv.FindAxisRepeat(startpos);

            startpos[0] = 15;
            startpos[1] = -1;
            startpos[2] = 8;
            startpos[3] = 4;
            long y = adv.FindAxisRepeat(startpos);

            startpos[0] = 8;
            startpos[1] = -2;
            startpos[2] = 2;
            startpos[3] = -5;
            long z = adv.FindAxisRepeat(startpos);

            //long reset = adv.FindMoonRepeat(x, y, z);
            long p1 = adv.LCM(x, y);
            long reset = adv.LCM(z, p1);

            return reset.ToString();

        }

        public string day13_p1()
        {

            // ---Day 13: Care Package ---

            //  As you ponder the solitude of space and the ever - increasing three - hour roundtrip for messages between you and Earth, 
            // you notice that the Space Mail Indicator Light is blinking.To help keep you sane, the Elves have sent you a care package.

            //   It's a new game for the ship's arcade cabinet! Unfortunately, the arcade is all the way on the other end of the ship.
            // Surely, it won't be hard to build your own - the care package even comes with schematics.
            //   The arcade cabinet runs Intcode software like the game the Elves sent(your puzzle input).
            // It has a primitive screen capable of drawing square tiles on a grid.
            // The software draws tiles to the screen with output instructions: every three output instructions specify the x position(distance from the left), 
            // y position(distance from the top), and tile id.The tile id is interpreted as follows:

            // 0 is an empty tile.No game object appears in this tile.
            // 1 is a wall tile.Walls are indestructible barriers.
            // 2 is a block tile.Blocks can be broken by the ball.
            // 3 is a horizontal paddle tile.The paddle is indestructible.
            // 4 is a ball tile.The ball moves diagonally and bounces off objects.

            // For example, a sequence of output values like 1, 2, 3, 6, 5, 4 would draw a horizontal paddle tile(1 tile from the left 
            // and 2 tiles from the top) and a ball tile(6 tiles from the left and 5 tiles from the top).

            //  Start the game.How many block tiles are on the screen when the game exits ?
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day13_input.txt");

            string ln = sr.ReadLine();
            sr.Close();
            long[] inp = adv.GetComputerInput64(ln);

            long blocks = adv.CountBlocks(inp);
            return blocks.ToString();
        }
        public string day13_p2()
        {
            // ---Part Two-- -
            // The game didn't run because you didn't put in any quarters. 
            // Unfortunately, you did not bring any quarters. Memory address 0 represents the number of quarters that have been inserted; set it to 2 to play for free.

            // The arcade cabinet has a joystick that can move left and right.The software reads the position of the joystick with input instructions:
            //   If the joystick is in the neutral position, provide 0.

            // If the joystick is tilted to the left, provide - 1.
            // If the joystick is tilted to the right, provide 1.
            // The arcade cabinet also has a segment display capable of showing a single number that represents the player's current score. 
            // When three output instructions specify X=-1, Y=0, the third output instruction is not a tile; 
            // the value instead specifies the new score to show in the segment display. 
            // For example, a sequence of output values like -1,0,12345 would show 12345 as the player's current score.
            // Beat the game by breaking all the blocks.What is your score after the last block is broken?
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day13_input.txt");

            string ln = sr.ReadLine();
            sr.Close();
            long[] inp = adv.GetComputerInput64(ln);
            long ballx = 0;
            long bally = 0;
            
            long score = 0;
            long round = 0;
            long blocks = 999;
            long paddlex = 0;
            inp[0] = 2;

            while (blocks > 0)
            {
                round++;
                
                // joystick = -1;      // ^not sure why, but this is what's needed for the app to finish.

                blocks = adv.PlayGame(inp, ref ballx, ref bally, ref paddlex, ref score);
                string outs = "Round: " + round.ToString() + " Ball x:" + ballx.ToString() + ", Ball Y: " + bally.ToString();
                outs += ", Paddle: " + paddlex.ToString() + ", Blocks: " + blocks.ToString() + ", Score: " + score.ToString();
                Debug.WriteLine(outs);
            }



            return score.ToString();


        }

        public string day14_p1()
        {
            // ---Day 14: Space Stoichiometry ---
            // As you approach the rings of Saturn, your ship's low fuel indicator turns on. There isn't any fuel here, 
            // but the rings have plenty of raw material.Perhaps your ship's Inter-Stellar Refinery Union brand nanofactory can turn these raw materials into fuel.

            //You ask the nanofactory to produce a list of the reactions it can perform that are relevant to this process(your puzzle input).Every reaction turns some quantities of specific input chemicals into some quantity of an output chemical.Almost every chemical is produced by exactly one reaction; the only exception, ORE, is the raw material input to the entire process and is not produced by a reaction.

            //You just need to know how much ORE you'll need to collect before you can produce one unit of FUEL.

            //Each reaction gives specific quantities for its inputs and output; reactions cannot be partially run, so only whole integer multiples of these quantities can be used. (It's okay to have leftover chemicals when you're done, though.) For example, the reaction 1 A, 2 B, 3 C => 2 D means that exactly 2 units of chemical D can be produced by consuming exactly 1 A, 2 B and 3 C.You can run the full reaction as many times as necessary; for example, you could produce 10 D by consuming 5 A, 10 B, and 15 C.


            //Suppose your nanofactory produces the following list of reactions:


            //10 ORE => 10 A
            //1 ORE => 1 B
            //7 A, 1 B => 1 C
            //7 A, 1 C => 1 D
            //7 A, 1 D => 1 E
            //7 A, 1 E => 1 FUEL


            //The first two reactions use only ORE as inputs; they indicate that you can produce as much of chemical A as you want(in increments of 10 units, each 10 costing 10 ORE) and as much of chemical B as you want(each costing 1 ORE).To produce 1 FUEL, a total of 31 ORE is required: 1 ORE to produce 1 B, then 30 more ORE to produce the 7 + 7 + 7 + 7 = 28 A(with 2 extra A wasted) required in the reactions to convert the B into C, C into D, D into E, and finally E into FUEL. (30 A is produced because its reaction requires that it is created in increments of 10.)

            //Or, suppose you have the following list of reactions:

            //9 ORE => 2 A
            //8 ORE => 3 B
            //7 ORE => 5 C
            //3 A, 4 B => 1 AB
            //5 B, 7 C => 1 BC
            //4 C, 1 A => 1 CA
            //2 AB, 3 BC, 4 CA => 1 FUEL

            //The above list of reactions requires 165 ORE to produce 1 FUEL:

            //                Consume 45 ORE to produce 10 A.
            //                Consume 64 ORE to produce 24 B.
            //                Consume 56 ORE to produce 40 C.
            //                Consume 6 A, 8 B to produce 2 AB.
            //                Consume 15 B, 21 C to produce 3 BC.
            //                Consume 16 C, 4 A to produce 4 CA.
            //                Consume 2 AB, 3 BC, 4 CA to produce 1 FUEL.

            //Here are some larger examples:

            //                13312 ORE for 1 FUEL:


            //                157 ORE => 5 NZVS
            //                165 ORE => 6 DCFZ
            //                44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
            //                12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
            //                179 ORE => 7 PSHF
            //                177 ORE => 5 HKGWZ

            //                7 DCFZ, 7 PSHF => 2 XJWVT

            //                165 ORE => 2 GPVTF

            //                3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT


            //                180697 ORE for 1 FUEL:


            //                2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG

            //                17 NVRVD, 3 JNWZP => 8 VPVL

            //                53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL

            //                22 VJHF, 37 MNCFX => 5 FWMGM

            //                139 ORE => 4 NVRVD

            //                144 ORE => 7 JNWZP

            //                5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC

            //                5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV

            //                145 ORE => 6 MNCFX

            //                1 NVRVD => 8 CXFTF

            //                1 VJHF, 6 MNCFX => 4 RFSQX

            //                176 ORE => 6 VJHF


            //                2210736 ORE for 1 FUEL:


            //                171 ORE => 8 CNZTR

            //                7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL

            //                114 ORE => 4 BHXH

            //                14 VRPVC => 6 BMBT

            //                6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL

            //                6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT

            //                15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW

            //                13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW

            //                5 BMBT => 4 WPTQ

            //                189 ORE => 9 KTJDG

            //                1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP

            //                12 VRPVC, 27 CNZTR => 2 XDBXC

            //                15 KTJDG, 12 BHXH => 5 XCVML

            //                3 BHXH, 2 VRPVC => 7 MZWV

            //                121 ORE => 7 VRPVC

            //                7 XCVML => 6 RJRHP

            //                5 BHXH, 4 VRPVC => 5 LTCX


            //            Given the list of reactions in your puzzle input, what is the minimum amount of ORE required to produce exactly 1 FUEL ?


            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day14_input.txt") ;
            // 2155881 too high
            // 9944133 too high
            // answers:
           //  337862   P2: 3687786
            string ln = "";
            Dictionary<string, string> reactions = new Dictionary<string, string>();
            Dictionary<string, long> amounts = new Dictionary<string, long>();
            //Dictionary<string, int> elements = new Dictionary<string, int>();
            string[] ch = new string[59];       // 59 for actual
            int pos = 0;
            while ((ln = sr.ReadLine())!=null)
            {
                int pos1 = ln.IndexOf("=>");
                int pos2 = ln.Length;
                string p = ln.Substring(pos1+3, pos2 - (pos1+3));
                
                string[] ps = p.Split(' ');
                amounts.Add(ps[1], int.Parse(ps[0]));

                
                string e = ln.Substring(0, pos1-1);

                reactions.Add(ps[1], e);
                ch[pos] = ln;
                pos++;
            }
            sr.Close();

            //adv.cheat(ch);
            Dictionary<string, long> curout = new Dictionary<string, long>();
            curout.Add("FUEL", 1);

            //string ore = adv.CreateChem2(reactions["FUEL"], 1, reactions, amounts, curout);
            string ores = adv.CreateChem2(reactions["FUEL"],  reactions, amounts, curout);
            //return ores.ToString();
            //string ore = adv.CreateChem("FUEL", "FUEL", 1, reactions, amounts);
            //string ore = adv.CreateChem("XJWVT", "44 XJWVT", 44, reactions, amounts);

            long oresum = adv.AddChemList(ores, reactions, amounts);
            return oresum.ToString() + " - " + ores;
            //return oresum.ToString();
        }
        public string day14_p2()
        {
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day14_input.txt");
            // 2155881 too high
            // 9944133 too high
            // answers:
            //  337862   P2: 3687786
            string ln = "";
            Dictionary<string, string> reactions = new Dictionary<string, string>();
            Dictionary<string, long> amounts = new Dictionary<string, long>();
            //Dictionary<string, int> elements = new Dictionary<string, int>();
            string[] ch = new string[59];       // 59 for actual
            int pos = 0;
            while ((ln = sr.ReadLine()) != null)
            {
                int pos1 = ln.IndexOf("=>");
                int pos2 = ln.Length;
                string p = ln.Substring(pos1 + 3, pos2 - (pos1 + 3));

                string[] ps = p.Split(' ');
                amounts.Add(ps[1], int.Parse(ps[0]));


                string e = ln.Substring(0, pos1 - 1);

                reactions.Add(ps[1], e);
                ch[pos] = ln;
                pos++;
            }
            sr.Close();

            //adv.cheat(ch);
            Dictionary<string, long> curout = new Dictionary<string, long>();

            //string ore = adv.CreateChem2(reactions["FUEL"], 1, reactions, amounts, curout);
            string ores = adv.CreateChem2(reactions["FUEL"], reactions, amounts, curout);
            //return ores.ToString();
            //string ore = adv.CreateChem("FUEL", "FUEL", 1, reactions, amounts);
            //string ore = adv.CreateChem("XJWVT", "44 XJWVT", 44, reactions, amounts);


            long oresum = adv.AddChemList(ores, reactions, amounts);
            long maxore = 1000000000000;
            
            long minfuel = maxore / oresum;
            long maxfuel = minfuel * 2;
            bool found = false;
            long curfuel = minfuel;
            long maxoreret = 0;
            long minoreret = 0;
            while (!found)
            {
                curout.Clear();
                //curout.Add("FUEL", (int)curfuel);
                ores = adv.CreateChem2(curfuel.ToString() + " FUEL", reactions, amounts, curout);
                long orecheck = adv.AddChemList(ores, reactions, amounts);
                if (orecheck > maxore)
                {
                    maxfuel = curfuel;
                    maxoreret = orecheck;
                }
                else
                {
                    minfuel = curfuel;
                    minoreret = orecheck;
                }
                
                
                //Debug.WriteLine(orecheck.ToString() + " - " + curfuel.ToString());
                curfuel = (maxfuel + minfuel) / 2;
                if (maxfuel==curfuel || curfuel == minfuel)
                {
                    break;
                }
            }
            
            string ret = "Max Ore: " + maxoreret.ToString() + " = " + maxfuel.ToString() + Environment.NewLine;
            ret += "Min Ore: " + minoreret.ToString() + " = " + minfuel.ToString() + Environment.NewLine;

            return ret;
        }
        public string day15_p1()
        {
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day15_input.txt");

            string ln = sr.ReadLine();
            sr.Close();
            long[] inp = adv.GetComputerInput64(ln);
            int max = 500;
            
            string[,] grid = new string[max, max];
            string gridstr = "";
            gridstr = adv.MoveDroid(inp, grid, max, true);
            inp = adv.GetComputerInput64(ln);
            gridstr = adv.MoveDroid(inp, grid, max, false);

            int minx = 230;
            int maxx = 268;
            int miny = 230;
            int maxy = 268;
            int mins = adv.FillGridOxygen(grid, minx, miny, maxx, maxy);
            mins--;
            return gridstr;
            
        }

        public string day15_p2()
        {

            // see p1

            return "fail";
        }
        public string day16_p1()
        {

            // ---Day 16: Flawed Frequency Transmission-- -
            // You're 3/4ths of the way through the gas giants. Not only do roundtrip signals to Earth take five hours, but the signal quality is quite bad as well.
            // You can clean up the signal with the Flawed Frequency Transmission algorithm, or FFT.

            // As input, FFT takes a list of numbers.In the signal you received(your puzzle input), each number is a single digit: data like 15243 represents the sequence 1, 5, 2, 4, 3.
            // FFT operates in repeated phases. In each phase, a new list is constructed with the same length as the input list. This new list is also used as the input for the next phase.
            // Each element in the new list is built by multiplying every value in the input list by a value in a repeating pattern and then adding up the results.
            // So, if the input list were 9, 8, 7, 6, 5 and the pattern for a given element were 1, 2, 3, the result would be 9 * 1 + 8 * 2 + 7 * 3 + 6 * 1 + 5 * 2(with each input element on the left and each value in the repeating pattern on the right of each multiplication).Then, only the ones digit is kept: 38 becomes 8, -17 becomes 7, and so on.

            // While each element in the output array uses all of the same input array elements, the actual repeating pattern to use depends on which output element is being calculated.
            // The base pattern is 0, 1, 0, -1.Then, repeat each value in the pattern a number of times equal to the position in the output list being considered.
            // Repeat once for the first element, twice for the second element, three times for the third element, and so on.
            // So, if the third element of the output list is being calculated, repeating the values would produce: 0, 0, 0, 1, 1, 1, 0, 0, 0, -1, -1, -1.

            // When applying the pattern, skip the very first value exactly once. (In other words, offset the whole pattern left by one.) 
            // So, for the second element of the output list, the actual pattern used would be: 0, 1, 1, 0, 0, -1, -1, 0, 0, 1, 1, 0, 0, -1, -1, ....

            // After using this process to calculate each element of the output list, the phase is complete, and the output list of this phase is used as the new input list for the next phase, if any.

            // Given the input signal 12345678, below are four phases of FFT. Within each phase, each output digit is calculated on a single line with the result at the far right; each multiplication operation shows the input digit on the left and the pattern value on the right:

            // Input signal: 12345678

            //1 * 1 + 2 * 0 + 3 * -1 + 4 * 0 + 5 * 1 + 6 * 0 + 7 * -1 + 8 * 0 = 4
            //1 * 0 + 2 * 1 + 3 * 1 + 4 * 0 + 5 * 0 + 6 * -1 + 7 * -1 + 8 * 0 = 8
            //1 * 0 + 2 * 0 + 3 * 1 + 4 * 1 + 5 * 1 + 6 * 0 + 7 * 0 + 8 * 0 = 2
            //1 * 0 + 2 * 0 + 3 * 0 + 4 * 1 + 5 * 1 + 6 * 1 + 7 * 1 + 8 * 0 = 2
            //1 * 0 + 2 * 0 + 3 * 0 + 4 * 0 + 5 * 1 + 6 * 1 + 7 * 1 + 8 * 1 = 6
            //1 * 0 + 2 * 0 + 3 * 0 + 4 * 0 + 5 * 0 + 6 * 1 + 7 * 1 + 8 * 1 = 1
            //1 * 0 + 2 * 0 + 3 * 0 + 4 * 0 + 5 * 0 + 6 * 0 + 7 * 1 + 8 * 1 = 5
            //1 * 0 + 2 * 0 + 3 * 0 + 4 * 0 + 5 * 0 + 6 * 0 + 7 * 0 + 8 * 1 = 8

            //After 1 phase: 48226158

            //4 * 1 + 8 * 0 + 2 * -1 + 2 * 0 + 6 * 1 + 1 * 0 + 5 * -1 + 8 * 0 = 3
            //4 * 0 + 8 * 1 + 2 * 1 + 2 * 0 + 6 * 0 + 1 * -1 + 5 * -1 + 8 * 0 = 4
            //4 * 0 + 8 * 0 + 2 * 1 + 2 * 1 + 6 * 1 + 1 * 0 + 5 * 0 + 8 * 0 = 0
            //4 * 0 + 8 * 0 + 2 * 0 + 2 * 1 + 6 * 1 + 1 * 1 + 5 * 1 + 8 * 0 = 4
            //4 * 0 + 8 * 0 + 2 * 0 + 2 * 0 + 6 * 1 + 1 * 1 + 5 * 1 + 8 * 1 = 0
            //4 * 0 + 8 * 0 + 2 * 0 + 2 * 0 + 6 * 0 + 1 * 1 + 5 * 1 + 8 * 1 = 4
            //4 * 0 + 8 * 0 + 2 * 0 + 2 * 0 + 6 * 0 + 1 * 0 + 5 * 1 + 8 * 1 = 3
            //4 * 0 + 8 * 0 + 2 * 0 + 2 * 0 + 6 * 0 + 1 * 0 + 5 * 0 + 8 * 1 = 8

            //After 2 phases: 34040438

            //3 * 1 + 4 * 0 + 0 * -1 + 4 * 0 + 0 * 1 + 4 * 0 + 3 * -1 + 8 * 0 = 0
            //3 * 0 + 4 * 1 + 0 * 1 + 4 * 0 + 0 * 0 + 4 * -1 + 3 * -1 + 8 * 0 = 3
            //3 * 0 + 4 * 0 + 0 * 1 + 4 * 1 + 0 * 1 + 4 * 0 + 3 * 0 + 8 * 0 = 4
            //3 * 0 + 4 * 0 + 0 * 0 + 4 * 1 + 0 * 1 + 4 * 1 + 3 * 1 + 8 * 0 = 1
            //3 * 0 + 4 * 0 + 0 * 0 + 4 * 0 + 0 * 1 + 4 * 1 + 3 * 1 + 8 * 1 = 5
            //3 * 0 + 4 * 0 + 0 * 0 + 4 * 0 + 0 * 0 + 4 * 1 + 3 * 1 + 8 * 1 = 5
            //3 * 0 + 4 * 0 + 0 * 0 + 4 * 0 + 0 * 0 + 4 * 0 + 3 * 1 + 8 * 1 = 1
            //3 * 0 + 4 * 0 + 0 * 0 + 4 * 0 + 0 * 0 + 4 * 0 + 3 * 0 + 8 * 1 = 8

            //After 3 phases: 03415518

            //0 * 1 + 3 * 0 + 4 * -1 + 1 * 0 + 5 * 1 + 5 * 0 + 1 * -1 + 8 * 0 = 0
            //0 * 0 + 3 * 1 + 4 * 1 + 1 * 0 + 5 * 0 + 5 * -1 + 1 * -1 + 8 * 0 = 1
            //0 * 0 + 3 * 0 + 4 * 1 + 1 * 1 + 5 * 1 + 5 * 0 + 1 * 0 + 8 * 0 = 0
            //0 * 0 + 3 * 0 + 4 * 0 + 1 * 1 + 5 * 1 + 5 * 1 + 1 * 1 + 8 * 0 = 2
            //0 * 0 + 3 * 0 + 4 * 0 + 1 * 0 + 5 * 1 + 5 * 1 + 1 * 1 + 8 * 1 = 9
            //0 * 0 + 3 * 0 + 4 * 0 + 1 * 0 + 5 * 0 + 5 * 1 + 1 * 1 + 8 * 1 = 4
            //0 * 0 + 3 * 0 + 4 * 0 + 1 * 0 + 5 * 0 + 5 * 0 + 1 * 1 + 8 * 1 = 9
            //0 * 0 + 3 * 0 + 4 * 0 + 1 * 0 + 5 * 0 + 5 * 0 + 1 * 0 + 8 * 1 = 8

            // After 4 phases: 01029498

                // Here are the first eight digits of the final output list after 100 phases for some larger inputs:

            //80871224585914546619083218645595 becomes 24176176.
            //19617804207202209144916044189917 becomes 73745418.
            //69317163492948606335995924319873 becomes 52432133.

            //After 100 phases of FFT, what are the first eight digits in the final output list ?


            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day16_input.txt");

            int[] basepattern = new int[4]{ 0,1,0,-1};

            string ln = sr.ReadLine();
            sr.Close();

            //ln = "80871224585914546619083218645595";
            //ln = "12345678";

            int[] signal = new int[ln.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] = int.Parse(ln.Substring(i, 1));
            }
            adv.GetPatterns(basepattern, 0);

            for (int phase = 1; phase <= 100; phase++)
            {
                signal = adv.FFS2(signal, phase, basepattern);
            }

            string sig = "";
            for (int i = 0; i < 8; i++)
            {
                sig += signal[i].ToString();
            }
                return sig;
        }
        public string day16_p2(System.Windows.Forms.TextBox tb) 
        {

            // ---Part Two-- -
            // Now that your FFT is working, you can decode the real signal.

            // The real signal is your puzzle input repeated 10000 times.
            // Treat this new signal as a single input list.Patterns are still calculated as before, and 100 phases of FFT are still applied.
            // The first seven digits of your initial input signal also represent the message offset. The message offset is the location of the eight - digit message in the final output list. Specifically, the message offset indicates the number of digits to skip before reading the eight-digit message.For example, if the first seven digits of your initial input signal were 1234567, the eight-digit message would be the eight digits after skipping 1,234,567 digits of the final output list. Or, if the message offset were 7 and your final output list were 98765432109876543210, the eight-digit message would be 21098765. (Of course, your real message offset will be a seven - digit number, not a one - digit number like 7.)
            // Here is the eight - digit message in the final output list after 100 phases.
            // The message offset given in each input has been highlighted. (Note that the inputs given below are repeated 10000 times to find the actual starting input lists.)

            //    03036732577212944063491565474664 becomes 84462026.
            //    02935109699940807407585447034323 becomes 78725270.
            //    03081770884921959731165446850517 becomes 53553731.

            //After repeating your input signal 10000 times and running 100 phases of FFT, what is the eight - digit message embedded in the final output list?

            //AdventShared adv = new AdventShared();
            //StreamReader sr = new StreamReader("c:\\temp\\advent_day16_input.txt");

            //int[] basepattern = new int[4] { 0, 1, 0, -1 };

            //string ln = sr.ReadLine();
            //ln = "03081770884921959731165446850517";
            ////ln = "12345678";
            ////string sig = ln;
            //int repeats = 2;

            //int[] signal = new int[ln.Length*repeats];

            //StringBuilder sb = new StringBuilder(ln);
            //for (int i = 0; i < (repeats-1); i++)
            //{
            //    sb.Append(ln);
            //}
            //string sig = sb.ToString();

            //for (int i = 0; i < signal.Length; i++)
            //{
            //    signal[i] = int.Parse(sig.Substring(i, 1));
            //}

            //adv.calcBasePatterns(ln.Length, repeats, basepattern);





            //for (int phase = 1; phase <= 100; phase++)
            //{
            //    signal = adv.FFS3(signal);

            //    string d = "";
            //    for (int i = 0; i < (signal.Length); i++)
            //    {
            //        d += signal[i].ToString();
            //    }
            //    Debug.WriteLine(phase.ToString() + " - " + d);
            //}

            //int offset = 5978783; // from input
            //offset = 0;
            //string sigr = "";
            //for (int i = 0+offset; i < 8+offset; i++)
            //{
            //    sigr += signal[i].ToString();
            //}
            //return sigr;

            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day16_input.txt"); // test answer = 18650834


            int[] basepattern = new int[4] { 0, 1, 0, -1 };

            string ln = sr.ReadLine();
            sr.Close();

            //ln = "12345678";
            //ln = "80871224";
            //ln = "52614";
            //ln = "03036732577212944063491565474664";
            //ln = "02935109699940807407585447034323";
            //ln = "03081770884921959731165446850517";

            // answer not: 47693165

            StringBuilder sb = new StringBuilder(ln);
            for (int i = 1; i < 10000; i++)
            {
                sb.Append(ln);
            }
            string sig = sb.ToString();
            //string sig = ln;

            //string i1 = sig.Substring(303672);
            //string i1 = sig.Substring(5978783);
            string i1 = sig.Substring((int.Parse(sig.Substring(0, 7)) - 2));
            //string i1 = sig;
            int[] i2 = new int[i1.Length];
            for (int i = 0; i < i1.Length; i++)
            {
                i2[i] = int.Parse(i1.Substring(i, 1));
            }
            Debug.WriteLine("0 - " + i1.Substring(i1.Length - 20));
            for (int phase = 1; phase <= 100; phase++)
            {
                //i1 = adv.FFT10k(i1);
                i2 = adv.FFT10kb(i2);
                string sigr2 = "";
                for (int i = i2.Length - 50; i < i2.Length; i++)
                {
                    sigr2 += i2[i];
                }
                sigr2 += " - ";
                for (int i = 0; i < 10; i++)
                {
                    sigr2 += i2[i];
                }
                Debug.WriteLine("phase " + phase.ToString() + " - " + sigr2);
            }

            //int toff = int.Parse(sig.Substring(0, 7));
            int toff = 0;
            string sigr = "";
            for (int i = toff; i < toff+50 ; i++)
            {
                sigr += i2[i];
            }
            //Debug.WriteLine(sigr);
            Debug.WriteLine("final: " + sigr);
            int[] signal = new int[sig.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] = int.Parse(sig.Substring(i, 1));
            }


            //adv.nextrow0 = -999;
            //adv.GetPatterns(basepattern, 0);
            for (int phase = 1; phase <= 100; phase++)
            {
                signal = adv.FFS2(signal, phase, basepattern);

                string d = "";
                for (int i = signal.Length - 50; i < signal.Length; i++)
                {
                    d += signal[i].ToString();
                }
                d += " - ";
                for (int i = 293508; i < 293518; i++)
                {
                    d += signal[i].ToString();
                }
                tb.Text = phase.ToString() + " - " + d;
                tb.Update();
                Debug.WriteLine(phase.ToString() + " - " + d);
            }

            //string dd = "";
            //for (int i = 0; i < signal.Length; i++)
            //{
            //    dd += signal[i].ToString();
            //}
            //Debug.WriteLine(dd);
            //int offset = 5978783; // from input
            //offset = 0;
            //string sigr = "";
            //for (int i = 0 + offset; i < 8 + offset; i++)
            //{
            //    sigr += signal[i].ToString();
            //}
            return sigr;


        }

        public string day17_p1()
        {
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day17_input.txt");

            string ln = sr.ReadLine();
            sr.Close();

            long[] inp = adv.GetComputerInput64(ln);

            long alignment = adv.AsciiRobotInit(inp);

            return alignment.ToString();
        }
        public string day17_p2()
        {
            AdventShared adv = new AdventShared();
            StreamReader sr = new StreamReader("c:\\temp\\advent_day17_input.txt");

            string ln = sr.ReadLine();
            sr.Close();
            long[] inp = adv.GetComputerInput64(ln);

            long alignment = adv.AsciiRobotMove(inp);

            return alignment.ToString();
        }

    }
}
