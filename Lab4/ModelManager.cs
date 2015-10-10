using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab4
{
    class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<BasicModel> models = new List<BasicModel>();

        Tank tank1;
        int playerX, playerY;

        public ModelManager(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //models.Add(new Ground(Game.Content.Load<Model>(@"Models/Ground/Ground")));
            tank1 = new Tank(Game.Content.Load<Model>(@"tank_xnb/tank"), ((Game1)Game).GraphicsDevice, ((Game1)Game).camera);
            models.Add(tank1);
            //models.Add(new NPC2(Game.Content.Load<Model>(@"tank_xnb/tank2"), tank1));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel model in models)
            {
                model.Draw(((Game1)Game).GraphicsDevice, ((Game1)Game).camera);
                model.Update(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
