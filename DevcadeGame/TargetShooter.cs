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
        private static Timer spawnTimer;
        private static GameTime gameTime;

        private Vector2 gravity = new Vector2(0,2f);

        private Player player1;
        private Player player2;

        private Texture2D targetTexture;
        private List<Target> targets = new List<Target>();

        private Random RNG = new Random();

        // Simple rectangle texture used for debugging and viewing hitboxes
        private static Texture2D whiteRect;

        // TODO:
        //      - Later, after there is a menu and whatnot, add gameTime as a static field, it's being used in a lot of places
        public TargetShooter(SpriteBatch spriteBatch, int width, int height, GraphicsDevice device, SpriteFont font, GameTime gameTime)
        {
            Color[] data = {Color.White};
            TargetShooter.whiteRect = new Texture2D(device, 1, 1);
            whiteRect.SetData(data);

            TargetShooter._spriteBatch = spriteBatch;
            TargetShooter.sWidth = width;
            TargetShooter.sHeight = height;
            TargetShooter.spawnTimer = new Timer(gameTime, 2f);
            TargetShooter.gameTime = gameTime;

            Texture2D crosshairTexture = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/Crosshair.png"));

            player1 = new Player(
                new Crosshair(
                    crosshairTexture,
                    new Vector2(sWidth/4, sHeight/2),
                    device.Viewport.Bounds,
                    font,
                    whiteRect
                ),
                gameTime
            );

            player2 = new Player(
                new Crosshair(
                    crosshairTexture,
                    new Vector2(3*sWidth/4, sHeight/2),
                    device.Viewport.Bounds,
                    font,
                    whiteRect
                ),
                gameTime
            );

            targetTexture = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0001.png"));
            createTarget();

        }

        public void moveCrosshair(Vector2 p1Dir, Vector2 p2Dir)
        {
            player1.moveCrosshair(p1Dir, gameTime);
            player2.moveCrosshair(p2Dir, gameTime);
        }

        public void drawCrosshairs() 
        {
            player1.drawCrosshair(_spriteBatch);
            player2.drawCrosshair(_spriteBatch);
        }

        public void shoot(int playerNum)
        {
            Player player; 

            if (playerNum == 1)
                player = player1;
            else
                player = player2;

            if ( player.shoot() )
            {
                foreach(Target target in targets)
                {
                    if ( player.GetCrosshair().getHitbox().Intersects(target.getHitbox()) ) // bruh
                    {
                        player.incrementScore();
                        targets.Remove(target);
                        break;
                    }
                }
            }

        }

        public void updateTimers() 
        {
            if (spawnTimer.update())
            {
                createTarget();
                spawnTimer.reset();
            }

            player1.updateReloadTimer();
            player2.updateReloadTimer();
        }

        public void reload(int playerNum)
        {
            Player player; 

            if (playerNum == 1)
                player = player1;
            else
                player = player2;

            player.startReload();
        }

        private void createTarget()
        {
            // TODO: Remove these targets when they get shot or fall of screen
            int xVel = RNG.Next(-7, 7);
            int yVel = RNG.Next(-80, -55);

            Target target = new Target(
                targetTexture, 
                new Vector2(sWidth/2, sHeight),
                new Vector2(xVel, yVel),
                whiteRect
            );
            targets.Add(target);
        }

        public void moveTargets()
        {
            foreach(Target target in targets)
            {
                target.move(gravity, gameTime);
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
            string p2ScoreString = "Player 2: " + player2.getScore().ToString();
            string p1ScoreString = "Player 1: " + player1.getScore().ToString();
            Vector2 p2ScoreSize = font.MeasureString(p2ScoreString);

            _spriteBatch.DrawString(
                font, 
                p1ScoreString,
                new Vector2(0,0),
                Color.Black
            );
        
            _spriteBatch.DrawString(
                font, 
                p2ScoreString,
                new Vector2(sWidth - p2ScoreSize.X ,0),
                Color.Black
            );

            string p1AmmoString = player1.getAmmo().ToString();
            string p2AmmoString = player2.getAmmo().ToString();
            Vector2 p2AmmoSize = font.MeasureString(player2.getAmmo().ToString());

            _spriteBatch.DrawString(
                font,
                p1AmmoString,
                new Vector2(0,3*sHeight/4),
                Color.Black
            );

            _spriteBatch.DrawString(
                font,
                p2AmmoString,
                new Vector2(sWidth - p2AmmoSize.X, 3*sHeight/4),
                Color.Black
            );
        }
    }
}