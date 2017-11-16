﻿using System;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game
{
	/// <summary>
	///     This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
        /// <summary>
        /// Static instance of the game
        /// </summary>
		private static Game GameInstance;

        /// <summary>
        /// Arial Font
        /// </summary>
		public static SpriteFont FontArial;

        /// <summary>
        /// Our <see cref="GraphicsDeviceManager"/>
        /// </summary>
		public GraphicsDeviceManager Graphics;

        /// <summary>
        /// <see cref="SpriteBatch"/> used for drawing
        /// </summary>
		private SpriteBatch SpriteBatch;

		public Game(params String[] args)
		{
			GameInstance = this;

			Graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			// Changing the window size
			Graphics.PreferredBackBufferHeight = 600;
			Graphics.PreferredBackBufferWidth = 800;
			GraphicsArea = new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

			GraphicsAreaRectangle = new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

			IsMouseVisible = true;
		}

		public static Vector2 GraphicsArea { get; private set; }

		public static Rectangle GraphicsAreaRectangle { get; private set; }

		public static Game GetInstance()
		{
			return GameInstance;
		}

		public GraphicsDeviceManager GetGraphicsDeviceManager()
		{
			return Graphics;
		}

		public static void ExitGame()
		{
			GetInstance().Exit();
		}

		/// <summary>
		///     Allows the game to perform any initialization it needs to before starting to run.
		///     This is where it can query for any required services and load any non-graphic
		///     related content.  Calling base.Initialize will enumerate through any components
		///     and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			ImageManager.SetContentManager(Content);
			FontManager.SetContentManager(Content);

			FontManager.GetInstance();

			base.Initialize();
		}

		/// <summary>
		///     LoadContent will be called once per game and is the place to load
		///     all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			FontArial = Content.Load<SpriteFont>("Fonts/Arial");

			// Create a new SpriteBatch, which can be used to draw textures.
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			// Images are loaded when needed. Don't load them here! We use the ImageManager.
		}

		/// <summary>
		///     UnloadContent will be called once per game and is the place to unload
		///     game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		///     Allows the game to run logic such as updating the world,
		///     checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			KeyboardManager.Update();

			if (KeyboardManager.IsKeyDown(Keys.Escape))
				SceneManager.CloseScene();



            if (KeyboardManager.IsKeyDown(Keys.P)) SceneManager.AddScene("menu");
            if (SceneManager.GetCurrentScene() == null) Exit();

			// This updates the current scene.

			SceneManager.Update(gameTime);
			//SceneManager.GetCurrentScene().Update(gameTime);

			Window.Title = $"SceneKey: {SceneManager.GetCurrentScene()?.SceneKey ?? "None"}";

			base.Update(gameTime);
		}

		/// <summary>
		///     This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (SceneManager.GetCurrentScene() == null) return;

			GraphicsDevice.Clear(new Color(0, 128, 255));

			// TODO: Add your drawing code here
			SpriteBatch.Begin();

			// Drawing the current Scene.
			SceneManager.Draw(SpriteBatch);
			//SceneManager.GetCurrentScene().Draw(SpriteBatch);

			// Only add something here if it affects the game globally!

			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}