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
    class Bonuses
    {
        public Bonuses()
        {

        }

    }


}
/*
namespace Match3Classic
{
    class Bonuses
    {
        public Bonuses()
        {


        }

        public void BlastHorLine(int i, int j, int FIELD_SIZE, int[,] _field)
        {
            for (int k = 0; k < FIELD_SIZE; k++)
            {
                if ((_field[k, j] >= 8) || (_field[k, j] < 13))       //horizontal line bonus
                    BlastHorLine(k, j);

                if ((_field[k, j] >= 13) || (_field[k, j] < 18))      //vert line bonus
                    BlastVertLine(k, j);

                if ((_field[k, j] >= 18) || (_field[k, j] < 23))      //bomb bonus
                    BlastBomb(k, j);

                _field[k, j] = 23;
            }
        }

        public void BlastVertLine(int i, int j)
        {
            for (int k = 0; k < FIELD_SIZE; k++)
            {
                if ((_field[i, k] >= 8) || (_field[i, k] < 13))       //horizontal line bonus
                    BlastHorLine(i, k);

                if ((_field[i, k] >= 13) || (_field[i, k] < 18))      //vert line bonus
                    BlastVertLine(i, k);

                if ((_field[i, k] >= 18) || (_field[i, k] < 23))      //bomb bonus
                    BlastBomb(i, k);

                _field[i, k] = 23;
            }

        }

        public void BlastBomb(int i, int j)
        {
            List<Match> BombBlast = new List<Match>();

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

            for (int k = 0; k < BombBlast.Count; k++)
            {
                if ((this._field[BombBlast[k].i, BombBlast[k].j] >= 8) || (this._field[BombBlast[k].i, BombBlast[k].j] < 13))       //horizontal line bonus
                    BlastHorLine(BombBlast[k].i, BombBlast[k].j);

                if ((this._field[BombBlast[k].i, BombBlast[k].j] >= 13) || (this._field[BombBlast[k].i, BombBlast[k].j] < 18))      //vert line bonus
                    BlastVertLine(BombBlast[k].i, BombBlast[k].j);

                if ((this._field[BombBlast[k].i, BombBlast[k].j] >= 18) || (this._field[BombBlast[k].i, BombBlast[k].j] < 23))      //bomb bonus
                    BlastBomb(BombBlast[k].i, BombBlast[k].j);

                this._field[BombBlast[k].i, BombBlast[k].j] = 23;
            }


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

    }

}

*/