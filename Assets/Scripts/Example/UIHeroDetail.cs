public class UIHeroDetail : UGUIController
{
    public void OnClickBackButton()
    {
        UIStack.GetInstance().CloseTop();
    } 
}