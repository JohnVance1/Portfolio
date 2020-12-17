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
    class MainMenu
    {
        private Rectangle start;
        private Texture2D startTexture;

        private Texture2D logoTexture;
        private Rectangle logoRect;

        private MouseState prevState;



        public MainMenu(Texture2D texture, int screenWidth, int screenHeight, Texture2D logo)
        {
            startTexture = texture;
            start = new Rectangle(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2, texture.Width, texture.Height);

            logoTexture = logo;
            logoRect = new Rectangle(screenWidth / 2 - logo.Width / 2, screenHeight / 4 - logo.Height / 2, logo.Width, logo.Height);
            prevState = new MouseState();
        }

        /// <summary>
        /// Checks for menu relevant input
        /// </summary>
        public void Update(ref GameState state)
        {
            MouseState ms = Mouse.GetState();
            if(ms.LeftButton == ButtonState.Pressed && prevState.LeftButton != ButtonState.Pressed)
            {
                if (ms.Position.X >= start.X && ms.Position.X <= start.X + start.Width
                    && ms.Position.Y >= start.Y && ms.Position.Y <= start.Y + start.Height)
                {
                    state = GameState.TransitionDown;
                }
            }
            prevState = ms;
        }

        /// <summary>
        /// Draws the main menu
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(startTexture, start, Color.White);
            spriteBatch.Draw(logoTexture, logoRect, Color.White);
        }
    }
}
