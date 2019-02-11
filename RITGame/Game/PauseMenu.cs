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
    class PauseMenu
    {
        KeyboardState prevState;

        private Texture2D backgroundTexture;
        private Rectangle background;



        public PauseMenu(Texture2D bgi, int screenWidth, int screenHeight)
        {
            backgroundTexture = bgi;
            background = new Rectangle(0, 0, screenWidth, screenHeight);


            prevState = Keyboard.GetState();
        }

        /// <summary>
        /// Checks for pause menu relevant input
        /// </summary>
        public void Update(ref GameState state)
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.P) && !prevState.IsKeyDown(Keys.P))
            {
                state = GameState.Overworld;
            }
            prevState = kb;
        }

        /// <summary>
        /// Sets the previous keyboard state to ensure that it is not blank when Update() is called
        /// </summary>
        /// <param name="kb">The current state of the keyboard</param>
        public void SetPrevState(KeyboardState kb)
        {
            prevState = kb;
        }

        /// <summary>
        /// Draws the pause menu
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, background, Color.White);
        }
    }
}
