using KEngine.Components.DrawableComponents;
using KEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEngine.Components;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ShrimpfulAdventure.Scenes {
    internal class Intro {

        public static void Load() {
            MediaPlayer.Play(KGame.GetContent<Song>("Music/IntroOutro"));
            MediaPlayer.IsRepeating = true;
            new GameObject("Intro",
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/intro"
                    },
                    new IntroComponent()
                ]
            ).Load();
        }
    }

    class IntroComponent :Component {
        public override void Update(float deltaTime) {
            if (Keyboard.GetState().GetPressedKeyCount() > 0)
                KGame.Instance.LoadScene("1-1");
        }
    }
}
