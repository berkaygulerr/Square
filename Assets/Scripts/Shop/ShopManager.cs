using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Color[] colors;
    public GameObject[] items;

    public GameObject colorItemPref;
    public Transform colorsItemGroup;

    private string saveFileName = "shopdata.dat";

    public SaveItemData colorItemData = new SaveItemData();

    public void Button()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        colorItemData = SaveManager.Load<SaveItemData>(colorItemData, saveFileName);

        CreateItem(colors.Length, colors, ItemType.Color);
        InitSelectItem();
        LoadItemData();
    }

    private void InitSelectItem()
    {
        if (colorItemData.bougthColorItems.Count == 0)
        {
            Item classicItem = items[0].GetComponent<Item>();

            ItemData itemData = new ItemData(classicItem.color);

            colorItemData.bougthColorItems.Add(itemData);
            colorItemData.selectedItemColor = itemData.itemDataColor;
        }
    }

    private void LoadItemData()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i].GetComponent<Item>();

            ItemDataColor slctdItemDataColor = colorItemData.selectedItemColor;
            Color selectedItemColor = new Color(slctdItemDataColor.r, slctdItemDataColor.g, slctdItemDataColor.b);

            for (int j = 0; j < colorItemData.bougthColorItems.Count; j++)
            {
                ItemData boughtItem = colorItemData.bougthColorItems[j];

                ItemDataColor bghtItemDataColor = boughtItem.itemDataColor;
                Color bghtItemColor = new Color(bghtItemDataColor.r, bghtItemDataColor.g, bghtItemDataColor.b);

                if (item.color == bghtItemColor)
                {
                    item.bought = true;

                    if (item.color == selectedItemColor)
                        item.selected = true;
                }
            }
        }
    }

    private void CreateItem(int count, Color[] colors, ItemType itemType)
    {
        items = new GameObject[count];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(colorItemPref, Vector2.zero, Quaternion.identity, colorsItemGroup);

            Item item = items[i].GetComponent<Item>();

            item.itemType = itemType;
            item.color = colors[i];

            SetOnlickButtons(i);
        }
    }

    private void SetOnlickButtons(int index)
    {
        Button clickedItemButton = items[index].transform.GetChild(1).GetComponent<Button>();
        clickedItemButton.onClick.AddListener(() => SelectButton(index));
    }

    public void SelectButton(int index)
    {
        Item selectedItem = items[index].GetComponent<Item>();

        if (!selectedItem.selected)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i].GetComponent<Item>();

                if (item.selected)
                {
                    item.selected = false;
                }
            }

            ItemData itemData = new ItemData(selectedItem.color);

            if (!selectedItem.bought)
                colorItemData.bougthColorItems.Add(itemData);

            colorItemData.selectedItemColor = itemData.itemDataColor;

            LoadItemData();

            SaveManager.Save<SaveItemData>(colorItemData, saveFileName);
        }
    }
}
