using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prism;
using Prism.API;
using Prism.API.Defs;
using Prism.API.Behaviours;
using Terraria;
using Terraria.Graphics.Effects;
using System.Diagnostics;

namespace Terrastrial.Behaviors
{
    class CelestialEaterBehavior : NpcBehaviour
    {
        int[] npcMainID = new int[7];
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
            npcMainID[0] = NPC.NewNPC((int)Entity.Center.X + 76, (int)Entity.Center.Y + 104, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[1] = NPC.NewNPC((int)Entity.Center.X - 77, (int)Entity.Center.Y + 104, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[2] = NPC.NewNPC((int)Entity.Center.X + 76, (int)Entity.Center.Y + 94, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[3] = NPC.NewNPC((int)Entity.Center.X - 77, (int)Entity.Center.Y + 94, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[4] = NPC.NewNPC((int)Entity.Center.X + 72, (int)Entity.Center.Y + 64, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[5] = NPC.NewNPC((int)Entity.Center.X - 73, (int)Entity.Center.Y + 64, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[6] = NPC.NewNPC((int)Entity.Center.X + 64, (int)Entity.Center.Y + 52, NpcDef.Defs["celestialEaterLeg"].Type);
            npcMainID[7] = NPC.NewNPC((int)Entity.Center.X - 65, (int)Entity.Center.Y + 52, NpcDef.Defs["celestialEaterLeg"].Type);
            projectileMainID[0] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[1] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[2] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[3] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            projectileMainID[4] = Projectile.NewProjectile(Entity.Center.X + 64f, Entity.Center.Y + 64, 0, 0, ProjectileDef.Defs["celestialEaterSpike"].Type, ProjectileDef.Defs["celestialEaterSpike"].Damage, 1f);
            this.Entity.position = new Vector2(Main.screenWidth / 2, Main.screenHeight - 16);            
        }
        public override void OnAI()
        {
            Stopwatch timer = new Stopwatch();
            AIState state = AIState.Null;
            Entity.TargetClosest();
            if (Entity.position.Y != Main.player[Main.myPlayer].position.Y)
                Entity.velocity.Y -= Entity.position.Y < Main.player[Main.myPlayer].position.Y + 64f ? -0.1f : 0.1f;
            if (state == AIState.Null && timer.ElapsedMilliseconds == 60)
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
        }
    }
}

