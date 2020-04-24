public class UIHome : UGUIController
{
    public void OnClickHeroButton()
    {
        UIStack.GetInstance().Open("UIHero");
    }

    public void OnClickInscriptionButton()
    {
        UIStack.GetInstance().Open("UIInscription");
    }

    public void OnClickBagButton()
    {
        UIStack.GetInstance().Open("UIBag");
    }

    public void OnClickWarButton()
    {
        UIStack.GetInstance().Open("UIWar");
    }

    public void OnClickBarButton()
    {
        UIStack.GetInstance().Open("UIBar");
    }
}