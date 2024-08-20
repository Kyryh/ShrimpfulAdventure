using KEngine;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level2_2 {
        public static void Load() {

            new GameObject(
                "Castle",
                position: new Vector2(22,-13),
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/castle"
                    }
                ]
            ).Load();

            MapLoader.LoadMap("2-2", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(0.5f, -20.5f));

            MapLoader.SpawnRock(new Vector2(16.5f, -16.5f));
            var td1 = MapLoader.SpawnTrapdoor(new Vector2(7.75f, -15.5f), MathHelper.Pi, false);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(9.5f, -3.75f), MathHelper.PiOver2, true);
            MapLoader.SpawnLever(new Vector2(26.5f, -7f), td1.Open, td1.Close);
            MapLoader.SpawnButton(new Vector2(24.5f, -21.3f), td2.Open, td2.Close);

            MapLoader.CreateBackground("Sprites/background3");

            MapLoader.AddEndTrigger("2-3", new Vector2(41.5f, -15), new Vector2(1, 20));
        }
    }
}
