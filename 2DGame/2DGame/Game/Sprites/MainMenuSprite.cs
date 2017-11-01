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
        private Dictionary<Keys, int> pressedKeys;
        // the current selected Index. 
        private int selectedIndex;
        private int UpShowIndex;

        private readonly int timeoutDelay = 60;

		private List<Texture2D> menuEntries;

        public MainMenuSprite() : base()
        {
            this.pressedKeys = new Dictionary<Keys, int>();
            this.selectedIndex = 0;
            this.UpShowIndex = 0;

			menuEntries = new List<Texture2D>();
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #1"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #2"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #3"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #4"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #5"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #6"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #7"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #8"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Scene #9"));
			menuEntries.Add(FontManager.CreateFontString("example", "Go to Example Scene!"));
            menuEntries.Add(FontManager.CreateFontString("example", "Exit"));

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Testing if the keys are in the dictionary to avoid any exceptions
            foreach (Keys k in ks.GetPressedKeys())
            {
                if (!pressedKeys.ContainsKey(k)) pressedKeys.Add(k, 0);
            }

			if (ks.IsKeyDown(Keys.Enter))
			{
				switch (selectedIndex)
				{
					case 0: SceneManager.SetCurrentScene("mainmenu"); break;
					case 1: SceneManager.SetCurrentScene("mainmenu"); break;
					case 2: SceneManager.SetCurrentScene("mainmenu"); break;
					case 3: SceneManager.SetCurrentScene("mainmenu"); break;
					case 4: SceneManager.SetCurrentScene("mainmenu"); break;
					case 5: SceneManager.SetCurrentScene("mainmenu"); break;
					case 6: SceneManager.SetCurrentScene("mainmenu"); break;
					case 7: SceneManager.SetCurrentScene("mainmenu"); break;
					case 8: SceneManager.SetCurrentScene("mainmenu"); break;
					case 9: SceneManager.SetCurrentScene("example"); break;
				}
				
			}

            // Getting index down
            if (ks.IsKeyDown(Keys.W) && pressedKeys[Keys.W] == 0 || ks.IsKeyDown(Keys.Up) && pressedKeys[Keys.Up] == 0)
            {
                selectedIndex--;
                if (UpShowIndex > 0) UpShowIndex--;
                if (selectedIndex < 0)
                {
                    UpShowIndex = menuEntries.Count - 10;
                    selectedIndex += menuEntries.Count;
                }
                pressedKeys[Keys.W] = timeoutDelay;
                pressedKeys[Keys.Up] = timeoutDelay;
            }
            else if (ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.Up))
            {
                pressedKeys[Keys.W] = 0;
                pressedKeys[Keys.Up] = 0;
            }

            // Getting the index up
            if (ks.IsKeyDown(Keys.S) && pressedKeys[Keys.S] == 0 || ks.IsKeyDown(Keys.Down) && pressedKeys[Keys.Down] == 0)
            {
                selectedIndex++;
                if (selectedIndex >= menuEntries.Count)
                {
                    selectedIndex = 0;
                    UpShowIndex = 0;
                }
                if ((selectedIndex - 10 - UpShowIndex) > 0)
                    UpShowIndex++;
                pressedKeys[Keys.S] = timeoutDelay;
                pressedKeys[Keys.Down] = timeoutDelay;
            }
            else if (ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.Down))
            {
                pressedKeys[Keys.S] = 0;
                pressedKeys[Keys.Down] = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO:

            spriteBatch.Draw(ImageManager.GetTexture2D("MenuItem/arrow"), new Vector2(50, 85 + selectedIndex * 50), Color.White);

			int idx = 0;
            if(menuEntries.Count<=10)
            {
                foreach (Texture2D menuItem in menuEntries)
                {
                    spriteBatch.Draw(menuItem, new Vector2(100, 85 + idx++ * 50), Color.White);
                }
            }
            else
            {
                for(int i=0;i<10;i++)
                    spriteBatch.Draw(menuEntries[i+ UpShowIndex], new Vector2(100, 85 + idx++ * 50), Color.White);
            }
            //spriteBatch.DrawString(Game.FontArial, "Something! " + selectedIndex, new Vector2(100, 80), Color.Black);
        }
    }
}
