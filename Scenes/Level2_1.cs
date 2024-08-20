using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level2_1 {
        public static void Load() {

            MapLoader.LoadMap("2-1", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(0.5f, -20.5f));


            var td = MapLoader.SpawnTrapdoor(new Vector2(24f, -19.5f), MathHelper.Pi, false);
            MapLoader.SpawnLever(new Vector2(22.5f, -15f), td.Open, td.Close);


            MapLoader.CreateBackground("Sprites/background2");

            MapLoader.AddEndTrigger("2-2", new Vector2(41.5f, -15), new Vector2(1, 20));
        }
    }
}
