using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
   // public static ShopManager Instance;
    public static Material material;
    //Mannequin for skin changing
    [SerializeField] private GameObject circleParent_Model;
    [SerializeField] private GameObject circleChild_Model;
    [SerializeField] private GameObject cylinder_Model;

    [SerializeField] private ItemShop[] items;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private Button buyBtn;
    private Animator buyBtnAnimator;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonHolder;
    private int coinValue;
    private void Awake()
    {
        buyBtnAnimator ??= GetComponent<Animator>();
    //    Instance = this;
        material = UserDataManager.Instance.skin;
        foreach (var item in items)
        {
            Instantiate(buttonPrefab, buttonHolder.transform).GetComponent<ItemButton>().item = item;
        }
        buyBtn.onClick.AddListener(() => PurchaseBtn());
    }
    // Start is called before the first frame update
    void Start()
    {
        coinValue = UserDataManager.Instance.coin;
    }

    // Update is called once per frame
    void Update()
    {
        if (material != null)
        {
            circleParent_Model.GetComponent<MeshRenderer>().material = material;
            circleChild_Model.GetComponent<MeshRenderer>().material = material;
            cylinder_Model.GetComponent<MeshRenderer>().material = material;
        }
        coinText.text = coinValue.ToString();
    }
    public void PurchaseBtn()
    {
        buyBtnAnimator.SetBool("active", true);
        if (material == UserDataManager.Instance.skin)
        {
            buyText.text = "Choose an item to purchase !";
        }
        else
        {
            buyText.text = "Item purchased completely !";
            UserDataManager.Instance.skin = material;
            UserDataManager.Instance.Save();
        }
        //  buyBtnAnimator.SetBool("active", false);
    }
    public void PurchaseAnimOff() //for animation event
    {
        buyBtnAnimator.SetBool("active", false);
    }
}
