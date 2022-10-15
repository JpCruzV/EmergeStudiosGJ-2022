using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour {

    [SerializeField] GameObject Hook;

    GameObject curHook;

    bool OnRange = false;

    private void Update() {

        if (OnRange == true) {

            Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            curHook = (GameObject)Instantiate(Hook, transform.position, Quaternion.identity);

            curHook.GetComponent<SwingingRope>().destiny = destiny;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "Vine") {

            OnRange = true;
        }
    }
}
