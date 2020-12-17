using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GameName
{
    class FileReader
    {
        #region Fields and Properties
        static private int playerHealth;
        static private int ammo;
        static private Tile[,] tiles;
        static private int bossHealth;
        static private int bossAttack;
        static private int zombieHealth;
        static private int zombieCount;
        static private int playerAttack;
        static private int zombieAttack;
        private string mapFile;
        private string infoFile;

        private const int X_TILES = 30;
        private const int Y_TILES = 136;

        static public int PlayerHealth { get { return playerHealth; } }
        static public int Ammo { get { return ammo; } }
        static public Tile[,] TILES { get { return tiles; } }
        static public int X_Tiles { get { return X_TILES; } }
        static public int Y_Tiles { get { return Y_TILES; } }
        static public int PlayerAttack { get { return playerAttack; } }
        static public int ZombieHealth { get { return zombieHealth; } }
        static public int ZombieAttack { get { return zombieAttack; } }
        static public int ZombieCount { get { return zombieCount; } }
        static public int BossHealth { get { return bossHealth; } }
        static public int BossAttack { get { return bossAttack; } }
        #endregion

        // Constructor
        public FileReader()
        {
            tiles = new Tile[Y_TILES, X_TILES];
            mapFile = "../../../../../mapFile.txt";
            infoFile = "../../../../../infoFile.txt";

            InfoSetup();
        }

        // Methods
        /// <summary>
        /// Sets up the map 2D array of tiles
        /// </summary>
        public void MapSetup(Dictionary<string, Texture2D> textures)
        {
            StreamReader input = null;
            try
            {
                input = new StreamReader(mapFile);

                string line = null;
                int row = 0;

                // loop through each line of the text file
                while ((line = input.ReadLine()) != null)
                {
                    string[] entireRow = line.Split(' ');  // create an array of the letters in each row

                    for (int col = 0; col < entireRow.Length; col++)  // for each letter in the row...
                    {
                        tiles[row, col] = new Tile(new Rectangle(col * Tile.TILE_SIZE, row * Tile.TILE_SIZE, Tile.TILE_SIZE, Tile.TILE_SIZE));  // create a tile object at that coordinate

                        tiles[row, col].CheckType(entireRow[col], textures);  // determine if the tile added is a wall
                    }

                    row++;
                }               
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }

        /// <summary>
        /// Sets up some information read from a file
        /// </summary>
        public void InfoSetup()
        {
            StreamReader input = null;
            try
            {
                input = new StreamReader(infoFile);

                // Player setup
                playerHealth = int.Parse(input.ReadLine());
                playerAttack = int.Parse(input.ReadLine());
                ammo = int.Parse(input.ReadLine());
                if (ammo == -1)
                    ammo = int.MaxValue;

                // Zombie setup
                zombieHealth = int.Parse(input.ReadLine());
                zombieAttack = int.Parse(input.ReadLine());
                zombieCount = int.Parse(input.ReadLine());

                // Boss setup
                bossHealth = int.Parse(input.ReadLine());
                bossAttack = int.Parse(input.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }
    }
}
