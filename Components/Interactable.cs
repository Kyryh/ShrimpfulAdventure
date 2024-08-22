using KEngine;
using KEngine.Components;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Interactable : Component {
        public Func<int, int> OnInteract { private get; init; }
        int state = 0;
        SoundEffect clickSound;

        public override void Initialize() {
            base.Initialize();
            clickSound = KGame.GetContent<SoundEffect>("Sound/lever");
        }

        public void Interact() {
            state = OnInteract.Invoke(state);
            clickSound.Play(0.2f, (float)ShrimpfulGame.Random.NextDouble() * 0.4f - 0.5f, 0);
        }
    }
}
