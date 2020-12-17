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
    class FinalBoss : Character
    {
        #region Fields and Properties
        private List<Zombie> spawns;
        private int damageTimer;
        private Texture2D zombieTexture1;
        private Texture2D zombieTexture2;
        private List<Texture2D> bossTextures;
        private const double SPAWNSPEED = .5;
        private bool isAlive;
        private bool isVunerable;
        private bool isDamaged;
        private int timer;
        private int maxHealth;
        private int phase;
        private Vector2 move;

        public List<Zombie> Spawns { get { return spawns; } }
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        public bool IsVunerable { get { return isVunerable; } }
        public bool IsDamaged
        {
            get { return isDamaged; }
            set { isDamaged = value; }
        }
        public int Attack { get { return FileReader.BossAttack; } }
        public int Phase { get { return phase; } }
        #endregion


        #region Constructor
        public FinalBoss(List<Texture2D> bossTexture, Texture2D zombieTexture1, Texture2D zombieTexture2)
            :base(bossTexture[0])
        {
            Health = FileReader.BossHealth;
            maxHealth = Health;
            spawns = new List<Zombie>();
            hitBox = new Rectangle(700, 200, 300, 300);
            damageTimer = 180;
            this.zombieTexture1 = zombieTexture1;
            this.zombieTexture2 = zombieTexture2;
            bossTextures = bossTexture;
            isAlive = true;
            isVunerable = false;
            isDamaged = false;
            timer = 600;
            phase = 1;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Updates the boss's movement - strafes side to side.
        /// </summary>
        public new void UpdatePos()
        {
            if (GetWorldPos().X <= 400)
            {
                MovementSpeed = 6;
            }
            else if (GetWorldPos().X >= 1200)
            {
                MovementSpeed = -6;
            }

            SetWorldPos(GetWorldPos().X + MovementSpeed, GetWorldPos().Y);
        }

        //  Boss summons zombies - add those to the spawns list.
        //  While the list is full, he is invunerable.
        //  When the list hits empty (all the zombie spawns are killed), he can take damage for so many seconds.
        //  Wash, rinse, repeat until someone dies.
        public void UpdateFight(Player p)
        {
            if(phase == 1)
            {
                UpdatePos();

                if (spawns.Count > 0)
                {
                    damageTimer = 180;
                    isVunerable = false;
                    timer--;
                }
                else
                {
                    timer = 600;
                    damageTimer--;
                    isVunerable = true;
                }

                if (damageTimer <= 0)
                {
                    Zombie z1 = new Zombie(zombieTexture1, 1500, 1);
                    Zombie z2 = new Zombie(zombieTexture1, 1500, 1);
                    Zombie z3 = new Zombie(zombieTexture1, 1500, 1);
                    Zombie z4 = new Zombie(zombieTexture1, 1500, 1);
                    Zombie z5 = new Zombie(zombieTexture1, 1500, 1);

                    z1.SetWorldPos(600, 400);
                    z2.SetWorldPos(800, 400);
                    z3.SetWorldPos(1000, 400);
                    z4.SetWorldPos(1200, 400);
                    z5.SetWorldPos(1400, 400);

                    z1.SetSpeed(2);
                    z2.SetSpeed(2);
                    z3.SetSpeed(2);
                    z4.SetSpeed(2);
                    z5.SetSpeed(2);

                    spawns.Add(z1);
                    spawns.Add(z2);
                    spawns.Add(z3);
                    spawns.Add(z4);
                    spawns.Add(z5);

                    foreach (Zombie z in spawns)
                    {
                        z.Health = 5;
                    }
                }

                if (timer <= 0)
                {
                    spawns.Clear();
                }

                if (bossTextures.Count > 1)
                {
                    if (Health <= maxHealth * .8) SetTexture(bossTextures[1]);
                    if (Health <= maxHealth * .6) SetTexture(bossTextures[2]);
                    if (Health <= maxHealth * .4) SetTexture(bossTextures[3]);
                    if (Health <= maxHealth * .2) SetTexture(bossTextures[4]);
                }
            }
            else if(phase == 2)
            {
                // Boss strafes around for a bit, pauses, then lunges at the player, dealing damage upon collision.
                if(timer > 0)
                {
                    UpdatePos();
                    timer--;
                }
                else
                {
                    if(timer > -60)
                    {
                        // Maybe add slight shaking or a noise for a telegraph here.
                        timer--;
                        if(timer <= -60)
                        {
                            GetAttackAngle(p);
                        }
                    }
                    else
                    {
                        DashAttackUpdate();

                        foreach(Rectangle r in Overworld.WALLS)
                        {
                            if(HitBox.Intersects(r))
                            {
                                SetWorldPos((int)(GetWorldPos().X - move.X), (int)(GetWorldPos().Y - move.Y));
                                timer = 180;
                            }
                        }

                        foreach (Rectangle r in Overworld.ChangeWalls)
                        {
                            if (HitBox.Intersects(r))
                            {
                                SetWorldPos((int)(GetWorldPos().X - move.X), (int)(GetWorldPos().Y - move.Y));
                                timer = 180;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Boss's way of dealing damage in phase 2, a lunging attack.
        /// </summary>
        private void GetAttackAngle(Player p)
        {
            int playerCenterX = p.GetWorldPos().X + p.GetWorldPos().Width / 2;
            int playerCenterY = p.GetWorldPos().Y + p.GetWorldPos().Height / 2;
            int bossCenterX = hitBox.X + hitBox.Width / 2;
            int bossCenterY = hitBox.Y + hitBox.Height / 2;
            double hypotenuse = Math.Sqrt(Math.Pow(playerCenterX - bossCenterX, 2) + Math.Pow(playerCenterY - bossCenterY, 2));

            float movementX = (float)(2 * (playerCenterX - bossCenterX) / hypotenuse);
            float movementY = (float)(2 * (playerCenterY - bossCenterY) / hypotenuse);
            move.X = movementX * 8;
            move.Y = movementY * 8;
        }

        /// <summary>
        /// Updates the movement of the boss during an attack
        /// </summary>
        public void DashAttackUpdate()
        {
            hitBox.X += (int)move.X;
            hitBox.Y += (int)move.Y;
        }

        new public bool CheckRanged(Projectile p)
        {
            if (p.IsColliding(HitBox))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes boss health based on the attack level of the projectile
        /// </summary>
        /// <param name="attack"></param>
        public void Damaged(int attack)
        {
            Health -= Math.Abs(attack);
            isDamaged = true;

            // Check if the boss's health is reduced to zero.
            if (Health <= 0)
            {
                if (phase == 1)
                {
                    phase = 2;
                    Health = FileReader.BossHealth;
                    timer = 180;
                }
                else if(phase == 2)
                {
                    isAlive = false;
                }             
            }
        }

        /// <summary>
        /// Pops the balloons according to how much health has been lost.
        /// </summary>
        public void ChangeState()
        {
            if(phase == 1)
            {
                if (Health == maxHealth * .8)
                {
                    if (bossTextures.Count > 1) SetTexture(bossTextures[1]);
                    SceneManager.soundEffects["pop"].Play();
                }
                else if (Health == maxHealth * .6)
                {
                    if (bossTextures.Count > 1) SetTexture(bossTextures[2]);
                    SceneManager.soundEffects["pop"].Play();
                }
                else if (Health == maxHealth * .4)
                {
                    if (bossTextures.Count > 1) SetTexture(bossTextures[3]);
                    SceneManager.soundEffects["pop"].Play();
                }
                else if (Health == maxHealth * .2)
                {
                    if (bossTextures.Count > 1) SetTexture(bossTextures[4]);
                    SceneManager.soundEffects["pop"].Play();
                }
            }
            else if(phase == 2)
            {
                SetTexture(bossTextures[5]);
            }
        }

        public FinalBoss ResetBoss()
        {
            return new FinalBoss(bossTextures, zombieTexture1, zombieTexture2);
        }

        #endregion
    }
}
