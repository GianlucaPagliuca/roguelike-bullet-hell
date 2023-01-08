using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Transform player;
    public float maxDistance = 2.0f;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void UpdatePosition() {
        // get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // convert the mouse position from screen space to world space
        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);

        // calculate the distance from the mouse to the player
        float distance = Vector2.Distance(player.position, mousePosInWorld);

        // if the distance is greater than the maxiumum distance, move the weapon towards the player,
        if (distance > maxDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, distance);
        }
        else
        {
            transform.position = mousePosInWorld;
        }
        UpdateRotation();
    }

    void UpdateRotation(){
        Vector2 weaponPos = transform.position;
        Vector2 playerPos = player.position;

        //calculate the andlge between the weapon and the player
        float angle = Vector2.SignedAngle(weaponPos - playerPos, player.up);

        // convert the angel to a quaternion and set the weapon's rotation
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    // Update is called once per frame
    void Update(){
        UpdatePosition();
    }
}
