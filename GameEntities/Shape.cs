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
            Square = 3,     //Green
            Heart = 4,      //Purple
            Sphere = 5,  //Red
            Pyramid = 6,       //Blue  
            Crystall = 7,      //Yellow

            LineHorSquare = 8,
            LineHorHeart = 9,
            LineHorSphere = 10,
            LineHorPyramid = 11,
            LineHorCrystall = 12,

            LineVertSquare = 13,
            LineVertHeart = 14,
            LineVertSphere = 15,
            LineVertPyramid = 16,
            LineVertCrystall = 17,

            BombSquare = 18,
            BombHeart = 19,
            BombSphere = 20,
            BombPyramid = 21,
            BombCrystall = 22,

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

        public Shape() //Shape constructor
        {
            //shape_map_create(grid);
        }
        public int ShapeCreate()
        {
            ShapeType[] shapes = ((ShapeType[])Enum.GetValues(typeof(ShapeType)));
            this.shapeType = shapes[new Random().Next(0, shapes.Length - 16)];      //16 - number of unspawnable objects
            return (int)shapeType;

        }
    }
}