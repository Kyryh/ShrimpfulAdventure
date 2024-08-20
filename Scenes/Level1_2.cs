using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Level1_2 {
        public static void Load() {

            MapLoader.LoadMap("1-2", "Sprites/tilesets1");

            MapLoader.SpawnShrimps(new Vector2(3.5f, 0f));


            var td1 = MapLoader.SpawnTrapdoor(new Vector2(5.5f, -11.75f), -MathHelper.PiOver2, true);
            var td2 = MapLoader.SpawnTrapdoor(new Vector2(7f, -9.5f), MathHelper.Pi, false, true);
            var td3 = MapLoader.SpawnTrapdoor(new Vector2(21.5f, -5.75f), MathHelper.PiOver2, false);

            MapLoader.SpawnLever(new Vector2(20.5f, -5f),
                () => {
                    td1.Switch();
                    td2.Switch();
                    td3.Switch();
                },
                () => {
                    td1.Switch();
                    td2.Switch();
                    td3.Switch();
                }
            );

            var td4 = MapLoader.SpawnTrapdoor(new Vector2(15.5f, -17.75f), MathHelper.PiOver2, true);


            MapLoader.SpawnLever(new Vector2(10.5f, -17), td4.Switch, td4.Switch);

            MapLoader.SpawnRock(new Vector2(14.5f, -15.5f));

            var td5 = MapLoader.SpawnTrapdoor(new Vector2(31.25f, -19.5f), MathHelper.Pi, true);

            MapLoader.SpawnButton(new Vector2(7.5f, -23.30f), td5.Switch, td5.Switch);


            MapLoader.SpawnCurrent(new Vector2(35.5f, -23.5f), 24, 5, 0f);

            MapLoader.CreateBackground("Sprites/background1");

            MapLoader.AddEndTrigger("1-3", new Vector2(35.5f, 0), new Vector2(8, 1));
        }
    }
}
