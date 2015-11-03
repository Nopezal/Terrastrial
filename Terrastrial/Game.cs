using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Prism.API;
using Prism.API.Audio;
using Prism.API.Behaviours;
using Prism.API.Defs;
using Terraria;

namespace Terrastrial
{
    class Game : GameBehaviour
    {
        public static KeyboardState prevKeyState = new KeyboardState();
        public bool GetKey(Keys k)
        {
            return Main.keyState.IsKeyDown(k);
        }
        public bool GetKey(Keys k, KeyState ks)
        {
            return ks == Main.keyState[k] && Main.keyState[k] != prevKeyState[k];
        }
        public override void PostUpdate()
        {
            if (GetKey(Keys.NumPad9, KeyState.Down))
                foreach(var kvp in Mod.DevArmor)
                Item.NewItem((int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y, 1, 1, kvp.Key);
            prevKeyState = Main.keyState;
        }
        public override void UpdateMusic(Ref<BgmEntry> current)
        {
            if (current.Value == VanillaBgms.Day)
                current.Value = Bgm.Entries["bossCelestialEater"];
        }
    }
}