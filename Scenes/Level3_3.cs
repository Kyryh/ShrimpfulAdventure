using KEngine;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level3_3 {
        public static void Load() {


            MapLoader.LoadMap("3-3", "Sprites/tilesets1");


            new GameObject(
                "Treasure",
                position: new Vector2(36.5f, -11.5f),
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/treasure"
                    },
                    new Interactable() {
                        OnInteract = (i) => {
                            KGame.Instance.LoadScene("End");
                            return 0;
                        }
                    },
                    new BoxCollider() {
                        Width = 4f,
                        Height = 2f,
                        IsTrigger = true
                    }
                ]
            ).Load();

            MapLoader.SpawnShrimps(new Vector2(0.5f, -14.75f));

            MapLoader.SpawnRock(new Vector2(18.5f, -14.5f));
            var td1 = MapLoader.SpawnTrapdoor(new Vector2(10.5f, -11.5f), MathHelper.Pi, true);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(16.5f, -11.5f), MathHelper.Pi, true);
            var td3 = MapLoader.SpawnTrapdoor(new Vector2(22.5f, -11.5f), MathHelper.Pi, true);
            var td4 = MapLoader.SpawnTrapdoor(new Vector2(28.5f, -11.5f), MathHelper.Pi, true);

            var td5 = MapLoader.SpawnTrapdoor(new Vector2(18.5f, -17.5f), MathHelper.Pi, true);
            var td6 = MapLoader.SpawnTrapdoor(new Vector2(28.5f, -17.5f), MathHelper.Pi, true);
            var td7 = MapLoader.SpawnTrapdoor(new Vector2(33.5f, -1.5f), MathHelper.Pi, true);

            MapLoader.SpawnLever(new Vector2(30.5f, -5f), () => {
                td1.Switch();
                td5.Switch();
            }, () => {
                td1.Switch();
                td5.Switch();
            });

            MapLoader.SpawnButton(new Vector2(26.5f, -23.3f), () => {
                td3.Switch();
                td7.Switch();
            }, () => {
                td3.Switch();
                td7.Switch();
            });

            MapLoader.SpawnLever(new Vector2(38.5f, -5), () => {
                td2.Switch();
                td6.Switch();
            }, () => {
                td2.Switch();
                td6.Switch();
            });

            MapLoader.SpawnLever(new Vector2(36.5f, -21), td4.Switch, td4.Switch);


            MapLoader.CreateBackground("Sprites/background4");

            MapLoader.AddEndTrigger("3-3", new Vector2(41.5f, -5), new Vector2(1, 5));
        }
    }
}
