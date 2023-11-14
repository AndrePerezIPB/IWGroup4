using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject room;
    [SerializeField]
    PlayerController playerControllerCs;

    Transform roomTransform;
    int currentRoomNum;
    int goalRoomNum;
    
    void Start()
    {
        //currentRoomNum = playerControllerCs.currentRoomNum;
       // goalRoomNum = playerControllerCs.goalRoomNum;
    }

    void Update()
    {
        
    }
}
