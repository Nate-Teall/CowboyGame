using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevcadeGame
{
    public class Target
    {
        private Texture2D targetTexture;
        private static int textureScale = 15;
        private static Vector2 gravity = new Vector2(0,2f);

        private static int speed = 75;
        private int score;
        private Vector2 velocity;
        private Rectangle hitbox;
        private Rectangle bounds;

        private bool destroyed;

        // Simple rectangle texture used for debugging and viewing hitboxes
        //private static Texture2D whiteRect;

        public Target(Texture2D texture, Vector2 startPos, Vector2 startVel, Rectangle bounds, int score)
        {
            //Target.whiteRect = rectangle;
            this.targetTexture = texture;
            this.velocity = startVel;
            this.score = score;
            this.destroyed = false;
            this.bounds = bounds;

            this.hitbox = new Rectangle(new Point((int)startPos.X, (int)startPos.Y), new Point(texture.Width*textureScale));
        }

        public Rectangle getHitbox() { return hitbox; }

        public int getScore() { return score; }

        public bool isDestroyed() { return destroyed; }
        public virtual void getShot() { destroyed = true; }

        protected Vector2 getVel() { return velocity; }
        protected Vector2 getGravity() { return gravity; }
        protected int getSpeed() { return speed; }

        public virtual void move(GameTime gameTime)
        {
            velocity += gravity;
            hitbox.Offset(velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Destory any targets off screen
            if (outOfBounds())
                getShot();

        }

        protected void setVel(Vector2 newVel) 
        {
            velocity = newVel;
        }

        protected bool outOfBounds()
        {
            bool oob = false;

            if (hitbox.Right < bounds.Left || hitbox.Left > bounds.Right || hitbox.Top > bounds.Bottom) {
                oob = true;
            } 

            return oob;
        }

        protected bool touchingSide() 
        {
            bool ts = false;

            if (hitbox.Left < bounds.Left || hitbox.Right > bounds.Right)
                ts = true;

            return ts;
        }

        protected bool touchingBottom()
        {
            bool tb = false;

            if (hitbox.Bottom > bounds.Bottom)
                tb = true;

            return tb;
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