using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Orbs
{
	/// <summary>
	///     A normal orb that does nothing special.
	/// </summary>
	public class LinearOrb : AbstractOrb
	{
		protected float Speed;

		public LinearOrb(Vector2 position, Vector2 direction, float speed) : this("orb3", position, direction, speed)
		{
		}

		public LinearOrb(string textureKey, Vector2 position, Vector2 direction, float speed) : base(textureKey, position,
			direction)
		{
			Speed = speed;
		}

		protected override Vector2 UpdatePosition(GameTime gameTime)
		{
			return Direction * Speed;
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