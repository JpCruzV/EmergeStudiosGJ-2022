using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour {


    [SerializeField] Animator anim;
    [SerializeField] float force;
  

    private void OnCollisionEnter2D(Collision2D collision) {

        anim.SetBool("Bounce", true);
        StartCoroutine(waitForAnim());

        if ( collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
        }
    }

    IEnumerator waitForAnim() {

        yield return new WaitForSeconds(.5f);
        anim.SetBool("Bounce", false);
    }
}
