using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.API;
using Prism.API.Defs;

namespace Terrastrial
{
    class Mod : ModDef
    {
        public static List<int> buffs = new List<int>();
        public static int[] DevArmor;
        protected override ContentHandler CreateContentHandler()
        {
            return new Content();
        }
        public override void OnLoad()
        {
            DevArmor = new int[] { ItemDef.Defs["myHelmet"].Type, ItemDef.Defs["myPlate"].Type, ItemDef.Defs["myPants"].Type};
            for(int i = 1; i < BuffDefs.Count() - 1; i++)
                buffs.Add(i);
        }
    }
}
