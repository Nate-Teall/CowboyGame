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

        private Texture2D[] buttons;
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

            Texture2D redButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0001.png"));
            Texture2D blueButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0002.png"));
            Texture2D yellowButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0003.png"));
            Texture2D greenButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0000.png"));
            buttons = new Texture2D[] {blueButton, greenButton, redButton, yellowButton};

            targets.Add(createTarget());

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
            p1Ammo--;
        }

        public void p2Shoot()
        {
            p2Ammo--;
        }

        public Target createTarget()
        {
            int colorInt = RNG.Next(4);
            Texture2D texture = buttons[colorInt];
            Input.ArcadeButtons button = Input.ArcadeButtons.A2;

            Target target = new Target(texture, button);

            return target;
        }

        public void destroyTarget(Input.ArcadeButtons button, int player)
        {
            foreach(Target target in targets)
            {
                if (target.getButton() == button)
                {
                    targets.Remove(target);

                    if (player == 1)
                    {
                        p1Score++;
                    }
                    else
                    {
                        p2Score++;
                    }

                    break;
                }
            }
        }

        public void drawTargets()
        {
            foreach(Target target in targets) 
            {
                target.drawSelf(_spriteBatch, new Vector2(sWidth/2, sHeight/2));
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