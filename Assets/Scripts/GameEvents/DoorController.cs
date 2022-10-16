using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    [SerializeField] Collider2D col;
    [SerializeField] Animator anim;


    private void Start() {

        GameEvents.current.onKeyTriggerEnter += onDoorwayOpen;
    }

    private void onDoorwayOpen() {

        anim.SetBool("Open", true);
        col.enabled = false;
    }
}
