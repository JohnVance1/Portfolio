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
    class Overworld
    {
        // Fields
        private List<Tile> emptyList;
        private List<Tile> brickList;
        private List<Tile> pathList;
        private List<Tile> grassList;
        private List<Tile> waterList;
        private List<Tile> bossList;
        private List<IDrawable> zombiesList;
        private List<Texture2D> zombieTextures;
        static private List<Rectangle> walls;
        static private List<Rectangle> bossWalls;
        static private List<Rectangle> changeWalls;
        private Random rand;
        private Random randTexture;
        private Zombie zomb;
        public bool zombList;
        private Dictionary<string, Texture2D> tileTextures;
        private List<Tile> bossDoor;

        private Texture2D zombieTexture1;
        private Texture2D zombieTexture2;
        private Player player;

        static public int NUMBER_OF_ZOMBIES = FileReader.ZombieCount;
        static public List<Rectangle> WALLS { get { return walls;} }
        static public List<Rectangle> BossWalls { get { return bossWalls; } }
        public List<Tile> BossList { get { return bossList; } }
        public Dictionary<string, Texture2D> TileTextures { get { return tileTextures; } }
        static public List<Rectangle> ChangeWalls { get { return changeWalls; } }
        public List<Tile> BossDoor { get { return bossDoor; } }

        // Constructor
        public Overworld(Dictionary<string, Texture2D> textures, Texture2D zombieTexture1, Texture2D zombieTexture2, Player player, List<Texture2D> zTextures)
        {
            this.zombieTexture1 = zombieTexture1;
            this.zombieTexture2 = zombieTexture2;
            this.player = player;
            zombieTextures = zTextures;

            // Read files to setup global properties
            FileReader reader = new FileReader();
            reader.MapSetup(textures);
            tileTextures = textures;

            walls = new List<Rectangle>();
            bossWalls = new List<Rectangle>();
            changeWalls = new List<Rectangle>();
            emptyList = new List<Tile>();
            brickList = new List<Tile>();
            pathList = new List<Tile>();
            grassList = new List<Tile>();
            waterList = new List<Tile>();
            bossList = new List<Tile>();
            bossDoor = new List<Tile>();
            zombList = true;


            Tile currentTile;
            for (int row = 0; row < FileReader.TILES.GetLength(0); row++)
            {
                for (int col = 0; col < FileReader.TILES.GetLength(1); col++)
                {
                    currentTile = (FileReader.TILES[row, col]);

                    if (((currentTile.GetWorldPos().X > (7 * Tile.TILE_SIZE) && currentTile.GetWorldPos().X <= (21 * Tile.TILE_SIZE)) && currentTile.GetWorldPos().Y == (29 * Tile.TILE_SIZE)))
                    {
                        bossList.Add(currentTile);
                        
                        bossWalls.Add(FileReader.TILES[row, col].GetWorldPos());
                    }

                    if (((currentTile.GetWorldPos().X > (7 * Tile.TILE_SIZE) && currentTile.GetWorldPos().X <= (21 * Tile.TILE_SIZE)) && currentTile.GetWorldPos().Y == (20 * Tile.TILE_SIZE)))
                    {
                        bossDoor.Add(currentTile);

                        changeWalls.Add(FileReader.TILES[row, col].GetWorldPos());
                    }

                    if (currentTile.TileType == "B") brickList.Add(currentTile);
                    else if (currentTile.TileType == "P") pathList.Add(currentTile);
                    else if (currentTile.TileType == "G") grassList.Add(currentTile);
                    else if (currentTile.TileType == "W") waterList.Add(currentTile);
                    else if (currentTile.TileType == "E") emptyList.Add(currentTile);
                    // Checks each tile to see if it is a wall, then adds them to a list of walls.
                    if (FileReader.TILES[row, col].IsWall)
                    {
                        walls.Add(FileReader.TILES[row, col].GetWorldPos());
                    }
                }
            }

            foreach (Tile element in bossList)
            {
                //element.CheckType("B", tileTextures);
                //element.SetHeight = element.SetHeight * 1.625;
                //element.SetTexture(tileTextures["Brick Wall 3D"]);
                //element.IsWall = true;
                //element.ChangeBossType(element.TileType, tileTextures, zombList);
                

            }

            //foreach (Tile element in bossDoor)
            //{
            //    element.ChangeBossDoor(element.TileType, tileTextures, true);

            //}

            

            /*
            int x_Pos;
            int y_Pos;
            random = new Random();
            zombiesList = new List<IDrawable>();
            while(zombiesList.Count < NUMBER_OF_ZOMBIES)
            {
                x_Pos = random.Next(FileReader.X_Tiles * Tile.TILE_SIZE);
                y_Pos = random.Next(FileReader.Y_Tiles * Tile.TILE_SIZE);
                zombie.SetWorldPos(x_Pos, y_Pos);
                bool shouldSpawn = true;
                foreach (Rectangle wall in walls)
                    if (wall.Intersects(zombie.GetWorldPos())) shouldSpawn = false;
                if (shouldSpawn)
                {
                    Zombie newZombie = new Zombie(zombie.GetTexture());
                    newZombie.SetWorldPos(x_Pos, y_Pos);
                    zombiesList.Add(newZombie);
                }
            }
            */

            rand = new Random();
            randTexture = new Random();
            int nextTexture;
            zombiesList = new List<IDrawable>();
            Zombie tankZombie = new Zombie(zombieTexture2, 1250, 5);
            tankZombie.SetWorldPos(750, 4000);
            tankZombie.Health = tankZombie.Health + 10;
            zombiesList.Add(tankZombie);
            Zombie tankZombie2 = new Zombie(zombieTexture2, 1250, 5);
            tankZombie2.SetWorldPos(200, 2000);
            tankZombie2.Health = tankZombie2.Health + 10;
            zombiesList.Add(tankZombie2);
            Zombie tankZombie3 = new Zombie(zombieTexture2, 1250, 5);
            tankZombie3.SetWorldPos(1300, 2000);
            tankZombie3.Health = tankZombie3.Health + 10; 
            zombiesList.Add(tankZombie3);
            zomb = new Zombie(zombieTexture1, 1500, 1);
            Zombie zomb2;
            int count = 0;
            int numX = 0;
            int numY = 0;
            while (count < NUMBER_OF_ZOMBIES)
            {
                int contains = 0;
                int intersects = 0;
                int nearPlayer = 0;
                int bossRoomCheck = 0;
                numX = rand.Next(0, FileReader.X_Tiles - 1);
                numY = rand.Next(0, FileReader.Y_Tiles - 1);
                zomb.SetWorldPos(numX * Tile.TILE_SIZE, numY * Tile.TILE_SIZE);

                foreach (Rectangle element in WALLS)
                {
                    if (zomb.GetWorldPos().Intersects(element))
                    {
                        intersects++;

                    }

                }

                for (int i = 0; i < zombiesList.Count; i++)
                {
                    if (zomb.GetWorldPos() == zombiesList[i].GetWorldPos())
                    {
                        contains++;

                    }

                }

                if ((player.HitBox.X + 100 < FileReader.X_Tiles * Tile.TILE_SIZE) && (player.HitBox.X - 100 > 0) && (player.HitBox.Y + 100 < FileReader.Y_Tiles * Tile.TILE_SIZE) && (player.HitBox.Y - 100 > 0))
                {
                    if (((player.HitBox.X + 100 <= zomb.HitBox.X) || (player.HitBox.X - 100 >= zomb.HitBox.X)) && ((player.HitBox.Y + 1000 <= zomb.HitBox.Y) || (player.HitBox.Y - 1000 >= zomb.HitBox.Y)))
                    {
                        nearPlayer = 1;

                    }

                }

                if(zomb.HitBox.Y >= 31 * Tile.TILE_SIZE)
                {
                    bossRoomCheck = 1;

                }

                if (intersects == 0 && contains == 0 && nearPlayer == 1 && bossRoomCheck == 1)
                {
                    nextTexture = randTexture.Next(0, 5);
                    ///Changes the color of the zombie created based on a radnom number generated
                    switch (nextTexture)
                    {
                        case 0:
                            zomb2 = new Zombie(zombieTextures[0], 1000, 1);
                            break;
                        case 1:
                            zomb2 = new Zombie(zombieTextures[1], 1000, 1);
                            break;
                        case 2:
                            zomb2 = new Zombie(zombieTextures[2], 1000, 1);
                            break;
                        case 3:
                            zomb2 = new Zombie(zombieTextures[3], 1000, 1);
                            break;
                        case 4:
                            zomb2 = new Zombie(zombieTextures[4], 1000, 1);
                            break;
                        case 5:
                            zomb2 = new Zombie(zombieTextures[5], 1000, 1);//Does not get to these at all?
                            break;
                        default:
                            zomb2 = new Zombie(zombieTextures[6], 1000, 1);//Does not get to these at all?
                            break;
                    }
                    
                    zomb2.SetWorldPos(zomb.HitBox.X, zomb.HitBox.Y);
                    zombiesList.Add(zomb2);
                    count++;

                }

            }

        }

        // Methods
        public List<IDrawable> DrawableObjects()
        {
            List<IDrawable> drawList = new List<IDrawable>();
            drawList.AddRange(waterList);
            drawList.AddRange(grassList);
            drawList.AddRange(pathList);
            drawList.AddRange(brickList);
            drawList.AddRange(emptyList);
            //drawList.AddRange(zombiesList);
            return drawList;
        }

        public List<IDrawable> GetZombies()
        {
            return zombiesList;
        }

        public void ClearBossLists()
        {
            bossWalls.Clear();
            bossList.Clear();

        }

        public void ClearBossDoor()
        {
            bossDoor.Clear();
            changeWalls.Clear();

        }

        public List<IDrawable> NewZombieList()
        {
            //Tile currentTile;
            //for (int row = 0; row < FileReader.TILES.GetLength(0); row++)
            //{
            //    for (int col = 0; col < FileReader.TILES.GetLength(1); col++)
            //    {
            //        currentTile = (FileReader.TILES[row, col]);

            //        if (((currentTile.GetWorldPos().X > (7 * Tile.TILE_SIZE) && currentTile.GetWorldPos().X <= (21 * Tile.TILE_SIZE)) && currentTile.GetWorldPos().Y == (29 * Tile.TILE_SIZE)))
            //        {
            //            bossList.Add(currentTile);

            //            bossWalls.Add(FileReader.TILES[row, col].GetWorldPos());
            //        }

            //        if (((currentTile.GetWorldPos().X > (7 * Tile.TILE_SIZE) && currentTile.GetWorldPos().X <= (21 * Tile.TILE_SIZE)) && currentTile.GetWorldPos().Y == (21 * Tile.TILE_SIZE)))
            //        {
            //            bossDoor.Add(currentTile);

            //            changeWalls.Add(FileReader.TILES[row, col].GetWorldPos());
            //        }

            //        if (currentTile.TileType == "B") brickList.Add(currentTile);
            //        else if (currentTile.TileType == "P") pathList.Add(currentTile);
            //        else if (currentTile.TileType == "G") grassList.Add(currentTile);
            //        else if (currentTile.TileType == "W") waterList.Add(currentTile);
            //        else if (currentTile.TileType == "E") emptyList.Add(currentTile);
            //        // Checks each tile to see if it is a wall, then adds them to a list of walls.
            //        if (FileReader.TILES[row, col].IsWall)
            //        {
            //            walls.Add(FileReader.TILES[row, col].GetWorldPos());
            //        }
            //    }
            //}

            foreach (Tile element in bossList)
            {
                //element.CheckType("B", tileTextures);
                //element.SetHeight = element.SetHeight * 1.625;
                //element.SetTexture(tileTextures["Brick Wall 3D"]);
                //element.IsWall = true;
                //element.ChangeBossType(element.TileType, tileTextures, zombList);


            }

            //foreach (Tile element in bossDoor)
            //{
            //    element.ChangeBossDoor(element.TileType, tileTextures, true);

            //}



            /*
            int x_Pos;
            int y_Pos;
            random = new Random();
            zombiesList = new List<IDrawable>();
            while(zombiesList.Count < NUMBER_OF_ZOMBIES)
            {
                x_Pos = random.Next(FileReader.X_Tiles * Tile.TILE_SIZE);
                y_Pos = random.Next(FileReader.Y_Tiles * Tile.TILE_SIZE);
                zombie.SetWorldPos(x_Pos, y_Pos);
                bool shouldSpawn = true;
                foreach (Rectangle wall in walls)
                    if (wall.Intersects(zombie.GetWorldPos())) shouldSpawn = false;
                if (shouldSpawn)
                {
                    Zombie newZombie = new Zombie(zombie.GetTexture());
                    newZombie.SetWorldPos(x_Pos, y_Pos);
                    zombiesList.Add(newZombie);
                }
            }
            */

            rand = new Random();
            randTexture = new Random();
            int nextTexture;
            zombiesList = new List<IDrawable>();
            Zombie tankZombie = new Zombie(zombieTexture2, 1250, 5);
            tankZombie.SetWorldPos(750, 4000);
            tankZombie.Health = tankZombie.Health + 10;
            zombiesList.Add(tankZombie);
            Zombie tankZombie2 = new Zombie(zombieTexture2, 1250, 5);
            tankZombie2.SetWorldPos(200, 2000);
            tankZombie2.Health = tankZombie2.Health + 10;
            zombiesList.Add(tankZombie2);
            Zombie tankZombie3 = new Zombie(zombieTexture2, 1250, 5);
            tankZombie3.SetWorldPos(1300, 2000);
            tankZombie3.Health = tankZombie3.Health + 10;
            zombiesList.Add(tankZombie3);
            zomb = new Zombie(zombieTexture1, 1500, 1);
            Zombie zomb2;
            int count = 0;
            int numX = 0;
            int numY = 0;
            while (count < NUMBER_OF_ZOMBIES)
            {
                int contains = 0;
                int intersects = 0;
                int nearPlayer = 0;
                int bossRoomCheck = 0;
                numX = rand.Next(0, FileReader.X_Tiles - 1);
                numY = rand.Next(0, FileReader.Y_Tiles - 1);
                zomb.SetWorldPos(numX * Tile.TILE_SIZE, numY * Tile.TILE_SIZE);

                foreach (Rectangle element in WALLS)
                {
                    if (zomb.GetWorldPos().Intersects(element))
                    {
                        intersects++;

                    }

                }

                for (int i = 0; i < zombiesList.Count; i++)
                {
                    if (zomb.GetWorldPos() == zombiesList[i].GetWorldPos())
                    {
                        contains++;

                    }

                }

                if ((player.HitBox.X + 100 < FileReader.X_Tiles * Tile.TILE_SIZE) && (player.HitBox.X - 100 > 0) && (player.HitBox.Y + 100 < FileReader.Y_Tiles * Tile.TILE_SIZE) && (player.HitBox.Y - 100 > 0))
                {
                    if (((player.HitBox.X + 100 <= zomb.HitBox.X) || (player.HitBox.X - 100 >= zomb.HitBox.X)) && ((player.HitBox.Y + 1000 <= zomb.HitBox.Y) || (player.HitBox.Y - 1000 >= zomb.HitBox.Y)))
                    {
                        nearPlayer = 1;

                    }

                }

                if (zomb.HitBox.Y >= 31 * Tile.TILE_SIZE)
                {
                    bossRoomCheck = 1;

                }

                if (intersects == 0 && contains == 0 && nearPlayer == 1 && bossRoomCheck == 1)
                {
                    nextTexture = randTexture.Next(0, 5);
                    ///Changes the color of the zombie created based on a radnom number generated
                    switch (nextTexture)
                    {
                        case 0:
                            zomb2 = new Zombie(zombieTextures[0], 1000, 1);
                            break;
                        case 1:
                            zomb2 = new Zombie(zombieTextures[1], 1000, 1);
                            break;
                        case 2:
                            zomb2 = new Zombie(zombieTextures[2], 1000, 1);
                            break;
                        case 3:
                            zomb2 = new Zombie(zombieTextures[3], 1000, 1);
                            break;
                        case 4:
                            zomb2 = new Zombie(zombieTextures[4], 1000, 1);
                            break;
                        case 5:
                            zomb2 = new Zombie(zombieTextures[5], 1000, 1);//Does not get to these at all?
                            break;
                        default:
                            zomb2 = new Zombie(zombieTextures[6], 1000, 1);//Does not get to these at all?
                            break;
                    }

                    zomb2.SetWorldPos(zomb.HitBox.X, zomb.HitBox.Y);
                    zombiesList.Add(zomb2);
                    count++;

                }

            }
            return zombiesList;
        }


        public List<IDrawable> DrawWall()
        {
            List<IDrawable> drawList = new List<IDrawable>();
            drawList.AddRange(bossList);
            return drawList;

        }

        public List<IDrawable> DrawDoor()
        {
            List<IDrawable> drawDoor = new List<IDrawable>();
            drawDoor.AddRange(bossDoor);
            return drawDoor;

        }
    }
}
