using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    [SerializeField] GameObject Hook;

    GameObject curHook;
    bool OnRangeFromVine = false;

    private void Update() {

        if (OnRangeFromVine == true) {

            Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            curHook = (GameObject)Instantiate(Hook, transform.position, Quaternion.identity);

            curHook.GetComponent<SwingingRope>().destiny = destiny;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Vine") {

            OnRangeFromVine = true;
        }
    }
}
