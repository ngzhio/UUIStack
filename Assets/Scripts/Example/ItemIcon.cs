using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private Image itemFramework;

    [SerializeField]
    private Text countText;

    private int itemID;

    private int quality;

    private int count;

    public ItemIcon SetItemID(int itemID)
    {
        this.itemID = itemID;

        var sprites = Resources.LoadAll<Sprite>("Arts/UI2/Item/AllItems");
        this.itemIcon.sprite = sprites[ItemIDs.ItemID2ItemIndex(itemID)];

        return this;
    }

    public ItemIcon SetQuality(int quality)
    {
        this.quality = quality;

        var sprites = Resources.LoadAll<Sprite>("Arts/UI2/ItemFrame/ItemFrame");
        this.itemFramework.sprite = sprites[quality - 1];

        return this;
    }

    public ItemIcon SetCount(int count)
    {
        this.count = count;
        this.countText.text = $"{count}";
        return this;
    }
}