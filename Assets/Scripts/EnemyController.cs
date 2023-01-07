using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Camera MainCamera;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));

    }

    float RandomAxisValues(float minPos, float maxPos, float playerPos){
        float pos = Random.Range(minPos, maxPos);
        if(pos >= playerPos - 1.5f && pos <= playerPos + 1.5f)
            RandomAxisValues(minPos, maxPos, playerPos);
        // if(pos == playerPos)
        //     RandomAxisValues(minPos, maxPos, playerPos);

        return pos;
    }

    void SpawnNewEnemy(GameObject player, bool del = false, float time = 0){
        float xPos = RandomAxisValues(screenBounds.x * -1, screenBounds.x, player.transform.position.x);
        float yPos = RandomAxisValues(screenBounds.y * -1, screenBounds.y, player.transform.position.y);
        Vector3 newPos = new Vector3(xPos, yPos);

        Instantiate(gameObject, newPos, gameObject.transform.rotation);

        if (del)
            Destroy(this.gameObject, time);

    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Player")){
            col.gameObject.GetComponent<PlayerController>().damagePlayer(10);

            SpawnNewEnemy(col.gameObject, true, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
