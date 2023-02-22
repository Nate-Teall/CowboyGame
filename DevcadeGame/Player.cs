using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Devcade;

namespace DevcadeGame 
{
    public class Player
    {
        private int score;
        private int ammo;

        private Crosshair crosshair;

        public Player(Crosshair crosshair) 
        {
            this.score = 0;
            this.ammo = 6;

            this.crosshair = crosshair;
        }

        public Crosshair GetCrosshair() { return crosshair; }

        public int getScore() { return score; }

        public int getAmmo() { return ammo; }

        public void incrementScore() { score++; }

        public void moveCrosshair(Vector2 dir, GameTime gameTime) { crosshair.move(dir, gameTime); }

        public void drawCrosshair(SpriteBatch spriteBatch) { crosshair.drawSelf(spriteBatch); }

        public Boolean shoot()
        {
            if ( ammo > 0 ){
                ammo--;
                return true;

            } else {
                return false;
            }
        }
    }
}