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
    internal class Level2_3 {
        public static void Load() {


            new GameObject(
                "Castle",
                position: new Vector2(24.5f, -15),
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/background2-3"
                    }
                ]
            ).Load();

            MapLoader.LoadMap("2-3", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(1.5f, -22.5f));

            var td1 = MapLoader.SpawnTrapdoor(new Vector2(20.5f, -16), MathHelper.PiOver2, false, true);

            var td2 = MapLoader.SpawnTrapdoor(new Vector2(28.75f, -3.5f), 0f, false, true);

            MapLoader.SpawnLever(new Vector2(34.5f, -15f),
                () => {
                    td1.Switch();
                    td2.Switch();
                },
                () => {
                    td1.Switch();
                    td2.Switch();
                }
            );


            MapLoader.CreateBackground("Sprites/background3");

            MapLoader.AddEndTrigger("3-1", new Vector2(41f, -13f), new Vector2(1, 3));
        }
    }
}
