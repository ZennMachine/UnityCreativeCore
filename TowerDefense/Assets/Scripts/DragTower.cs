using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GridTileMap;
using TMPro;
using UnityEngine.UI;

public class DragTower : MonoBehaviour, IDragHandler, IDropHandler
{
    public GameObject towerObject;
    private Vector3 startPosition;
    private TowerDefenseManager tdm;
    private int myTowerCost;
    [SerializeField]
    private TextMeshProUGUI priceText;
    private Sprite mySprite;
    [SerializeField]
    private Image myImage;
    private void Awake()
    {
        
        tdm = GameObject.Find("GameManager").GetComponent<TowerDefenseManager>();
        Tower myTower = towerObject.GetComponent<Tower>();
        myTowerCost = myTower.towerCost;
        startPosition = transform.position;
        priceText.text = myTowerCost.ToString();
        mySprite = myTower.towerSprite;
        myImage.sprite = mySprite;
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
        if (tdm.coins >= myTowerCost)
        {
            tdm.RemoveCoins(myTowerCost);

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
        transform.position = startPosition;
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
