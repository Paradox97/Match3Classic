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
		private set;
	}

    public Texture2D[] _textureset2D
    {
        get;
        private set;
    }

    public int state;
    public int maxstates;

	public Vector2 _position2D;
    public Vector2 _origin;
    public Vector2 _positionInit;

    public float _rotation,
        RotationSpeed = 3f, MoveSpeed = 3f,
        RotationSoFar = 0f;
    
    public float Scale = 1f;

    const int stateByTime = 10;

    public bool 
        isDragged, isRotating;
    public Input Input;
    public MouseInput MouseInput;

    public float[] WindowSize;

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

    public Sprite(Texture2D[] textureset, float WindowWidth, float WindowHeight)
    {
        this.WindowSize = new float[2];
        this.WindowSize[0] = WindowWidth;
        this.WindowSize[1] = WindowHeight;
        this._textureset2D = textureset;
        this._texture2D = textureset[0];
        this.maxstates = textureset.Length - 3;
    }

    public ButtonState LeftButton { get; }
    public int CheckIfTarget()
    {
        if (this.MouseInput != null)
        {

            Vector2 hitBoxDelta = new Vector2(          //hitbox of a figure is smaller than it appears to avoid collision
                this._texture2D.Width / 10,
                this._texture2D.Height / 10
                );

            this.lastMouseState = this.currentMouseState;

            this.currentMouseState = Mouse.GetState();

            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);


            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                if (
                    ((this._position2D.X - this._origin.X + hitBoxDelta.X <= mousePosition.X))
                    &&
                    ((this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= mousePosition.X))
                    &&
                    ((this._position2D.Y - this._origin.Y + hitBoxDelta.Y <= mousePosition.Y))
                    &&
                    ((this._position2D.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= mousePosition.Y))
                   )
                {
                    this.isDragged = true;
                    this.isRotating = true;
                }
            }

            /*
            if (currentMouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
            {
                if (
                    ((this._position2D.X - this._origin.X + hitBoxDelta.X <= mousePosition.X))
                    &&
                    ((this._position2D.X - this._origin.X + this._texture2D.Width - hitBoxDelta.X >= mousePosition.X))
                    &&
                    ((this._position2D.Y - this._origin.Y + hitBoxDelta.Y <= mousePosition.Y))
                    &&
                    ((this._position2D.Y - this._origin.Y + this._texture2D.Height - hitBoxDelta.Y >= mousePosition.Y))
                   )
                    this.isRotating = true;


            }
            */

            if (this.isDragged == true)
            {
                //this._position2D = mousePosition;
                this._rotation += MathHelper.ToRadians(RotationSpeed);

                this.state = this.state + 1; //% maxstates;

                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    this._rotation = 0f;
                    this.isRotating = false;
                    this._position2D = this._positionInit;
                    this.state = 0;
                    this.isDragged = false;
                }

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

    public void Falldown()
    {

    }
	public void Update(Texture2D[] textureset)
    {
        this._textureset2D = textureset;

        if (CheckIfTarget() == 0)
            return;

        this._texture2D = this._textureset2D[(state / stateByTime) % maxstates];
    }

    public void Update()
    {
        this._texture2D = _textureset2D[(state / stateByTime) % maxstates];

        if (CheckIfTarget() == 0)
            return;
    }

    public void Update(Texture2D texture)
    {
        this._texture2D = texture;

        if (CheckIfTarget() == 0)
            return;

        //this._texture2D = _textureset2D[state];
    }

    private void Move()
    {

        if (Keyboard.GetState().IsKeyDown(this.Input.Down))
        {
            this._position2D.Y += this.MoveSpeed;
        }

        if (Keyboard.GetState().IsKeyDown(this.Input.Left))
        {
            this._position2D.X -= this.MoveSpeed;
        }

        if (Keyboard.GetState().IsKeyDown(this.Input.Right))
        {
            this._position2D.X += this.MoveSpeed;
        }

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
