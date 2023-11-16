using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject player;
    [SerializeField]
    PlayerController playerControllerCs;

    [SerializeField]
    GameObject[] allRooms;

    List<GameObject> availableDoors = new List<GameObject>();
    int numAvailableDoors;
    int nearestDoor;
   


    Transform roomTransform;
    int currentRoomNum;
    int goalRoomNum;
    
    void Start()
    {

        CountDoors();
    }

    void Update()
    {
        for(int i = 0; i < numAvailableDoors; i++)
        {
            //Distance only in the xz plane
            var vectorToTarget = availableDoors[i].transform.position - player.transform.position;
            vectorToTarget.y = 0;
            var distanceToTarget = vectorToTarget.magnitude;

            //If the player is near a door it detects it
            if (distanceToTarget < 1)
            {
                nearestDoor = i;
                //Debug.Log("Character near a door");
                break;
            }
            else
            {
                nearestDoor = -1;
            }
        }

     
        if(nearestDoor != -1)
        {
            //Check for mouse click 
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(nearestDoor);
                RaycastHit raycastHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, 100f))
                {
                    if (raycastHit.transform != null)
                    {
                        
                        CurrentClickedGameObject(raycastHit.transform.gameObject);
                    }
                }
            }
        }
        


    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        //Debug.Log(nearestDoor);
        //Debug.Log(gameObject.tag);
        if (gameObject == availableDoors[nearestDoor])
        {
            
            //Distance only in the xz plane
            var vectorToTarget = allRooms[currentRoomNum].transform.position - availableDoors[nearestDoor].transform.position;
            vectorToTarget.y = 0;
            vectorToTarget.Normalize();
            //Debug.Log(vectorToTarget);
            changeRoom(vectorToTarget);
        }
    }

    void changeRoom(Vector3 distanceToCenter)
    {
        
        if(distanceToCenter.x > 0.7)
        {
            //Left door
            currentRoomNum--;
            currentRoomNum--;
            mainCamera.transform.position += new Vector3(-10, 0, 0);
            Vector3 playerPos = player.transform.position;
            float newPos = allRooms[currentRoomNum].transform.position.x + 2;
            player.transform.position = new Vector3(newPos, playerPos.y, playerPos.z);
        }
        else if(distanceToCenter.x < -0.7)
        {
            //Right door
           currentRoomNum++;
            currentRoomNum++;
            mainCamera.transform.position += new Vector3(10, 0, 0);
            Vector3 playerPos = player.transform.position;
            float newPos = allRooms[currentRoomNum].transform.position.x - 2;
            player.transform.position = new Vector3(newPos, playerPos.y, playerPos.z);

        }
        else if(distanceToCenter.z > 0.7)
        {
            //Front door
            mainCamera.transform.position += new Vector3(0, -7, 0);
            player.transform.position += new Vector3(0, -7, 0);
            currentRoomNum += 5;
            currentRoomNum += 5;
        }
        else if (distanceToCenter.z < 0.7)
        {
            //Back door
            mainCamera.transform.position += new Vector3(0, 7, 0);
            player.transform.position += new Vector3(0, 7, 0);
            currentRoomNum -= 5;
            currentRoomNum -= 5;
        }

        CountDoors();
    }


    void CountDoors()
    {
        availableDoors.Clear();
        //Debug.Log(availableDoors.Count);
        foreach (Transform child in allRooms[currentRoomNum].transform)
        {
            if (child.tag == "normalDoor")
                availableDoors.Add(child.gameObject);
            //Debug.Log(child.tag);
            //Debug.Log(availableDoors.Count);
        }
        numAvailableDoors = availableDoors.Count;
    }

}
