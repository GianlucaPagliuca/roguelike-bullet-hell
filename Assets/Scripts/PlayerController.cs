using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5000)]
    private float speed;
    private Rigidbody2D rb;
    private int health;
    private Animator animator;
    public float bowDistance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

        health = 100;
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void damagePlayer(int dmg){
        if(health > 0)
            health -= dmg;
        else
            Debug.Log("Game Over");

        Debug.Log(health);
    }

    public void healPlayer(int heal){
        health += heal;
    }

    // Update is called once per frame
    float originSpeed = 0;
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)){
            originSpeed = speed;
            speed = 4.0f;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)){
            speed = originSpeed;
        }

        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");
        bool shiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? true : false;

        Vector2 movement = new Vector2(horizontalValue, verticalValue);
        rb.velocity = movement * speed;

        


        if(horizontalValue != 0 || verticalValue != 0){
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", shiftHeld);
        }else{
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        animator.SetFloat("inputHorizontal", horizontalValue);
        animator.SetFloat("inputVertical", verticalValue);
    }
}
