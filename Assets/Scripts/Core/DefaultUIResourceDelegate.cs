using UnityEngine;

public class DefaultUIResourceDelegate : IUIResourceDelegate
{
    public static readonly string UI_ROOT_NAME = "UIRoot";

    private GameObject uiRoot;

    public UIController Load(string uiName)
    {
        var res = Resources.Load<GameObject>($"UI/{uiName}");
        if (res == null)
        {
            Debug.LogError($"Load the resource of {uiName} failed!");
            return null;
        }

        if (this.uiRoot == null)
        {
            this.uiRoot = GameObject.Find(UI_ROOT_NAME);
            if (this.uiRoot == null)
            {
                this.uiRoot = new GameObject();
                this.uiRoot.name = UI_ROOT_NAME;
            }
        }

        var obj = GameObject.Instantiate(res);
        obj.transform.SetParent(this.uiRoot.transform);

        return obj.GetComponent<UIController>();
    }

    public void Release(UIController controller)
    {
        GameObject.Destroy(controller.gameObject);
    }
}