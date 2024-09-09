using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SpriteGenTool
{
    public class UISpriteGenerator : MonoBehaviour
    {
        public List<GameObject> treesToProcess;
        public List<GameObject> flowersToProcess;
        public List<GameObject> mediumPlantablesToProcess;
        private Stack<GameObject> plantables = new Stack<GameObject>();
        public string plantablesFolderLocation;
        public string outputUIPath;
        [SerializeField]
        private Transform photoPos;
        private Camera cam;

        private float treeRenderCamPos = 9.02f;
        private float treeRenderSize = 13.12f;
        private float grassFlowerRenderCamPos = 0.7f;
        private float grassFlowerSize = 1.95f;
        private float mediumRenderCamPos = 2.62f;
        private float mediumRenderSize = 4.68f;



        public void ProcessByBounds()
        {
            GetPlantables(plantablesFolderLocation);
            GenerateSprites(true);
        }

        public void Process()
        {
            cam = Camera.main;
            // First render all the trees with camera at correct distance
            cam.transform.position = new Vector3(cam.transform.position.x, treeRenderCamPos, cam.transform.position.z);
            cam.orthographicSize = treeRenderSize;
            //GetPlantables(plantablesFolderLocation);
            for (int i = 0; i < treesToProcess.Count; i++)
            {
                plantables.Push(treesToProcess[i]);
            }
            GenerateSprites();

            // Second render all flowers and grasses at a closer distance
            cam.transform.position = new Vector3(cam.transform.position.x, grassFlowerRenderCamPos, cam.transform.position.z);
            cam.orthographicSize = grassFlowerSize;
            for (int i = 0; i < flowersToProcess.Count; i++)
            {
                plantables.Push(flowersToProcess[i]);
            }
            GenerateSprites();

            cam.transform.position = new Vector3(cam.transform.position.x, mediumRenderCamPos, cam.transform.position.z);
            cam.orthographicSize = mediumRenderSize;
            for (int i = 0; i < mediumPlantablesToProcess.Count; i++)
            {
                plantables.Push(mediumPlantablesToProcess[i]);
            }
            GenerateSprites();
        }

        private void GetPlantables(string location)
        {
            string[] _locations = { location };
            if (Directory.Exists(location))
            {
                string[] guids = AssetDatabase.FindAssets("t:Prefab", _locations);

                foreach (var guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    plantables.Push(go);
                }
            }
            else
            {
                Debug.Log(location + "Not Found!");
            }
        }

        public void GenerateSprites()
        {
            while (plantables.Count > 0)
            {
                GameObject current = plantables.Peek();

                Debug.Log(current);

                GameObject obj = Instantiate(current, photoPos);
                RenderImageFromCam(current);
                DestroyImmediate(obj);
                plantables.Pop();
            }
        }

        public void GenerateSprites(bool usesBounds)
        {
            while (plantables.Count > 0)
            {
                GameObject current = plantables.Peek();

                Debug.Log(current);

                Renderer currentMesh = current.GetComponentInChildren<Renderer>();
                Bounds currentBounds = currentMesh.bounds;

                cam.transform.position = currentBounds.center - new Vector3(0, 1.5f, 10);
                cam.orthographicSize = currentBounds.size.magnitude / 2.2f;

                GameObject obj = Instantiate(current, photoPos);
                RenderImageFromCam(current);
                DestroyImmediate(obj);
                plantables.Pop();
            }
        }

        private void RenderImageFromCam(GameObject currentObj)
        {
            var currentRT = RenderTexture.active;
            RenderTexture.active = cam.targetTexture;

            cam.Render();

            // Make a new texture and read the active render texture into it
            Texture2D _img = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
            _img.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
            _img.Apply();
            RenderTexture.active = currentRT;

            byte[] _bytes = _img.EncodeToPNG();
            DestroyImmediate(_img);

            File.WriteAllBytes(outputUIPath + "/" + currentObj.name + ".png", _bytes);
        }
    }
}
