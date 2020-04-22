using UnityEngine;

public class DefaultUIResourceDelegate : IUIResourceDelegate
{
    public UIController Load(string uiName)
    {
        var obj = GameObject.Instantiate(Resources.Load<GameObject>($"UI/{uiName}"));
        return obj.GetComponent<UIController>();
    }

    public void Release(UIController controller)
    {
        GameObject.Destroy(controller.gameObject);
    }
}