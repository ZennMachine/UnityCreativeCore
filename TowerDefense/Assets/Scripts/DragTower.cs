using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GridTileMap;

public class DragTower : MonoBehaviour, IDragHandler, IDropHandler
{
    public GameObject towerObject;
    public Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.position = startPosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Tiles");
        if (Physics.Raycast(ray, out hit, 1000.0f, mask))
        {

            if (hit.collider.CompareTag("Buildable"))
            {
                PlaceTower(hit.collider.GetComponent<Tile>());
            }
        }

    }

    public void PlaceTower(Tile tile)
    {
        Debug.Log("Tower Built");
        Instantiate(towerObject, tile.center);
        tile.isOccupied = true;
    }
}
