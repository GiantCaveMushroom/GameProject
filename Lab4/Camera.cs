using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }

        // Camera vectors
        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;

        float speed = 20;
        float totalPitch = MathHelper.PiOver4;
        float currentPitch = 0;

        MouseState prevMouse;

        public Camera(Game1 game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            // Build camera view matrix
            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();
            projection = Matrix.CreatePerspectiveFieldOfView
                (MathHelper.PiOver4, (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1, 10000);
        }
        public override void Initialize()
        {
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            prevMouse = Mouse.GetState();
            base.Initialize();
        }

        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }

        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            int mouseTempX = Mouse.GetState().X;
            int mouseTempY = Mouse.GetState().Y;
            KeyboardState state = Keyboard.GetState();

            //CAMERA ROTATIONS-----------------------------------
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                // Yaw rotation
                cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * (mouseTempX - prevMouse.X)));

                // Pitch rotation
                float pitchAngle = (MathHelper.PiOver4 / 150) * (mouseTempY + (prevMouse.Y * -1));

                if (Math.Abs(currentPitch + pitchAngle) < totalPitch)
                {
                    cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), pitchAngle));
                    currentPitch += pitchAngle;
                }
            }

            prevMouse = Mouse.GetState();
            //--------------------------------------------------

            // WALKING-------------------------------------------
            // Move Forward and Backward
            if (state.IsKeyDown(Keys.W))
            {
                cameraPosition += cameraDirection * speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                cameraPosition -= cameraDirection * speed;
            }
            // Move side to side
            if (state.IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection * speed);
            }

            //----------------------------------------------------


            CreateLookAt();
            base.Update(gameTime);
        }

    }
}
