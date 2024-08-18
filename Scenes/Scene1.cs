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

        static float GetValue(string str) {
            var p = str.Split(" ")[^1];
            return float.Parse(p);
        }
        public static void Load() {


            string directory = Path.Combine(Directory.GetCurrentDirectory(), "Content");
            var data = File.ReadAllLines(Path.Combine(directory, "valori.txt"));

            new GameObject(
                "Map",
                components: [
                    new TileMap() {
                        MapName = "map1"
                    }
                ]
            ).Load();

            new GameObject(
                "Shrimp",
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/ShrimpTogether"
                    },
                    new FatherController() {
                        //Acceleration = 0.1f,
                        //MaxSpeed = 0.05f,
                        //JumpForce = 0.1f,
                        //Gravity = 0.25f,
                        //HorizontalVelocityInfluenceOnJump = 1f,
                        //CoyoteTimeSeconds = 0.2f
                        GroundAcceleration = GetValue(data[1]),
                        AirAcceleration = GetValue(data[2]),
                        GroundDeceleration = GetValue(data[3]),
                        AirDeceleration = GetValue(data[4]),
                        MaxSpeed = GetValue(data[5]),
                        JumpForce = GetValue(data[6]),
                        Gravity = GetValue(data[7]),
                        HorizontalVelocityInfluenceOnJump = GetValue(data[8]),
                        CoyoteTimeSeconds = GetValue(data[9])
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
                                //Acceleration = 0.1f,
                                //MaxSpeed = 0.05f,
                                //JumpForce = 0.1f,
                                //Gravity = 0.25f,
                                //HorizontalVelocityInfluenceOnJump = 1f,
                                //CoyoteTimeSeconds = 0.2f
                                GroundAcceleration = GetValue(data[12]),
                                AirAcceleration = GetValue(data[13]),
                                GroundDeceleration = GetValue(data[14]),
                                AirDeceleration = GetValue(data[15]),
                                MaxSpeed = GetValue(data[16]),
                                JumpForce = GetValue(data[17]),
                                Gravity = GetValue(data[18]),
                                HorizontalVelocityInfluenceOnJump = GetValue(data[19]),
                                CoyoteTimeSeconds = GetValue(data[20])
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
