using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingPortal : MonoBehaviour {


    [SerializeField] GameObject portal;


    private void Update() {

        if (Input.GetButtonDown("Fire1")) {

            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x > -3 && mousePos.x < 3 && mousePos.y > -6 && mousePos.y < -2) {

                mousePos.z = 2.0f;       // we want 2m away from the camera position
                Vector3 objectPos = Camera.current.ScreenToWorldPoint(mousePos);
                Instantiate(portal, objectPos, Quaternion.identity);
            }
        }
    }
}
