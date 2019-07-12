using Darwinizator;
using FbonizziMonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private bool _rendering = true;

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
            else if (keyboardState.IsKeyUp(Keys.R) && _lastKey.IsKeyDown(Keys.R))
                _rendering = !_rendering;
            else if (keyboardState.IsKeyUp(Keys.D) && _lastKey.IsKeyDown(Keys.D))
                _debugMode = !_debugMode;

            _lastKey = keyboardState;

            if (_paused)
                return;

            _simulator.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (!_rendering)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(
                    _font,
                    "Rendering disabled " +
                    Environment.NewLine +
                    "Press 'R' to switch",
                    new Vector2(100, 100),
                    Color.Yellow);
                _spriteBatch.End();
                return;
            }

            _spriteBatch.Begin();

            foreach(var veg in _simulator.Vegetables)
            {
                _spriteBatch.DrawRectangle(
                    veg.Mass.ToXnaRectangle(),
                    Color.ForestGreen);
            }

            foreach (var specie in _simulator.Population)
            {
                foreach (var animal in specie.Value)
                {
                    _spriteBatch.DrawRectangle(
                        rectangleDefinition: animal.Mass.ToXnaRectangle(),
                        fillColor: _hexToColorConverter.ConvertToXnaColor(animal.Color));

                    if (_debugMode)
                    {
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
