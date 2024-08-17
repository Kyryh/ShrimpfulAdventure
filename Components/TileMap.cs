using KEngine;
using KEngine.Components;
using KEngine.Components.DrawableComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class TileMap : DrawableComponent {
        byte[,,] map;
        static readonly string mapDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Maps");
        public string MapName { get; init; }
        Texture2D texture;

        public override void Initialize() {
            base.Initialize();
            LoadMap(MapName + ".txt");
            texture = KGame.GetContent<Texture2D>("white");
        }

        void LoadMap(string fileName) {
            var mapData = File.ReadAllLines(Path.Combine(mapDirectory, fileName));
            var mapWidth = int.Parse(mapData[0]);
            var mapHeight = int.Parse(mapData[1]);
            var mapLayers = int.Parse(mapData[2]);

            map = new byte[mapLayers, mapHeight, mapWidth];

            for (int i = 0; i < mapLayers; i++) {
                Console.WriteLine("new layer");
                for (int j = 0; j < mapHeight; j++)
                {
                    var row = mapData[3 + i * mapHeight + j];
                    for (int k = 0; k < mapWidth; k++) {
                        var tile = byte.Parse(row[(k*4)..(k*4+3)]);
                        map[i, j, k] = tile;
                    }
                }
            }
        }

        Color GetColor(byte index) {
            return index switch {
                0 => Color.Transparent,
                _ => Color.White
            };
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            for (int i = map.GetLength(0)-1; i >= 0; i--)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        Camera.MainCamera.Draw(spriteBatch, texture, new Vector2(k, -j), null, GetColor(map[i, j, k]), 0f, new Vector2(0.5f, 0.5f), Vector2.One, SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
