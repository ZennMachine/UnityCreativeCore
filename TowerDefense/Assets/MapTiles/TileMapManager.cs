using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GridTileMap
{
    public class TileMapManager : MonoBehaviour
    {
        TileGridGenerator tg; 
        public GameObject gridSquare;
        private GameObject gridParent;
        [SerializeField]
        public int gridMapLength; // Size of the map in grid squres
        [Range(0.0f, 50.0f)]
        public float heightStrength;

        public int seed;

        // Width and height of the texture in pixels.
        public int pixWidth;
        public int pixHeight;

        public Mesh baseMesh;

        // The origin of the sampled area in the plane.
        private float xOrg;
        private float yOrg;

        // The number of cycles of the basic noise pattern that are repeated
        // over the width and height of the texture.
        public float scale = 1.0F;

        public Texture2D noiseTex;
        private Color[] pix;
        private Renderer rend;

        void Awake()
        {
            // Set up the texture and a Color array to hold pixels during processing.
            noiseTex = new Texture2D(pixWidth, pixHeight);
            pix = new Color[noiseTex.width * noiseTex.height];

            if (!tg)
            {
                tg = gameObject.AddComponent<TileGridGenerator>();
            }
            if(!gridParent)
            {
                gridParent = Instantiate(new GameObject("GridParent"), transform);
            }
        }

        // Testing GUI Button
        public void BuildTerrain()
        {
            foreach (Transform child in gridParent.transform)
            {
                Destroy(child.gameObject);
            }

            tg.gridTiles.Clear();

            CalcNoise();
            int gridSize = 10;

            tg.GenerateTileGrid(gridMapLength, gridSize, gridSquare, gridParent, noiseTex, heightStrength);

            GameObject.Find("Player").GetComponent<GridMovement>().SetPlayerPos(tg.gridTiles[1].GetComponent<Tile>());
        }

        public void BuildTerrainMeshBase()
        {
            foreach(Transform child in gridParent.transform)
            { Destroy(child.gameObject); }
            tg.gridTiles.Clear();

            CalcNoise();
            int gridSize = 1;
            tg.GenerateTileGrid(baseMesh, gridSize, gridSquare, gridParent);
        }

        void CalcNoise()
        {
            // For each pixel in the texture...
            float y = 0.0F;
            noiseTex = new Texture2D(pixWidth, pixHeight);

            if(seed != 0)
            {
                UnityEngine.Random.InitState(seed);
            }
                
            xOrg = UnityEngine.Random.Range(1.0f, 200.0f);
            yOrg = UnityEngine.Random.Range(1.0f, 200.0f);

            while (y < noiseTex.height)
            {
                float x = 0.0F;
                while (x < noiseTex.width)
                {
                    float xCoord = xOrg + x / noiseTex.width * scale;
                    float yCoord = yOrg + y / noiseTex.height * scale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    //pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                    noiseTex.SetPixel((int)x, (int)y, new Color(sample, sample, sample));
                    x++;
                }
                y++;
            }

            // Copy the pixel data to the texture and load it into the GPU.;
            noiseTex.Apply();
        }

    }
}