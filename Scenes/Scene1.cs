using KEngine;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Scene1 {
        public static void Load() {

            MapLoader.LoadMap("1-1", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(1.5f, -22.5f));


            var td1 = MapLoader.SpawnTrapdoor(new Vector2(15.5f, -15.25f), MathHelper.PiOver2, true);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(7.25f, -5.5f), MathHelper.Pi, true);
            var td3 = MapLoader.SpawnTrapdoor(new Vector2(33.25f, -17.5f), MathHelper.Pi, true);

            MapLoader.SpawnLever(new Vector2(22.5f, -19f), td1.Open, td1.Close);

            MapLoader.SpawnButton(new Vector2(23.5f, -15.30f), td2.Open, td2.Close);

            MapLoader.SpawnLever(new Vector2(2.5f, -9f), td3.Open, td3.Close);

            MapLoader.SpawnCurrent(new Vector2(36.5f, -21.5f), 24, 5, 0f);

            MapLoader.CreateBackground("Sprites/background1");
        }
    }
}
