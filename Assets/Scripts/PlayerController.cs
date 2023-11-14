using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5.0f;
    
    public int currentRoomNum = 23;
    public int goalRoomNum;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        this.transform.Translate(playerSpeed * moveDirection * Time.deltaTime);

        if(horizontalInput > 0)
        {
            spriteRenderer.flipX = true;

        }
        else if(horizontalInput < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
