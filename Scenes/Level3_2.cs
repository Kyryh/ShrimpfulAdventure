using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level3_2 {
        public static void Load() {


            MapLoader.LoadMap("3-2", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(5.5f, -0.75f));

            MapLoader.SpawnRock(new Vector2(6.5f, -10.5f));
            var td1 = MapLoader.SpawnTrapdoor(new Vector2(27.5f, -8f), MathHelper.PiOver2, false);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(27.5f, -9f), MathHelper.PiOver2, false);
            var td3 = MapLoader.SpawnTrapdoor(new Vector2(18.5f, -9.5f), MathHelper.Pi, false);
            var td4 = MapLoader.SpawnTrapdoor(new Vector2(3.5f, -9f), -MathHelper.PiOver2, false);
            MapLoader.SpawnLever(new Vector2(8.5f, -23f), td1.Open, td1.Close);
            MapLoader.SpawnButton(new Vector2(12.5f, -23.3f), td2.Open, td2.Close);
            MapLoader.SpawnButton(new Vector2(22.5f, -7.3f), td3.Open, td3.Close);
            MapLoader.SpawnLever(new Vector2(24.5f, -23f), td4.Open, td4.Close);

            MapLoader.CreateBackground("Sprites/background4");

            MapLoader.AddEndTrigger("3-3", new Vector2(41.5f, -5), new Vector2(1, 5));
        }
    }
}
