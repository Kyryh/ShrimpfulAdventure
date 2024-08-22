using KEngine;
using KEngine.Components;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Outro {

        public static void Load() {

            MediaPlayer.Play(KGame.GetContent<Song>("Music/IntroOutro"));
            MediaPlayer.IsRepeating = true;

            new GameObject("End",
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/outro"
                    }
                ]
            ).Load();

            Camera.MainCamera.Transform.Position = Vector2.Zero;
            Camera.MainCamera.Size = 10f;
        }
    }
}
