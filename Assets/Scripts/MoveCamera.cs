using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] GameObject playerRef;
    [HideInInspector] public bool onPlayer = false;
    Vector3 playerPos;
    [HideInInspector] public bool teleportedToPlayer = false;


    float posx = 0;
    float posy = 0;

    private void Start() {

        onPlayer = true;
    }

    private void Update() {


        if (playerRef.GetComponent<PlayerController>().hasTeleported == true && teleportedToPlayer == false) {

            teleportedToPlayer = true;
            transform.position = new Vector3(playerPos.x , playerPos.y);
        }



        if (playerRef.transform.position.y <= -2)
        {

            posy = 2;
        }

        else if (playerRef.transform.position.y >= 50)
        {

            posy = 50;
        }

        else
            posy = playerRef.transform.position.y;


        if (playerRef.transform.position.x <= -18)
        {

            posx = -18;
        }

        else if (playerRef.transform.position.x >= 18)
        {
            posx = 18;
        }

        else
            posx = playerRef.transform.position.x;

        playerPos = new Vector3(posx, posy, -10);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * 50);
    }
}
