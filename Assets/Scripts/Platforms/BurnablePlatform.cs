using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnablePlatform : MonoBehaviour {

    [SerializeField] List<GameObject> burnables;
    GameObject lastObject;

    private void Start() {

        //burnables = GameObject.FindGameObjectsWithTag("Burnable");
    }




    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController>().hasFirePower == true)
        {
            collision.gameObject.GetComponent<PlayerController>().hasFirePower = false;
            Debug.Log("Entre");
            foreach (GameObject go in burnables) 
            {
                Destroy(go.gameObject);
            }

            /*burnables = GameObject.FindGameObjectsWithTag("Burnable");

            foreach (GameObject go in burnables)
            {
                if (lastObject != null && Vector3.Distance(go.transform.position, collision.transform.position) < 10)
                {
                    Debug.Log(""+Vector3.Distance(lastObject.transform.position, go.transform.position));
                    
                    if (Vector3.Distance(lastObject.transform.position, go.transform.position) < 35)
                    {

                        Destroy(lastObject.gameObject);
                        lastObject = go;
                    }
                }
                else
                    lastObject = go;
            }*/
        }
    }
}
