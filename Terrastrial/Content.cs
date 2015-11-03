using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Prism;
using Prism.API;
using Prism.API.Audio;
using Prism.API.Defs;
using Prism.API.Behaviours;
using Terraria;
using Microsoft.Xna.Framework;
using Terrastrial.Behaviors;

namespace Terrastrial
{
    class Content : ContentHandler
    {
        protected override Dictionary<string, ProjectileDef> GetProjectileDefs()
        {
            return new Dictionary<string, ProjectileDef>
            {
                { "dragonBall", new ProjectileDef("Dragon Ball", null, () => GetResource<Texture2D>("Resources/Projectiles/dragonBall.png"))
                    {
                        Size = new Point(48, 48),

                    }
                },
                { "celestialEaterSpike", new ProjectileDef("celestialEaterSpike", null, () => GetResource<Texture2D>("Resources/Projectiles/celestialEaterSpike.png"))
                    {
                        Size = new Point(26, 50),
                    }
                }
            };
        }
        protected override Dictionary<string, BgmEntry> GetBgms()
        {
            return new Dictionary<string, BgmEntry>
            {
                { "bossCelestialEater", new BgmEntry(new XAudioBgm(GetResource<SoundEffect>("Resources/Music/bossCelestiaEater.wav")), BgmPriority.Boss, () => false) }
            };
        }
        protected override Dictionary<string, NpcDef> GetNpcDefs()
        {
            return new Dictionary<string, NpcDef>
            {
                { "celestialEaterHead", new NpcDef("Celestial Eater", () => new CelestialEaterBehavior(), 100000, () => GetResource<Texture2D>("Resources/Textures/NPCs/celestialEaterHead.png"), () => GetResource<Texture2D>("Resources/Textures/NPCs/BossHeads/celestialEaterHead"))
                    {
                        Music = new BgmRef("bossCelestialEater"),
                        AiStyle = NpcAiStyle.None,
                        Defense = 55,
                        Damage = 175,
                        MaxLife = 70000,
                        IsTechnicallyABoss = true,
                        IsSummonableBoss = true,
                        ExcludedFromDeathTally = true,
                        NotOnRadar = true,
                        NpcSlots = 15,
                        Value = new NpcValue(new CoinValue(150000), new CoinValue(25000)),
                        KnockbackResistance = 0,
                        BuffImmunities = Mod.buffs,
                        Height = 88,
                        Width = 110,
                        IgnoreGravity = true,
                        IgnoreTileCollision = true,
                        SoundOnHit = VanillaSfxes.NpcHit[13],
                        SoundOnDeath = VanillaSfxes.NpcKilled[11],
                    }
                },
                { "celestialEaterHead", new NpcDef("Celestial Eater Leg", null, 1, () => GetResource<Texture2D>("Resources/Textures/NPCs/celestialEaterLeg.png"))
                    {
                        AiStyle = NpcAiStyle.None,
                        IsImmortal = true,
                    }
                },
            };
        }
        protected override Dictionary<string, ItemDef> GetItemDefs()
        {
            return new Dictionary<string, ItemDef>
            {
                { "myHeadgear", new ItemDef("Nopezal's Headgear", null, () => GetResource<Texture2D>("Resources/Textures/Items/Dev/myHelmet.png") )
                    {
                        ArmourData = new ItemArmourData( () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myHelmet"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myPlate"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myArms"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myPants")),
                        Defense = 22,
                        Description = new ItemDescription("Nopezal's developer armor helmet: Recolor of the Rune Wizard Hat!", null, false, true, false, false, false),
                        SetName = "nopezalDev",
                        MaxStack = 1,
                        Height = 30,
                        Width = 40,
                        Rarity = ItemRarity.Rainbow,
                    }
                },
                { "myPlate", new ItemDef("Nopezal's Plate", null, () => GetResource<Texture2D>("Resources/Textures/Items/Dev/myPlate.png") )
                    {
                        ArmourData = new ItemArmourData( () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myHelmet"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myPlate"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myArms"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myPants")),
                        Defense = 26,
                        Description = new ItemDescription("Nopezal's developer armor plate: Recolor of the Jungle Platemail!", null, false, true, false, false, false),
                        SetName = "nopezalDev",
                        MaxStack = 1,
                        Height = 30,
                        Width = 40,
                        Rarity = ItemRarity.Rainbow,
                    }
                },
                { "myPants", new ItemDef("Nopezal's Pants", null, () => GetResource<Texture2D>("Resources/Textures/Items/Dev/myPants.png") )
                    {
                        ArmourData = new ItemArmourData( () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myHelmet"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myPlate"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myArms"), () => GetResource<Texture2D>("Resource/Textures/Armor/Dev/myPants")),
                        Defense = 20,
                        Description = new ItemDescription("Nopezal's developer armor pants: Recolor of the Creeper Legs!", null, false, true, false, false, false),
                        SetName = "nopezalDev",
                        MaxStack = 1,
                        Height = 30,
                        Width = 40,
                        Rarity = ItemRarity.Rainbow,
                    }
                },
                { "dragonOrbStaff", new ItemDef("Dragon Orb Staff", null, () => GetResource<Texture2D>("Resource/Items/Dev/DragonBallStaff") )
                    {
                        Damage = 600,
                        ManaConsumption = 200,
                        DamageType = ItemDamageType.Magic,
                        MaxStack = 1,
                        Description = new ItemDescription("Nopezal's developer weapon: Sprited by myself", "Shoots a magnet-sphere like projectile which warps near enemies to hit them", false, true, false, false, false),
                        Height = 64,
                        Width = 64,
                        UseTime = 2,
                        ShootProjectile = new ProjectileRef("dragonBall"),
                        Rarity = ItemRarity.Rainbow,
                    }
                }
            };
        }
    }
}