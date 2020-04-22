using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private string uiName;

    public string Name 
    {
        get
        {
            return uiName;
        }
    }

    public void SetUIVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public virtual void OnCreate(IUIArguments arguments)
    {
        Debug.Log($"UIController '{this.Name}' created with arguments {arguments}.");
    }

    public virtual void OnOpen(IUIArguments arguments)
    {
        Debug.Log($"UIController '{this.Name}' opened with arguments {arguments}.");
    }

    public virtual void OnClose()
    {
        Debug.Log($"UIController '{this.Name}' closed.");
    }

    public virtual void OnTop()
    {
        Debug.Log($"UIController '{this.Name}' on the top.");
    }
}
