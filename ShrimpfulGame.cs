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
        public static Random Random { private set; get; }
        public ShrimpfulGame() {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            drawingLayers = [
                "Background",
                "Bubbles",
                "Default",
            ];
            Random = new Random();
            //debugDrawGameObjectsPosition = true;
            //debugDrawColliders = true;
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
                new Sprite("Sprites/background3", false, offset: new Vector2(-.4875f, -.475f), scale: new Vector2(0.15f)),
                new Sprite("Sprites/background4", false, offset: new Vector2(-.4875f, -.475f), scale: new Vector2(0.15f)),
                new Sprite("Sprites/castle", false, scale: new Vector2(0.125f)),
                new Sprite("Sprites/background2-3", false, scale: new Vector2(0.125f)),
                new Sprite("Sprites/bubbles", true, scale: new(0.5f)),
                new SpriteSheet("Sprites/treasure", 2,1, false, scale: new(0.125f), offset: new Vector2(0.5f, 0.6f)),
                new Sprite("Sprites/intro", false, scale: new(1/33f)),
                new Sprite("Sprites/outro", false, scale: new(1/33f))
            );

            SetScenes(
                ("Intro", Intro.Load),
                ("1-1", Level1_1.Load),
                ("1-2", Level1_2.Load),
                ("1-3", Level1_3.Load),
                ("2-1", Level2_1.Load),
                ("2-2", Level2_2.Load),
                ("2-3", Level2_3.Load),
                ("3-1", Level3_1.Load),
                ("3-2", Level3_2.Load),
                ("3-3", Level3_3.Load),
                ("End", Outro.Load)
            );
        }

        protected override void Update(GameTime gameTime) {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            Time = (float)gameTime.TotalGameTime.TotalSeconds;

            
            Input.Update();

            if (Input.RestartPressed())
                KGame.Instance.ReloadScene();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
