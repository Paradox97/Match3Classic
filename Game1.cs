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

        const int TEXTURE_SIZE = 50;

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

        public struct TextureList
        {
            List<Texture2D> textures;

            public TextureList(List<Texture2D> textures)
            {
                this.textures = textures;
            }
        }

        private List<TextureList> _texturelist;

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
            //grid.Render();
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

            List<Texture2D> temp = new List<Texture2D>();

            this.textures = new List<Texture2D>();
            this._texturelist = new List<TextureList>();

            textures.Add(Content.Load<Texture2D>("img/text/match 3"));      //0
            textures.Add(Content.Load<Texture2D>("img/text/classic_resize"));   //1
            textures.Add(Content.Load<Texture2D>("img/fields/field2")); //2

            this._texturelist.Add(new TextureList(textures));

            //Initial Textures
            textures.Add(Content.Load<Texture2D>("img/figures/квадрат/квадратек")); //3         
            textures.Add(Content.Load<Texture2D>("img/figures/сердце/пердечко")); //4
            textures.Add(Content.Load<Texture2D>("img/figures/шар/шарек"));  //5
            textures.Add(Content.Load<Texture2D>("img/figures/пирамида/перамидка")); //6
            textures.Add(Content.Load<Texture2D>("img/figures/кристалл/кристаллек"));  //7

            //Texture States
            //квадрат 0
            temp.Add(Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 1")); //0
            temp.Add(Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 2")); //1
            temp.Add(Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 3")); //2

            temp.Add(Content.Load<Texture2D>("img/figures/квадрат/стрелка квадратек вбок")); //3
            temp.Add(Content.Load<Texture2D>("img/figures/квадрат/стрелка кавдратек вверх")); //4
            temp.Add(Content.Load<Texture2D>("img/figures/квадрат/бомба квадратек")); //5

            temp = new List<Texture2D>();
            //сердце 1
            temp.Add(Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 1")); //0
            temp.Add(Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 2")); //1
            temp.Add(Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 3")); //2

            temp.Add(Content.Load<Texture2D>("img/figures/сердце/стрелка сердечко вбок")); //3
            temp.Add(Content.Load<Texture2D>("img/figures/сердце/стрелка сердечко вверх")); //4
            temp.Add(Content.Load<Texture2D>("img/figures/сердце/бомба пердечко")); //5

            temp = new List<Texture2D>();
            //шар 2
            temp.Add(Content.Load<Texture2D>("img/figures/шар/сияние шарек 1")); //0
            temp.Add(Content.Load<Texture2D>("img/figures/шар/сияние шарек 2")); //1
            temp.Add(Content.Load<Texture2D>("img/figures/шар/сияние шарек 3")); //2

            temp.Add(Content.Load<Texture2D>("img/figures/шар/стрелка шарек вбок")); //3
            temp.Add(Content.Load<Texture2D>("img/figures/шар/стрелка шарек вверх")); //4
            temp.Add(Content.Load<Texture2D>("img/figures/шар/бомба шарек")); //5

            temp = new List<Texture2D>();
            //пирамида 3
            temp.Add(Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 1")); //0
            temp.Add(Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 2")); //1
            temp.Add(Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 3")); //2

            temp.Add(Content.Load<Texture2D>("img/figures/пирамида/стрелка пирамидка вбок")); //3
            temp.Add(Content.Load<Texture2D>("img/figures/пирамида/стрелка пирамидка вверх")); //4
            temp.Add(Content.Load<Texture2D>("img/figures/пирамида/бомба перамидка")); //5

            temp = new List<Texture2D>();
            //кристалл 4
            temp.Add(Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 1")); //31
            temp.Add(Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 2")); //32
            temp.Add(Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 3 ")); //33

            temp.Add(Content.Load<Texture2D>("img/figures/кристалл/стрелка кристаллек вбок")); //34
            temp.Add(Content.Load<Texture2D>("img/figures/кристалл/стрелка кристаллек вверх")); //35
            temp.Add(Content.Load<Texture2D>("img/figures/кристалл/бомба кресталлек")); //36

            temp = new List<Texture2D>();
            //эффекты 
            temp.Add(Content.Load<Texture2D>("img/fx/нитро вверх")); //8
            temp.Add(Content.Load<Texture2D>("img/fx/нитро вниз")); //9
            temp.Add(Content.Load<Texture2D>("img/fx/нитро лево")); //10
            temp.Add(Content.Load<Texture2D>("img/fx/нитро право")); //11
            temp.Add(Content.Load<Texture2D>("img/fx/огонек")); //12

            temp = new List<Texture2D>();

            float deltaField = 5f;

            //float[] logoLocation = new float[2] {1f,0f};
            float [] logoLocation = this.screen.GetLogoLocation(Window, textures[0]);
            //_texture2D = Content.Load<Texture2D>("img/match 3");


            //logos
            this.sprites = new List<Sprite>() {
                new Sprite(textures[0], Window.ClientBounds.Width, Window.ClientBounds.Height)
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
                    )
                }
                ,
                new Sprite(textures[1], Window.ClientBounds.Width, Window.ClientBounds.Height)
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
                    )
                },
                new Sprite(textures[2], Window.ClientBounds.Width, Window.ClientBounds.Height)
                {
                    _position2D = new Vector2
                    (
                        5, 
                        0+textures[0].Height+textures[1].Height
                    )
                }   
            };

            //figures
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                        this.sprites.Add(new Sprite(textures[grid._field[i, j]], Window.ClientBounds.Width, Window.ClientBounds.Height)
                        {
                            _positionInit = new Vector2(
                                sprites[2]._position2D.X + i * deltaField + i * TEXTURE_SIZE + TEXTURE_SIZE / 2 + deltaField,
                                sprites[2]._position2D.Y + j * deltaField + j * TEXTURE_SIZE + TEXTURE_SIZE / 2 + deltaField
                                ),

                            _position2D = new Vector2           //в конструктор из positionInit
                            (
                                sprites[2]._position2D.X + i * deltaField + i * TEXTURE_SIZE + TEXTURE_SIZE / 2 + deltaField,
                                sprites[2]._position2D.Y + j * deltaField + j * TEXTURE_SIZE + TEXTURE_SIZE / 2 + deltaField
                            ),

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 3; i < sprites.Count; i++)
            {
                sprites[i].Update(textures[grid._field[(i-3) % 8, (i-3)/8]]);
            }

            base.Draw(gameTime);
            base.Update(gameTime);
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
