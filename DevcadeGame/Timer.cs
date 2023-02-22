using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DevcadeGame
{
    public class Timer
    {
        private static GameTime gameTime;

        private float endTime;
        private float currentTime;

        public Timer(GameTime gameTime, float endTime)
        {
            Timer.gameTime = gameTime;
            this.endTime = endTime;
            this.currentTime = 0;
        }

        public bool update()
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime > endTime)
                return true;
            else 
                return false;
        }

        public void reset()
        {
            this.currentTime = 0;
        }
    }
}