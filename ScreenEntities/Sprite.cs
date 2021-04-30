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

	public Vector2 _position2D;
    public Vector2 _origin;
    public Vector2 _positionInit;

    public float _rotation,
        RotationSpeed = 3f, MoveSpeed = 3f;
    
    public float Scale = 1f;
    
    public bool 
        isDragged, isRotating;
    public Input Input;

    MouseState lastMouseState, currentMouseState;
    public Sprite(Texture2D texture)
	{
        this._texture2D = texture;
	}

    public ButtonState LeftButton { get; }
    public void CheckIfTarget()
    {
        Vector2 hitBoxDelta = new Vector2(          //hitbox of a figure is smaller than it appears to avoid collision
            this._texture2D.Width / 10,
            this._texture2D.Height / 10
            );

        this.lastMouseState = this.currentMouseState;

        this.currentMouseState = Mouse.GetState();

        Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

        /*
        if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
        {
            if (
                ((this._position2D.X + hitBoxDelta.X <= mousePosition.X))
                &&
                ((this._position2D.X + this._texture2D.Width - hitBoxDelta.X >= mousePosition.X))
                &&
                ((this._position2D.Y + hitBoxDelta.Y <= mousePosition.Y))
                &&
                ((this._position2D.Y + this._texture2D.Height - hitBoxDelta.Y >= mousePosition.Y))
               )
                this.isDragged = true;
            
            
        }*/

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
                this.isDragged = true;


        }


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


        if (this.isDragged == true)
        {
            this._position2D = mousePosition;
            if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
            {
                this._position2D = this._positionInit;
                this.isDragged = false;
            }
        }

        if (this.isRotating == true)
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

                this._rotation += MathHelper.ToRadians(RotationSpeed);
                if
                    (currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                {
                    this.isRotating = false;
                }
            }
        }



    }

	public void Update()
    {
        CheckIfTarget();
        //Move();

        //var newState = Keyboard.GetPres;
        
        /*Keyboard key = Keyboard.GetState();
        

        switch (key)
        {
            case (Keys.W):
                this._position2D.Y -= Speed;
                break;
            case (Keys.S):
                this._position2D.Y -= Speed;
                break;
            case (Keys.A):
                this._position2D.Y -= Speed;
                break;
            case (Keys.D):
                this._position2D.Y -= Speed;
                break;
        }*/
        
    }

    private void Move()
    {
        //if (Input == null)
          //  return;

        if (Keyboard.GetState().IsKeyDown(this.Input.Up))
        {
            // if (this._position2D.Y > 0)
            // {
            this._position2D.Y -= this.MoveSpeed;
            // }
        }

        if (Keyboard.GetState().IsKeyDown(this.Input.Down))
        {
            //  if (this._position2D.X > 0)
            // {
            this._position2D.Y += this.MoveSpeed;
            // }
        }

        if (Keyboard.GetState().IsKeyDown(this.Input.Left))
        {
            //if (this._position2D.X > 0)
            // {
            this._position2D.X -= this.MoveSpeed;
            // }

        }

        if (Keyboard.GetState().IsKeyDown(this.Input.Right))
        {
            //if (this._position2D.X > 0)
            // {
            this._position2D.X += this.MoveSpeed;
            // }
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
