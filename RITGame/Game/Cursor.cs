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
    class Cursor
    {
        private Texture2D cursorTexture;
        private Rectangle cursorRect;
        private int mouseX;
        private int mouseY;

        public Texture2D CursorTexture
        {
            get { return cursorTexture; }

        }

        public Rectangle GetScreenPos()
        {
            return cursorRect;

        }

        public Texture2D GetTexture()
        {
            return cursorTexture;

        }

        public Cursor(Texture2D texture)
        {
            cursorTexture = texture;
            cursorRect = new Rectangle(mouseX, mouseY, 15, 15);
            
        }

        public void UpdateMouse()
        {
            MouseState mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;
            cursorRect.X = mouseState.X;
            cursorRect.Y = mouseState.Y;

        }



    }
}
