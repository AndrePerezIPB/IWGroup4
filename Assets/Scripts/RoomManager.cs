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

    [SerializeField]
    GameObject[] redDoors;

    [SerializeField]
    GameObject[] blueDoors;

    List<GameObject> availableDoors = new List<GameObject>();
    int numAvailableDoors;
    int nearestDoor;
   
    public int currentRoomNum = 5;

    private void Awake()
    {
        DoorVisibility();
    }
    void Start()
    {
        //CountDoors();
        
    }

    void Update()
    {
        //This shouldn't be necessary, it's here to check that it works using the inspector
        DoorVisibility();

        //Detect if the character is near a door
        for (int i = 0; i < numAvailableDoors; i++)
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
            mainCamera.transform.position += new Vector3(-10, 0, 0);
            Vector3 playerPos = player.transform.position;
            float newPos = allRooms[currentRoomNum].transform.position.x + 2;
            player.transform.position = new Vector3(newPos, playerPos.y, playerPos.z);
        }
        else if(distanceToCenter.x < -0.7)
        {
            //Right door
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
        }
        else if (distanceToCenter.z < 0.7)
        {
            //Back door
            mainCamera.transform.position += new Vector3(0, 7, 0);
            player.transform.position += new Vector3(0, 7, 0);
            currentRoomNum -= 5;
        }
        //Count the doors and call the doors visibility function
        //CountDoors();
        DoorVisibility();
    }


    void CountDoors()
    {
        availableDoors.Clear();

        
        bool? visibleDoors = CheckPills();
        foreach (Transform child in allRooms[currentRoomNum].transform)
        {
            if (child.tag == "normalDoor")
                availableDoors.Add(child.gameObject);
            else if(child.tag == "redDoor" && visibleDoors == true)
            {
                availableDoors.Add(child.gameObject);
            }
            else if (child.tag == "blueDoor" && visibleDoors == false)
            {
                availableDoors.Add(child.gameObject);
            }

        }
        numAvailableDoors = availableDoors.Count;
    }


    //It should be called every time the player takes pills
    public void DoorVisibility()
    {
        CountDoors();

        bool? whichDoor = CheckPills();
        if (whichDoor == null)
        {
            //Switches off all the doors
            for (int i = 0; i < redDoors.Length; i++)
            {
                redDoors[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < blueDoors.Length; i++)
            {
                blueDoors[i].gameObject.SetActive(false);
            }
        }
        else if(whichDoor == true)
        {
            //Switches on the red doors
            for (int i = 0; i < redDoors.Length; i++)
            {
                redDoors[i].gameObject.SetActive(true);
            }
        }
        else if (whichDoor == false)
        {
            //Switches on the blue doors
            for (int i = 0; i < blueDoors.Length; i++)
            {
                blueDoors[i].gameObject.SetActive(true);
            }
        }
    }

    //Returns true if there are 2 more red pills than blue. False if 2 more blue pills than red.
    bool? CheckPills()
    {
        int morePillsThan = playerControllerCs.redPillCount - playerControllerCs.bluePillCount;

        if (morePillsThan >= 2)
        {
            return true;
        }
        else if (morePillsThan <= -2)
        {
            return false;
        }

        return null;
    }

}
