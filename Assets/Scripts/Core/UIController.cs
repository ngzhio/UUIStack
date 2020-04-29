using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public string Name 
    {
        get
        {
            return this.GetType().Name;
        }
    }

    public virtual void OnCreate(IUIArguments arguments)
    {
        Debug.Log($"UIController '{this.Name}' created with arguments {arguments}.");
    }

    public virtual void OnOpen(IUIArguments arguments, int depth)
    {
        Debug.Log($"UIController '{this.Name}' opened with arguments {arguments} and depth {depth}.");
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
