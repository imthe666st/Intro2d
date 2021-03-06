﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Pattern
{
	public class LambdaPattern : IPattern
	{
		private readonly Action<AbstractSprite, GameTime> LambdaAction;

		public LambdaPattern(Action<AbstractSprite, GameTime> lambdaAction)
		{
			this.LambdaAction = lambdaAction;
		}

		public bool Execute(AbstractSprite host, GameTime gameTime)
		{
			LambdaAction(host, gameTime);

			return true;
		}
	}
}
