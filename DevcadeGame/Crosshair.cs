using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Devcade;

namespace DevcadeGame 
{
    public class Crosshair
    {
        private static int speed = 125;
        private static Vector2 decelleration = new Vector2(0.97f, 0.97f);
        private Vector2 pos;
        private Vector2 velocity;

        private Texture2D texture;

        public Crosshair(Texture2D texture, Vector2 defaultPos) 
        {
            this.texture = texture;
            this.pos = defaultPos;
            this.velocity = new Vector2(0,0);
        }

        public void move(Vector2 acceleration, GameTime gameTime)
        {
            // Flip Y because up on the stick should decrease yPos
            acceleration.Y *= -1;
            velocity += acceleration;

            pos += velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Once vel is less than 1, just set it to zero. Unless the stick is being moved, then move normally
            if (velocity.Length() > 1 || acceleration.Length() > 0f)
            {
                velocity *= decelleration;
            }
            else
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
        }

        public void drawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                pos,
                null,
                Color.White,
                0f,
                new Vector2(texture.Width / 2, texture.Height / 2),
                12f,
                SpriteEffects.None,
                1f
            );
        }
    }
}