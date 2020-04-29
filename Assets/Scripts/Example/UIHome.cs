public class UIHome : UIController
{
    public void OnClickHeroButton()
    {
        UIManager.Instance.Open(UINames.UIHero);
    }

    public void OnClickInscriptionButton()
    {
        UIManager.Instance.Open(UINames.UIInscription);
    }

    public void OnClickBagButton()
    {
        UIManager.Instance.Open(UINames.UIBag);
    }

    public void OnClickWarButton()
    {
        UIManager.Instance.Open(UINames.UIWar);
    }

    public void OnClickBarButton()
    {
        UIManager.Instance.Open(UINames.UIBar);
    }
}