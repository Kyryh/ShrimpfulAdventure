using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using KEngine.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class Current : DrawableComponent {
        Vector2 direction;
        Sprite bubble;
        public int Length { private get; init; }
        public int Width { private get; init; }
        public override void Initialize() {
            base.Initialize();
            bubble = KGame.GetSprite("Sprites/bubbles");
            var collider = GameObject.GetComponent<Collider>();
            collider.OnCollision += Current_OnCollision;
            var rotationMatrix = Matrix.CreateRotationZ(Transform.Rotation);
            var direction = GameConstants.Vector2.Up*0.02f;
            Vector2.Transform(ref direction, ref rotationMatrix, out this.direction);
        }

        private void Current_OnCollision(Collider other, Collider.HitInfo hitInfo) {
            if (other.GameObject.TryGetComponent<ShrimpController>(out var shrimp)) {
                shrimp.velocity += direction;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            var position = Transform.GlobalPosition;
            var numWaves = MathF.Max(Width/2, 3);
            var increment = MathHelper.TwoPi / numWaves;
            var size = Width / 2;
            for (int i = 0; i < Length; i++) {
                for (int j = 0; j < numWaves; j++) {
                    Camera.MainCamera.Draw(
                        spriteBatch,
                        bubble.Texture,
                        position + new Vector2(MathF.Sin(i+ShrimpfulGame.Time+j*increment)*size, (i + ShrimpfulGame.Time*7)%Length),
                        null,
                        Color.White,
                        0f,
                        bubble.Offset,
                        bubble.Scale,
                        SpriteEffects.None,
                        0f
                    );
                }
                
            }
            
        }
    }
}
