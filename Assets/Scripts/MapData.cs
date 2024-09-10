using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
  
    [SerializeField] private int level;
    [SerializeField] private int nextLevel;
    [SerializeField] public int timeMap;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.time = timeMap;
        GameManager.levelOfMap = level;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
