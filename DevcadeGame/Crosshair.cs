using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Devcade;

namespace DevcadeGame 
{
    public class Crosshair : GameObject
    {
        private Texture2D texture;
        private static int textureScale = 12;

        private static int speed = 125;
        private static Vector2 decelleration = new Vector2(0.97f, 0.97f);
        private static Rectangle bounds;

        private Vector2 pos;
        private Vector2 velocity;
        private Rectangle hitbox;

        // Simple rectangle texture used for debugging and viewing hitboxes
        private static Texture2D whiteRect;
        // Font used for showing debug information
        private static SpriteFont debugFont;

        public Crosshair(Texture2D texture, Vector2 startPos, Rectangle bounds, SpriteFont font, Texture2D rectangle) // font and texture are for debugging
        {
            Crosshair.debugFont = font;
            Crosshair.bounds = bounds;

            this.texture = texture;
            this.pos = startPos;
            this.velocity = new Vector2(0,0);

            Crosshair.whiteRect = rectangle;

            // The first point in the rect construction gives the top left of the hitbox, while pos will be the center of the crosshair
            this.hitbox = new Rectangle(new Point(0,0), new Point(texture.Width*textureScale/2));

            // We simply can't set the center of a rectangle for some reason :(, so we have to do ~math~ (:O
            // This point is the top left of the crosshair texture, So the center of the rectangles WILL match
            //      - each value is divided by two because the hitbox is half the size of the texture.
            // TODO: try to see if changing the crosshair texture origin will simplify this
            hitbox.Offset(startPos.X - texture.Width*textureScale/4, startPos.Y - texture.Height*textureScale/4);
        }

        public Rectangle getHitbox() { return this.hitbox; }

        public void move(Vector2 acceleration, GameTime gameTime)
        {
            // Flip Y because up on the stick should decrease yPos
            acceleration.Y *= -1;
            velocity += acceleration;

            // The texture will move differently from the hitbox rec 
            // This is because rectangle positions are saved as INTS while pos is FLOATS. So we cast int for all movement. I hope to find a better thing for this
            Vector2 distance = velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitbox.Offset(distance);
            pos.X += (int)distance.X; 
            pos.Y += (int)distance.Y;

            // Stop the crosshair from moving offscreen
            // This will check if the crosshair is offscreen, then move it back
            //      - alternate idea: check if the crosshair *will* be offscreen, then correct the distance vector (probably dumb idea)
            Vector2 correction = new Vector2(0,0);

            if (hitbox.Left < bounds.Left) {
                correction.X -= hitbox.Left;
                velocity.X = 0;
            
            } else if (hitbox.Right > bounds.Right) {
                correction.X = bounds.Right - hitbox.Right;
                velocity.X = 0;
            }

            if (hitbox.Top < bounds.Top) {
                correction.Y -= hitbox.Top;
                velocity.Y = 0;
            
            } else if (hitbox.Bottom > bounds.Bottom) {
                correction.Y = bounds.Bottom - hitbox.Bottom;
                velocity.Y = 0;
            }

            hitbox.Offset(correction);
            pos.X += (int)correction.X; 
            pos.Y += (int)correction.Y;

            // Once vel is less than 1, just set it to zero. Unless the stick is being moved, then move normally
            if (velocity.Length() > 1 || acceleration.Length() > 0f) {
                velocity *= decelleration;

            } else {
                velocity.X = 0;
                velocity.Y = 0;
            }
        }

        public void drawSelf(SpriteBatch spriteBatch)
        {
            /*
            // Draw a rectangle showing the hitbox of the Crosshair
            spriteBatch.Draw(
                whiteRect,
                hitbox,
                Color.Black
            ); */
            spriteBatch.Draw(
                texture,
                pos,
                null,
                Color.White,
                0f,
                new Vector2(texture.Width / 2, texture.Height / 2),
                textureScale,
                SpriteEffects.None,
                1f
            );
            
        }
    }
}