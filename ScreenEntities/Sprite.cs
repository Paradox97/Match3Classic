using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

public class Sprite
{
	public Texture2D _texture2D
	{
		get;
		set;
	}

    public Texture2D[] _textureset2D
    {
        get;
        set;
    }

    public int state;
    public int maxstates;

    public int[] gridPosition;

    public int[] gridPositionNextLeft;
    public int[] gridPositionNextRight;
    public int[] gridPositionNextUp;
    public int[] gridPositionNextDown;

    public Vector2 _position2D;

    public Vector2 _nextright;
    public Vector2 _nextleft;
    public Vector2 _nextup;
    public Vector2 _nextdown;

    public Vector2 _origin;
    public Vector2 _positionInit;

    public float _rotation,
        RotationSpeed = 3f, MoveSpeed = 3f,
        RotationSoFar = 0f;
    
    public float Scale = 1f;

    public int stateByTime = 15;

    public float field_shift;

    public bool 
        isDragged, isRotating;
    public Input Input;
    public MouseInput MouseInput;

    public float[] WindowSize;

    public Vector2 hitBoxDelta;

    public Vector2 LastMousePosition, CurrentMousePosition;
    MouseState lastMouseState, currentMouseState;
    public Sprite(Texture2D texture, float WindowWidth, float WindowHeight)
	{
        this.WindowSize = new float[2];
        this.WindowSize[0] = WindowWidth; 
        this.WindowSize[1] = WindowHeight;
        this._texture2D = texture;
        this._textureset2D = new Texture2D[1];
        this._textureset2D[0] = texture;
        this.maxstates = 1;             //for immovable objects
	}

    public Sprite(Texture2D[] textureset, Vector2 positioninit, float WindowWidth, float WindowHeight, int gridposition_x, int gridposition_y)
    {
        this.WindowSize = new float[2];
        this.WindowSize[0] = WindowWidth;
        this.WindowSize[1] = WindowHeight;
        this._textureset2D = textureset;
        this._texture2D = textureset[0];
        this.maxstates = textureset.Length - 3;

        this._positionInit = positioninit;
        
        this._position2D = positioninit;
        Console.WriteLine(this._position2D);

        this.gridPosition = new int[2] { gridposition_x, gridposition_y };

        //Console.WriteLine("Element : " + string.Join(",", gridPosition));


        Vector2 hitBoxDelta = new Vector2(          //hitbox of a figure is smaller than it appears to avoid collision
            this._texture2D.Width / 20,
            this._texture2D.Height / 20
            );

        //Console.WriteLine(0);
        GetNextElement();
    }

    public ButtonState LeftButton { get; }

