using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Match3Classic
{
    class SpriteField
    {
        const int TEXTURE_SIZE = 50;

        public Input Input;
        public MouseInput MouseInput;

        Shape shape;
        Field grid;

        int outsidestate;

        public GameTime gametime;

        public char[,] field
        {
            get; set;
        }

        public TimeSpan TotalGameTime
        {
            get;
            set;
        }

        private List<Sprite> sprites;
        private List<Texture2D> textures;

        public Vector2 hitBoxDelta;

        public Vector2 LastMousePosition, CurrentMousePosition;
        MouseState lastMouseState, currentMouseState;

        public struct TextureList
        {
            public List<Texture2D> textures;

            public TextureList(List<Texture2D> textures)
            {
                this.textures = textures;
            }
        }

        private TextureList[] _texturelist;

        public Texture2D[] texturelist;

        public Texture2D[][] TextureArray;

        public Vector2 topleft;
        public Vector2 topright;
        public Vector2 bottomleft;
        public Vector2 bottomright;

        public int[] current_sprite, comp_sprite;

        public float field_offset;


        public SpriteField(List<Sprite> sprites, Texture2D[][]textureArray, float fieldoffset)
        {
            this.grid = new Field();
            shape = new Shape();
            this.sprites = sprites;
            this.TextureArray = textureArray;
            this.outsidestate = 0;

            this.field_offset = fieldoffset;

            this.topleft = sprites[GetSpritePosition(0, 0)]._position2D;      //0,0
            this.topright = sprites[GetSpritePosition(7, 0)]._position2D;    //7,0
            this.bottomleft = sprites[GetSpritePosition(0, 7)]._position2D;  //0,7
            this.bottomright = sprites[GetSpritePosition(7, 7)]._position2D; //7,7

            Console.WriteLine(topleft.ToString()+topright.ToString()+bottomleft.ToString()+bottomright.ToString());

            this.current_sprite = new int[2] { -1, -1 };
            this.comp_sprite = new int[2] { -1, -1 };
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.sprites.Count; i++)
            {
                this.sprites[i].Draw(spriteBatch);
            }
        }

        public int GetSpritePosition(int i, int j)
        {
            if (i == -1)
                return 0;

            switch (i, j)
            {
                case (0, 0):
                        return 3;
                case (0, 1):
                        return 4;
                case (0, 2):
                        return 5;
                case (0, 3):
                        return 6;
                case (0, 4):
                        return 7;
                case (0, 5):
                        return 8;
                case (0, 6):
                        return 9;
                case (0, 7):
                        return 10;
                case (1, 0):
                        return 11;
                case (1, 1):
                        return 12;
                case (1, 2):
                        return 13;
                case (1, 3):
                        return 14;
                case (1, 4):
                        return 15;
                case (1, 5):
                        return 16;
                case (1, 6):
                        return 17;
                case (1, 7):
                        return 18;
                case (2, 0):
                        return 19;    
                case (2, 1):
                        return 20;                   
                case (2, 2):
                        return 21;                   
                case (2, 3):
                        return 22;
                case (2, 4):
                        return 23;
                case (2, 5):
                        return 24;
                case (2, 6):
                        return 25;
                case (2, 7):
                        return 26;
                case (3, 0):
                        return 27;
                case (3, 1):
                        return 28;
                case (3, 2):
                        return 29;
                case (3, 3):
                        return 30;
                case (3, 4):
                        return 31;
                case (3, 5):
                        return 32;
                case (3, 6):
                        return 33;
                case (3, 7):
                        return 34;
                case (4, 0):
                        return 35;
                case (4, 1):
                        return 36;
                case (4, 2):
                        return 37;
                case (4, 3):
                        return 38;
                case (4, 4):
                        return 39;
                case (4, 5):
                        return 40;
                case (4, 6):
                        return 41;
                case (4, 7):
                        return 42;
                case (5, 0):
                        return 43;
                case (5, 1):
                        return 44;
                case (5, 2):
                        return 45;
                case (5, 3):
                        return 46;
                case (5, 4):
                        return 47;
                case (5, 5):
                        return 48;
                case (5, 6):
                        return 49;
                case (5, 7):
                        return 50;
                case (6, 0):
                        return 51;
                case (6, 1):
                        return 52;
                case (6, 2):
                        return 53;
                case (6, 3):
                        return 54;
                case (6, 4):
                        return 55;
                case (6, 5):
                        return 56;
                case (6, 6):
                        return 57;
                case (6, 7):
                        return 58;
                case (7, 0):
                        return 59;
                case (7, 1):
                        return 60;
                case (7, 2):
                        return 61;
                case (7, 3):
                        return 62;
                case (7, 4):
                        return 63;
                case (7, 5):
                        return 64;
                case (7, 6):
                        return 65;
                case (7, 7):
                        return 66;
            }

            return 0;
        }


        /*
         * 
         * 
         *
         *
         * 
         
         for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (sprites[GetSpritePosition(i,j)]._position2D.X - this.field_offset - sprites[GetSpritePosition(i,j)]._texture2D.Width
        
                       (this.topleft.X - this.field_offset - TEXTURE_SIZE/2 <= CurrentMousePosition.X)//(this._position2D.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X))
                       &&
                       (this.topright.X + this.field_offset + TEXTURE_SIZE/2 >= CurrentMousePosition.X)//(this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
                       &&
                       ((this.topleft.Y - this.field_offset - TEXTURE_SIZE/2 <= CurrentMousePosition.Y))
                      &&
                       ((this.bottomleft.Y + this.field_offset  + TEXTURE_SIZE/2 >= CurrentMousePosition.Y))
        
        
        
        
        if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                if (
                    ((this._position2D.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X))
                    &&
                    ((this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
                    &&
                    ((this._position2D.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y))
                    &&
                    ((this._position2D.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= CurrentMousePosition.Y))
                   )
                {
                    this.isDragged = true;
                    this.isRotating = true;
                    Console.WriteLine(this._nextright);
                    //Console.WriteLine(this._position2D);
                    //Console.WriteLine(this._nextleft);
                    //Console.WriteLine(this._nextup);
                    //Console.WriteLine(this._nextdown);
                    //Console.WriteLine("Element : " + string.Join(",", this.gridPosition));
                    //Console.WriteLine("Position :", this._position2D);
                    // Console.WriteLine("Next right : " + this._nextright);
                }
            }
                    
                    this.sprites.Add(new Sprite(this._CountArray[grid._field[j, i] - 3], 
                        new Vector2(
                            sprites[2]._position2D.X + i * deltaField + i * TEXTURE_SIZE + TEXTURE_SIZE / 2 + deltaField,
                            sprites[2]._position2D.Y + j * deltaField + j * TEXTURE_SIZE + TEXTURE_SIZE / 2 + deltaField
                            ), 
                        Window.ClientBounds.Width, Window.ClientBounds.Height, i, j)
                    {
                        _origin = new Vector2
                            (
                            TEXTURE_SIZE / 2,
                            TEXTURE_SIZE / 2
                            ),

                        Input = new Input()
                        {
                        },
                        MouseInput = new MouseInput()
                        {

                        },
                        field_shift = deltaField
                    }) ;
                }
            }
          */

        /*
         * 
         * 
         * 
         * (
                    ((this._position2D.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X))
                    &&
                    ((this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
                    &&
                    ((this._position2D.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y))
                    &&
                    ((this._position2D.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= CurrentMousePosition.Y))
                   )
        */
        public void GetFieldPosition(MouseState position)
        {
            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    /*
                    if ((sprites[GetSpritePosition(i, j)]._position2D.X - sprites[GetSpritePosition(i, j)]._origin.X + this.field_offset <= position.X)
                        &&
                        (sprites[GetSpritePosition(i, j)]._position2D.X - sprites[GetSpritePosition(i, j)]._origin.X + sprites[GetSpritePosition(i,j)]._texture2D.Width - this.field_offset >= position.X)
                        &&
                        (sprites[GetSpritePosition(i, j)]._position2D.Y - sprites[GetSpritePosition(i, j)]._origin.Y + this.field_offset <= position.Y)
                        &&
                        (sprites[GetSpritePosition(i, j)]._position2D.Y - sprites[GetSpritePosition(i, j)]._origin.Y + sprites[GetSpritePosition(i, j)]._texture2D.Height - this.field_offset <= position.Y))
                    {
                        sprites[i].Update();
                    }
                    */
                    if (sprites[i].CheckIfTarget(position) == 1)
                        return;
                        //sprites[i].LateUpdate();
                        //sprites[i].Update();
                } 
            }

            return;
        }


        public void mouseAction()//int position)
        {
            this.lastMouseState = this.currentMouseState;
            this.LastMousePosition = this.CurrentMousePosition;

            this.currentMouseState = Mouse.GetState();
            this.CurrentMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                if (
                       (this.topleft.X - this.field_offset - TEXTURE_SIZE/2 <= CurrentMousePosition.X)//(this._position2D.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X))
                       &&
                       (this.topright.X + this.field_offset + TEXTURE_SIZE/2 >= CurrentMousePosition.X)//(this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
                       &&
                       ((this.topleft.Y - this.field_offset - TEXTURE_SIZE/2 <= CurrentMousePosition.Y))
                      &&
                       ((this.bottomleft.Y + this.field_offset  + TEXTURE_SIZE/2 >= CurrentMousePosition.Y))
                      )
                {

                    GetFieldPosition(currentMouseState);
                    Console.WriteLine("click");


                }
                else return;
            }

         /*
            for (int i = 0; i < this.sprites.Count; i++)
            {
                this.sprites[i].Update();//this.sprites[i]._texture2D  = this.sprites[i]._textureset2D[(this.sprites[i].state + this.outsidestate / this.sprites[i].stateByTime)%this.sprites[i].maxstates];
            }
         */

            //sprites[position].CheckIfTarget();
            /*
            if (this.MouseInput != null)
            {
                this.lastMouseState = this.currentMouseState;
                this.LastMousePosition = this.CurrentMousePosition;

                this.currentMouseState = Mouse.GetState();
                this.CurrentMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

                //Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);


                if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    if (
                        ((this._position2D.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X))
                        &&
                        ((this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
                        &&
                        ((this._position2D.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y))
                        &&
                        ((this._position2D.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= CurrentMousePosition.Y))
                       )
                    {
                        this.isDragged = true;
                        this.isRotating = true;
                        Console.WriteLine(this._nextright);
                    }
                }


                if (this.isDragged == true)
                {
                    this._rotation += MathHelper.ToRadians(RotationSpeed);
                    this.state = this.state + 1;

                    //Console.WriteLine(isDragged.ToString());

                    if ((                                                                                                                           //right sprite
                    ((this._nextright.X + hitBoxDelta.X <= CurrentMousePosition.X))
                    &&
                    ((this._nextright.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
                    &&
                    ((this._nextright.Y + hitBoxDelta.Y <= CurrentMousePosition.Y))
                    &&
                    ((this._nextright.Y - hitBoxDelta.Y + this._texture2D.Height >= CurrentMousePosition.Y))
                   )
                   &&
                   ((currentMouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)) && (_position2D != _nextright))
                    {
                        this.isDragged = false;
                        this._rotation = 0f;
                        this.isRotating = false;
                        this._position2D = this._positionInit;
                        this.state = 0;
                    }

            return 0;
            */

        }

        public void Update()
        {
            //outsidestate += 1;

            //mouseAction();

            int[] curr = new int[2] { -1, -1 };
            int[] comp = new int[2] { -1, -1 };


            for (int i = 3; i < this.sprites.Count; i++)
            {
                curr = sprites[i].Update();

                if ((curr[0] != -1) && (curr[1] != -1))
                {
                    this.current_sprite[0] = curr[0];
                    this.current_sprite[1] = curr[1];
                }

                if ((current_sprite[0] != comp_sprite[0]) && (current_sprite[1] != comp_sprite[1])&&((comp_sprite[0] != -1) && (comp_sprite[1] != -1)))
                {
                    this.grid.Swap(current_sprite[0], current_sprite[1], comp_sprite[0], comp_sprite[1]);

                    if ((GetSpritePosition(current_sprite[0], current_sprite[1]) != 0) && (GetSpritePosition(current_sprite[0], current_sprite[1]) != 0))
                    {
                        Sprite Temp = sprites[GetSpritePosition(current_sprite[0],current_sprite[1])];
                        
                        sprites[GetSpritePosition(current_sprite[0], current_sprite[1])].Update(sprites[GetSpritePosition(comp_sprite[0],comp_sprite[1])]._textureset2D);
                        sprites[GetSpritePosition(comp_sprite[0], comp_sprite[1])].Update(Temp._textureset2D);
                        
                        current_sprite = new int[2] { -1, -1 };
                        comp_sprite = new int[2] { -1, -1 };

                        for (int k = 0; k < this.sprites.Count; k++)
                        {
                            sprites[i].Update(false);
                        }

                        //return;
                        //sprites[i].Update(this.TextureArray[GetSpritePosition(comp_sprite[0], comp_sprite[1])]);
                    }
     
                }

                this.comp_sprite[0] = current_sprite[0];
                this.comp_sprite[1] = current_sprite[1];
                
            }

            for (int i = 0; i < this.sprites.Count; i++)
            {
                sprites[i].Update();
            }

                // this.current_sprite = new int[2] { -1, -1 };

                /*
                 for (int i = 3; i < this.sprites.Count; i++)
                 {
                     sprites[i].Update(this.TextureArray[grid._field[(i - 3) % 8, (i - 3) / 8] - 3]);
                 }
                */


            }

    }

}
