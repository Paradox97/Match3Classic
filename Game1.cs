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

        private SpriteField spritefield;
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

        public Texture2D[][] _CountArray;

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
            shape = new Shape();
            //grid.Render();
            this.screen = new Screen();
            //this._CountArray = new Texture2D[5][];

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //gametime = new GameTime();
            _graphics.PreferredBackBufferWidth = 455;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 650;   // set this value to the desired height of your window
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            this._CountArray = new Texture2D[6][];

            this.textures = new List<Texture2D>();

            textures.Add(Content.Load<Texture2D>("img/text/match 3"));      //0
            textures.Add(Content.Load<Texture2D>("img/text/classic_resize"));   //1
            textures.Add(Content.Load<Texture2D>("img/fields/field2")); //2

            this.texturelist = new Texture2D[43];

            this.texturelist[0] = Content.Load<Texture2D>("img/text/match 3");
            this.texturelist[1] = Content.Load<Texture2D>("img/text/classic_resize");
            this.texturelist[2] = Content.Load<Texture2D>("img/fields/field2");


            //Initial Textures
            textures.Add(Content.Load<Texture2D>("img/figures/квадрат/квадратек")); //3         
            textures.Add(Content.Load<Texture2D>("img/figures/сердце/пердечко")); //4
            textures.Add(Content.Load<Texture2D>("img/figures/шар/шарек"));  //5
            textures.Add(Content.Load<Texture2D>("img/figures/пирамида/перамидка")); //6
            textures.Add(Content.Load<Texture2D>("img/figures/кристалл/кристаллек"));  //7


            //Texture States
            //квадрат 1

            this._CountArray[0] = new Texture2D[9] {
            Content.Load<Texture2D>("img/figures/квадрат/квадратек"),
            Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 1"),
            Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 2"),
            Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 3"),
            Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 2"),
            Content.Load<Texture2D>("img/figures/квадрат/сияние квадратек 1"),
            Content.Load<Texture2D>("img/figures/квадрат/стрелка квадратек вбок"),
            Content.Load<Texture2D>("img/figures/квадрат/стрелка кавдратек вверх"),
            Content.Load<Texture2D>("img/figures/квадрат/бомба квадратек")
            };

            //сердце 2

            this._CountArray[1] = new Texture2D[9]{
            Content.Load<Texture2D>("img/figures/сердце/пердечко"),
            Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 1"),
            Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 2"),
            Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 3"),
            Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 2"),
            Content.Load<Texture2D>("img/figures/сердце/сияние пердечко 1"),
            Content.Load<Texture2D>("img/figures/сердце/стрелка сердечко вбок"),
            Content.Load<Texture2D>("img/figures/сердце/стрелка сердечко вверх"),
            Content.Load<Texture2D>("img/figures/сердце/бомба пердечко")
            };

            //this._CountArray[1] = Temp;
            //шар 2
            this._CountArray[2] = new Texture2D[9] {
            Content.Load<Texture2D>("img/figures/шар/шарек"),
            Content.Load<Texture2D>("img/figures/шар/сияние шарек 1"),
            Content.Load<Texture2D>("img/figures/шар/сияние шарек 2"),
            Content.Load<Texture2D>("img/figures/шар/сияние шарек 3"),
            Content.Load<Texture2D>("img/figures/шар/сияние шарек 2"),
            Content.Load<Texture2D>("img/figures/шар/сияние шарек 1"),
            Content.Load<Texture2D>("img/figures/шар/стрелка шарек вбок"),
            Content.Load<Texture2D>("img/figures/шар/стрелка шарек вверх"),
            Content.Load<Texture2D>("img/figures/шар/бомба шарек")
            };

            //пирамида 3

            this._CountArray[3] = new Texture2D[9] {
            Content.Load<Texture2D>("img/figures/пирамида/перамидка"),
            Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 1"),
            Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 2"),
            Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 3"),
            Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 2"),
            Content.Load<Texture2D>("img/figures/пирамида/сияние перамидка 1"),
            Content.Load<Texture2D>("img/figures/пирамида/стрелка пирамидка вбок"),
            Content.Load<Texture2D>("img/figures/пирамида/стрелка пирамидка вверх"),
            Content.Load<Texture2D>("img/figures/пирамида/бомба перамидка")
            };
            //кристалл 4

            this._CountArray[4] = new Texture2D[9] {
            Content.Load<Texture2D>("img/figures/кристалл/кристаллек"),
            Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 1"),
            Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 2"),
            Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 3 "),
            Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 2"),
            Content.Load<Texture2D>("img/figures/кристалл/кристалл сияние 1"),
            Content.Load<Texture2D>("img/figures/кристалл/стрелка кристаллек вбок"),
            Content.Load<Texture2D>("img/figures/кристалл/стрелка кристаллек вверх"),
            Content.Load<Texture2D>("img/figures/кристалл/бомба кресталлек")        //добавить появление
            };
            //эффекты 
            this._CountArray[5] = new Texture2D[5] {
            Content.Load<Texture2D>("img/fx/нитро вверх"),
            Content.Load<Texture2D>("img/fx/нитро вниз"),
            Content.Load<Texture2D>("img/fx/нитро лево"),
            Content.Load<Texture2D>("img/fx/нитро право"),
            Content.Load<Texture2D>("img/fx/огонек")
            };

            float deltaField = 5f;

            //float[] logoLocation = new float[2] {1f,0f};
            float [] logoLocation = this.screen.GetLogoLocation(Window, textures[0]);
            //_texture2D = Content.Load<Texture2D>("img/match 3");
            
            Texture2D[] logolist = new Texture2D[3];
            logolist[0] = this.texturelist[0];
            logolist[1] = this.texturelist[1];
            logolist[2] = this.texturelist[2];



            /*
            for (int k = 0; k < 8; k++)
            {
                texturelist[] = 
            }
            */

            //logos
            this.sprites = new List<Sprite>() {
                new Sprite(logolist[0], Window.ClientBounds.Width, Window.ClientBounds.Height)
                {
                    _position2D = new Vector2
                    (
                       logoLocation[0],
                       logoLocation[1]
                    )
                }
                ,
                new Sprite(logolist[1], Window.ClientBounds.Width, Window.ClientBounds.Height)
                {
                    _position2D = new Vector2
                    (
                        logoLocation[0]+logolist[0].Width-logolist[1].Width,
                        0+logolist[0].Height
                    )
                },
                new Sprite(logolist[2], Window.ClientBounds.Width, Window.ClientBounds.Height)
                {
                    _position2D = new Vector2
                    (
                        5, 
                        0+logolist[0].Height+logolist[1].Height
                    )
                }   
            };

            Texture2D[] CountArray = new Texture2D[40];

            //Texture2D[][] _CountArray = new Texture2D[5][];

            int length = this.texturelist.Length;

            Vector2[][][] NextElements = new Vector2[8][][];

            int k = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
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

            this.spritefield = new SpriteField(this.sprites, this._CountArray, deltaField);

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           // for (int i = 0; i < 3; i++)
           // {
                //sprites[i].Update(textures[grid._field[(i-3) % 8, (i-3)/8]]);
                //sprites[i].Update(this._CountArray[grid._field[(i-3) % 8, (i-3) / 8] - 3]);
             //   sprites[i].Update();
           // }

            //spritefield.Draw(_spriteBatch);
            spritefield.Update();
            base.Draw(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            //foreach (var sprite in this.sprites)
            //sprite.Draw(_spriteBatch);

            //sprites[0].Draw(_spriteBatch);
            //sprites[1].Draw(_spriteBatch);
            //sprites[2].Draw(_spriteBatch);

            spritefield.Draw(_spriteBatch);

            //_spriteBatch.Draw(_texture2D, _position2D, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Update()
        {
        
        }



    }
}
