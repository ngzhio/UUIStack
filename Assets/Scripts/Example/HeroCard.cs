using UnityEngine;
using UnityEngine.UI;

public class HeroCard : MonoBehaviour
{
    [SerializeField]
    private Image heroImage;

    [SerializeField]
    private Text heroName;

    private int heroID;

    public void SetHeroID(int heroID)
    {
        this.heroID = heroID;

        string heroImagePath = $"Arts/UI2/HeroIcon/{heroID}/HeroSkin/{heroID}2101";
        var sprites = Resources.LoadAll<Sprite>(heroImagePath);
        if (sprites.Length == 0)
        {
            Debug.LogWarning($"Load '{heroImagePath}' failed!");
            return;
        }
        heroImage.sprite = sprites[0];
    }

    public void OnClick()
    {
        UIStack.GetInstance().Open("UIHeroDetail", new UIHeroDetailArguments(this.heroID));
    }
}