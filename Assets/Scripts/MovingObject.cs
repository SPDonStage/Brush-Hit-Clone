using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MovingThing
{
    private SphereCollider sphereCollider;
    private void Awake()
    {
        sphereCollider ??= GetComponent<SphereCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onNumOfDes < destinations.Length)
        {
            sphereCollider.center = new Vector3(model.localPosition.x, sphereCollider.center.y, model.localPosition.z);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            player.isDied = true;
        }
    }
}
