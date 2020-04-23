using UnityEngine;

public class DefaultUIResourceDelegate : IUIResourceDelegate
{
    public UIController Load(string uiName)
    {
        var res = Resources.Load<GameObject>($"UI/{uiName}");
        if (res == null)
        {
            Debug.LogError($"Load the resource of {uiName} failed!");
            return null;
        }

        var obj = GameObject.Instantiate(res);
        return obj.GetComponent<UIController>();
    }

    public void Release(UIController controller)
    {
        GameObject.Destroy(controller.gameObject);
    }
}