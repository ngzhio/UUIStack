using UnityEngine;

public interface IUIResourceDelegate
{
    UIController Load(string uiName);
    void Release(UIController controller);
}