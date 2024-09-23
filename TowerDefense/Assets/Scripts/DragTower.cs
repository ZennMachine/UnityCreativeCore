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
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Vector3 dropPos = eventData.pointerCurrentRaycast.worldPosition;
        GameObject hitObj;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, dropPos, out hit, 100.0f, 7))
        {
            hitObj = hit.collider.gameObject;
            Debug.Log(hitObj.name);
            if (hitObj.CompareTag("Buildable"))
            {
                PlaceTower(hitObj.GetComponent<Tile>());
            }
        }

        //transform.position = startPosition;
        //GameObject hitObj = eventData.pointerCurrentRaycast.gameObject;

        
    }

    public void PlaceTower(Tile tile)
    {
        Instantiate(towerObject, tile.center);
    }
}
