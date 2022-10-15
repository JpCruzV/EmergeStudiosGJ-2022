using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour {


    [SerializeField] float slip;

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.tag == "Player") {

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.AddForce(this.transform.right * slip, ForceMode2D.Impulse);
        }
    }
}
