using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TreeBasedSearchAssignment1
{
    public class FileReader
    {
        private string line, grid, agentpos, goal;
        private List<string> walls = new List<string>();
        private StreamReader reader;

        public string Line { get => line; set => line = value; }
        public string Grid { get => grid; set => grid = value; }
        public string Agentpos { get => agentpos; set => agentpos = value; }
        public string Goal { get => goal; set => goal = value; }
        public List<string> Walls { get => walls; set => walls = value; }

        public FileReader(string filename)
        {
            reader = new StreamReader(filename);

        }


        public void Read()
        {
            int linenum = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (linenum == 0)
                {
                    grid = line;
                }

                else if (linenum == 1)
                {
                    agentpos = line;
                }

                else if (linenum == 2)
                {
                    goal = line;
                }

                else
                {
                    walls.Add(line);
                }

                linenum++;
            }

        }

        public List<int> getGrid()
        {
            List<int> number = new List<int>();

            string[] stringnum = Regex.Split(grid, @"\D+");

            foreach (string value in stringnum)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    number.Add(i);
                }
            }

            return number;
        }

        public List<int> getAgent()
        {
            List<int> number = new List<int>();

            string[] stringnum = Regex.Split(agentpos, @"\D+");

            foreach (string value in stringnum)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    number.Add(i);
                }
            }

            return number;
        }

        public List<int> getGoal()
        {
            List<int> number = new List<int>();

            string[] stringnum = Regex.Split(goal, @"\D+");

            foreach (string value in stringnum)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    number.Add(i);
                }
            }

            return number;
        }

        public List<List<int>> getWall()
        {
            List<List<int>> wallintlist = new List<List<int>>();

            for (int i = 0; i < walls.Count; i++)
            {
                List<int> number = new List<int>();

                string[] stringnum = Regex.Split(walls[i], @"\D+");

                foreach (string value in stringnum)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int num = int.Parse(value);
                        number.Add(num);
                    }
                }

                wallintlist.Add(number);
            }

            return wallintlist;
        }

    }

}
