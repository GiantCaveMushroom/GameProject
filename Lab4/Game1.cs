using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4
{
    public class Game1 : Game
    {
        //public property
        public Camera camera { get; protected set; }

        //private property
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ModelManager modelManager;

        Level area;
        

        Vector3 pos1 = Vector3.Zero;
        Vector3 pos2 = new Vector3 (100f, 0f, 400f);
        Vector3 pos3 = new Vector3 (100f, 0f, 400f);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            area = new Level();
        }

        protected override void Initialize()
        {
            camera = new Camera(this, new Vector3(800, 400, 900), new Vector3(0, 50, 0), Vector3.Up);
            Components.Add(camera);

            modelManager = new ModelManager(this);
            Components.Add(modelManager);

            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Box(pos1)
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            base.Update(gameTime);
        }

        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            base.Draw(gameTime);
        }
    }
}
