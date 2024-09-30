using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GridTileMap;

public class DragTower : MonoBehaviour, IDragHandler, IDropHandler
{
    public GameObject towerObject;
    public Vector3 startPosition;
    private TowerDefenseManager tdm;
    private int thisTowerCost;

    private void Awake()
    {
        tdm = GameObject.Find("GameManager").GetComponent<TowerDefenseManager>();
        thisTowerCost = towerObject.GetComponent<Tower>().towerCost;
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
        if (tdm.coins >= thisTowerCost)
        {
            tdm.RemoveCoins(thisTowerCost);

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
        else
        {
            Debug.Log("YOURE BROKE");
        }
    }

    public void PlaceTower(Tile tile)
    {
        if ((tile.isOccupied))
        {
            Debug.Log("Cant build here");
            return;
        }
        Debug.Log("Tower Built");
        Instantiate(towerObject, tile.center);
        tile.isOccupied = true;
    }
}
