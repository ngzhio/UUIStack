public class UIHero : UGUIController
{
    public void OnClickBackButton()
    {
        UIStack.GetInstance().CloseTop();
    }
}