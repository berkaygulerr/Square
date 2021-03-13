using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int Price { get; private set; }

    public ItemType itemType;

    public Color color;

    private const int colorPrice = 50;

    public bool selected;
    public bool bought;

    private Image buttonImage;
    private Image itemImage;

    public Sprite selectedSprite;
    public Sprite selectSprite;
    public Sprite fiftyPriceSprite;

    private void Start()
    {
        buttonImage = transform.GetChild(1).GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();

        itemImage.color = color;
        SetPrice();
    }

    private void Update()
    {
        ChangeButtonImage();
    }

    private void ChangeButtonImage()
    {
        if (selected)
            buttonImage.sprite = selectedSprite;
        else if (bought)
            buttonImage.sprite = selectSprite;
    }

    private void SetPrice()
    {
        switch (itemType)
        {
            case ItemType.Color:
                Price = colorPrice;
                break;
            default:
                break;
        }
    }
}

public enum ItemType
{
    Color
}
