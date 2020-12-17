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
    class DeathMenu
    {
        private MouseState prevState;

        private Rectangle playAgain;
        private Texture2D playAgainTexture;

        private Texture2D exitTexture;
        private Rectangle exit;

        private Texture2D backgroundTexture;
        private Rectangle background;


        public DeathMenu(Texture2D bgi, Texture2D playAgainTexture, Texture2D exitTexture, int screenWidth, int screenHeight)
        {
            this.exitTexture = exitTexture;
            this.playAgainTexture = playAgainTexture;
            backgroundTexture = bgi;

            background = new Rectangle(0, 0, screenWidth, screenHeight);


            ///repositioning of buttons needs to be checked on a desktop
            exit = new Rectangle(screenWidth / 3 + exitTexture.Width / 2 + 200, (screenHeight / 3) * 2 - exitTexture.Height / 2, exitTexture.Width, exitTexture.Height);
            playAgain = new Rectangle(screenWidth / 3 - playAgainTexture.Width / 2 - 200, (screenHeight / 3) * 2 - playAgainTexture.Height / 2, playAgainTexture.Width, playAgainTexture.Height);


            prevState = new MouseState();
        }

        /// <summary>
        /// Checks for pause menu relevant input
        /// </summary>
        public void Update(ref GameState state)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && prevState.LeftButton != ButtonState.Pressed)
            {
                if (ms.Position.X >= playAgain.X && ms.Position.X <= playAgain.X + playAgain.Width
                    && ms.Position.Y >= playAgain.Y && ms.Position.Y <= playAgain.Y + playAgain.Height)
                {
                    state = GameState.TransitionDown;
                }
                else if (ms.Position.X >= exit.X && ms.Position.X <= exit.X + exit.Width
                   && ms.Position.Y >= exit.Y && ms.Position.Y <= exit.Y + exit.Height)
                {
                    state = GameState.MainMenu;
                }
            }
            prevState = ms;
        }

        /// <summary>
        /// Sets the previous keyboard state to ensure that it is not blank when Update() is called
        /// </summary>
        /// <param name="kb">The current state of the keyboard</param>
        public void SetPrevState(MouseState ms)
        {
            prevState = ms;
        }

        /// <summary>
        /// Draws the pause menu
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
            spriteBatch.Draw(playAgainTexture, playAgain, Color.White);
            spriteBatch.Draw(exitTexture, exit, Color.White);
        }
    }
}
