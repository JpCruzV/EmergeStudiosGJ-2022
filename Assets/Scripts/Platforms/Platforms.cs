using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {


    [SerializeField] Animator anim;
    [SerializeField] float force;
    [SerializeField] int id;
    SpriteRenderer sprite;
    GameObject playerRef;


    private void Awake() {

        sprite = GetComponent<SpriteRenderer>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnCollisionEnter2D(Collision2D collision) {


        if (id == 1) {

            anim.SetBool("Bounce", true);
            StartCoroutine(waitForAnim());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force * 2, ForceMode2D.Impulse);
        }


        else if (id == 0) {

            anim.SetBool("Bounce", true);
            StartCoroutine(waitForAnim());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
        }


        else if (id == 3) {

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * force, ForceMode2D.Impulse);
        }
    }

    IEnumerator waitForAnim() {

        yield return new WaitForSeconds(1f);
        anim.SetBool("Bounce", false);
    }
}
