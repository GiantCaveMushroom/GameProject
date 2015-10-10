using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4
{
    class NPC2 : BasicModel
    {
        Matrix translation = Matrix.Identity;
        ModelBone turretBone, cannonBone, hatchBone, tankBone;
        ModelBone lFrontWBone, rFrontWBone, lSteer, rSteer, lBackWBone, rBackWBone;
        Matrix turretTransform, lFrontTransform, rFrontTransform, lBackTransform, rBackTransform;

        Tank tank;
        Vector3 target, position, difference;
        float dirX, dirZ;
        float distance = 150;
        float distant;

        public NPC2(Model model, Tank destination) : base(model)
        {
            #region boneyard
            turretBone = model.Bones["turret_geo"];
            cannonBone = model.Bones["canon_geo"];
            hatchBone = model.Bones["hatch_geo"];
            lFrontWBone = model.Bones["l_front_wheel_geo"];
            rFrontWBone = model.Bones["r_front_wheel_geo"];
            lSteer = model.Bones["l_steer_geo"];
            rSteer = model.Bones["r_steer_geo"];
            lBackWBone = model.Bones["l_back_wheel_geo"];
            rBackWBone = model.Bones["r_back_wheel_geo"];
            tankBone = model.Bones["tank_geo"];
            #endregion
            #region boneTransform
            if (turretBone != null) turretTransform = turretBone.Transform;
            if (lFrontWBone != null) lFrontTransform = lFrontWBone.Transform;
            if (rFrontWBone != null) rFrontTransform = rFrontWBone.Transform;
            if (lBackWBone != null) lBackTransform = lBackWBone.Transform;
            if (rBackWBone != null) rBackTransform = rBackWBone.Transform;
            #endregion
            tank = destination;
            translation *= Matrix.CreateTranslation(0, 0, -1200);
            distant = distance + 20;
        }

        public override void Update(GameTime gameTime)
        {
            target.X = tank.position.X;
            target.Z = tank.position.Z;

            position = translation.Translation;
            difference = target - position; //For rotation
            dirX = (target.X - position.X) / 100;
            dirZ = (target.Z - position.Z) / 100;


            if ((target.X > position.X + distance && target.Z > position.Z + distance)
                || (target.X < position.X - distance && target.Z > position.Z + distance)
                || (target.X > position.X + distance && target.Z < position.Z - distance)
                || (target.X < position.X - distance && target.Z < position.Z - distance)
                || (target.X > position.X + distance)
                || (target.X < position.X - distance)
                || (target.Z > position.Z + distance)
                || (target.Z < position.Z - distance))
                
            {
                translation *= Matrix.CreateTranslation(dirX, 0, dirZ); //gradually moves the tank towards target
            }

            tankBone.Transform = Matrix.CreateRotationY(-(float)Math.Atan2(difference.Z, difference.X) + MathHelper.PiOver2); //rotates the tank to face the target.

            if ((target.X > position.X + distant && target.Z > position.Z + distant) || (target.X < position.X - distant && target.Z > position.Z + distant)
                || (target.X > position.X + distant && target.Z < position.Z - distant) || (target.X < position.X - distant && target.Z < position.Z - distant))
            {
                rotateX(lFrontWBone, lFrontTransform, (MathHelper.PiOver2 / 50));
                rotateX(rFrontWBone, rFrontTransform, (MathHelper.PiOver2 / 50));
                rotateX(lBackWBone, lBackTransform, (MathHelper.PiOver2 / 50));
                rotateX(rBackWBone, rBackTransform, (MathHelper.PiOver2 / 50));
            }

            base.Update(gameTime);
        }

        public void rotateX(ModelBone bone, Matrix tr, float rotation) //X axis rotation (for wheels)
        {
            Vector3 oringalTrans = tr.Translation;
            bone.Transform *= Matrix.CreateRotationX(rotation);
            Vector3 newTrans = bone.Transform.Translation;
            bone.Transform *= Matrix.CreateTranslation(oringalTrans - newTrans);
        }
        public void rotateY(ModelBone bone, Matrix tr, float rotation) //Y axis rotation (for turret)
        {
            Vector3 oringalTrans = tr.Translation;
            bone.Transform *= Matrix.CreateRotationY(rotation);
            Vector3 newTrans = bone.Transform.Translation;
            bone.Transform *= Matrix.CreateTranslation(oringalTrans - newTrans);
        }
        public override void Draw(GraphicsDevice device, Camera camera)
        {
            base.Draw(device, camera);
        }
        protected override Matrix GetWorld()
        {
            return Matrix.CreateScale(0.3f) * translation;
        }
    }
}
