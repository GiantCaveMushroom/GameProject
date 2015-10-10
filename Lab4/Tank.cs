using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4
{
    class Tank : BasicModel
    {
        Matrix translation = Matrix.Identity;
        ModelBone turretBone, cannonBone, hatchBone, tankBone;
        ModelBone lFrontWBone, rFrontWBone, lSteer, rSteer, lBackWBone, rBackWBone;
        MouseState prevMouse;
        MousePick mousePick;
        Vector3 target = Vector3.Zero;
        public Vector3 position = Vector3.Zero;
        Vector3 difference = Vector3.Zero;
        float dirX, dirZ;
        float angle = 0;

        Matrix turretTransform, lFrontTransform, rFrontTransform, lBackTransform, rBackTransform;

        public Tank(Model model, GraphicsDevice device, Camera camera) : base(model)
        {
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
            mousePick = new MousePick(device, camera);

            if (turretBone != null) turretTransform = turretBone.Transform;
            if (lFrontWBone != null) lFrontTransform = lFrontWBone.Transform;
            if (rFrontWBone != null) rFrontTransform = rFrontWBone.Transform;
            if (lBackWBone != null) lBackTransform = lBackWBone.Transform;
            if (rBackWBone != null) rBackTransform = rBackWBone.Transform;
        }

        public override void Update(GameTime gameTime)
        {
            Vector3? collisionPosition = mousePick.GetPosition();
            if (Mouse.GetState().RightButton == ButtonState.Pressed && collisionPosition.HasValue == true)
            {
                target = collisionPosition.Value;
            }

            //The following is used to get values between position and distance
            position = translation.Translation;
            dirX = (target.X - position.X)/100;
            dirZ = (target.Z - position.Z)/100;
            if (dirX > 10) {dirX = 10;};
            if (dirZ > 10) {dirZ = 10;};
            if (dirX < -10) {dirX = -10;};
            if (dirZ < -10) {dirZ = -10;};

            //The following moves the tank
            difference = target - position; //For rotation
            //difference.Normalize(); //wtf is this?
            translation *= Matrix.CreateTranslation(dirX, 0, dirZ); //gradually moves the tank towards target
            tankBone.Transform = Matrix.CreateRotationY(-(float)Math.Atan2(difference.Z, difference.X) + MathHelper.PiOver2); //rotates the tank to face the target.

            //The following rotates the turret
            rotateY(turretBone, turretTransform, MathHelper.PiOver2 / 400);

            //The following rotates the wheels while moving
            if ((target.X > position.X + 20 && target.Z > position.Z + 20) || (target.X < position.X - 20 && target.Z > position.Z + 20)
                || (target.X > position.X + 20 && target.Z < position.Z - 20) || (target.X < position.X - 20 && target.Z < position.Z - 20))
            {
                rotateX(lFrontWBone, lFrontTransform, (MathHelper.PiOver2 / 50));
                rotateX(rFrontWBone, rFrontTransform, (MathHelper.PiOver2 / 50));
                rotateX(lBackWBone, lBackTransform, (MathHelper.PiOver2 / 50));
                rotateX(rBackWBone, rBackTransform, (MathHelper.PiOver2 / 50));
            }

            //I forgot what this is for
            prevMouse = Mouse.GetState();
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
            return Matrix.CreateScale(0.3f) * translation * Matrix.CreateTranslation(0, 0, 0);
        }
    }

}
