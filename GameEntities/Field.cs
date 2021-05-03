using System;
using System.Linq;
using System.Drawing;
using System.Timers;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.IO;
using System.Collections.Generic;


namespace Match3Classic
{
    public class Field
    {
        Shape shape;
        const int FIELD_SIZE = 8,
            FIELDSIZE = 64,
            PROXIMITY_DISTANCE = 2,
            MATCH_CONDITION = 3;

        public int
            width, height,
            block_size,
            start_x, start_y,
            score, period;



        public bool
            gameover,
            exit, pause;

        public float percentPreMatch;

        public char[,] area;

        public int[,] _field;

        string player_name;

        public struct text_menu
        {
            public string order;
            public string message;

            public text_menu(string order, string message)
            {
                this.order = order;
                this.message = message;
            }

        }

        public int start_finding = 0;


        public struct Match
        {
            public int i;
            public int j;
            //public bool isVisited;

            public Match(int i, int j)
            {
                this.i = i;
                this.j = j;
                //this.isVisited = isVisited;
            }

        }

        public List<Match> _match;
        public Match _horizontalMatch;
        public Match _verticalMatch;

        public List<text_menu> text_menus;

        public Field(int width, int height, int start_x, int start_y, int block_size, int difficulty, string player_name) //user defined field constructor 
        {
            this.height = height;
            this.width = width;
            this.start_x = start_x;
            this.start_y = start_y;
            this.block_size = block_size;
            this.player_name = player_name;
            this.period = 150;  //from constructor

            switch (difficulty)
            {
                case 1:
                    this.period = 150;
                    break;
                case 2:
                    this.period = 120;
                    break;
                case 3:
                    this.period = 90;
                    break;
            }

            this.area = new char[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    this.area[i, j] = ' ';
                }
            }
        }

        public void FieldCreate(Shape shape, int[,] delta)
        {

            for (var i = 0; i < FIELD_SIZE; i++)
            {
                for (var j = 0; j < FIELD_SIZE; j++)
                {
                    this._field[i, j] = 6;
                }
            }

            this._field[0, 0] = 4;

            //check if 10-15% 
            //Console.WriteLine(shapeType.ToString());

            // Render();

        }

        public Field()
        {
            this._match = new List<Match>();
            this.shape = new Shape();

            this._field = new int[FIELD_SIZE, FIELD_SIZE];

            FieldCreate(shape, this._field);
        }


