using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Devcade;

namespace DevcadeGame 
{
    public interface GameObject
    {
        public Rectangle getHitbox();

        public void move(Vector2 acceleration, GameTime gameTime);

        public void drawSelf(SpriteBatch spriteBatch);
    }
}