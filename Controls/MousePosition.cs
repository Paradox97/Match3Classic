using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Match3Classic.Controls
{
    class MousePosition
    {
        private static MousePosition input;

        MouseState mouseInput;
        public MousePosition()
        {
           this.mouseInput = Mouse.GetState();
        }

        public static MousePosition getMouseInput()
        {
            if (input == null)
                input = new MousePosition();
            return input;
        }

    }
}
