using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

public class Screen
{
	public Screen()
	{
	}

	public float[] GetLogoLocation(GameWindow window, Texture2D logo)
	{
		return new float[2] { (window.ClientBounds.Width-logo.Width)/2, 0 };
		//return new float[2] { window.ClientBounds.Width/2, 0 };
    }
}
