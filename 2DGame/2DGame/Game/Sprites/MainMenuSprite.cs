﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Scenes;

namespace Intro2DGame.Game.Sprites
{
    public class MainMenuSprite : AbstractSprite
    {
        // Dictionary used so we change the index only when the key is pressed down.
        // holding down the button shouldn't change the index.
        private readonly Dictionary<Keys, int> PressedKeys;
        // the current selected Index. 
        private int SelectedIndex;
        private int UpShowIndex;

        private readonly int TimeoutDelay = 60;

		private readonly List<Texture2D> MenuEntries;

	    private const int MAX_MENU_ENTRIES = 10;

	    public MainMenuSprite() : base()
        {
            this.PressedKeys = new Dictionary<Keys, int>();
            this.SelectedIndex = 0;
            this.UpShowIndex = 0;

			MenuEntries = new List<Texture2D>
			{
				FontManager.CreateFontString("example", "Introductions"),
				FontManager.CreateFontString("example", "Knockout Round"),
				FontManager.CreateFontString("example", "Round 1"),
				FontManager.CreateFontString("example", "Round 2"),
				FontManager.CreateFontString("example", "Finals"),
				FontManager.CreateFontString("example", "Go to Scene #6"),
				FontManager.CreateFontString("example", "Go to Example Scene!"),
				FontManager.CreateFontString("example", "Exit")
			};

		}

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Testing if the keys are in the dictionary to avoid any exceptions
            foreach (Keys k in ks.GetPressedKeys())
            {
                if (!PressedKeys.ContainsKey(k)) PressedKeys.Add(k, 0);
            }

			if (ks.IsKeyDown(Keys.Enter))
			{
				switch (SelectedIndex)
				{
					case 0: SceneManager.SetCurrentScene("mainmenu"); break;
					case 1: SceneManager.SetCurrentScene("mainmenu"); break;
					case 2: SceneManager.SetCurrentScene("mainmenu"); break;
					case 3: SceneManager.SetCurrentScene("mainmenu"); break;
					case 4: SceneManager.SetCurrentScene("mainmenu"); break;
					case 5: SceneManager.SetCurrentScene("mainmenu"); break;
					case 6: SceneManager.SetCurrentScene("example"); break;
					case 7: Game.ExitGame(); break;
				}
				
			}

            // Getting index down
            if (ks.IsKeyDown(Keys.W) && PressedKeys[Keys.W] == 0 || ks.IsKeyDown(Keys.Up) && PressedKeys[Keys.Up] == 0)
            {
                SelectedIndex--;
                if (UpShowIndex > 0) UpShowIndex--;
                if (SelectedIndex < 0)
                {
                    UpShowIndex = MenuEntries.Count - MAX_MENU_ENTRIES;
					if (UpShowIndex < 0) UpShowIndex = 0;
                    SelectedIndex += MenuEntries.Count;
                }
                PressedKeys[Keys.W] = TimeoutDelay;
                PressedKeys[Keys.Up] = TimeoutDelay;
            }
            else if (ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.Up))
            {
                PressedKeys[Keys.W] = 0;
                PressedKeys[Keys.Up] = 0;
            }

            // Getting the index up
            if (ks.IsKeyDown(Keys.S) && PressedKeys[Keys.S] == 0 || ks.IsKeyDown(Keys.Down) && PressedKeys[Keys.Down] == 0)
            {
                SelectedIndex++;
                if (SelectedIndex >= MenuEntries.Count)
                {
                    SelectedIndex = 0;
                    UpShowIndex = 0;
                }

                if (SelectedIndex >= UpShowIndex + MAX_MENU_ENTRIES)
					UpShowIndex++;
				PressedKeys[Keys.S] = TimeoutDelay;
                PressedKeys[Keys.Down] = TimeoutDelay;
            }
            else if (ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.Down))
            {
                PressedKeys[Keys.S] = 0;
                PressedKeys[Keys.Down] = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO:

            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"), new Vector2(50, 85 + (SelectedIndex - UpShowIndex) * 50), Color.White);

			int d = (this.MenuEntries.Count > MAX_MENU_ENTRIES) ? MAX_MENU_ENTRIES : this.MenuEntries.Count;

			int idx = 0;
            if(MenuEntries.Count <= MAX_MENU_ENTRIES)
            {
                foreach (Texture2D menuItem in MenuEntries)
                {
                    spriteBatch.Draw(menuItem, new Vector2(100, 85 + idx++ * 50), Color.White);
                }
            }
            else
            {
                for(int i=0;i<d;i++)
                    spriteBatch.Draw(MenuEntries[i + UpShowIndex], new Vector2(100, 85 + idx++ * 50), Color.White);
            }
            //spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
        }
    }
}
