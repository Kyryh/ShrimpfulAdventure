using KEngine;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using ShrimpfulAdventure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Scenes {
    internal class Scene1 {
        public static void Load() {

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
                    new FatherController(),
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
                            new BabyController(),
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
                "Cube",
                components: [
                    new BoxCollider()
                ],
                position: new Vector2(1, -2)
            ).Load();


        }
    }
}
