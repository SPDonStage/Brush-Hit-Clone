using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item Shop", menuName = "Scriptable Objects/Item Shop", order = 0)]
public class ItemShop : ScriptableObject
{
    [SerializeField] public int value;
    [SerializeField] public Material spriteMaterial;
    [SerializeField] public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
