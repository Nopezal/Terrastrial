using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prism.API.Defs;
using Prism.API.Behaviours;
using Terraria;


namespace Terrastrial.Behaviors
{
    class CelestialEaterBehavior : NpcBehaviour
    {
        int[] legMainID = new int[7];
        int[] otherLegMainID = new int[7];
        int[] projectileMainID = new int[4];
        public enum AIState
        {
            Null = 0,
            ShootSpikes,
            Eat,
            Lasers,
            Web,
            HomingOrbs,
            SpawnMobs,
        }
        public override void OnInit()
        {
            Main.spriteBatch.Draw(Mod.ContentHandler.GetResource<Texture2D>("Resources/Textures/NPCs/celestialEaterBody.png"), Entity.position + new Vector2(0, 94), new Color());
            legMainID[0] = NPC.NewNPC((int)Entity.Center.X + 76, (int)Entity.Center.Y + 104, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[1] = NPC.NewNPC((int)Entity.Center.X - 77, (int)Entity.Center.Y + 104, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[2] = NPC.NewNPC((int)Entity.Center.X + 76, (int)Entity.Center.Y + 94, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[3] = NPC.NewNPC((int)Entity.Center.X - 77, (int)Entity.Center.Y + 94, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[4] = NPC.NewNPC((int)Entity.Center.X + 72, (int)Entity.Center.Y + 64, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[5] = NPC.NewNPC((int)Entity.Center.X - 73, (int)Entity.Center.Y + 64, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[6] = NPC.NewNPC((int)Entity.Center.X + 64, (int)Entity.Center.Y + 52, NpcDef.Defs["celestialEaterLeg"].Type);
            legMainID[7] = NPC.NewNPC((int)Entity.Center.X - 65, (int)Entity.Center.Y + 52, NpcDef.Defs["celestialEaterLeg"].Type);
            projectileMainID[0] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[1] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[2] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[3] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[4] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            this.Entity.position = new Vector2(Main.screenWidth / 2, Main.screenHeight - 16);            
        }
        public override void OnAI()
        {
            //initialization
            Stopwatch timer = new Stopwatch();
            AIState state = AIState.Null;
            Entity.TargetClosest();
            //movement
            Entity.velocity.Y += Entity.position.Y < Main.player[Main.myPlayer].position.Y + 64f ? -0.1f : 0.1f;
            Entity.velocity.X += Entity.position.X < Main.player[Main.myPlayer].position.X ? -0.1f : 0.1f;
            //ai
            if (state == AIState.Null && timer.ElapsedMilliseconds == 360)
            {
                state = (AIState)Main.rand.Next(1, 6);
            }
            if (state == AIState.ShootSpikes)
            {
                foreach (Projectile p in Main.projectile)
                {
                    p.friendly = false;
                }
            }
            else if (state == AIState.Eat)
            {
                if (Entity.position.Y > Main.player[Main.myPlayer].position.Y)
                    Entity.velocity.Y += 0.1f;
                else if (Entity.position.Y <= Main.player[Main.myPlayer].position.Y && Entity.position.Y < Main.screenWidth / 2)
                    Entity.velocity.Y += 0.1f;
            }
            else if (state == AIState.HomingOrbs)
            {
                Stopwatch spawntimer = new Stopwatch();
                spawntimer.Start();
                int projectile = 0;
                if (spawntimer.ElapsedMilliseconds == 30)
                {
                    projectile = Projectile.NewProjectile(Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenHeight), 0, 0, ProjectileDef.Defs["celestialOrb"].Type, Main.expertMode ? 87 : 101, 0f);
                }
                if (spawntimer.ElapsedMilliseconds == 60)
                {
                    float angle = (float)Math.Atan2(Main.player[Main.myPlayer].position.Y - Main.projectile[projectile].position.Y, Main.player[Main.myPlayer].position.X - Main.projectile[projectile].position.Y);
                    Main.projectile[projectile].velocity += 10 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    spawntimer.Restart();
                }
            }
            else if (state == AIState.Lasers)
            {
                Stopwatch dodgeTimer = new Stopwatch();
                dodgeTimer.Start();
                Vector2 eyeVector1 = Vector2.Zero;
                Vector2 eyeVector2 = Vector2.Zero;
                float angle1 = 0;
                float angle2 = 0;
                float[] oldPositions = { 0, 0, 0, 0 };
                Texture2D laser = Mod.ContentHandler.GetResource<Texture2D>("Resources/Textures/Projectiles/celestialLaser.png");
                if (dodgeTimer.ElapsedMilliseconds == 1)
                {
                    //why am i unable to use the bracket initialization thingy here
                    oldPositions[0] = Entity.position.X;
                    oldPositions[1] = Entity.position.Y;
                    oldPositions[2] = Main.player[Main.myPlayer].position.X;
                    oldPositions[3] = Main.player[Main.myPlayer].position.Y;
                    eyeVector1 = new Vector2(oldPositions[0] - oldPositions[2] - 16f, oldPositions[1] - oldPositions[3]);
                    eyeVector2 = new Vector2(oldPositions[0] - oldPositions[2] + 16f, oldPositions[1] - oldPositions[3]);
                    angle1 = (float)Math.Atan2(oldPositions[0] - oldPositions[2] - 16f, oldPositions[1] - oldPositions[3]);
                    angle2 = (float)Math.Atan2(oldPositions[0] - oldPositions[2] + 16f, oldPositions[1] - oldPositions[3]);
                }
                if (dodgeTimer.ElapsedMilliseconds == 60)
                {
                    for (float k = 0; k < 1f; k += 0.01f)
                    {
                        Main.spriteBatch.Draw(laser, k * eyeVector1, null, new Color(0, 0, 0), angle1, Vector2.Zero, 1f, SpriteEffects.None, 0);
                        Main.spriteBatch.Draw(laser, k * eyeVector2, null, new Color(0, 0, 0), angle2, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    }
                    if (dodgeTimer.ElapsedMilliseconds == 65)
                    {
                        dodgeTimer.Restart();
                        laser.Dispose();
                    }

                    if ((Main.player[Main.myPlayer].position.X >= oldPositions[0] - 16f && Main.player[Main.myPlayer].position.X <= oldPositions[0] + 16f) && (Main.player[Main.myPlayer].position.Y >= oldPositions[1] - 16f && Main.player[Main.myPlayer].position.Y <= oldPositions[1] + 16f))
                    {
                        Main.player[Main.myPlayer].Hurt(Main.rand.Next(200, 250), 0);
                    }
                }
            }
            else if (state == AIState.SpawnMobs)
            {
                for (int i = 0; i < Main.rand.Next(5, 8), i++)
                {
                    NPC.NewNPC((int)this.Entity.position.X, (int)this.Entity.position.Y, NpcDef.Defs["celestialFly"].Type);
                }
            }
            else if (state == AIState.Web)
            {
                int projectile = Projectile.NewProjectile(this.Entity.position.X, this.Entity.position.Y, 0, 0, ProjectileDef.Defs["celestialWeb"].Type, Main.expertMode ? 21 : 15, 0f);
                float angle = (float)Math.Atan2(Main.player[Main.myPlayer].position.Y - Main.projectile[projectile].position.Y, Main.player[Main.myPlayer].position.X - Main.projectile[projectile].position.Y);
                Main.projectile[projectile].velocity = 20 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }
        }
    }
}

