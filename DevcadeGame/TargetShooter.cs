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

        private int player1Score;
        private int player2Score;

        private Texture2D[] buttons;
        private List<Target> targets = new List<Target>();

        private Random RNG = new Random();

        public TargetShooter(SpriteBatch spriteBatch, int width, int height, GraphicsDevice device)
        {
            _spriteBatch = spriteBatch;
            sWidth = width;
            sHeight = height;

            player1Score = 0;
            player2Score = 0;

            Texture2D redButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0001.png"));
            Texture2D blueButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0002.png"));
            Texture2D yellowButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0003.png"));
            Texture2D greenButton = Texture2D.FromStream(device, File.OpenRead("Content/Sprites/tile_0000.png"));
            buttons = new Texture2D[] {blueButton, greenButton, redButton, yellowButton};

            targets.Add(createTarget());

        }

        public Target createTarget()
        {
            int colorInt = RNG.Next(4);
            Texture2D texture = buttons[colorInt];
            Input.ArcadeButtons button = Input.ArcadeButtons.A1;

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
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
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

        public void drawScores(SpriteFont font) 
        {
            string p2ScoreString = "Player 2: " + player2Score.ToString();
            Vector2 p2Size = font.MeasureString(p2ScoreString);

            _spriteBatch.DrawString(
                font, 
                "Player 1: " + player1Score.ToString(),
                new Vector2(0,0),
                Color.Black
            );
        
            _spriteBatch.DrawString(
                font, 
                p2ScoreString,
                new Vector2(sWidth - p2Size.X ,0),
                Color.Black
            );
        }
    }
}