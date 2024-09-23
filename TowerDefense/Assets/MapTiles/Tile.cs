using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GridTileMap
{
    public class Tile : MonoBehaviour
    {
        public Transform center;
        private int tileSize = 10;
        public bool isBuildableTerrain;
        public bool isOccupied;
        public int GetSize()
        {
            int size = (int)Math.Round(GetComponent<Collider>().bounds.size.x);
            Debug.Log(size);
            tileSize = size;
            return size;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(tileSize, 0.5f, tileSize));
        }
    }
}
