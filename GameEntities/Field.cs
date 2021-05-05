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
        public const int FIELD_SIZE = 8,
            FIELDSIZE = 64,
            PROXIMITY_DISTANCE = 2,
            MATCH_CONDITION = 3,
            LINE_BONUS = 4,
            BOMB_BONUS = 5,
            OFFSET = 5;
           // BOMB_OFFSET = 3,
           // HOR_LINE_OFFSET = 1,
           // VERT_LINE_OFFSET = 2;

        public int
            width, height,
            block_size,
            start_x, start_y,
            score, period;


        

        Bonuses _bonus;


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

        public struct Bonus
        {
            public int i;
            public int j;
            public int value;           //0 - none, 1 - LineHor, 2 - line Vert, 3 - bomb

            public Bonus(int i, int j, int value)
            {
                this.i = i;
                this.j = j;
                this.value = value;
            }

        }



        public List<Match> _match;
        public Match _horizontalMatch;
        public Match _verticalMatch;

        public List<text_menu> text_menus;

        public void FieldCreate(Shape shape)
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
            this._bonus = new Bonuses();

            this._field = new int[FIELD_SIZE, FIELD_SIZE];

            FieldCreate(shape);
        }


        public float[] userInput(int i1, int j1, int i2, int j2)
        {
            float[] values = new float[4];

            if (IfMatch(i2, j2) == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    values[i] = -1;
                }
                return values;
            }
            Swap(i1, j1, i2, j2);       //swap successfull

            if (IfMatch(i1, j1) == 1)
            {
                DoBlast(i1, j1);
                DoBlast(i2, j2);

                values[0] = i2;
                values[1] = j2;
                values[2] = i1;
                values[3] = j1;
            }
            else
            {
                DoBlast(i2, j2);

                values[0] = i2;
                values[1] = j2;
                values[2] = -1;     //"empty" entry
                values[3] = -1;
            }


            return values;
        }

        public List<Bonus> BlastHorLine(int i, int j)
        {
            List<Bonus> ToBlast = new List<Bonus>();

            List<Bonus> Temp = new List<Bonus>();

            int value = 1;

            ToBlast.Add(new Bonus(i,j, value));

            for (int k = 0; k < FIELD_SIZE; k++)
            {
                if ((this._field[k, j] >= 8) || (this._field[k, j] < 13))       //horizontal line bonus
                {
                    Temp = BlastHorLine(k, j);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                if ((this._field[k, j] >= 13) || (this._field[k, j] < 18))      //vert line bonus
                {
                    Temp = BlastVertLine(k, j);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                if ((this._field[k, j] >= 18) || (this._field[k, j] < 23))      //bomb bonus
                {
                    Temp = BlastBomb(k, j);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                this._field[k, j] = 23;
            }

            List<Bonus> NoDupes = ToBlast.Distinct().ToList();

            return NoDupes;
        }

        public List<Bonus> BlastVertLine(int i, int j)
        {
            int value = 2;

            List<Bonus> ToBlast = new List<Bonus>();

            List<Bonus> Temp = new List<Bonus>();

            ToBlast.Add(new Bonus(i, j, value));

            for (int k = 0; k < FIELD_SIZE; k++)
            {
                if ((this._field[i, k] >= 8) || (this._field[i, k] < 13))       //horizontal line bonus
                {
                    Temp = BlastHorLine(i, k);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                if ((this._field[i, k] >= 13) || (this._field[i, k] < 18))      //vert line bonus
                {
                    Temp = BlastVertLine(i, k);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                if ((this._field[i, k] >= 18) || (this._field[i, k] < 23))      //bomb bonus
                {
                    Temp = BlastBomb(i, k);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                this._field[i, k] = 23;
            }

            List<Bonus> NoDupes = ToBlast.Distinct().ToList();

            return NoDupes;

        }

        public List<Bonus> BlastBomb(int i, int j)
        {
            int value = 3;

            List<Match> BombBlast = new List<Match>();

            List<Bonus> ToBlast = new List<Bonus>();

            List<Bonus> Temp = new List<Bonus>();

            ToBlast.Add(new Bonus(i, j, value));

            if (i - 1 > 0)
            {
                if (j - 1 > 0)
                {
                    BombBlast.Add(new Match(i - 1, j - 1));
                }
                if (j + 1 < FIELD_SIZE)
                {
                    BombBlast.Add(new Match(i - 1, j + 1));
                }
                BombBlast.Add(new Match(i - 1, j));
            }

            if (i + 1 > 0)
            {
                if (j - 1 > 0)
                {
                    BombBlast.Add(new Match(i + 1, j - 1));
                }
                if (j + 1 < FIELD_SIZE)
                {
                    BombBlast.Add(new Match(i + 1, j + 1));
                }
                BombBlast.Add(new Match(i + 1, j));
            }

            if (j - 1 > 0)
            {
                BombBlast.Add(new Match(i, j - 1));
            }
            if (j + 1 < FIELD_SIZE)
            {
                BombBlast.Add(new Match(i, j + 1));
            }
            BombBlast.Add(new Match(i, j));

            int c, z;

            for (int k = 0; k < BombBlast.Count; k++)
            {
                c = BombBlast[k].i;
                z = BombBlast[k].j;

                if ((this._field[c, z] >= 8) || (this._field[c, z] < 13))       //horizontal line bonus
                {
                    Temp = BlastHorLine(c, z);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                if ((this._field[c, z] >= 13) || (this._field[c, z] < 18))      //vert line bonus
                {
                    Temp = BlastVertLine(c, z);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                if ((this._field[c, z] >= 18) || (this._field[c, z] < 23))      //bomb bonus
                {
                    Temp = BlastBomb(c, z);

                    for (int m = 0; m < Temp.Count; m++)
                    {
                        ToBlast.Add(new Bonus(Temp[m].i, Temp[m].j, Temp[m].value));
                    }

                    Temp = new List<Bonus>();
                }

                this._field[c, z] = 23;
            }


            List<Bonus> NoDupes = ToBlast.Distinct().ToList();
            

            return NoDupes;
        }

        public void SpawnHorLine(int i, int j, int value)
        {
            this._field[i, j] = GetHorLineValue(value);
        }

        public void SpawnVertLine(int i, int j, int value)
        {
            this._field[i, j] = GetVertLineValue(value);
        }

        public void SpawnBomb(int i, int j, int value)
        {
            this._field[i, j] = GetBombValue(value);
        }

        public int GetHorLineValue(int value)
        {
            return value + OFFSET;
        }

        public int GetBombValue(int value)
        {
            return value + OFFSET * 3;
        }

        public int GetVertLineValue(int value)
        {
            return value + OFFSET * 2;
        }

        /*
        LineVertGreen = 8,      
            LineVertPurple = 9,
            LineVertRed = 10,
            LineVertBlue = 11,
            LineVertYellow = 12,

            LineHorGreen = 13,
            LineHorPurple = 14,
            LineHorRed = 15,
            LineHorBlue = 16,
            LineHorYellow = 17,

            BombGreen = 18,
            BombPurple = 19,
            BombRed = 20,
            BombBlue = 21,
            BombYellow = 22,

            Nothing = 23      
        */

        public int blast(int i, int j)
        {


            return 1;
        }

        public List<Bonus> DoBlast(int i, int j)            //0 - just blast, 1 - hor line blast, 2 - vert line blast, 3 - bomb blast
        {
            List<Match> match = new List<Match>();

            List<Bonus> toprocede = new List<Bonus>();

            List<Bonus> temp = new List<Bonus>();

            match = WhereMatch(i, j);

            int value = this._field[match[0].i, match[0].j];

            int toWhere = match.Count - 1;
            int BonusType = 0;



            List<Bonus> Bonuses = new List<Bonus>();

            if (match[match.Count - 1].i > FIELD_SIZE)
            {
                toWhere = match.Count;          //Bonus present
                BonusType = match[match.Count - 1].i;
            }


            int c, z;

            for (int m = 0; m < toWhere; m++)
            {
                c = match[m].i;
                z = match[m].j;
                
                if ((this._field[c, z] >= 8) || (this._field[c, z] < 13))       //horizontal line bonus
                {
                    temp = BlastHorLine(c, z);

                    for (int p = 0; p < temp.Count; p++)
                    {
                        toprocede.Add(new Bonus(temp[m].i, temp[m].j, temp[m].value));
                    }

                    temp = new List<Bonus>();
                }

                if ((this._field[c, z] >= 13) || (this._field[c, z] < 18))      //vert line bonus
                {
                    temp = BlastVertLine(c, z);

                    for (int p = 0; p < temp.Count; p++)
                    {
                        toprocede.Add(new Bonus(temp[m].i, temp[m].j, temp[m].value));
                    }

                    temp = new List<Bonus>(); 
                }

                if ((this._field[c, z] >= 18) || (this._field[c, z] < 23))      //bomb bonus
                {
                    temp = BlastVertLine(c, z);

                    for (int p = 0; p < temp.Count; p++)
                    {
                        toprocede.Add(new Bonus(temp[m].i, temp[m].j, temp[m].value));
                    }

                    temp = new List<Bonus>();
                }
            }

            switch (BonusType)
            {
                case 444:
                    SpawnHorLine(i, j, this._field[i, j]);
                    break;
                case 555:
                    SpawnVertLine(i, j, this._field[i, j]);
                    break;
                case 666:
                    SpawnBomb(i, j, this._field[i, j]);
                    break;
                case 0:
                    break;
            }

            Bonuses = toprocede.Distinct().ToList();
            return Bonuses;
        }

        public void DoBlast(int i1, int j1, int i2, int j2)
        {
            List<Match> match1 = new List<Match>();
            match1 = WhereMatch(i1, j1);

            List<Match> match2 = new List<Match>();
            match2 = WhereMatch(i2, j2);

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
                for (int m = 0; m < matches.Count - 1; m++) {       //without bonuses

                            notmatch = shape.ShapeCreate();
                            while (notmatch == value)
                                {
                                    notmatch = shape.ShapeCreate();
                                }
                            this._field[matches[m].i, matches[m].j] = notmatch;   

                }
            }

            //Render();



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

            int[] bonus_values = new int[3];

            bonus_values[0] = GetHorLineValue(value);
            bonus_values[1] = GetVertLineValue(value);
            bonus_values[2] = GetBombValue(value);


            List<Match> Match = new List<Match>();

            List<Match> Temp = new List<Match>();

            Match Bonus = new Match();

            List<Match> MatchTypeHor = new List<Match>();
            List<Match> MatchTypeVert = new List<Match>();

            int BombIndex = 666;
            int horizontalIndex = 444;
            int verticalIndex = 555;


            if (i < FIELD_SIZE)
            {
               for (int k = i; k < FIELD_SIZE; k++)
                {
                    if ((this._field[k, j] == value) || (this._field[k, j] == bonus_values[0]) || (this._field[k, j] == bonus_values[1]) || (this._field[k, j] == bonus_values[2]))
                        Temp.Add(new Match(k, j));
                    else break;
                }

                foreach (Match m in Temp)
                {
                    Match.Add(m);
                    MatchTypeHor.Add(m);
                }

                Temp = new List<Match>();
            }

            if(j < FIELD_SIZE)
            {
                for (int k = j; k < FIELD_SIZE; k++)
                {
                    if ((this._field[i, k] == value) || (this._field[i, k] == bonus_values[0]) || (this._field[i, k] == bonus_values[1]) || (this._field[i, k] == bonus_values[2]))
                        Temp.Add(new Match(i, k));
                    else break;
                }

                foreach (Match m in Temp)
                {
                    Match.Add(m);
                    MatchTypeVert.Add(m);
                }

                Temp = new List<Match>();
            }
            
            if (i > 0)
            {
                for (int k = i; k > 0; k--)
                {
                    if ((this._field[k, j] == value) || (this._field[k, j] == bonus_values[0]) || (this._field[k, j] == bonus_values[1]) || (this._field[k, j] == bonus_values[2]))
                        Temp.Add(new Match(k, j));
                    else break;
                }

                foreach (Match m in Temp)
                {
                    Match.Add(m);
                    MatchTypeHor.Add(m);
                }

                Temp = new List<Match>();
            }

            if (j > 0)
            {
                for (int k = j; k > 0; k--)
                {
                    if ((this._field[i, k] == value) || (this._field[i, k] == bonus_values[0]) || (this._field[i, k] == bonus_values[1]) || (this._field[i, k] == bonus_values[2]))
                        Temp.Add(new Match(i, k));
                    else break;
                }

                foreach (Match m in Temp)
                {
                    Match.Add(m);
                    MatchTypeVert.Add(m);
                }

                Temp = new List<Match>();
            }

            List<Match> NoDuplicatesHor = MatchTypeHor.Distinct().ToList();
            List<Match> NoDuplicatesVert = MatchTypeVert.Distinct().ToList();
            List<Match> NoDuplicates = Match.Distinct().ToList();

            if (NoDuplicatesHor.Count >= BOMB_BONUS)
            {                                               //addding info about bonuses
                Bonus = new Match(BombIndex, BombIndex);
                NoDuplicates.Add(Bonus);
                return NoDuplicates;
            }

            if (NoDuplicatesVert.Count >= BOMB_BONUS)
            {
                Bonus = new Match(BombIndex, BombIndex);
                NoDuplicates.Add(Bonus);
                return NoDuplicates;
            }

            if ((NoDuplicatesVert.Count >= LINE_BONUS) || (NoDuplicatesHor.Count >= LINE_BONUS))
            {
                if (NoDuplicatesVert.Count > NoDuplicatesHor.Count)
                {
                    Bonus = new Match(verticalIndex, verticalIndex);
                }
                else
                    Bonus = new Match(horizontalIndex, horizontalIndex);

                NoDuplicates.Add(Bonus);
                return NoDuplicates;
            }

            return NoDuplicates;
        }


        public int IfMatch(int i, int j)
        {
            int value = this._field[i, j];

            if (value == 23)
                return 0;

            int[] bonus_values = new int[3];

            bonus_values[0] = GetHorLineValue(value);
            bonus_values[1] = GetVertLineValue(value);
            bonus_values[2] = GetBombValue(value);



            try
            {
                if ((this._field[i + 1, j] == value) || (this._field[i + 1, j] == bonus_values[0]) || (this._field[i + 1, j]  == bonus_values[1]) || (this._field[i + 1, j] == bonus_values[2])) 
                {
                    if ((this._field[i + 2, j] == value) || (this._field[i + 2, j] == bonus_values[0]) || (this._field[i + 2, j] == bonus_values[1]) || (this._field[i + 2, j] == bonus_values[2])) 
                    {
                        return 1;
                    }

                    if ((this._field[i - 1, j] == value) || (this._field[i - 1, j] == bonus_values[0]) || (this._field[i - 1, j] == bonus_values[1]) || (this._field[i - 1, j] == bonus_values[2])) 
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
                if ((this._field[i - 1, j] == value) || (this._field[i - 1, j] == bonus_values[0]) || (this._field[i - 1, j] == bonus_values[1]) || (this._field[i - 1, j] == bonus_values[2])) 
                {
                    if ((this._field[i - 2, j] == value) || (this._field[i - 2, j] == bonus_values[0]) || (this._field[i - 2, j] == bonus_values[1]) || (this._field[i - 2, j] == bonus_values[2])) 
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
                if ((this._field[i, j + 1] == value) || (this._field[i, j + 1] == bonus_values[0]) || (this._field[i, j + 1] == bonus_values[1]) || (this._field[i, j + 1] == bonus_values[2])) 
                {
                    if((this._field[i, j + 2] == value) || (this._field[i, j + 2] == bonus_values[0]) || (this._field[i, j + 2] == bonus_values[1]) || (this._field[i, j + 2] == bonus_values[2])) 
                    {
                        return 1;
                    }

                    if ((this._field[i, j - 1] == value) || (this._field[i, j - 1] == bonus_values[0]) || (this._field[i, j - 1] == bonus_values[1]) || (this._field[i, j - 1] == bonus_values[2])) 
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
                if ((this._field[i, j - 1] == value) || (this._field[i, j - 1] == bonus_values[0]) || (this._field[i, j - 1] == bonus_values[1]) || (this._field[i, j - 1] == bonus_values[2])) 
                {
                    if ((this._field[i, j - 2] == value) || (this._field[i, j - 2] == bonus_values[0]) || (this._field[i, j - 2] == bonus_values[1]) || (this._field[i, j - 2] == bonus_values[2])) 
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