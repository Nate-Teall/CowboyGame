using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Devcade;

namespace DevcadeGame
{
    public class Target : GameObject
    {
        private Texture2D targetTexture;
        private static int textureScale = 15;

        private static int speed = 100;
        private Vector2 velocity;
        private Rectangle hitbox;

        // Simple rectangle texture used for debugging and viewing hitboxes
        private static Texture2D whiteRect;

        public Target(Texture2D texture, Vector2 startPos, Vector2 startVel, Texture2D rectangle)
        {
            Target.whiteRect = rectangle;
            this.targetTexture = texture;
            this.velocity = startVel;

            this.hitbox = new Rectangle(new Point((int)startPos.X, (int)startPos.Y), new Point(texture.Width*textureScale));
        }

        public Rectangle getHitbox() { return hitbox; }

        public void move(Vector2 acceleration, GameTime gameTime)
        {
            velocity += acceleration;
            hitbox.Offset(velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void drawSelf(SpriteBatch spriteBatch)
        {
            /*
            // Draw a rectangle showing the hitbox
            spriteBatch.Draw(
                whiteRect,
                hitbox,
                Color.Black
            ); */

            // Uses the hitbox to determine the target's position and scale
            // Instead of a pos vector, we use the rectangle because it will also help track collision
            spriteBatch.Draw(
                targetTexture, // texture
                hitbox, // pos and scale
                null, // nullable sourceRect
                Color.White, 
                0f, // rotation
                new Vector2(0,0), // rotation origin
                SpriteEffects.None,
                1
            );

        }

    }
}