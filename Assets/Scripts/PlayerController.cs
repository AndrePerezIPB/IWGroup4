using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    public int redPillCount = 0;
    [SerializeField]
    public int bluePillCount = 0;

    //To count switch the doors on and off on the RoomManager
    [SerializeField]
    public RoomManager roomManagerCs;


    private SpriteRenderer spriteRenderer;
    public Animator animator;

    //pills
    public List<GameObject> RedPills;
    public List<GameObject> BluePills;
    public float distanceToCollectPill = 1f;
    private float distanceToPill;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        RedPills = new List<GameObject>(GameObject.FindGameObjectsWithTag("RedPill"));
        BluePills = new List<GameObject>(GameObject.FindGameObjectsWithTag("BluePill"));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetPills();
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        float verticalMove = Input.GetAxisRaw("Vertical") * playerSpeed;

        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("FrontSpeed", verticalMove);
        animator.SetFloat("BackSpeed", verticalMove);

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

    private void GetPills()
    {
        for (int i = 0; i < RedPills.Count; i++) 
        {
            distanceToPill = Vector3.Distance(RedPills[i].transform.position, this.transform.position);

            if (distanceToPill < distanceToCollectPill)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    redPillCount++;
                    Destroy(RedPills[i]);
                    RedPills.RemoveAt(i);

                    //Check whether the doors should be visible or not
                    roomManagerCs.DoorVisibility();
                }
            }
        }

        for (int i = 0; i < BluePills.Count; i++) 
        {
            distanceToPill = Vector3.Distance(BluePills[i].transform.position, this.transform.position);

            if (distanceToPill < distanceToCollectPill)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    bluePillCount++;
                    Destroy(BluePills[i]);
                    BluePills.RemoveAt(i);

                    //Check whether the doors should be visible or not
                    roomManagerCs.DoorVisibility();
                }
            }
        }
    }
}
