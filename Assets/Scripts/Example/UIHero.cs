using UnityEngine;

public class UIHero : UIController
{
    [SerializeField]
    private GameObject heroCardTemplate;

    [SerializeField]
    private GameObject heroCardHolder;

    public void OnClickBackButton()
    {
        UIManager.Instance.CloseTop();
    }

    public override void OnOpen(IUIArguments arguments, int depth)
    {
        base.OnOpen(arguments, depth);
        
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