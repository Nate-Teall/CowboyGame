using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using Devcade;

namespace DevcadeGame
{
    public class TargetShooter 
    {
        private static SpriteBatch _spriteBatch;
        private static int sWidth;
        private static int sHeight;

        private int p1Score;
        private int p2Score;
        private int p1Ammo;
        private int p2Ammo;

        private Crosshair p1Crosshair;
        private Crosshair p2Crosshair;

        private Texture2D targetTexture;
        private List<Target> targets = new List<Target>();

        private Random RNG = new Random();

        public TargetShooter(SpriteBatch spriteBatch, int width, int height, GraphicsDevice device)
        {
            _spriteBatch = spriteBatch;
            sWidth = width;
            sHeight = height;

            p1Score = 0;
            p2Score = 0;
            p1Ammo = 6;
            p2Ammo = 6;

            Texture2D crosshairTexture = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/Crosshair.png"));

            p1Crosshair = new Crosshair(
                crosshairTexture,
                new Vector2(sWidth/4, sHeight/2)
            );

            p2Crosshair = new Crosshair(
                crosshairTexture,
                new Vector2(3*sWidth/4, sHeight/2)
            );

            targetTexture = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0001.png"));

            createTarget();

        }

        public void moveCrosshair(Vector2 p1Dir, Vector2 p2Dir, GameTime gameTime)
        {
            p1Crosshair.move(p1Dir, gameTime);
            p2Crosshair.move(p2Dir, gameTime);
        }

        public void drawCrosshairs() 
        {
            p1Crosshair.drawSelf(_spriteBatch);
            p2Crosshair.drawSelf(_spriteBatch);
        }

        public void p1Shoot()
        {
            p1Ammo = p1Ammo > 0 ? p1Ammo-1 : 0;
        }

        public void p2Shoot()
        {
            p2Ammo = p2Ammo > 0 ? p2Ammo-- : 0;
        }

        public void createTarget()
        {
            // TODO: Remove these targets when they get shot or fall of screen
            int xVel = RNG.Next(-10, 10);
            int yVel = RNG.Next(-80, -55);

            Target target = new Target(
                targetTexture, 
                new Vector2(sWidth/2, sHeight),
                new Vector2(xVel, yVel)
            );
            targets.Add(target);
        }

        public void moveTargets(GameTime gameTime)
        {
            foreach(Target target in targets)
            {
                target.move(gameTime);
            }
        }
        public void drawTargets()
        {
            foreach(Target target in targets) 
            {
                target.drawSelf(_spriteBatch);
            }
        }

        public void drawHUD(SpriteFont font) 
        {
            string p2ScoreString = "Player 2: " + p2Score.ToString();
            Vector2 p2Size = font.MeasureString(p2ScoreString);

            _spriteBatch.DrawString(
                font, 
                "Player 1: " + p1Score.ToString(),
                new Vector2(0,0),
                Color.Black
            );
        
            _spriteBatch.DrawString(
                font, 
                p2ScoreString,
                new Vector2(sWidth - p2Size.X ,0),
                Color.Black
            );

            Vector2 p2AmmoSize = font.MeasureString(p2Ammo.ToString());
            Vector2 p1AmmoSize = font.MeasureString(p2Ammo.ToString());

            _spriteBatch.DrawString(
                font,
                p1Ammo.ToString(),
                new Vector2(0,3*sHeight/4 - p1AmmoSize.Y),
                Color.Black
            );

            _spriteBatch.DrawString(
                font,
                p2Ammo.ToString(),
                new Vector2(sWidth - p2AmmoSize.X, 3*sHeight/4 - p2AmmoSize.Y),
                Color.Black
            );
        }
    }
}