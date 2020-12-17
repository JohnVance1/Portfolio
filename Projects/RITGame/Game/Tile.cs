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
    class Tile : IDrawable
    {
        // Fields
        private Texture2D sprite;
        private bool isWall;
        public bool IsWall { get { return isWall; } set { isWall = value; } }
        private Rectangle rectangle;
        static public int TILE_SIZE = 64;
        string tileType;
        private bool foughtBoss = false;
        private bool noZomb = false;
        private bool check1 = false;
        private bool check2 = false;
        private double back;

        

        // Constructor
        public Tile(Rectangle rectangle)
        {
            isWall = false;
            this.rectangle = rectangle;
            //back = rectangle.Height;
        }

        public String TileType { get { return tileType; } }

        // Methods
        public Rectangle GetWorldPos()
        {
            return rectangle;
        }

        public Texture2D GetTexture()
        {
            return sprite;
        }

        public void SetTexture(Texture2D texture)
        {
            sprite = texture;
        }

        public double SetHeight
        {
            get { return rectangle.Height; }
            set { rectangle.Height = (int)value; }

        }

        public bool NoZomb
        {
            get { return noZomb; }
            set { noZomb = value; }

        }

        /// <summary>
        /// Checks the type of the tile object, assigns a sprite based on the letter parameter
        /// </summary>
        /// <param name="type"></param>
        public void CheckType(string type, Dictionary<string, Texture2D> textures)
        {
            if (type == "B")
            {
                sprite = textures["Brick Wall 3D"];
                tileType = type;
                double height = rectangle.Height * 1.625;
                rectangle.Height = (int)height;
                rectangle.Y -= (rectangle.Height / 3);
                isWall = true;
            }
            else if(type == "P")
            {
                sprite = textures["Path 3D"];
                tileType = type;
                double height = rectangle.Height * 1.125;
                rectangle.Height = (int)height;
            }
            else if(type == "G")
            {
                sprite = textures["Grass 3D"];
                tileType = type;
                double height = rectangle.Height * 1.1875;
                rectangle.Height = (int)height;
            }
            else if(type == "W")
            {
                sprite = textures["Water"];
                tileType = type;
                isWall = true;
            } 
            else if(type == "E")
            {
                sprite = textures["Empty Space"];
                tileType = type;
                isWall = true;
            }
        }

        public void ChangeBossType(string type, Dictionary<string, Texture2D> textures, bool zombies)
        {
            back = rectangle.Height;
            if (type == "P" && zombies == true)
            {
                sprite = textures["Brick Wall 3D"];
                tileType = type;
                if (!check1)
                {
                    rectangle.Height = (int)back;
                    double height = rectangle.Height * 1.45;
                    rectangle.Height = (int)height;
                    rectangle.Y -= (rectangle.Height / 3);
                    back = rectangle.Height / 1.45;
                    check1 = true;
                }
                
                isWall = true;
            }

            else if (type == "P" && zombies == false && noZomb == false)
            {
                sprite = textures["Path 3D"];
                tileType = type;
                if (!check2)
                {
                    rectangle.Height = (int)back;
                    double height = rectangle.Height * 0.72;
                    rectangle.Height = (int)height;
                    double y = rectangle.Y + 33;
                    rectangle.Y = (int)y;
                    back = rectangle.Height / 0.72;
                    check2 = true;
                }
                
                isWall = false;
                noZomb = true;
            }

           
        }

        public void ChangeBossDoor(string type, Dictionary<string, Texture2D> textures, bool door)
        {            
            if (type == "P" && door == true && foughtBoss == false)
            {
                sprite = textures["Path 3D"];
                tileType = type;
                double height = rectangle.Height * 0.65;
                rectangle.Height = (int)height;
                double y = rectangle.Y + 38;
                rectangle.Y = (int)y;
                isWall = false;
                foughtBoss = true;

            }

            else if(type == "P" && door == false)
            {
                sprite = textures["Brick Wall 3D"];
                tileType = type;
                double height = rectangle.Height * 1.625;
                rectangle.Height = (int)height;
                rectangle.Y -= (rectangle.Height / 3);
                isWall = true;

            }

            
        }
    }
}