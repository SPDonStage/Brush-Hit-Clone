using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fXCapsuleHitPool : MonoBehaviour
{
    [SerializeField] private GameObject fxPrefab;
    [SerializeField] private int howMany;
    public static int onNumber = -1;
    public static Vector3 pos;
    public static bool isTriggered;
    IEnumerator Idisable;
    private void Awake()
    {
        for (int i = 0; i < howMany; i++) //create pool
        {
            Instantiate(fxPrefab, transform).SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPaused && !GameManager.isWon)
        {
            if (onNumber >= 0 && onNumber < howMany)
            {
                GameObject fx = transform.GetChild(onNumber).gameObject;
                if (fx.activeSelf == false && isTriggered)
                {
                    fx.transform.position = pos;
                    fx.SetActive(true);
                    //   fx.GetComponent<ParticleSystem>().Play();
                    Idisable = disable(fx.gameObject);
                    StartCoroutine(Idisable);
                    isTriggered = false;
                }
            }
            if (onNumber >= howMany - 1)
            {
                onNumber = -1; //reset list
            }
        }
    }
    IEnumerator disable(GameObject gameObject)
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    
    }
}
