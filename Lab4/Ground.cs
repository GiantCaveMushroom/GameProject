using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Lab4
{
    class Ground : BasicModel
    {
        public Ground(Model model)
            : base(model)
        {

        }

        public override void Draw(GraphicsDevice device, Camera camera)
        {
            base.Draw(device, camera);
        }
    }

}
