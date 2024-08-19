using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Trapdoor : Component {
        public bool IsLong { get; init; }

        public bool IsOpen => !collider.IsActive;

        SpriteRenderer sr;
        Collider collider;
        public override void Initialize() {
            base.Initialize();
            sr = GameObject.GetComponent<SpriteRenderer>();
            collider = GameObject.GetComponent<Collider>();
        }

        public void Switch() {
            if (IsOpen)
                Close();
            else
                Open();
        }
        public void Open() {
            sr.spriteIndex = 2;
            collider.Active = false;
        }

        public void Close() {
            sr.spriteIndex = IsLong ? 1 : 0;
            collider.Active = true;
        }
    }
}
