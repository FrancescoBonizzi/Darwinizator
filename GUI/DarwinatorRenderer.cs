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

        public bool DebugMode { get; set; } = false;
        public bool Paused { get; set; } = false;
        public bool Rendering { get; set; } = true;

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
            if (Paused)
                return;

            _simulator.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (!Rendering)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(
                    _font,
                    "Rendering disabled",
                    new Vector2(20, 20),
                    Color.Yellow);
                _spriteBatch.End();
                return;
            }

            _spriteBatch.Begin();

            foreach(var veg in _simulator.Vegetables)
            {
                _spriteBatch.DrawRectangle(
                     rectangleDefinition: veg.Mass.ToXnaRectangle(),
                     fillColor: _hexToColorConverter.CalculateColor(veg));
            }

            foreach (var specie in _simulator.Population)
            {
                foreach (var animal in specie.Value)
                {
                    _spriteBatch.DrawRectangle(
                        rectangleDefinition: animal.Mass.ToXnaRectangle(),
                        fillColor: _hexToColorConverter.CalculateColor(animal));

                    if (DebugMode)
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
