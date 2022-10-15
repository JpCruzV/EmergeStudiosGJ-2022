using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingRope : MonoBehaviour {

    public Vector2 destiny;

    [SerializeField] float speed = 1;
    [SerializeField] float dist = 2;
    [SerializeField] GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;

    bool done = false;

    private void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        lastNode = transform.gameObject;
    }


    private void Update() {

        transform.position = Vector2.MoveTowards(transform.position, destiny, speed);

        if ((Vector2)transform.position != destiny) {

            if (Vector2.Distance(player.transform.position, lastNode.transform.position) > dist) {

                CreateNode();
            }
        }
        else if (done == false) {

            done = true;
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();

        }
    }


    void CreateNode() {

        Vector2 pos2Create = (player.transform.position - lastNode.transform.position).normalized;
        pos2Create *= dist;
        pos2Create += (Vector2)lastNode.transform.position;

        GameObject go = (GameObject)Instantiate(nodePrefab, pos2Create, Quaternion.identity);
        go.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        lastNode = go;
    }
}
