using Darwinizator;
using FbonizziMonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace GUI
{
    public class DarwinatorRenderer : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; }
        private SpriteBatch _spriteBatch;
        private readonly Simulator _simulator;

        private readonly int _cellSize;

        public DarwinatorRenderer(
            Simulator simulator,
            int cellSize)
        {
            _simulator = simulator;
            _cellSize = cellSize;

            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight
            };

            GraphicsDeviceManager.PreferredBackBufferWidth = simulator.WorldXSize * _cellSize;
            GraphicsDeviceManager.PreferredBackBufferHeight = simulator.WorldYSize * _cellSize;

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
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
                    // TODO Ovviamente ognuno avrà il suo rettangolo,
                    // fai un wrapper "sprite" per ogni oggetto del simulator
                    // crearlo nuovo ogni giro è lentissimo
                    _spriteBatch.DrawRectangle(
                        new Rectangle(
                            (int)(animal.PosX * _cellSize),
                            (int)(animal.PosY * _cellSize),
                            _cellSize,
                            _cellSize),
                        animal.Color.ConvertToXnaColor());
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
