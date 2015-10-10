using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab4
{
    class BasicModel
    {
        public Model model { get; protected set; }

        protected Matrix world = Matrix.Identity;
        //public Matrix translation, rotation, scale;

        public BasicModel(Model model)
        {
            this.model = model;
            //translation = rotation = scale = Matrix.Identity;
            world = Matrix.Identity;
        }

        public BasicModel(Texture2D texture, int number)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GraphicsDevice device, Camera camera)
        {

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = transforms[mesh.ParentBone.Index] * GetWorld();
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }

        protected virtual Matrix GetWorld()
        {
            return world;
        }
    }
}
