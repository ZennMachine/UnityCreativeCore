using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridTileMap;
public class GridMovement : MonoBehaviour
{
    enum directions
    {
        north,
        east,
        south,
        west
    }

    Tile currentTile;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0)
            CheckForTileMove(directions.east);
        if (horizontal < 0)
            CheckForTileMove(directions.west);
        if (vertical > 0)
            CheckForTileMove(directions.north);
        if (vertical < 0)
            CheckForTileMove(directions.south);
    }

    public void SetPlayerPos(Tile startTile)
    {
        currentTile = startTile;
        transform.position = currentTile.GetComponent<Tile>().center.position;
    }

    void CheckForTileMove(directions dir)
    {
        switch(dir)
        {
            case directions.north:
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                }
                break;
            case directions.east:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                }
                break;
            case directions.south:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                }
                break;
            case directions.west:
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                }
                break;
        }
    }
}
