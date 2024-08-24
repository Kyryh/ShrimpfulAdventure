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
            byte[,] map = LoadMap(mapName + ".bin");

            List<Component> components = CalculateColliders(map);

            components.Add(TileMapRenderer.FromMap(map, (SpriteSheet)KGame.GetSprite(tileSheet)));

            new GameObject("Map", components:
                components.ToArray()
            ).Load();

        }

        static List<Component> CalculateColliders(byte[,] map) {
            var result = new List<Component>();

            for (int i = 0; i < map.GetLength(0); i++) {
                int startPos = -1;
                int length = 0;
                for (int j = 0; j < map.GetLength(1); j++) {
                    if (map[i, j] != 0) {
                        length++;
                    } else {
                        if (length > 0)
                            result.Add(new BoxCollider() {
                                Width = length,
                                Offset = new Vector2(startPos + 0.5f + length / 2f, -i),
                                IsStatic = true
                            });
                        length = 0;
                        startPos = j;
                    }
                }
                if (length > 0)
                    result.Add(new BoxCollider() {
                        Width = length,
                        Offset = new Vector2(startPos + 0.5f + length / 2f, -i),
                        IsStatic = true
                    });
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
            using FileStream mapData = File.OpenRead(Path.Combine(mapDirectory, fileName));
            var mapWidth = mapData.ReadByte();
            var mapHeight = mapData.ReadByte();

            var map = new byte[mapHeight, mapWidth];

            for (int j = 0; j < mapHeight; j++) {
                for (int k = 0; k < mapWidth; k++) {
                    //var tile = byte.Parse(row[(k * 4)..(k * 4 + 3)]);
                    //map[j, k] = tile;
                    map[j, k] = (byte)mapData.ReadByte();
                }
            }
            return map;
        }

        public static void SpawnShrimps(Vector2 position) {
            new GameObject(
                "Shrimp",
                position: position,
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/ShrimpTogether"
                    },
                    new AnimationController() {
                        StartingAnimation = "Idle",
                        Animations = [
                            new Animation("Idle", 
                                new Animation.Frame(2, TimeSpan.Zero)
                            ),
                            new Animation("Childless",
                                new Animation.Frame(0, TimeSpan.Zero)
                            ),
                            new Animation("Jump", "Idle",
                                //new Animation.Frame(3, TimeSpan.FromSeconds(0.05f)),
                                //new Animation.Frame(4, TimeSpan.FromSeconds(0.05f)),
                                //new Animation.Frame(5, TimeSpan.FromSeconds(0.05f)),
                                //new Animation.Frame(6, TimeSpan.FromSeconds(0.05f)),
                                //new Animation.Frame(7, TimeSpan.FromSeconds(0.05f)),
                                new Animation.Frame(8, TimeSpan.FromSeconds(0.1f)),
                                new Animation.Frame(9, TimeSpan.FromSeconds(0.1f)),
                                new Animation.Frame(11, TimeSpan.FromSeconds(0.1f)),
                                new Animation.Frame(12, TimeSpan.FromSeconds(0.1f)),
                                new Animation.Frame(13, TimeSpan.FromSeconds(0.1f)),
                                new Animation.Frame(14, TimeSpan.FromSeconds(0.1f))
                            )
                        ]
                    },
                    new FatherController() {
                        GroundAcceleration = 0.2f,
                        AirAcceleration = 0.2f,
                        GroundDeceleration = 0.4f,
                        AirDeceleration = 0.4f,
                        MaxSpeed = 0.1f,
                        JumpForce = 0.32f,
                        Gravity = 0.9f,
                        CoyoteTimeSeconds = 0.2f,
                        PushingForce = 0.06f
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
                ]
            ).Load();
            new GameObject(
                "Baby",
                components: [
                    new SpriteRenderer() {
                        spriteName = "Sprites/ShrimpBaby"
                    },

                    new AnimationController() {
                        StartingAnimation = "Idle",
                        Animations = [
                            new Animation("Idle",
                                new Animation.Frame(0, TimeSpan.Zero)
                            )
                        ]
                    },
                    new BabyController() {
                        GroundAcceleration = 0.2f,
                        AirAcceleration = 0.2f,
                        GroundDeceleration = 0.4f,
                        AirDeceleration = 0.4f,
                        MaxSpeed = 0.15f,
                        JumpForce = 0.18f,
                        Gravity = 0.4f,
                        CoyoteTimeSeconds = 0.2f,
                        PushingForce = -0.005f,
                        LittleJumpForce = 0.03f
                    },
                    new BoxCollider() {
                        Width = 7/8f,
                        Height = 7/8f,
                        Layer = "ShrimpBaby"
                    }
                ],
                active: false
            ).Load();
        }

        public static void SpawnRock(Vector2 position) {

            new GameObject(
                "Rock",
                components: [
                    new BoxCollider() {
                        Width = 1.9f,
                        Height = 1.9f
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
                if (other.GameObject.Name == "Shrimp" || other.GameObject.Name == "Baby") {
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
