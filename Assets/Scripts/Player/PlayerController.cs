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

    [Header("Saving Platform")]
    [SerializeField] GameObject savingPlatform;
    int savingPlatformCount = 3;
    bool savingPlatformActive = false;
    bool hasTeleported = false;

    //PowerUp
    bool hasWindPower = false;
    [HideInInspector] public bool hasFirePower = false;
    SpriteRenderer sprite;

    float buttonCooldown = .5f;
    int buttonCount = 0;


    void myInput() {

        if (cam.GetComponent<MoveCamera>().onPlayer == true) {

            HorizontalInput = Input.GetAxis("Horizontal");
            Vector2 dir = new Vector2(HorizontalInput, 0);
            rb.AddForce(dir * speed, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnVine == true) {

            OnVine = false;
            doneWithVine = false;
        }


        else if (Input.GetKeyDown(KeyCode.Space) && OnVine == false && savingPlatformCount != 0 && savingPlatformActive == false) {

            savingPlatformActive = true;
            StartCoroutine(ActivateSavingPlatform());
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


    IEnumerator ActivateSavingPlatform() {

        savingPlatform.SetActive(true);
        yield return new WaitForSeconds(5);

        savingPlatform.SetActive(false);
        savingPlatformActive = false;
    }


    private void Awake() {

        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }


    private void Start() {

        savingPlatform.SetActive(false);
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

            hasTeleported = true;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
            Transform CurPortal;
            
            do {
                CurPortal = portals[Random.Range(0, portals.Length)].transform;
            }
            while (Vector3.Distance(CurPortal.position, this.transform.position) < 2);
            transform.position = new Vector3(CurPortal.position.x, CurPortal.position.y);
            StartCoroutine(PortalCooldown());
        }
    }

    IEnumerator PortalCooldown() {
        yield return new WaitForSeconds(5);
        hasTeleported = false;
    }

    [SerializeField] GameObject portal;


    private void SavingPortal()
    {

        if (Input.GetButtonDown("Fire1"))
        {

            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x > -3 && mousePos.x < 3 && mousePos.y > -6 && mousePos.y < -2)
            {

                Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                Instantiate(portal, objectPos, Quaternion.identity);
            }
        }
    }
}
