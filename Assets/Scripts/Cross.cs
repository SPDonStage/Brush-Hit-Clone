using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    [SerializeField] private int spinSpeed;
    [SerializeField] private float addLongValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!GameManager.isPaused)
        {
            transform.Rotate(transform.up * spinSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!GameManager.isPaused && !GameManager.isWon)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.GetComponentInParent<PlayerController>().isAddLong = true;
                collision.GetComponentInParent<PlayerController>().AddLong(addLongValue);
                CameraController.isAddFOV = true;
                Destroy(gameObject);
            }
        }
    }
}
