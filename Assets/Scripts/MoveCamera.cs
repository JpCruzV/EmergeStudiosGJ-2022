using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] GameObject playerRef;
    [SerializeField] Transform EndRef;
    [HideInInspector] public bool onPlayer = false;
    Vector3 endPos;
    Vector3 playerPos;
    bool stopCinematic = false;
    [HideInInspector] public bool teleportedToPlayer = false;

    private void Start() {

        endPos = new Vector3(EndRef.transform.position.x, 0, -10);
        transform.position = endPos;
    }


    private void Update() {


        playerPos = new Vector3(playerRef.transform.position.x + 10, 0, -10);

        if (playerRef.GetComponent<PlayerController>().hasTeleported == true && teleportedToPlayer == false) {

            teleportedToPlayer = true;
            transform.position = new Vector3(playerPos.x , playerPos.y);
        }

        if(transform.position != playerPos) {

            transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * 15);
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
