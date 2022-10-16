using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] GameObject playerRef;
    [HideInInspector] public bool onPlayer = false;
    Vector3 playerPos;
    bool stopCinematic = false;
    [HideInInspector] public bool teleportedToPlayer = false;


    private void Update() {


        playerPos = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y, -10);

        if (playerRef.GetComponent<PlayerController>().hasTeleported == true && teleportedToPlayer == false) {

            teleportedToPlayer = true;
            transform.position = new Vector3(playerPos.x , playerPos.y);
        }

        if(transform.position != playerPos) {

            if (playerPos.y > -5)
                transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * 50);


            else if (playerPos.y <= -5) {
                Vector3 newPlayerPos = new Vector3(playerRef.transform.position.x, 2, -10);
                transform.position = Vector3.MoveTowards(transform.position, newPlayerPos, Time.deltaTime * 50);
            }
        }


        if (this.transform.position == playerPos && stopCinematic == false) {

            stopCinematic = true;
            onPlayer = true;
        }

        else {

            Vector3.Lerp(transform.position, playerPos, 10);
        }
    }
}
