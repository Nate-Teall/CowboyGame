using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Devcade;

namespace DevcadeGame
{
    public class Target
    {
        private Texture2D targetTexture;

        private static int speed = 100;
        private static Vector2 gravity = new Vector2(0,2f);
        private Vector2 velocity;
        private Vector2 pos;

        public Target(Texture2D texture, Vector2 startPos, Vector2 startVel)
        {
            this.pos = startPos;
            this.targetTexture = texture;
            this.velocity = startVel;
        }

        public void move(GameTime gameTime)
        {
            pos += velocity * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity += gravity;
        }

        public void drawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
				targetTexture, // texture
				pos, // pos
				null, // nullable sourceRect
				Color.White, 
				0f, // rotation
				new Vector2(targetTexture.Width/2, targetTexture.Height/2), // rotation origin
				15,
				SpriteEffects.None,
				1
			);
        }

    }
}