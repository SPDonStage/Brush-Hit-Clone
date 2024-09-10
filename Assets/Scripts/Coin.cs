using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int value;
    [SerializeField] private int spinSpeed;
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
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.isPaused && !GameManager.isWon)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                AudioManager.Instance.fxPlay(AudioManager.Instance.coinHit);
                UserDataManager.Instance.coin += value;
                Destroy(gameObject);
            }
        }
    }
}
