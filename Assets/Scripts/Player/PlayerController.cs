using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Vector2 inputValue;


    [HideInInspector] public bool hasTeleported = false;

    //PowerUp
    bool hasWindPower = false;
    [HideInInspector] public bool hasFirePower = false;
    SpriteRenderer sprite;

    [SerializeField] Animator anim;
    string curSceneName;

    void myInput() {

        if (cam.GetComponent<MoveCamera>().onPlayer == true) { // && !_grabbedByCannon) {

            inputValue.x = Input.GetAxis("Horizontal");
            Vector2 dir = new Vector2(inputValue.x, 0);
            rb.AddForce(dir * speed, ForceMode2D.Impulse);
            anim.SetFloat("Horizontal", inputValue.x);
            anim.SetFloat("Speed", inputValue.sqrMagnitude);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (OnVine) {

                OnVine = false;
                doneWithVine = false; 
	        }
            else if(_grabbedByCannon) {
                
                LaunchBall();
            }
            else if (hasWindPower == true && !_grabbedByCannon) {

                hasWindPower = false;
                rb.AddForce(transform.up * doubleJumpForce, ForceMode2D.Impulse);
            }
        }
    }




    private void Awake() {

        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start() {

        curSceneName = SceneManager.GetActiveScene().ToString();
    }

    private void Update() {

        SavingPortal();
        myInput();
        VineLogic();
        if (hasFirePower == false) {

            sprite.color = new Color(1, 1, 1, 1f);
        }
        if ( transform.position.y < -15 ) {

            RestartLevel();
        }
    }


    void RestartLevel() {

        SceneManager.LoadScene("Nivel 1");
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Vine") {

            destiny = collision.transform.position;
            OnVine = true;
        }

        if (collision.tag == "Wind") {

            hasWindPower = true;
        }

        if (collision.tag == "Fire") {

            hasFirePower = true;
            sprite.color = new Color(1, .6f, .6f, .8f);
        }

        if (collision.tag == "Portal" && hasTeleported == false) {

            cam.GetComponent<MoveCamera>().teleportedToPlayer = false;
            hasTeleported = true;
            GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
            Transform CurPortal;

            do
            {
                CurPortal = portals[Random.Range(0, portals.Length)].transform;
            }
            while (Vector3.Distance(CurPortal.position, this.transform.position) < 2);
            transform.position = new Vector3(CurPortal.position.x, CurPortal.position.y);
            StartCoroutine(PortalCooldown());
        }

        if (collision.tag == "Spikes") {

            RestartLevel();
        }
    }


    IEnumerator PortalCooldown() {
        yield return new WaitForSeconds(3);
        hasTeleported = false;
    }


    private void SavingPortal()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePos = Input.mousePosition;
            Vector2 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (objectPos.y < 0)
            {

                GameObject instantiatedPortal = Instantiate(portal, objectPos, Quaternion.identity);

                Destroy(instantiatedPortal, 3);
            }
        }
    }



    /// <summary>
    /// Cannon
    /// </summary>

    #region Cannon
    [SerializeField] GameObject portal;
    private Cannon _cannonGrabbingMe;
    private bool _grabbedByCannon;

    public void SetGrabbedByCannon(Cannon pCannon)
    {
        _cannonGrabbingMe = pCannon;
        _grabbedByCannon = true;

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        this.transform.position = pCannon.transform.position;
    }

    public void LaunchBall() {

        if(_cannonGrabbingMe != null) {

            rb.isKinematic = false;
            rb.velocity = _cannonGrabbingMe.transform.up * _cannonGrabbingMe.GetLaunchSpeed();

            _cannonGrabbingMe.ReleasePlayer();
            _cannonGrabbingMe = null;
        }
    }
    #endregion
}
