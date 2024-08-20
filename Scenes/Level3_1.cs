using KEngine.Components.DrawableComponents;
using KEngine;
using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level3_1 {
        public static void Load() {


            MapLoader.LoadMap("3-1", "Sprites/tilesets1"); 

            MapLoader.SpawnShrimps(new Vector2(0.75f, -0.75f));

            MapLoader.SpawnRock(new Vector2(31.5f, -10.5f));
            var td1 = MapLoader.SpawnTrapdoor(new Vector2(33f, -11.5f), 0f, false);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(38.5f, -17.5f), MathHelper.Pi, true);
            MapLoader.SpawnLever(new Vector2(37.5f, -5f), td1.Open, td1.Close);
            MapLoader.SpawnButton(new Vector2(34.5f, -23.3f), td2.Open, td2.Close);

            MapLoader.CreateBackground("Sprites/background4");

            MapLoader.AddEndTrigger("3-2", new Vector2(41.5f, -15), new Vector2(1, 20));
        }
    }
}
