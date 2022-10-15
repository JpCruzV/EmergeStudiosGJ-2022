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
    [HideInInspector] public bool OnVine = false;
    bool doneWithVine = false;
    Vector2 destiny;

    private void VineLogic() {

        if (OnVine == true && doneWithVine == false) {

            doneWithVine = true;

            curHook = (GameObject)Instantiate(Hook, transform.position, Quaternion.identity);

            curHook.GetComponent<SwingingRope>().destiny = destiny;
        }
    }
    #endregion


    GameObject cam;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float doubleJumpForce;
    Rigidbody2D rb;
    float HorizontalInput;


    [HideInInspector] public bool hasTeleported = false;
    bool onCannon = false;
    bool hasShot = false;

    //PowerUp
    bool hasWindPower = false;
    [HideInInspector] public bool hasFirePower = false;
    SpriteRenderer sprite;

    float buttonCooldown = .5f;
    int buttonCount = 0;

    void myInput() {

        if (cam.GetComponent<MoveCamera>().onPlayer == true || onCannon == true) {

            HorizontalInput = Input.GetAxis("Horizontal");
            Vector2 dir = new Vector2(HorizontalInput, 0);
            rb.AddForce(dir * speed, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnVine == true) {

            OnVine = false;
            doneWithVine = false;
        }


        else if (Input.GetKeyDown(KeyCode.Space) && OnVine == false && onCannon == true) {

            hasShot = true;
            onCannon = false;
            this.rb.gravityScale = 1;

        }


        //DoubleTap
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && hasWindPower == true && OnVine == false) {

            if (buttonCooldown > 0 && buttonCount == 1) {

                hasWindPower = false;
                rb.AddForce(transform.up * doubleJumpForce, ForceMode2D.Impulse);
            }
            else {

                buttonCooldown = .5f;
                buttonCount += 1;
            }
        }


        if (buttonCooldown > 0) {

            buttonCooldown -= 1 * Time.deltaTime;
        }
        else
            buttonCount = 0;
    }




    private void Awake() {

        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }


    private void Update() {

        SavingPortal();
        myInput();
        VineLogic();
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Vine")
        {

            destiny = collision.transform.position;
            OnVine = true;
        }

        if (collision.tag == "Wind")
        {

            hasWindPower = true;
        }

        if (collision.tag == "Fire")
        {

            hasFirePower = true;
            sprite.color = new Color(1, 0, 0, .8f);
        }

        if (hasFirePower == false) {

            sprite.color = new Color(1, 1, 1, 1f);
        }

        if (collision.tag == "Portal" && hasTeleported == false) {

            cam.GetComponent<MoveCamera>().teleportedToPlayer = false;
            hasTeleported = true;
            GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
            Transform CurPortal;
            
            do {
                CurPortal = portals[Random.Range(0, portals.Length)].transform;
            }
            while (Vector3.Distance(CurPortal.position, this.transform.position) < 2);
            transform.position = new Vector3(CurPortal.position.x, CurPortal.position.y);
            StartCoroutine(PortalCooldown());
        }

        if (collision.tag == "Cannon" && onCannon == false) {

            onCannon = true;
            this.rb.gravityScale = 0;
            StartCoroutine(waitingForShot());
        }
    }

    IEnumerator waitingForShot() {
        
        while (hasShot == false) {

            Quaternion rotateTo = Quaternion.Euler(0, 0, 30);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * 10);
            yield return new WaitForSeconds(1.5f);
        }
        
    }

    IEnumerator PortalCooldown() {
        yield return new WaitForSeconds(3);
        hasTeleported = false;
    }


    [SerializeField] GameObject portal;


    private void SavingPortal() {

        if (Input.GetMouseButtonDown(0)) {

            Vector2 mousePos = Input.mousePosition;
            Vector2 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (objectPos.y < 0) {

                Instantiate(portal, objectPos, Quaternion.identity);
            }
        }
    }
}
