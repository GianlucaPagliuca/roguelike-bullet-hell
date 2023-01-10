using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponScript : MonoBehaviour
{
    private Transform player;
    private float maxDistance;
    private float maxWeaponCharge = 1.0f, weaponCharge = 0.0f;
    public float shotStrength = 0.0f;
    public bool weaponFullyCharged = false;
    public bool fireWeapon = false;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        maxDistance = player.GetComponent<PlayerController>().bowDistance;
    }

    void UpdatePosition() {
        // get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // convert the mouse position from screen space to world space
        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);

        // calculate the distance from the mouse to the player
        float distance = Vector2.Distance(player.position, mousePosInWorld);

        // if the distance is greater than the maxiumum distance, move the weapon towards the player,
        if (distance > maxDistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position, distance);
        }
        else{
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
        if (Input.GetMouseButton(0)) {
            weaponCharge += 1 * Time.deltaTime;
            
            if(weaponCharge >= maxWeaponCharge){
                weaponCharge = maxWeaponCharge;

                weaponFullyCharged = true;

                Debug.Log("Weapon Charged");
            }
            Debug.Log(weaponCharge);
        }
        else if(Input.GetMouseButtonUp(0)){
            fireWeapon = true;
            weaponCharge = weaponCharge > maxWeaponCharge ? 2 : weaponCharge;
            shotStrength = (float)(weaponCharge / 1) * 100;
            Debug.Log(shotStrength);
            weaponCharge = 0;
        }
    }
}
