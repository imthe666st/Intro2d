using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game
{
	public static class KeyboardManager
	{
		private static Keys[] CurrentPressedKeys;
		private static Keys[] LastPressedKeys;

		public static void Update()
		{
			KeyboardState ks = Keyboard.GetState();

			// Add all keys to the dictionary
			LastPressedKeys = CurrentPressedKeys;
			CurrentPressedKeys = ks.GetPressedKeys();
		}

		public static bool IsKeyDown(Keys key)
		{
			return CurrentPressedKeys.Contains(key) && !LastPressedKeys.Contains(key);
		}

		public static bool IsKeyPressed(Keys key)
		{
			return CurrentPressedKeys.Contains(key);
		}
	}
}
