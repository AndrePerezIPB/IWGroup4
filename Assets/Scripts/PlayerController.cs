using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5.0f;
    
    public int currentRoomNum = 5;
    public int goalRoomNum;

    private SpriteRenderer spriteRenderer;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        float verticalMove = Input.GetAxisRaw("Vertical") * playerSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);

        this.transform.Translate(moveDirection * Time.deltaTime);

        if(horizontalMove > 0)
        {
            spriteRenderer.flipX = true;

        }
        else if(horizontalMove < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
