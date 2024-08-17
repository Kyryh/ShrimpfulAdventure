using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class ShrimpController : Component {
        AnimationController ac;
        Collider collider;
        TimeSpan timeSinceGrounded = TimeSpan.Zero;
        public override void Initialize() {
            base.Initialize();
            ac = GameObject.GetComponent<AnimationController>();
            collider = GameObject.GetComponent<Collider>();
            collider.OnCollision += OnCollision;
        }

        private void OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (hitInfo.direction == GameConstants.Vector2.Up) {
                Console.WriteLine("touched gorund");
                timeSinceGrounded = TimeSpan.Zero;
            }
        }



        public override void Update(float deltaTime) {
            base.Update(deltaTime);

            var kstate = Keyboard.GetState();

            if (timeSinceGrounded < TimeSpan.FromSeconds(0.2f))

            Transform.Position += new Vector2(0, -0.05f);
        }
    }
}
