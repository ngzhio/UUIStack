using UnityEngine;
using UnityEngine.UI;

public class UIHeroDetailArguments : IUIArguments
{
    public int heroID;

    public UIHeroDetailArguments(int heroID)
    {
        this.heroID = heroID;
    }
}

public class UIHeroDetail : UGUIController
{
    [SerializeField]
    private Image heroImage;

    private int heroID;

    public void OnClickBackButton()
    {
        UIStack.GetInstance().CloseTop();
    }

    public override void OnOpen(IUIArguments arguments)
    {
        base.OnOpen(arguments);

        var _args = (UIHeroDetailArguments) arguments;
        if (_args != null)
        {
            heroID = _args.heroID;

            string heroImagePath = $"Arts/UI2/HeroIcon/{heroID}/HeroSkin/{heroID}2101";
            var sprites = Resources.LoadAll<Sprite>(heroImagePath);
            if (sprites.Length == 0)
            {
                Debug.LogWarning($"Load '{heroImagePath}' failed!");
                return;
            }
            heroImage.sprite = sprites[0];
        }
    }
}