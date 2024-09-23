using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GridTileMap
{
    public class TileGridGenerator : MonoBehaviour
    {
        public List<GameObject> gridTiles;

        private void Awake()
        {
            gridTiles = new List<GameObject>();
        }
        public void GenerateTileGrid(int mapLength, int tileSize, GameObject gridSquare, GameObject gridParent)
        {

            for (int z = 0; z < mapLength * tileSize; z += tileSize)
            {
                for (int x = 0; x < mapLength * tileSize; x += tileSize)
                {
                    gridTiles.Add(Instantiate(gridSquare, new Vector3(x, 0, z), Quaternion.identity, gridParent.transform));
                }
            }
        }

        // Generates a grid of tiles while sampling a texture to decide what height the tile is placed at.
        // This works well if you have a terrain map you would like the tiles to sit above.
        public void GenerateTileGrid(int mapLength, int tileSize, GameObject gridSquare, GameObject gridParent, Texture2D heightmap, float hieghtStrength)
        {
            Vector2 gridCenter = new Vector2
            (mapLength / 2, mapLength / 2);
            Vector2 gridSize = new Vector2(mapLength, mapLength);
            Rect rect = new Rect(gridCenter, gridSize);

            for (int z = 0; z < mapLength * tileSize; z += tileSize)
            {
                for (int x = 0; x < mapLength * tileSize; x += tileSize)
                {
                    float y = Mathf.Ceil(heightmap.GetPixel(x, z).r * hieghtStrength);
                    gridTiles.Add(Instantiate(gridSquare, new Vector3(x, y, z), Quaternion.identity, gridParent.transform));
                }
            }
        }

        public void GenerateTileGrid(Mesh refMesh, int tileSize, GameObject gridSquare, GameObject gridParent)
        {
            for (int z = 0; z < refMesh.bounds.size.z * tileSize; z += tileSize)
            {
                for (int x = 0; x < refMesh.bounds.size.x * tileSize; x += tileSize)
                {
                    gridTiles.Add(Instantiate(gridSquare, new Vector3(x, 0.0f, z), Quaternion.identity, gridParent.transform));
                }
            }
        }
    }
}
