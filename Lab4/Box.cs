using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4
{
    public class Box
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D crate;
        public Vector3 boxPosition = Vector3.Zero;
        Matrix translation = Matrix.Identity;
        Matrix rotation = Matrix.Identity;
        Matrix scaling = Matrix.Identity;
        VertexPositionNormalTexture[] texVerts1;
        VertexBuffer vertBuffer;
        IndexBuffer indexBuffer;
        short[] indices;
        BasicEffect effect;

        Vector3 cratePos;

        public Box(Vector3 position)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            cratePos = pos;
        }

        protected override void LoadContent()
        {
            crate = Content.Load<Texture2D>(@"Textures/crates");
        }

        void drawBox(Vector3 pos)
        {
            float BoxSize = 50;
            pos.Y = pos.Y + BoxSize;

            //Vertex of a box
            Vector3 FrontTopLeftVertex = new Vector3(pos.X - BoxSize, pos.Y + BoxSize, pos.Z + BoxSize);
            Vector3 FrontTopRightVertex = new Vector3(pos.X + BoxSize, pos.Y + BoxSize, pos.Z + BoxSize);
            Vector3 FrontDownRightVertex = new Vector3(pos.X + BoxSize, pos.Y - BoxSize, pos.Z + BoxSize);
            Vector3 FrontDownLeftVertex = new Vector3(pos.X - BoxSize, pos.Y - BoxSize, pos.Z + BoxSize);

            Vector3 RearTopLeftVertex = new Vector3(pos.X - BoxSize, pos.Y + BoxSize, pos.Z - BoxSize);
            Vector3 RearTopRightVertex = new Vector3(pos.X + BoxSize, pos.Y + BoxSize, pos.Z - BoxSize);
            Vector3 RearDownRightVertex = new Vector3(pos.X + BoxSize, pos.Y - BoxSize, pos.Z - BoxSize);
            Vector3 RearDownLeftVertex = new Vector3(pos.X - BoxSize, pos.Y - BoxSize, pos.Z - BoxSize);

            //top left surface
            Vector2 Front_UpLeft = new Vector2(0, 0);
            Vector2 Front_UpRight = new Vector2(0.5f, 0);
            Vector2 Front_DownRight = new Vector2(0.5f, 0.5f);
            Vector2 Front_DownLeft = new Vector2(0, 0.5f);

            //top right surface
            Vector2 Rear_UpLeft = new Vector2(0.5f, 0);
            Vector2 Rear_UpRight = new Vector2(1f, 0);
            Vector2 Rear_DownRight = new Vector2(1f, 0.5f);
            Vector2 Rear_DownLeft = new Vector2(0.5f, 0.5f);

            //bottom left surface
            Vector2 Side1_UpLeft = new Vector2(0, 0.5f);
            Vector2 Side1_UpRight = new Vector2(0.5f, 0.5f);
            Vector2 Side1_DownRight = new Vector2(0.5f, 1f);
            Vector2 Side1_DownLeft = new Vector2(0, 1f);

            Vector3 topLife = new Vector3(0, 1, 0);

            texVerts1 = new VertexPositionNormalTexture[24]
            {
                //FRONT
                new VertexPositionNormalTexture (FrontTopLeftVertex, topLife, Side1_UpLeft),
                new VertexPositionNormalTexture (FrontTopRightVertex, topLife, Side1_UpRight),
                new VertexPositionNormalTexture (FrontDownRightVertex, topLife, Side1_DownRight),
                new VertexPositionNormalTexture (FrontDownLeftVertex, topLife, Side1_DownLeft),
                
                //LEFT
                new VertexPositionNormalTexture (RearTopLeftVertex, topLife, Side1_UpLeft),
                new VertexPositionNormalTexture (FrontTopLeftVertex, topLife, Side1_UpRight),
                new VertexPositionNormalTexture (FrontDownLeftVertex, topLife, Side1_DownRight),
                new VertexPositionNormalTexture (RearDownLeftVertex, topLife, Side1_DownLeft),

               //REAR
                new VertexPositionNormalTexture (RearTopRightVertex, topLife, Front_UpLeft),
                new VertexPositionNormalTexture (RearTopLeftVertex, topLife, Front_UpRight),
                new VertexPositionNormalTexture (RearDownLeftVertex, topLife, Front_DownRight),
                new VertexPositionNormalTexture (RearDownRightVertex, topLife, Front_DownLeft),

                //RIGHT
                new VertexPositionNormalTexture (FrontTopRightVertex, topLife, Side1_UpLeft),
                new VertexPositionNormalTexture (RearTopRightVertex, topLife, Side1_UpRight),
                new VertexPositionNormalTexture (RearDownRightVertex, topLife, Side1_DownRight),
                new VertexPositionNormalTexture (FrontDownRightVertex, topLife, Side1_DownLeft),

                //TOP
                new VertexPositionNormalTexture (RearTopLeftVertex, topLife, Rear_UpLeft),
                new VertexPositionNormalTexture (RearTopRightVertex, topLife, Rear_UpRight),
                new VertexPositionNormalTexture (FrontTopRightVertex, topLife, Rear_DownRight),
                new VertexPositionNormalTexture (FrontTopLeftVertex, topLife, Rear_DownLeft),

                //BOTTOM
                new VertexPositionNormalTexture (FrontDownLeftVertex, topLife, Side1_UpLeft),
                new VertexPositionNormalTexture (FrontDownRightVertex, topLife, Side1_UpRight),
                new VertexPositionNormalTexture (RearDownRightVertex, topLife, Side1_DownRight),
                new VertexPositionNormalTexture (RearDownLeftVertex, topLife, Side1_DownLeft),
            };

            vertBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), texVerts1.Length, BufferUsage.None);
            vertBuffer.SetData(texVerts1);

            indices = new short[36]
            {
                0,1,2,
                0,2,3,
                4,5,6,
                4,6,7,
                8,9,10,
                8,10,11,
                12,13,14,
                12,14,15,
                16,17,18,
                16,18,19,
                20,21,22,
                20,22,23
            };

            indexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, sizeof(short) * indices.Length, BufferUsage.None);
            indexBuffer.SetData(indices);
            GraphicsDevice.Indices = indexBuffer;
            effect = new BasicEffect(GraphicsDevice);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetVertexBuffer(vertBuffer);
            effect.World = scaling * rotation * translation;
            effect.View = camera.view;
            effect.Projection = camera.projection;

            effect.VertexColorEnabled = false;

            effect.Texture = crate;
            effect.TextureEnabled = true;

            effect.EnableDefaultLighting();
            effect.LightingEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 24);
            }

            base.Draw(gameTime);
        }
    }
}
