﻿using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Orbs
{
	/// <summary>
	///     Orb that slowly accelerates
	/// </summary>
	public class LinearIncreasingOrb : AbstractOrb
	{
		private readonly float Speed2;

		// Don't use this with uncapped framerate yet. 
		public LinearIncreasingOrb(Vector2 position, Vector2 direction, float speed, float speed2) : base("orb3", position,
			direction)
		{
			Direction *= speed;

			Speed2 = speed2;
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			// Visualizing
			// DON'T ENABLE THIS ON WEAK HARDWARE

			//List<LinearIncreasingOrb> orbs = SceneManager.GetSprites<LinearIncreasingOrb>();
			//int i = -1;
			//foreach (var o in orbs)
			//{
			//	if ((o.Position - this.Position).Length() < 32) i++;
			//}

			//i %= 64;
			//int r = (i / 16) * 0x40;
			//i %= 16;
			//int g = (i / 4) * 0x40;
			//i %= 4;
			//int b = i * 0x40;

			//if (r >= 0x100) r = 0xff;
			//if (g >= 0x100) g = 0xff;
			//if (b >= 0x100) b = 0xff;

			//this.Hue = new Color(r ^ 0xff, g ^ 0xff, b ^ 0xff);


			Direction = Direction * (1.0f + Speed2 * (float) gameTime.ElapsedGameTime.TotalSeconds);
			return Direction;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var players = SceneManager.GetSprites<PlayerSprite>();
			foreach (var ps in players)
			{
				if (!ps.DoesCollide(this)) continue;

				ps.Damage((int) GameConstants.Difficulty);
				Delete();
			}
		}
	}
}