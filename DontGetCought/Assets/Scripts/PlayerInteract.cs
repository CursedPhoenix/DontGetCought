using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    bool doorArea = false;
    public bool treasureFound = false;
    List<Collider> activeObjects = new List<Collider>();
    GameObject trackLoot;

    void Start() {
        trackLoot = GameObject.FindGameObjectWithTag("Looting");
        if (trackLoot != null) print("trackloot erstellt");
        if (trackLoot == null) print("trackloot konnte nicht erstellt werden");
    }

    void OnTriggerEnter(Collider col)
    {
        if (!activeObjects.Contains(col))
        {
            activeObjects.Add(col);
        }
        //print(activeObjects.Count);
        switch (col.tag)
        {
            case "Door":
                doorArea = true;
                break;
            case "Treasure":
                treasureFound = true;
                print("Diebesgut gefunden!");
                break;
            default:
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        activeObjects.Remove(col);
        print(activeObjects.Count);
        switch (col.tag)
        {
            case "Door":
                doorArea = false;
                break;
            case "Treasure":
                treasureFound = false;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (treasureFound == true) {
                var treasure = activeObjects.Find(coll => coll.tag == "Treasure");
                trackLoot.GetComponent<TrackLoot>().collectTreasure(treasure.gameObject);
                activeObjects.Remove(treasure);
                treasureFound = false;
            }
            else if (doorArea == true && treasureFound == false)
            {
                Debug.Log("Door triggered");
                ToggleDoor();
            }
            else if (doorArea == false)
            {
                print("not in Door Range");
            }
        }
    }

    void ToggleDoor()
    {
        var doors = activeObjects.FindAll(coll => coll.tag == "Door");
        foreach (var door in doors)
        {
            door.GetComponent<DoorBehavior>().ChangeDoorState();
        }
    }
}