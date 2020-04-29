public class UIBag : UIController
{
    public void OnClickBackButton()
    {
        UIManager.Instance.CloseTop();
    }
}