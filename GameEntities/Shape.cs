using System;
using System.Linq;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;


namespace Match3Classic
{
    public class Shape
    {
        //public int x_coord, y_coord;
        public const int SHAPE_QUANTITY = 5, SHAPE_SIZE = 4;

        public enum ShapeType
        {
            Circle = 3,     //Green
            Heart = 4,      //Purple
            Rectangle = 5,  //Red
            Star = 6,       //Blue  
            Star2 = 7,      //Yellow

            LineHorGreen = 8,
            LineHorPurple = 9,
            LineHorRed = 10,
            LineHorBlue = 11,
            LineHorYellow = 12,

            LineVertGreen = 13,
            LineVertPurple = 14,
            LineVertRed = 15,
            LineVertBlue = 16,
            LineVertYellow = 17,

            BombGreen = 18,
            BombPurple = 19,
            BombRed = 20,
            BombBlue = 21,
            BombYellow = 22,

            Nothing = 23
        }

        int center, shape_type;

        public ShapeType shapeType;
        public struct shape_block
        {
            public int x_coord;
            public int y_coord;

            public shape_block(int x, int y)
            {
                this.x_coord = x;
                this.y_coord = y;
            }

        }

        public List<shape_block> shape_map;

        public Shape(Field grid) //Shape constructor
        {
            //shape_map_create(grid);
        }

        public Shape()
        {


        }

        public int ShapeCreate()
        {
            ShapeType[] shapes = ((ShapeType[])Enum.GetValues(typeof(ShapeType)));
            this.shapeType = shapes[new Random().Next(0, shapes.Length - 16)];      //16 - number of unspawnable objects
            return (int)shapeType;

        }
    }
}