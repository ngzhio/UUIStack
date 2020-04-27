public class UIHome : UGUIController
{
    public void OnClickHeroButton()
    {
        UIStack.GetInstance().Open(UINames.UIHero);
    }

    public void OnClickInscriptionButton()
    {
        UIStack.GetInstance().Open(UINames.UIInscription);
    }

    public void OnClickBagButton()
    {
        UIStack.GetInstance().Open(UINames.UIBag);
    }

    public void OnClickWarButton()
    {
        UIStack.GetInstance().Open(UINames.UIWar);
    }

    public void OnClickBarButton()
    {
        UIStack.GetInstance().Open(UINames.UIBar);
    }
}