using Darwinizator;
using FbonizziMonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public class DarwinatorRenderer : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; }
        private SpriteBatch _spriteBatch;

        private readonly Simulator _simulator;
        private readonly HexToColorConverter _hexToColorConverter;

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
                    _spriteBatch.DrawRectangle(
                        rectangleDefinition: animal.Mass.ToXnaRectangle(),
                        fillColor: _hexToColorConverter.ConvertToXnaColor(animal.Color));
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
