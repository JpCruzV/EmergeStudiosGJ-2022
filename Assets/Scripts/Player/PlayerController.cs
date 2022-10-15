using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// Logica de los vines,  cuando se acerca al trigger collider de el vine se lanza un gancho donde se encuentra vine y se balancea.
    /// Cuando se esta conectado a un vine y se presiona espacio, se suelta del vine
    /// </summary> 

    #region Hook

    [Header("Hook Logic")]
    [SerializeField] GameObject Hook;

    GameObject curHook;
    [HideInInspector] public bool OnRangeFromVine = false;
    bool doneWithVine = false;
    Vector2 destiny;

    private void VineLogic() {

        if (OnRangeFromVine == true && doneWithVine == false) {

            doneWithVine = true;

            curHook = (GameObject)Instantiate(Hook, transform.position, Quaternion.identity);

            curHook.GetComponent<SwingingRope>().destiny = destiny;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Vine") {

            destiny = collision.transform.position;
            OnRangeFromVine = true;
        }
    }

    #endregion


    [Header("Movement")]
    [SerializeField] float speed;
    Rigidbody2D rb;
    float HorizontalInput;

    void myInput() {

        HorizontalInput = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(HorizontalInput, 0);
        rb.AddForce(dir * speed, ForceMode2D.Impulse);

        if (Input.GetKeyDown(KeyCode.Space) && OnRangeFromVine == true) {

            OnRangeFromVine = false;
            doneWithVine = false;
        }
    }

    private void Start() {

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {

        myInput();
        VineLogic();
    }
}
