using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] Transform playerRef;
    [SerializeField] Transform EndRef;
    [SerializeField] GameObject borders;
    [HideInInspector] public bool onPlayer = false;
    Vector3 endPos;
    Vector3 playerPos;
    bool stopCinematic = false;

    private void Start() {

        endPos = new Vector3(EndRef.transform.position.x, 0, -10);
        transform.position = endPos;
        borders.SetActive(false);
    }


    private void Update() {


        playerPos = new Vector3(playerRef.transform.position.x + 2, 0, -10);

        if(transform.position != playerPos) {

            transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * 5);
        }

        if (this.transform.position == playerPos && stopCinematic == false) {

            stopCinematic = true;
            borders.SetActive(true);
            onPlayer = true;
        }

        else {

            Vector3.Lerp(transform.position, playerPos, 5);
        }
    }
}
