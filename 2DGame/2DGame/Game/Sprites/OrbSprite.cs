using System;
using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites
{
	public class OrbSprite : AbstractSprite
	{
		// I'll just use this sprite to test things for now.


		public OrbSprite(Vector2 position) : base("orb", position)
		{
			Random r = new Random();
			this.Hue = new Color(r.Next(0xFF), r.Next(0xFF), r.Next(0xFF));
		}

		private Vector2 LastMovement;
		public override void Update(GameTime gameTime)
		{
			Vector2 bufferedMovement = new Vector2();
			List<PlayerSprite> playerList = SceneManager.GetSprites<PlayerSprite>();
			foreach (PlayerSprite ps in playerList) {
				Vector2 dist = (ps.GetPosition() - this.Position);
                
				bufferedMovement += (dist / (float)Math.Pow(dist.Length(), 1.5));
			}

			bufferedMovement *= 0.9992f; // Basically drag
			LastMovement += bufferedMovement;
			this.Position += LastMovement;
			
			// Prevents player from leaving the screen
			if ((this.Position.X + this.Texture.Width / 2) > Game.GraphicsArea.X) this.Position.X = Game.GraphicsArea.X - this.Texture.Width / 2;
			if ((this.Position.Y + this.Texture.Height / 2) > Game.GraphicsArea.Y) this.Position.Y = Game.GraphicsArea.Y - this.Texture.Height / 2;
			if ((this.Position.X - this.Texture.Width / 2) < 0) this.Position.X = this.Texture.Width / 2;
			if ((this.Position.Y - this.Texture.Height / 2) < 0) this.Position.Y = this.Texture.Height / 2;

		}
	}
}
