using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using KEngine.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using ShrimpfulAdventure.Components;
using ShrimpfulAdventure.Scenes;

namespace ShrimpfulAdventure {
    public class ShrimpfulGame : KGame {
        
        public ShrimpfulGame() {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            drawingLayers = [
                "Default"
            ];
            debugDrawGameObjectsPosition = true;
            debugDrawColliders = true;
        }

        public static void Main() {
            new ShrimpfulGame().Run();
        }

        //protected override void Initialize() {
        //    base.Initialize();
        //}

        protected override void LoadContent() {
            base.LoadContent();

            SetDrawingLayersSettings(new(SamplerState: SamplerState.PointClamp), new(){ });

            Collider.AddToCollisionMatrix(
                ("Default", "ShrimpFather"),
                ("Default", "ShrimpBaby"),
                ("BabyFatherInteraction", "ShrimpBaby")
            );

            InitSprites(
                new SpriteSheet("Sprites/ShrimpTogether", new Vector2(16,25), false, scale: new Vector2(1/16f), offset: new Vector2(0, 11/64f)),
                new SpriteSheet("Sprites/ShrimpBaby", new Vector2(8,16), false, scale: new Vector2(1/16f), offset: new Vector2(0, 7 / 32f)),
                new SpriteSheet("Sprites/tilesets1", new Vector2(16))
            );

            SetScenes(
                ("Scene1", Scene1.Load)
            );
        }

        protected override void Update(GameTime gameTime) {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            Input.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
