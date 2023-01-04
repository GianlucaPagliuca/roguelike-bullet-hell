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
    // Start is called before the first frame update
    void Start()
    {
        speed = 500f;
        health = 5;
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

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = movement * speed * Time.deltaTime;
    }

    void FixedUpdate(){
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
    }
}
