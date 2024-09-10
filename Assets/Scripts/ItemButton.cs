using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IDeselectHandler, ISelectHandler
{
    private Button button;
    public ItemShop item;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    private void Awake()
    {
        button ??= GetComponent<Button>();
      //  GetComponent<Button>().onClick.AddListener( => );
    }
    // Start is called before the first frame update
    void Start()
    {
        if (item != null)
        {
            text.text = item.value.ToString();
            image.material = item.spriteMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDeselect(BaseEventData eventData)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.white;
        button.colors = colorBlock;
     //   ShopManager.onSkinSelecting = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (item != null)
        {
            ShopManager.material = item.material;
        }
    }
}
