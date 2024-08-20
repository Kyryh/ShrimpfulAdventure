using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level1_3 {

        public static void Load() {

            MapLoader.LoadMap("1-3", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(1.5f, -22.5f));


            var td1 = MapLoader.SpawnTrapdoor(new Vector2(5.5f, -5f), -MathHelper.PiOver2, true);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(31f, -21.5f), MathHelper.Pi, false);
            //var td3 = MapLoader.SpawnTrapdoor(new Vector2(33.25f, -17.5f), MathHelper.Pi, true);

            MapLoader.SpawnLever(new Vector2(35.5f, -23f), td1.Open, td1.Close);

            MapLoader.SpawnButton(new Vector2(15.5f, -23.30f), td2.Open, td2.Close);

            //MapLoader.SpawnLever(new Vector2(2.5f, -9f), td3.Open, td3.Close);

            //MapLoader.SpawnCurrent(new Vector2(36.5f, -21.5f), 24, 5, 0f);
            MapLoader.SpawnCurrent(new Vector2(3.5f, -25f), 20, 5, 0f);
            MapLoader.SpawnCurrent(new Vector2(14.5f, -2.5f), 10, 5, MathHelper.PiOver2);
            MapLoader.SpawnCurrent(new Vector2(37f, -14.5f), 15, 5, 0f);


            MapLoader.CreateBackground("Sprites/background1");

            MapLoader.AddEndTrigger("2-1", new Vector2(36.5f, 0), new Vector2(5, 1));
        }
    }
}
