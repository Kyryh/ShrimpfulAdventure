using KEngine;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Scene1 {
        public static void Load() {

            TileMapLoader.Load("Map1", "Sprites/tilesets1");

            new GameObject(
                "Shrimp",
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/ShrimpTogether"
                    },
                    new FatherController() {
                        GroundAcceleration = 0.1f,
                        AirAcceleration = 0.1f,
                        GroundDeceleration = 0.1f,
                        AirDeceleration = 0.1f,
                        MaxSpeed = 0.05f,
                        JumpForce = 0.1f,
                        Gravity = 0.25f,
                        HorizontalVelocityInfluenceOnJump = 1f,
                        CoyoteTimeSeconds = 0.2f
                    },
                    new BoxCollider() {
                        Width = 7/8f,
                        Height = 7/8f,
                        Layer = "ShrimpFather"
                    },
                    new BoxCollider() {
                        IsTrigger = true,
                        Layer = "BabyFatherInteraction"
                    }
                ],
                children: [
                    new GameObject(
                        "Baby",
                        components: [
                            new SpriteRenderer() {
                                spriteName = "Sprites/ShrimpBaby"
                            },
                            new BabyController() {
                                GroundAcceleration = 0.1f,
                                AirAcceleration = 0.1f,
                                GroundDeceleration = 0.1f,
                                AirDeceleration = 0.1f,
                                MaxSpeed = 0.05f,
                                JumpForce = 0.1f,
                                Gravity = 0.25f,
                                HorizontalVelocityInfluenceOnJump = 1f,
                                CoyoteTimeSeconds = 0.2f,
                                PushingForce = -.005f
                            },
                            new BoxCollider() {
                                Width = 7/16f,
                                Height = 7/16f,
                                Layer = "ShrimpBaby"
                            }
                        ],
                        active: false
                    )
                ]
            ).Load();

            new GameObject(
                "Platform",
                components: [
                    new BoxCollider() {
                        IsStatic = true
                    }
                ],
                position: new Vector2(0,-3),
                scale: new Vector2(10,1)
            ).Load();

            new GameObject(
                "Platform",
                components: [
                    new BoxCollider() {
                        IsStatic = true
                    }
                ],
                position: new Vector2(5, -.5f),
                scale: new Vector2(5, 1)
            ).Load();

            new GameObject(
                "Cube",
                components: [
                    new BoxCollider()
                ],
                position: new Vector2(1, -2)
            ).Load();


        }
    }
}
