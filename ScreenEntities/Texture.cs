using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Match3Classic.ScreenEntities
{
    class Texture
    {
        Texture2D[] textureset;
        Texture2D currentTexture;
        int state, maxstates,
            stateByTime = 15;
        const int SPECIALSTATES = 2;

        public Texture(Texture2D[] _textureset)
        {
            this.textureset = _textureset;
            this.maxstates = _textureset.Length - SPECIALSTATES;
            this.state = 0;
            this.currentTexture = this.textureset[state];
        }

        public void ChangeState()
        {
            this.currentTexture = textureset[(state / stateByTime) % maxstates];
        }

        public Texture2D GetTexture()
        {
            return this.currentTexture;
        }
    }
}
