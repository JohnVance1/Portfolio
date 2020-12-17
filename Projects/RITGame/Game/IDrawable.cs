using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameName
{
    interface IDrawable
    {
        /// <summary>
        /// Gets a rectangle representing the drawable object's position in the world
        /// </summary>
        Rectangle GetWorldPos();
        /// <summary>
        /// Gets the texture associated with the drawable object
        /// </summary>
        Texture2D GetTexture();
    }
}
