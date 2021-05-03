using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Match3Classic
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Shape shape;
        Field grid;
        public GameTime gametime;
        
        public char[,] field 
        { 
            get; set; 
        }

        public TimeSpan TotalGameTime { 
            get;
            set;
        }

        private List<Sprite> sprites;
        private List<Texture2D> textures;
        private Screen screen;

        /*
        public TimeSpan Total
        {
            get { return Total; }
            set { Total = value; }
        }


        public TimeSpan ElapsedGameTime
        {
            get { return ElapsedGameTime; }
            set { ElapsedGameTime = value; }
        }

         public GameTime(TimeSpan TotalGameTime, TimeSpan ElapsedGameTime) 
        {

        }
        */

        public Game1()
        {
            this.grid = new Field();
            shape = new Shape(grid);
            grid.Render();
            this.screen = new Screen();


            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //gametime = new GameTime();
            _graphics.PreferredBackBufferWidth = 1024;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            this.textures = new List<Texture2D>();

            

            
            textures.Add(Content.Load<Texture2D>("img/text/match 3"));
            textures.Add(Content.Load<Texture2D>("img/text/classic_resize"));
            textures.Add(Content.Load<Texture2D>("img/fields/field2"));
            textures.Add(Content.Load<Texture2D>("img/figures/Circle"));
            textures.Add(Content.Load<Texture2D>("img/figures/Heart"));
            textures.Add(Content.Load<Texture2D>("img/figures/Rectangle"));
            textures.Add(Content.Load<Texture2D>("img/figures/Star"));
            textures.Add(Content.Load<Texture2D>("img/figures/Star2"));

           

            float deltaField = 5f;

            //float[] logoLocation = new float[2] {1f,0f};
            float [] logoLocation = this.screen.GetLogoLocation(Window, textures[0]);
            //_texture2D = Content.Load<Texture2D>("img/match 3");


            this.sprites = new List<Sprite>() {
                new Sprite(textures[0])
                {
                   /* _origin = new Vector2
                    (
                       logoLocation[0]/2,
                       logoLocation[1]/2
                     ),
                   */
                    _position2D = new Vector2
                    (
                       logoLocation[0],
                       logoLocation[1]
                    ),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                        Left = Keys.A,
                        Right = Keys.D
                    }
                }
                ,
                new Sprite(textures[1])
                {
                    /*
                    _origin = new Vector2
                    (
                        textures[1].Width/2 ,
                        textures[1].Height/2
                     ),
                    */
                    _position2D = new Vector2
                    (
                        logoLocation[0]+textures[0].Width-textures[1].Width,
                        0+textures[0].Height
                    ),

                    Input = new Input()
                    {
                    }
                },
                new Sprite(textures[2])
                {
                    _position2D = new Vector2
                    (
                        10, 
                        0+textures[0].Height+textures[1].Height
                    ),

                    Input = new Input()
                    {
                    }
                }   
            };


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.sprites.Add(new Sprite(textures[grid._field[i,j]])
                    {
                        _positionInit = new Vector2(
                            sprites[2]._position2D.X + i * deltaField + i * textures[grid._field[i, j]].Width + textures[grid._field[i, j]].Width/2,
                            sprites[2]._position2D.Y + j * deltaField + j * textures[grid._field[i, j]].Height + textures[grid._field[i, j]].Height / 2 + deltaField
                            ),

                        _position2D = new Vector2           //в конструктор из positionInit
                        (
                            sprites[2]._position2D.X + i * deltaField + i * textures[grid._field[i, j]].Width + textures[grid._field[i,j]].Width / 2,
                            sprites[2]._position2D.Y + j * deltaField + j * textures[grid._field[i, j]].Height + textures[grid._field[i, j]].Height / 2 + deltaField
                        ),

                        _origin = new Vector2
                            (
                            textures[grid._field[i, j]].Width / 2,
                            textures[grid._field[i, j]].Height / 2
                            ),

                        Input = new Input()
                        {
                        },
                        MouseInput = new MouseInput()
                        {

                        }
                    });
                }
             }
    

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (Mouse.GetState.)
            //Keyboard keyboard = Keyboard.GetState();
            /*switch (key)
            {
                case (key.W)

            }*/

            foreach (var sprite in this.sprites)
                sprite.Update();
             //for (int i = 0; i < this.sprites.Count; i++) {
              //  this.sprites[i].Update();
             // }
         

            //sprite1.Update();

            // TODO: Add your update logic here
            base.Draw(gameTime);
            base.Update(gameTime);
            //Debug.WriteLine("Time", gametime.TotalGameTime.TotalSeconds);
            //System.Threading.Thread.Sleep(500);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (var sprite in this.sprites)
                sprite.Draw(_spriteBatch);

            //_spriteBatch.Draw(_texture2D, _position2D, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
