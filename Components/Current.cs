using KEngine;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Current : DrawableComponent {
        Vector2 direction;
        public override void Initialize() {
            base.Initialize();
            GameObject.GetComponent<Collider>().OnCollision += Current_OnCollision;
            var rotationMatrix = Matrix.CreateRotationZ(Transform.Rotation);
            var direction = GameConstants.Vector2.Up*0.02f;
            Vector2.Transform(ref direction, ref rotationMatrix, out this.direction);
        }

        private void Current_OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (other.GameObject.TryGetComponent<ShrimpController>(out var shrimp)) {
                shrimp.velocity += direction;
            }
        }
    }
}
