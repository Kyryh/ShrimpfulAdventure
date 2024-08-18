using KEngine.Components;
using KEngine.Components.DrawableComponents;
using KEngine.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpfulAdventure.Components {
    internal class TileMapRenderer : DrawableComponent {
        SpriteSheet spriteSheet;
        Tile[] tiles;
        public static TileMapRenderer FromMap(byte[,,] map, SpriteSheet tilesSheet) {
            List<Tile> tiles = new();
            var renderer = new TileMapRenderer();
            renderer.spriteSheet = tilesSheet;
            for (int i = -1; i < map.GetLength(1); i++) {
                for (int j = -1; j < map.GetLength(2); j++) {
                    byte topLeft = GetTile(map, j, i);
                    byte topRight = GetTile(map, j+1, i);
                    byte bottomLeft = GetTile(map, j, i+1);
                    byte bottomRight = GetTile(map, j+1, i + 1);
                    HashSet<byte> presentTiles = new([topLeft, topRight, bottomLeft, bottomRight]);
                    foreach (var tileType in presentTiles.Where(i => i != 0))
                    {
                        int index = (tileType-1)*16;
                        if (bottomRight == tileType)
                            index += 1;
                        if (bottomLeft == tileType)
                            index += 2;
                        if (topRight == tileType)
                            index += 4;
                        if (topLeft == tileType)
                            index += 8;
                        tiles.Add(new Tile(index, new Vector2(j, -i)));
                    }
                }
            }
            renderer.tiles = tiles.ToArray();
            return renderer;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            foreach (var tile in tiles) {
                Camera.MainCamera.Draw(spriteBatch, spriteSheet.Texture, tile.Position, spriteSheet.GetSourceRectangle(tile.Index), Color.White, 0f, Vector2.Zero, spriteSheet.Scale, SpriteEffects.None, 0f);
            }
        }

        static byte GetTile(byte[,,] map, int x, int y) {
            if (x < 0 || x >= map.GetLength(2) || y < 0 || y >= map.GetLength(1)) {
                return 0;
            }
            return map[2, y, x];
        }

        record Tile(int Index, Vector2 Position);
    }
}