        public void Render()
        {
            Console.SetCursorPosition(0, 0);
            //FieldCreate(shape);

            string output = string.Empty;

          
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    this.start_finding = 1;
                   // this._match.Add(new Match(i, j, true));
                   // BFS(i, j, _field[i, j], 0, this._match);    //check matches
                    output += this._field[j, i];
                    output += ' ';
                }
                output += "|\n";
            }

            Console.WriteLine(IfMatch(0, 0));

            Console.Write(output);

        }

        /*
        public void BFS(int i, int j, int value, int MatchType, List<Match> matches)
        {
            if (value == 11)
                return;

            int k = i;
            int z = j;
            bool isvisited;

            List<Match> Temp = new List<Match>();

            if (this.start_finding == 1)
            {
                this.start_finding = 0;

                if ((k + 1 < FIELD_SIZE) && (_field[k + 1, z] == value) )    //horizontal match - right 
                {
                    Temp = matches;
                    Temp.Add(new Match(k + 1, z, true));
                    BFS(k + 1, z, value, 0, Temp);
                }

                if ((k - 1 > 0) && (_field[k - 1, z] == value))         //horizontal match - left
                {
                    Temp = matches;
                    Temp.Add(new Match(k - 1, z, true));
                    BFS(k - 1, z, value, 0, Temp);
                }

                if ((z + 1 < FIELD_SIZE) && (_field[k, z + 1] == value))        //vertical match - down
                {
                    Temp = matches;
                    Temp.Add(new Match(k, z + 1, true));
                    BFS(k, z + 1, value, 0, Temp);
                }

                if ((z - 1 > 0) && (_field[k, z - 1] == value))         //vertical match - up
                {
                    Temp = matches;
                    Temp.Add(new Match(k, z - 1, true));
                    BFS(k, z - 1, value, 0, Temp);
                }
            }

            if (MatchType == 0)   //horizontal match
            {

                if ((k + 1 < FIELD_SIZE) && (_field[k + 1, z] == value) && (IsVisited(matches, k + 1, z) == 1))    //horizontal match - right  if not visited
                {
                   
                    Temp = matches;
                    Temp.Add(new Match(k + 1, z, true));
                    BFS(k + 1, z, value, 0, Temp);
                }

                if ((k - 1 > 0) && (_field[k - 1, z] == value) && (IsVisited(matches, k - 1, z) == 1))         //horizontal match - left
                {
                    Temp = matches;
                    Temp.Add(new Match(k - 1, z, true));
                    BFS(k - 1, z, value, 0, Temp);
                }

                if ((z + 1 < FIELD_SIZE) && (_field[k, z + 1] == value) && (IsVisited(matches, k, z + 1) == 1))        //vertical match - down
                {
                    Temp.Add(new Match(k, z, true));
                    Temp.Add(new Match(k, z + 1, true));
                    BFS(k, z + 1, value, 1, Temp);
                }

                if ((z - 1 > 0) && (_field[k, z - 1] == value) && (IsVisited(matches, k, z - 1) == 1))         //vertical match - up
                {
                    Temp.Add(new Match(k, z, true));
                    Temp.Add(new Match(k, z - 1, true));
                    BFS(k, z - 1, value, 1, Temp);
                }

                if (Temp.Count >= MATCH_CONDITION)
                {
                    for (int m = 0; m < Temp.Count; m++)
                    {
                        matches.Add(Temp[m]);
                    }
                    this._match = matches;
                }
            }


            if (MatchType == 1)     //vertical match
            {

                if ((k + 1 < FIELD_SIZE) && (_field[k + 1, z] == value) && (IsVisited(matches, k + 1, z) == 1))    //horizontal match - right 
                {
                    Temp.Add(new Match(k, z, true));
                    Temp.Add(new Match(k + 1, z, true));
                    BFS(k + 1, z, value, 0, Temp);
                }

                if ((k - 1 > 0) && (_field[k - 1, z] == value) && (IsVisited(matches, k - 1, z) == 1))         //horizontal match - left
                {
                    Temp.Add(new Match(k, z, true));
                    Temp.Add(new Match(k - 1, z, true));
                    BFS(k - 1, z, value, 0, Temp);
                }

                if ((z + 1 < FIELD_SIZE) && (_field[k, z + 1] == value) && (IsVisited(matches, k, z + 1) == 1))        //vertical match - down
                {
                    Temp = matches;
                    Temp.Add(new Match(k, z + 1, true));
                    BFS(k, z + 1, value, 1, Temp);
                }

                if ((z - 1 > 0) && (_field[k, z - 1] == value) && (IsVisited(matches, k, z - 1) == 1))         //vertical match - up
                {
                    Temp = matches;
                    Temp.Add(new Match(k, z - 1, true));
                    BFS(k, z - 1, value, 1, Temp);
                }

                if (Temp.Count >= MATCH_CONDITION)
                {
                    for (int m = 0; m < Temp.Count; m++)
                    {
                        matches.Add(Temp[m]);
                    }
                    this._match = matches;
                }
            }
        }
        */

        /*

        public int IsVisited(List<Match> matches, int i, int j)
        {
            for (int m = 0; m < matches.Count; m++)
            {
                if ((matches[m].i == i) && (matches[m].j == j))
                {
                    if (matches[m].isVisited == true)
                        return 0;
                    else
                        return 1;
                }
            }
            return -1;
        }*/

        public void CheckMatch(int i, int j)
        {
            int[,] temp = new int[FIELD_SIZE, FIELD_SIZE];
            int value = this._field[i, j];

            if (value == 11)
                return;

            List<Match> hor_match = new List<Match>();
            List<Match> vert_match = new List<Match>();
            List<Match> all_matches = new List<Match>();

            if ((i + 2 < FIELD_SIZE) && (this._field[i + 1, j] == value) && (this._field[i + 2, j] == value))
            {
                hor_match.Add(new Match(i, j));
                hor_match.Add(new Match(i + 1, j));
                hor_match.Add(new Match(i + 2, j));
            }
        


            if (j + 2 < FIELD_SIZE)
            {
                if ((this._field[i, j + 1] == value) && (this._field[i, j + 2] == value))
                {
                    vert_match.Add(new Match(i, j));
                    vert_match.Add(new Match(i, j + 1));
                    vert_match.Add(new Match(i, j + 2));




                }

            }

        }

        public int IfMatch(int i, int j)
        {
            int value = this._field[i, j];

            if (value == 11)
                return 0;

            try
            {
                if(this._field[i + 1, j] == value)
                {
                    if (this._field[i + 2, j] == value)
                    {
                        return 1;
                    }

                    if (this._field[i - 1, j] == value)
                    {
                        return 1;
                    }
                }
            }
            catch
            {
            }

            try
            {
                if (this._field[i - 1, j] == value)
                {
                    if (this._field[i - 2, j] == value)
                    {
                        return 1;
                    }
                }
            }
            catch
            {
            }

            try
            {
                if (this._field[i, j + 1] == value)
                {
                    if (this._field[i, j + 2] == value)
                    {
                        return 1;
                    }

                    if (this._field[i, j - 1] == value)
                    {
                        return 1;
                    }
                }
            }
            catch
            {
            }

            try
            {
                if (this._field[i, j - 1] == value)
                {
                    if (this._field[i, j - 2] == value)
                    {
                        return 1;
                    }
                }
            }
            catch
            {
            }

            return 0;       //No Matches

        }

        void RemoveMatch(List<Match> match)
        {
            for(int i = 0; i < match.Count; i++)
            {
                


            }


        }


    }

}