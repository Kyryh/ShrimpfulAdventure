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
        public static float Time { private set; get; }
        public ShrimpfulGame() {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            drawingLayers = [
                "Background",
                "Bubbles",
                "Default",
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
                new Sprite("Sprites/background1", false, offset: new Vector2(-.4875f, -.475f), scale: new Vector2(0.15f)),
                new Sprite("Sprites/background2", false, offset: new Vector2(-.4875f, -.475f), scale: new Vector2(0.15f)),
                new Sprite("Sprites/bubbles", true, scale: new(0.5f))
            );

            SetScenes(
                ("2-1", Level2_1.Load),
                ("1-1", Level1_1.Load),
                ("1-2", Level1_2.Load),
                ("1-3", Level1_3.Load)
            );
        }

        protected override void Update(GameTime gameTime) {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            Time = (float)gameTime.TotalGameTime.TotalSeconds;

            var mstate = Mouse.GetState();

            var pos = Camera.MainCamera.ScreenToWorld(new Vector2(mstate.X, mstate.Y));
            Console.WriteLine(new Vector2(MathF.Round(pos.X), MathF.Round(pos.Y)));

            Input.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
