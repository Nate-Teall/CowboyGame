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
        private Timer reloadTimer;

        private bool reloading;

        private Crosshair crosshair;

        public Player(Crosshair crosshair, GameTime gameTime) 
        {
            this.score = 0;
            this.ammo = 6;
            this.reloadTimer = new Timer(gameTime, 0.35f);

            this.crosshair = crosshair;
        }

        public Crosshair GetCrosshair() { return crosshair; }

        public int getScore() { return score; }

        public int getAmmo() { return ammo; }

        public void incrementScore() { score++; }

        public void moveCrosshair(Vector2 dir, GameTime gameTime) { crosshair.move(dir, gameTime); }

        public void drawCrosshair(SpriteBatch spriteBatch) { crosshair.drawSelf(spriteBatch); }

        public bool shoot()
        {
            reloading = false;

            if ( ammo > 0 ){
                ammo--;
                return true;

            } else {
                return false;
            }
        }
    
        public void startReload() { 
            if (ammo < 6)
                reloading = true; 
        }

        public void updateReloadTimer()
        {
            if (reloading)
            {
                if (reloadTimer.update())
                {
                    ammo++;
                    if (ammo == 6)
                        reloading = false;
                    else
                        reloadTimer.reset();
                }
            }
        }

    }
}