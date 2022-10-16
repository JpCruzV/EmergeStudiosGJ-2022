using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnablePlatform : MonoBehaviour {

    [SerializeField] List<GameObject> burnables;


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController>().hasFirePower == true)
        {
            collision.gameObject.GetComponent<PlayerController>().hasFirePower = false;
            Debug.Log("Entre");
            foreach (GameObject go in burnables) 
            {
                Destroy(go.gameObject);
            }
        }
    }
}
