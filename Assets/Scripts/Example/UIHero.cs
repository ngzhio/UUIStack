using UnityEngine;

public class UIHero : UGUIController
{
    [SerializeField]
    private GameObject heroCardTemplate;

    [SerializeField]
    private GameObject heroCardHolder;

    public void OnClickBackButton()
    {
        UIStack.GetInstance().CloseTop();
    }

    public override void OnOpen(IUIArguments arguments)
    {
        LoadHeroCards();
    }

    private void LoadHeroCards()
    {
        for (int id = 1001; id <= 1026; id++)
        {
            var heroCard = GameObject.Instantiate(heroCardTemplate).GetComponent<HeroCard>();
            heroCard.SetHeroID(id);
            heroCard.transform.SetParent(heroCardHolder.transform);
        }
    }
}