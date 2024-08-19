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
                "Background",
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
                new SpriteSheet("Sprites/ShrimpTogether", new Vector2(16,25), false, scale: new Vector2(1/8f), offset: new Vector2(0, 11/64f)),
                new SpriteSheet("Sprites/ShrimpBaby", new Vector2(8,16), false, scale: new Vector2(1/8f), offset: new Vector2(0, 7 / 32f)),
                new SpriteSheet("Sprites/tilesets1", new Vector2(8)),
                new Sprite("Sprites/rock", scale: new Vector2(2)),
                new SpriteSheet("Sprites/trapdoor", 3, 1, false, scale: new Vector2(0.125f), offset: new Vector2(0.5f,1)),
                new SpriteSheet("Sprites/lever", 3, 1, offset: new Vector2(0.5f)),
                new SpriteSheet("Sprites/button", 2, 1, false, new Vector2(0.5f,0.5f), new Vector2(0.125f)),
                new Sprite("Sprites/background1", false, offset: new Vector2(-.4875f, -.5f), scale: new Vector2(0.125f))
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
