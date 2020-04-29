public interface IUIResourceAgent
{
    UIController Load(string uiName);
    void Release(UIController controller);
}