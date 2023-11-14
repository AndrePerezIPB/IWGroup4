using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraLocation;

    // Start is called before the first frame update
    void Start()
    {
        cameraLocation = transform.Find("CameraLocation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
