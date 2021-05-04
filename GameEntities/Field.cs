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
                    this._field[i, j] = shape.ShapeCreate();
                }
            }

            GenerateByDifficulty(1);

            for (var i = 0; i < FIELD_SIZE; i++)
            {
                for (var j = 0; j < FIELD_SIZE; j++)
                {
                    Shuffle(i, j);
                }
            }

            
            //this._field[0, 0] = 4;

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

            /*
          
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                   // this.start_finding = 1;
                   // this._match.Add(new Match(i, j, true));
                   // BFS(i, j, _field[i, j], 0, this._match);    //check matches
                    output += this._field[j, i];
                    output += ' ';
                }
                output += "|\n";
            }

            Console.WriteLine(IfMatch(0, 0));

            Console.Write(output);
            */
        }

        public void Shuffle(int i, int j)
        {
            if (IfMatch(i, j) == 0)
                return;

            List<Match> matches = new List<Match>();

            int notmatch;
            int value = this._field[i, j];
            matches = WhereMatch(i, j);


            while (IfMatch(i, j) == 1)
            {
                foreach (Match m in matches)
                {
                     //   if ((m.i + 1 < FIELD_SIZE) && (this._field[m.i + 1, m.j] != value))
                     //   {
                      //     Swap(m.i, m.j, m.i + 1, m.j);
                     //   }
                     //   else
                      //  if ((m.j + 1 < FIELD_SIZE) && (this._field[m.i, m.j + 1] != value))
                      //  {
                      //      Swap(m.i, m.j, m.i, m.j + 1);
                     //  }
                     //   else
                     //   {
                            notmatch = shape.ShapeCreate();
                            while (notmatch == value)
                                {
                                    notmatch = shape.ShapeCreate();
                                }
                            this._field[m.i, m.j] = notmatch;
                     //   }
                    
                }
            }

            Render();



        }

        public void Swap(int i1, int j1, int i2, int j2)
        {
            int temp = this._field[i1,j1];
            this._field[i1, j1] = this._field[i2, j2];
            this._field[i2, j2] = temp;
        }

        public void GenerateByDifficulty(int difficulty_seed)
        {
            Random _random = new Random();

            int Steps, Seed; 
            int i, j;   //indices of field

            //const int min = 5, max = 7;

            switch (difficulty_seed)
            {
                case 1:
                    Steps = _random.Next(5, 7);

                    Console.WriteLine(Steps);

                    for (int k = 0; k < Steps; k++)
                    {
                        i = _random.Next(0, FIELD_SIZE - 1);
                        j = _random.Next(0, FIELD_SIZE - 1);
                        Seed = _random.Next(0, 3);
                        Generate(i, j, Seed);
                    }
                    break;
                case 2:
                    Steps = _random.Next(3, 5);

                    for (int k = 0; k < Steps; k++)
                    {
                        i = _random.Next(0, FIELD_SIZE - 1);
                        j = _random.Next(0, FIELD_SIZE - 1);
                        Seed = _random.Next(0, 3);
                        Generate(i, j, Seed);
                    }

                    break;
                case 3:
                    Steps = _random.Next(2, 4);

                    for (int k = 0; k < Steps; k++)
                    {
                        i = _random.Next(0, FIELD_SIZE - 1);
                        j = _random.Next(0, FIELD_SIZE - 1);
                        Seed = _random.Next(0, 3);
                        Generate(i, j, Seed);
                    }

                    break;
            }




        }

        public void Generate(int i, int j, int seed)
        {
            int value = this._field[i, j];

            if (value == 11)
                return;

            Console.WriteLine(i.ToString()+ " |||| " + j.ToString());

            switch (seed)
            {
                case 0:
                    try
                    {
                        if (i + 2 < FIELD_SIZE)
                            this._field[i + 2, j] = value;
                        this._field[i + 1, j] = value;

                        if (i - 1 > 0)
                            this._field[i - 1, j] = value;
                    }
                    catch
                    {

                    }
                        break;
                case 1:
                    try
                    {
                        this._field[i - 1, j] = value;
                        this._field[i - 2, j] = value;
                    }
                    catch
                    {
                    }
                    break;

                case 2:
                    try
                    {
                        if (i + 2 < FIELD_SIZE)
                            this._field[i, j + 1] = value;
                        this._field[i, j + 2] = value;
                        if (j - 1 > 0)
                            this._field[i, j - 1] = value;
                    }
                    catch
                    {
                    }
                    break;

                case 3:
                    try
                    {
                        this._field[i, j - 1] = value;
                        this._field[i, j - 2] = value;
                    }
                    catch
                    {
                    }
                    break;
            }
           



            return;
        }

        public List<Match> WhereMatch(int i, int j)
        {
            int value = this._field[i, j];


            List<Match> Match = new List<Match>();

            List<Match> Temp = new List<Match>();


            if (i < FIELD_SIZE)
            {
               for (int k = i; k < FIELD_SIZE; k++)
                {
                    if (this._field[k, j] == value)
                        Temp.Add(new Match(k, j));
                    else break;
                }
               if (Temp.Count >= MATCH_CONDITION)
                {
                    foreach (Match m in Temp)
                        Match.Add(m);
                }
                Temp = new List<Match>();
            }

            if(j < FIELD_SIZE)
            {
                for (int k = j; k < FIELD_SIZE; k++)
                {
                    if (this._field[i, k] == value)
                        Temp.Add(new Match(i, k));
                    else break;
                }
                if (Temp.Count >= MATCH_CONDITION)
                {
                    foreach (Match m in Temp)
                        Match.Add(m);
                }
                Temp = new List<Match>();
            }
            
            if (i > 0)
            {
                for (int k = i; k > 0; k--)
                {
                    if (this._field[k, j] == value)
                        Temp.Add(new Match(k, j));
                    else break;
                }
                if (Temp.Count >= MATCH_CONDITION)
                {
                    foreach (Match m in Temp)
                        Match.Add(m);
                }
                Temp = new List<Match>();
            }

            if (j > 0)
            {
                for (int k = j; k > 0; k--)
                {
                    if (this._field[i, k] == value)
                        Temp.Add(new Match(i, k));
                    else break;
                }
                if (Temp.Count >= MATCH_CONDITION)
                {
                    foreach (Match m in Temp)
                        Match.Add(m);
                }
                Temp = new List<Match>();
            }

            List<Match> NoDuplicates = Match.Distinct().ToList();

            return Match;
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



    }

}