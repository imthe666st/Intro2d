using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Fonts
{
	public class FontManager
	{
		private static FontManager Instance;

		private static ContentManager Content;

		private readonly Dictionary<string, CustomFont> Fonts;

		private FontManager()
		{
			Instance = this;

			Fonts = new Dictionary<string, CustomFont>();

			new ExampleFont();
		}

		public static void SetContentManager(ContentManager contentManager)
		{
			Content = contentManager;
		}

		public static FontManager GetInstance()
		{
			if (Content == null) return null;
			if (Instance == null) new FontManager();

			return Instance;
		}

		public void RegisterFont(string key, CustomFont font)
		{
			Fonts.Add(key, font);
		}

		public static Texture2D CreateFontString(string font, params string[] text)
		{
			if (font == null) throw new ArgumentNullException(nameof(font));
			if (text == null) throw new ArgumentNullException(nameof(text));

			if (!GetInstance().Fonts.ContainsKey(font)) return null;

			var cf = GetInstance().Fonts[font];
			return cf.CreateTexture(text);
		}

		public static void DrawString(SpriteBatch spriteBatch, string font, Vector2 position, params string[] text)
		{
			if (font == null) throw new ArgumentNullException(nameof(font));
			if (text == null) throw new ArgumentNullException(nameof(text));

			if (!GetInstance().Fonts.ContainsKey(font)) return;

			var cf = GetInstance().Fonts[font];
			cf.DrawString(spriteBatch, position, text);
		}
	}
}