    public void GetNextElement()
    {
        //if (this.gridPosition == null)
         //   return;

        this._nextright = new Vector2(this._position2D.X + this._texture2D.Width + field_shift, this._position2D.Y);
       // Console.WriteLine("Next right", _nextright);
        this._nextleft = new Vector2(this._position2D.X - this._texture2D.Width - field_shift, this._position2D.Y);

        this._nextup = new Vector2(this._position2D.X, this._position2D.Y + this._texture2D.Height + field_shift);
        this._nextdown = new Vector2(this._position2D.X, this._position2D.Y - this._texture2D.Height - field_shift);

        this.gridPositionNextRight = new int [2] { this.gridPosition[0] + 1, this.gridPosition[1]};
        this.gridPositionNextLeft = new int[2] { this.gridPosition[0] - 1, this.gridPosition[1] };
        this.gridPositionNextUp = new int[2] { this.gridPosition[0], this.gridPosition[1] + 1};
        this.gridPositionNextDown = new int[2] { this.gridPosition[0], this.gridPosition[1] - 1};


        int[] topLeft = new int[2] { 0, 0 };
        int[] bottomLeft = new int[2] { 0, 7 };
        int[] topRight = new int[2] { 7, 0 };
        int[] BottomRight = new int[2] { 7, 7 };

        if ((this.gridPosition[0] == topLeft[0]) && (this.gridPosition[1] == topLeft[1]))          //top left  
        {
            this._nextup = _position2D;
            this._nextleft = _position2D;

            this.gridPositionNextLeft = new int[2] { 0, 0 };
            this.gridPositionNextUp = new int[2] { 0, 0 };
            
            Console.WriteLine(0);
        }

        if ((this.gridPosition[0] == bottomLeft[0]) && (this.gridPosition[1] == bottomLeft[1]))       //bottom left
        {
            this._nextdown = _position2D;
            this._nextleft = _position2D;

            this.gridPositionNextLeft = new int[2] { 0, 7 };
            this.gridPositionNextUp = new int[2] { 0, 7 };

            Console.WriteLine(1);
        }

        if ((this.gridPosition[0] == topRight[0]) && (this.gridPosition[1] == topRight[1]))       //top right
        {
            this._nextup = _position2D;
            this._nextright = _position2D;

            this.gridPositionNextLeft = new int[2] { 7, 0 };
            this.gridPositionNextUp = new int[2] { 7, 0 };

            Console.WriteLine(2);
        }

        if ((this.gridPosition[0] == BottomRight[0]) && (this.gridPosition[1] == BottomRight[1]))       //bottom right
        {
            this._nextdown = _position2D;
            this._nextright = _position2D;

            this.gridPositionNextLeft = new int[2] { 7, 7 };
            this.gridPositionNextUp = new int[2] { 7, 7 };

            Console.WriteLine(3);
        }

    }
    public int CheckIfTarget()
    {
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
                    //Console.WriteLine(this._nextright);
                    //Console.WriteLine(this._position2D);
                    //Console.WriteLine(this._nextleft);
                    //Console.WriteLine(this._nextup);
                    //Console.WriteLine(this._nextdown);
                    //Console.WriteLine("Element : " + string.Join(",", this.gridPosition));
                    //Console.WriteLine("Position :", this._position2D);
                    // Console.WriteLine("Next right : " + this._nextright);
                }
            }

            if (this.isDragged == true)
            {
                //this._position2D = mousePosition;

              //  if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Released)
             //   {
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

                return 1;

                /*if ((                                                                                                                               //down sprite
                 (this._nextdown.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X)
                 &&
                 (this._nextdown.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X)
                 &&
                 (this._nextdown.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y)
                 &&
                 (this._nextdown.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= CurrentMousePosition.Y)
                ) 
                &&
                ((currentMouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)))
                 {
                     this.isDragged = false;
                     this._rotation = 0f;
                     this.isRotating = false;
                     this._position2D = this._positionInit;
                     this.state = 0;
                 }

                 if ((                                                                                                                               //left sprite
                 (this._nextleft.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X)
                 &&
                 (this._nextleft.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X)
                 &&
                 (this._nextleft.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y)
                 &&
                 (this._nextleft.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= CurrentMousePosition.Y)
                )
                &&
                ((currentMouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)))
                 {
                     this.isDragged = false;
                     this._rotation = 0f;
                     this.isRotating = false;
                     this._position2D = this._positionInit;
                     this.state = 0;
                 }

                 if ((                                                                                                                               //up sprite
                 (this._nextup.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X)
                 &&
                 (this._nextup.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X)
                 &&
                 (this._nextup.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y)
                 &&
                 (this._nextup.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= CurrentMousePosition.Y)
                ) 
                &&
                ((currentMouseState.LeftButton == ButtonState.Pressed)&&(lastMouseState.LeftButton == ButtonState.Released)))
                 {
                     this.isDragged = false;
                     this._rotation = 0f;
                     this.isRotating = false;
                     this._position2D = this._positionInit;
                     this.state = 0;
                 }
                */

                // }

                // if ()
                //    { 

                // this.state = this.state + 1; //% maxstates;
                //   this._rotation = 0f;
                //    this.isRotating = false;
                //   this._position2D = this._positionInit;
                //    this.state = 0;
                //    this.isDragged = false;
                //   }

            }

            /*
            if (this.isRotating == true)
            {
                this._rotation += MathHelper.ToRadians(RotationSpeed);
                if
                    (currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                {
                    this._rotation = 0f;
                    this.isRotating = false;
                }
            }
            */
        }
        return 0;
    }


    public int CheckIfTarget(MouseState current)
    {
        if (this.MouseInput != null)
        {
            this.lastMouseState = this.currentMouseState;
            this.LastMousePosition = this.CurrentMousePosition;

            this.currentMouseState = current;
            this.CurrentMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                if (
                    ((this._position2D.X - this._origin.X + hitBoxDelta.X <= CurrentMousePosition.X))
                    &&
                    ((this._position2D.X - this._origin.X + this._texture2D.Width + hitBoxDelta.X >= CurrentMousePosition.X))
                    &&
                    ((this._position2D.Y - this._origin.Y + hitBoxDelta.Y <= CurrentMousePosition.Y))
                    &&
                    ((this._position2D.Y - this._origin.Y + this._texture2D.Height + hitBoxDelta.Y >= CurrentMousePosition.Y))
                   )
                {
                    this.isDragged = true;
                    this.isRotating = true;
                    //Console.WriteLine(this._nextright);
                    //Console.WriteLine(this._position2D);
                    //Console.WriteLine(this._nextleft);
                    //Console.WriteLine(this._nextup);
                    //Console.WriteLine(this._nextdown);
                    //Console.WriteLine("Element : " + string.Join(",", this.gridPosition));
                    //Console.WriteLine("Position :", this._position2D);
                    // Console.WriteLine("Next right : " + this._nextright);
                }
            }

            if (this.isDragged == true)
            {
                //this._position2D = mousePosition;

                //  if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Released)
                //   {
                this._rotation += MathHelper.ToRadians(RotationSpeed);
                this.state = this.state + 1;

                //Console.WriteLine(isDragged.ToString());

                //if ((                                                                                                                           //right sprite
                //((this._nextright.X <= CurrentMousePosition.X))))
             //    &&
              //  ((this._nextright.X + this._texture2D.Width - hitBoxDelta.X >= CurrentMousePosition.X))
             //   &&
              //  ((this._nextright.Y + hitBoxDelta.Y <= CurrentMousePosition.Y))
              //  &&
              //  ((this._nextright.Y - hitBoxDelta.Y + this._texture2D.Height >= CurrentMousePosition.Y))
             //  )
             //  &&
             //  ((currentMouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)) && (_position2D != _nextright))
                this.isDragged = false;
                this._rotation = 0f;
                this.isRotating = false;
                this._position2D = this._positionInit;
                this.state = 0;

                return 1;
            }
        }
        return 0;
    }

    public void Update(bool marker)
    {
        if (marker == false)
        {
            this.state = 0;
            this.isDragged = false;
            this.isRotating = false;
            this._rotation = MathHelper.ToRadians(0);
        }

        return;
    }

    public int Update(int i)
    {
        this.isDragged = true;
        this.isRotating = true;
        this._rotation += MathHelper.ToRadians(RotationSpeed);
        this.state = this.state + 1;
        Console.WriteLine("DDD" , this._position2D);
        this._texture2D = _textureset2D[(state / stateByTime) % maxstates];

        /*
            if (this.isDragged == true)
            {
                //this._position2D = mousePosition;

                //  if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Released)
                //   {
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
        }
        */
        return 0;
    }

    public void Falldown()
    {

    }
	public void Update(Texture2D[] textureset)
    {
        this._textureset2D = textureset;
        this._texture2D = textureset[0];

        //if (CheckIfTarget() == 0)
        //  return;
        this.state = 0;
        //this._texture2D = this._textureset2D[(state / stateByTime) % maxstates];
    }

    public int[] Update()
    {
        this._texture2D = _textureset2D[(state / stateByTime) % maxstates];

        if (CheckIfTarget() == 0)
            return new int[2] { -1, -1 };

        //Console.WriteLine("D");
        return new int[2] { gridPosition[0], gridPosition[1] };
    }

    public void Update(Texture2D texture)
    {
        this._texture2D = texture;

        if (CheckIfTarget() == 0)
            return;

        //this._texture2D = _textureset2D[state];
    }

	public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(
            _texture2D, _position2D, null,
            Color.White, _rotation, _origin,
            this.Scale, SpriteEffects.None, 0f
            );
    }
}
