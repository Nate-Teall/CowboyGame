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
        private Input.ArcadeButtons button;

        public Target(Texture2D texture, Input.ArcadeButtons button)
        {
            this.targetTexture = texture;
            this.button = button;
        }

        public Input.ArcadeButtons getButton() { return this.button; }

        public void drawSelf(SpriteBatch spriteBatch, Vector2 pos)
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