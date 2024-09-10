using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MovingThing
{
    [SerializeField] private Transform platformHoldPlayer;
    private BoxCollider boxCollider;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canRotate;
    [SerializeField] private float spinSpeed;
    private void Awake()
    {
        boxCollider ??= GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (onNumOfDes < destinations.Length)
            {
                boxCollider.center = new Vector3(model.localPosition.x, boxCollider.center.y, model.localPosition.z);
                platformHoldPlayer.transform.position = model.transform.position; //control position of model parent for hold player as child
                model.transform.Translate((destinations[onNumOfDes].position - model.transform.position).normalized * speed * Time.fixedDeltaTime);
                if ((destinations[onNumOfDes].position - model.transform.position).sqrMagnitude * speed * Time.fixedDeltaTime <= 0.01f)
                {
                    onNumOfDes++;
                }
            }
            else
            {
                onNumOfDes = 0;
            }
        }
        if (canRotate)
        {
            transform.Rotate(transform.up * spinSpeed * Time.fixedDeltaTime);
        }
    
    }
    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player.hit.collider)
            {
                if (player.hit.collider.gameObject.layer == LayerMask.NameToLayer("Moving Platform"))
                {
                    player.transform.SetParent(player.hit.collider.GetComponentInParent<MovingPlatform>().platformHoldPlayer.transform);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
