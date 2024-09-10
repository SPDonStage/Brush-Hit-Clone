using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private bool isHit;
    private ConfigurableJoint joint;
    [SerializeField] private Material inItMaterial;
    [SerializeField] private Material hitMaterial;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = inItMaterial;
        joint ??= GetComponent<ConfigurableJoint>();
        if (transform.parent.parent.parent.gameObject.layer == LayerMask.NameToLayer("Moving Platform"))
        {
            joint.connectedBody = transform.parent.parent.parent.GetComponent<Rigidbody>(); //get the platform
        }
    }
    // Start is called before the first frame update
    void Start()
    {
     
        if (!GameManager.isPaused || !GameManager.isWon)
        {
           
            GameManager.countTotal++;
            isHit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.isPaused && !GameManager.isWon && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!isHit)
            {
                isHit = true;
                AudioManager.Instance.fxPlay(AudioManager.Instance.capsuleHit);
                fXCapsuleHitPool.onNumber++;
                fXCapsuleHitPool.pos = transform.position;
                fXCapsuleHitPool.isTriggered = true;
       
                meshRenderer.material = hitMaterial; //when hit change material
                GameManager.count++; //increase capsule count hit
            }
        }
    }
}
