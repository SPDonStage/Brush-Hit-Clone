using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThing : MonoBehaviour
{
    public int onNumOfDes;
    [SerializeField] public float speed;
    [SerializeField] public Transform[] destinations;
    [SerializeField] public Transform model;
    // Start is called before the first frame update
    void Start()
    {
        onNumOfDes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
