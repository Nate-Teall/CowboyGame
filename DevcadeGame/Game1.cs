using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;

namespace DevcadeGame
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private TargetShooter targetShooter;

		private SpriteFont font;

		/// <summary>
		/// Game constructor
		/// </summary>
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;
		}

		/// <summary>
		/// Does any setup prior to the first frame that doesn't need loaded content.
		/// </summary>
		protected override void Initialize()
		{
			Input.Initialize(); // Sets up the input library

			// Set window size if running debug (in release it will be fullscreen)
			#region
#if DEBUG
			_graphics.PreferredBackBufferWidth = 1080;
			_graphics.PreferredBackBufferHeight = 2000; //2520
			_graphics.ApplyChanges();
#else
			_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();
#endif
			#endregion

			base.Initialize();
		}

		/// <summary>
		/// Does any setup prior to the first frame that needs loaded content.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("Fonts/font");

		}

		/// <summary>
		/// Your main update loop. This runs once every frame, over and over.
		/// </summary>
		/// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
		protected override void Update(GameTime gameTime)
		{
			Input.Update(); // Updates the state of the input library

			// Exit when both menu buttons are pressed (or escape for keyboard debuging)
			// You can change this but it is suggested to keep the keybind of both menu
			// buttons at once for gracefull exit.
			if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
				(Input.GetButton(1, Input.ArcadeButtons.Menu) &&
				Input.GetButton(2, Input.ArcadeButtons.Menu)))
			{
				Exit();
			}

			if (targetShooter == null)
			{
				if (Input.GetButtonDown(1, Input.ArcadeButtons.Menu))
				{
					targetShooter = new TargetShooter(_spriteBatch, 
						_graphics.PreferredBackBufferWidth, 
						_graphics.PreferredBackBufferHeight,
						GraphicsDevice,
						font,
						gameTime
					);
				}
			}
			else
			{
				// Dpad input for testing on laptop
				#region
#if DEBUG
				Vector2 dPad = new Vector2(0,0);

				if (GamePad.GetState(0).IsButtonDown(Buttons.DPadUp))
				{
					dPad.Y = 1;
				}

				if (GamePad.GetState(0).IsButtonDown(Buttons.DPadDown))
				{
					dPad.Y = -1;
				}

				if (GamePad.GetState(0).IsButtonDown(Buttons.DPadRight))
				{
					dPad.X = 1;
				}

				if (GamePad.GetState(0).IsButtonDown(Buttons.DPadLeft))
				{
					dPad.X = -1;
				}

				targetShooter.moveCrosshair(dPad, Input.GetStick(2));
#else
				// Devcade stick input
				targetShooter.moveCrosshair(Input.GetStick(1), Input.GetStick(2), gameTime);
#endif
				#endregion

				if (Input.GetButtonDown(1, Input.ArcadeButtons.A1))
					targetShooter.shoot(1);

				if (Input.GetButtonDown(2, Input.ArcadeButtons.A1))
					targetShooter.shoot(2);

				if (Input.GetButtonDown(1, Input.ArcadeButtons.A2))
					targetShooter.reload(1);

				if (Input.GetButtonDown(2, Input.ArcadeButtons.A2))
					targetShooter.reload(2);

				targetShooter.updateTimers();
				targetShooter.moveTargets();
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// Your main draw loop. This runs once every frame, over and over.
		/// </summary>
		/// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// Using SamplerState.PointClamp will fix scaled pixel art being blurry
			_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
			
			if (targetShooter != null) 
			{
				targetShooter.drawTargets();
				targetShooter.drawCrosshairs();
				targetShooter.drawHUD(font);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}