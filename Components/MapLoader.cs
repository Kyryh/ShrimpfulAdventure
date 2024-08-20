using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
using KEngine.Components.DrawableComponents;
using KEngine.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal static class MapLoader {


        static float GetValue(string str) {
            var p = str.Split(" ")[^1];
            return float.Parse(p);
        }

        static readonly string mapDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Maps");
        public static void LoadMap(string mapName, string tileSheet) {
            byte[,] map = LoadMap(mapName + ".txt");

            List<Component> components = CalculateColliders(map);

            components.Add(TileMapRenderer.FromMap(map, (SpriteSheet)KGame.GetSprite(tileSheet)));

            new GameObject("Map", components:
                components.ToArray()
            ).Load();

        }

        static List<Component> CalculateColliders(byte[,] map) {
            var result = new List<Component>();
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    if (map[i, j] != 0) {
                        result.Add(new BoxCollider() {
                            Offset = new Vector2(j, -i),
                            IsStatic = true
                        });
                    }
                }
            }
            result.AddRange([
                new BoxCollider() {
                    Height = 26,
                    IsStatic = true,
                    Offset = new Vector2(-1,-12.5f)
                },
                new BoxCollider() {
                    Height = 26,
                    IsStatic = true,
                    Offset = new Vector2(42,-12.5f)
                },
                new BoxCollider() {
                    Width = 42,
                    IsStatic = true,
                    Offset = new Vector2(20.5f,1)
                },
            ]);
            return result;
        }

        static byte[,] LoadMap(string fileName) {
            var mapData = File.ReadAllLines(Path.Combine(mapDirectory, fileName));
            var mapWidth = int.Parse(mapData[0]);
            var mapHeight = int.Parse(mapData[1]);

            var map = new byte[mapHeight, mapWidth];

            for (int j = 0; j < mapHeight; j++) {
                var row = mapData[2 + j];
                for (int k = 0; k < mapWidth; k++) {
                    var tile = byte.Parse(row[(k * 4)..(k * 4 + 3)]);
                    map[j, k] = tile;
                }
            }
            return map;
        }

        public static void SpawnShrimps(Vector2 position) {

            string directory = Path.Combine(Directory.GetCurrentDirectory(), "Content");
            var data = File.ReadAllLines(Path.Combine(directory, "valori.txt"));

            new GameObject(
                "Shrimp",
                position: position,
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/ShrimpTogether"
                    },
                    new FatherController() {
                        //GroundAcceleration = 0.1f,
                        //AirAcceleration = 0.1f,
                        //GroundDeceleration = 0.1f,
                        //AirDeceleration = 0.1f,
                        //MaxSpeed = 0.05f,
                        //JumpForce = 0.1f,
                        //Gravity = 0.25f,
                        //HorizontalVelocityInfluenceOnJump = 1f,
                        //CoyoteTimeSeconds = 0.2f,
                        //PushingForce = 0.02f
                        GroundAcceleration = GetValue(data[1]),
                        AirAcceleration = GetValue(data[2]),
                        GroundDeceleration = GetValue(data[3]),
                        AirDeceleration = GetValue(data[4]),
                        MaxSpeed = GetValue(data[5]),
                        JumpForce = GetValue(data[6]),
                        Gravity = GetValue(data[7]),
                        CoyoteTimeSeconds = GetValue(data[9]),
                        PushingForce =GetValue(data[10])
                    },
                    new BoxCollider() {
                        Width = 7/4f,
                        Height = 7/4f,
                        Layer = "ShrimpFather"
                    },
                    new BoxCollider() {
                        IsTrigger = true,
                        Width = 2f,
                        Height = 2f,
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
                                //GroundAcceleration = 0.1f,
                                //AirAcceleration = 0.1f,
                                //GroundDeceleration = 0.1f,
                                //AirDeceleration = 0.1f,
                                //MaxSpeed = 0.05f,
                                //JumpForce = 0.1f,
                                //Gravity = 0.25f,
                                //HorizontalVelocityInfluenceOnJump = 1f,
                                //CoyoteTimeSeconds = 0.2f,
                                //PushingForce = -.005f
                                GroundAcceleration = GetValue(data[13]),
                                AirAcceleration = GetValue(data[14]),
                                GroundDeceleration = GetValue(data[15]),
                                AirDeceleration = GetValue(data[16]),
                                MaxSpeed = GetValue(data[17]),
                                JumpForce = GetValue(data[18]),
                                Gravity = GetValue(data[19]),
                                CoyoteTimeSeconds = GetValue(data[21]),
                                PushingForce =GetValue(data[22])
                            },
                            new BoxCollider() {
                                Width = 7/8f,
                                Height = 7/8f,
                                Layer = "ShrimpBaby"
                            }
                        ],
                        active: false
                    )
                ]
            ).Load();
        }

        public static void SpawnRock(Vector2 position) {

            new GameObject(
                "Rock",
                components: [
                    new BoxCollider() {
                        Width = 2f,
                        Height = 2f
                    },
                    new SpriteRenderer() {
                        spriteName = "Sprites/rock"
                    },
                    new Rock()
                ],
                position: position
            ).Load();
        }

        
        public static Trapdoor SpawnTrapdoor(Vector2 position, float rotation, bool isLong, bool open = false) {
            Trapdoor trapdoor = new Trapdoor() {
                IsLong = isLong,
                InitiallyOpen = open
            };
            new GameObject(
                "Trapdoor",
                position: position,
                rotation: rotation,
                components: [
                    new BoxCollider() {
                        Width = .5f,
                        Height = isLong ? 4f : 2f,
                        Offset = new Vector2(0, isLong ? 2 : 1),
                        IsStatic = true
                    },
                    new SpriteRenderer() {
                        spriteName = "Sprites/trapdoor",
                        spriteIndex = isLong ? 1 : 0
                    },
                    trapdoor
                ]
            ).Load();
            return trapdoor;
        }
        public static void SpawnLever(Vector2 position, Action leverOn, Action leverOff) {
            var ac = new AnimationController() {
                StartingAnimation = "unpulled",
                Animations = [
                    new Animation("unpulled",
                        new Animation.Frame(1, TimeSpan.Zero)
                    ),
                    new Animation("pull", "pulled",
                        new Animation.Frame(0, TimeSpan.FromSeconds(0.2))
                    ),
                    new Animation("pulled",
                        new Animation.Frame(2, TimeSpan.Zero)
                    ),
                    new Animation("unpull", "unpulled",
                        new Animation.Frame(0, TimeSpan.FromSeconds(0.2))
                    )
                ]
            };
            new GameObject(
                "Lever",
                position,
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/lever"
                    },
                    new BoxCollider() {
                        IsTrigger = true,
                        Width = 1.5f
                    },
                    ac,
                    new Interactable() {
                        OnInteract = (state) => {
                            if (state == 0) {
                                leverOn();
                                ac.SetAnimation("pull");
                                return 1;
                            } else {
                                leverOff();
                                ac.SetAnimation("unpull");
                                return 0;
                            }
                        }
                    }
                ]
            ).Load();
        }

        public static void SpawnButton(Vector2 position, Action onPress, Action onDepress) {
            new GameObject(
                "Button",
                position,
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/button"
                    },
                    new BoxCollider() {
                        Width=2f,
                        Height=0.5f,
                        IsTrigger = true
                    },
                    new Button() {
                        OnPress = onPress,
                        OnDepress = onDepress
                    }
                ]
            ).Load();
        }

        public static void SpawnCurrent(Vector2 position, int length, int width, float rotation) {
            new GameObject(
                "Current",
                position,
                rotation,
                components: [
                    new BoxCollider() {
                        Width = width,
                        Height = length,
                        Offset = new Vector2(0, length/2f),
                        IsTrigger = true
                    },
                    new Current() {
                        drawingLayer = "Bubbles",
                        Length = length,
                        Width = width
                    }
                ]
            ).Load();
        }

        public static void CreateBackground(string backgroundName) {
            new GameObject(
                "Background",
                components: [
                    new SpriteRenderer() {
                        drawingLayer = "Background",
                        spriteName = backgroundName
                    }
                ]
            ).Load();
        }

        public static void AddEndTrigger(string nextLevel, Vector2 position, Vector2 dimension) {
            var collider = new BoxCollider() {
                Width = dimension.X,
                Height = dimension.Y,
                IsTrigger = true
            };
            collider.OnCollision += (other, _) => {
                if (other.GameObject.Name == "Shrimp") {
                    KGame.Instance.LoadScene(nextLevel);
                }
            };
            new GameObject(
                "EndTrigger",
                position,
                components: [
                    collider
                ]
            ).Load();
        }

    }
}
