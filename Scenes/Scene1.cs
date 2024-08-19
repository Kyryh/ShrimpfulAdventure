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

            MapLoader.SpawnRock(new Vector2(22.5f, -15f));

            var td = MapLoader.SpawnTrapdoor(new Vector2(16, -13), 1f, false);

            MapLoader.SpawnLever(new Vector2(24.5f, -15f), td.Open, td.Close);


        }
    }
}
