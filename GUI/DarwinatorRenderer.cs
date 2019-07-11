using Darwinizator;
using FbonizziMonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GUI
{
    public class DarwinatorRenderer : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; }
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private readonly Simulator _simulator;
        private readonly HexToColorConverter _hexToColorConverter;

        private bool _debugMode = false;
        private bool _paused = false;

        private KeyboardState _lastKey;

        public DarwinatorRenderer(Simulator simulator)
        {
            _simulator = simulator;

            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight
            };

            GraphicsDeviceManager.PreferredBackBufferWidth = simulator.WorldXSize;
            GraphicsDeviceManager.PreferredBackBufferHeight = simulator.WorldYSize;

            IsMouseVisible = true;
            _hexToColorConverter = new HexToColorConverter();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("TextFont");
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyUp(Keys.P) && _lastKey.IsKeyDown(Keys.P))
                _paused = !_paused;
            _lastKey = keyboardState;

            if (_paused)
                return;

            _simulator.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (var specie in _simulator.Population)
            {
                foreach (var animal in specie.Value)
                {
                    _spriteBatch.DrawRectangle(
                        rectangleDefinition: animal.Mass.ToXnaRectangle(),
                        fillColor: _hexToColorConverter.ConvertToXnaColor(animal.Color));

                    if (_debugMode)
                    {
                        // TODO fai hover
                        _spriteBatch.DrawString(
                            _font, 
                            animal.Name, 
                            new Vector2(animal.Mass.PosX, animal.Mass.PosY), 
                            Color.White);
                    }
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
