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
                    new ShrimpController(),
                    new BoxCollider() {
                        Width = 7/8f,
                        Height = 7/8f,
                        //Offset = new Vector2(0,-0.25f)
                        Offset = new Vector2(0,-0.28125f)
                    }
                ]
            ).Load();

            new GameObject(
                "Platform",
                components: [
                    new BoxCollider() {
                        IsStatic = true
                    }
                ],
                position: new Vector2(0,-1),
                scale: new Vector2(10,1)
            ).Load();

            new GameObject(
                "Cube",
                components: [
                    new BoxCollider()
                ],
                position: new Vector2(1, 0)
            ).Load();


        }
    }
}
