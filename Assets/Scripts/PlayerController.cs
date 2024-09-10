using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public bool canControl;
    [SerializeField] LayerMask layer;
    [SerializeField] private float spinSpeed;
    [SerializeField] public GameObject circleParent; //use for camera
    [SerializeField] private GameObject circleChild;
    [SerializeField] private GameObject cylinder;
    public RaycastHit hit;
    private Animator animator;
  //  private PlayerInput playerInput;
    public bool isAddLong;
    public bool isDied;
    [SerializeField] private GameObject deathFX;
    private void Awake()
    {
       
        animator ??= GetComponent<Animator>();
 //       playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        
        
        //       playerInput.Enable();
    }
    private void OnDisable()
    {
  //      playerInput.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        circleParent.GetComponent<MeshRenderer>().material = UserDataManager.Instance.skin;
        circleChild.GetComponent<MeshRenderer>().material = UserDataManager.Instance.skin;
        cylinder.transform.GetChild(0).GetComponent<MeshRenderer>().material = UserDataManager.Instance.skin;
        isDied = false;
        canControl = false;
        animator.CrossFade("PlayerSpawn", .2f);
        cylinder.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false; //trigger true in animation
        circleParent.GetComponent<BoxCollider>().enabled = false; //trigger true in animation
        circleChild.GetComponent<BoxCollider>().enabled = false; //trigger true in animation

        /* if (UserDataManager.Instance.isLoading)
         {
             circleParent.GetComponent<MeshRenderer>().material = UserDataManager.Instance.skin;
             circleChild.GetComponent<MeshRenderer>().material = UserDataManager.Instance.skin;
             cylinder.GetComponent<MeshRenderer>().material = UserDataManager.Instance.skin;
         }*/
    }

    // Update is called once per frame
    void Update()
    {
     
        if (!GameManager.isPaused && !GameManager.isWon)
        {
            Physics.Raycast(circleParent.transform.position, -circleParent.transform.up, out hit, 10, layer);
            Debug.DrawRay(circleParent.transform.position, -circleParent.transform.up, Color.green, 10);
            //idea: player from moving platform go out to another platform
            if (hit.collider)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
                {
                    if (transform.parent != null)
                    {
                        transform.SetParent(null);
                    }
                }
            }
            //
            if (!canControl)
            {
                UserClick.isClick = false;
            }
            if (UserClick.isClick && canControl)
            {
                //    Debug.Log("Click");
                AudioManager.Instance.fxPlay(AudioManager.Instance.playerSwing);
                canControl = false;
                Invoke("DelayControl", .5f);
                
                SwapCircle(circleParent, circleChild);
                spinSpeed = -spinSpeed; //change spin direction
                UserClick.isClick = false;
            }

            if (isDied)
            {
                AudioManager.Instance.fxPlay(AudioManager.Instance.explosion);
                Instantiate(deathFX, circleParent.transform.position, circleParent.transform.rotation);
                GameManager.isLost = true;
                Destroy(gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.isPaused)
        {
            circleParent.transform.Rotate(circleParent.transform.up * spinSpeed * Time.fixedDeltaTime);
        }
       
    }
    private void LateUpdate()
    {
        if (!GameManager.isPaused || !GameManager.isWon)
        {
            //Check Death
            
            if (hit.collider)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("DeathZone"))
                {
                    isDied = true;
                }
            }
        }
    }
    void DelayControl() //for invoke
    {
        canControl = true;
    }
    public void Spawn() //use for animation event
    {
        canControl = true;
    }
    void SwapCircle(GameObject circleParent, GameObject circleChild)
    { //idea: unparent child objects then parent 'em to parent
        if (!GameManager.isPaused)
        {
            this.cylinder.transform.parent = this.transform;
            circleChild.transform.parent = this.transform;
            this.cylinder.transform.SetParent(circleChild.transform);
            this.circleParent.transform.SetParent(circleChild.transform);
            this.circleChild = circleParent;
            this.circleParent = circleChild;
        }
    }
    public void AddLong(float addLongValue)
    {
        //idea: scale cylinder then relocate cylinder and circleChild position
        if (isAddLong)
        {
            if (spinSpeed > 0) //right rotation
            {
                cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, cylinder.transform.localScale.y, cylinder.transform.localScale.z + addLongValue);
                cylinder.transform.localPosition = new Vector3(cylinder.transform.localPosition.x, cylinder.transform.localPosition.y, cylinder.transform.localPosition.z + addLongValue);
                circleChild.transform.localPosition = new Vector3(circleChild.transform.localPosition.x, circleChild.transform.localPosition.y,
                    circleChild.transform.localPosition.z + addLongValue * 2);             
            }
            else //left rotation
            {
                cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x, cylinder.transform.localScale.y, cylinder.transform.localScale.z + addLongValue);
                cylinder.transform.localPosition = new Vector3(cylinder.transform.localPosition.x, cylinder.transform.localPosition.y, cylinder.transform.localPosition.z - addLongValue);
                circleChild.transform.localPosition = new Vector3(circleChild.transform.localPosition.x, circleChild.transform.localPosition.y,
                    circleChild.transform.localPosition.z - addLongValue * 2);
            }
            isAddLong = false;
        } 
    }
}
