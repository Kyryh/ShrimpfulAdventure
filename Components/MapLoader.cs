using KEngine;
using KEngine.Components;
using KEngine.Components.Colliders;
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

        static readonly string mapDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Maps");
        public static void LoadMap(string mapName, string tileSheet) {
            byte[,] map = LoadMap(mapName + ".txt");

            var texture = KGame.GetContent<Texture2D>("Sprites/white");

            List<Component> components = CalculateColliders(map);

            components.Add(TileMapRenderer.FromMap(map, (SpriteSheet)KGame.GetSprite(tileSheet)));

            new GameObject("Map", components:
                components.ToArray()
            ).Load();

            //for (int i = map.GetLength(0) - 1; i >= 0; i--) {
            //    for (int j = 0; j < map.GetLength(1); j++) {
            //        for (int k = 0; k < map.GetLength(2); k++) {
            //            //Camera.MainCamera.Draw(spriteBatch, texture, new Vector2(k, -j), null, GetColor(map[i, j, k]), 0f, new Vector2(0.5f, 0.5f), Vector2.One, SpriteEffects.None, 0f);
                        
            //        }
            //    }
            //}
        }

        static List<Component> CalculateColliders(byte[,] map) {
            var result = new List<Component>();
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    //Camera.MainCamera.Draw(spriteBatch, texture, new Vector2(k, -j), null, GetColor(map[i, j, k]), 0f, new Vector2(0.5f, 0.5f), Vector2.One, SpriteEffects.None, 0f);
                    if (map[i, j] != 0) {
                        result.Add(new BoxCollider() {
                            Offset = new Vector2(j, -i),
                            IsStatic = true
                        });
                    }
                }
            }
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
    }
}
