using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnablePlatform : MonoBehaviour {

    GameObject playerRef;

    
    void Start() {

        playerRef = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.tag == "Player" && playerRef.GetComponent<PlayerController>().hasFirePower == true) {

            playerRef.GetComponent<PlayerController>().hasFirePower = false;
            Destroy(this.gameObject);
        }
    }
}
