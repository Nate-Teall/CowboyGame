using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevcadeGame
{
    public class BonusTarget : Target
    {
        private int hp;
        private Random RNG;

        public BonusTarget(Texture2D texture, Vector2 startPos, Vector2 startVel, Random RNG, Rectangle bounds) : base(texture, startPos, startVel, bounds, 20)
        {
            this.hp = 2;
            this.RNG = RNG;
        }

        public override void getShot()
        {
            if (hp > 0) {
                bounce();
                hp--;
            } else {
                base.getShot(); // Maybe this will just make it destroyed?
            }
        }

        private void bounce()
        {
            int xVel = RNG.Next(-25, 25);
            int yVel = RNG.Next(-70, -50);
            base.setVel(new Vector2(xVel, yVel));
        }

        public override void move(GameTime gameTime)
        {
            Rectangle hitbox = base.getHitbox();

            // Bounce off bottom, left, and right edge
            // Sometimes goes through the bottom! lol!
            if (base.touchingSide()) {
                base.setVel( new Vector2(
                    base.getVel().X * -1, 
                    base.getVel().Y
                ));

            } else if (base.touchingBottom()) {
                bounce();
            }

            base.move(gameTime);
        }
    }
}