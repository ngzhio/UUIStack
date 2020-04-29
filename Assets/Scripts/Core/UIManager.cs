using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager of the UI stack.
/// </summary>
public class UIManager
{
    /// <summary>
    /// Top is at the 0 index
    /// </summary>
    /// <typeparam name="UIInfo"></typeparam>
    /// <returns></returns>
    private Stack<UIInfo> uiStack = new Stack<UIInfo>();

    private IUIResourceAgent uIResourceAgent;

    private IUIDepthAgent uIDepthAgent;

    private static UIManager instance;

    public static UIManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }   
    }

    private UIManager()
    {

    }

    public UIManager Init(IUIResourceAgent uIResourceAgent, IUIDepthAgent uIDepthAgent)
    {
        this.uIResourceAgent = uIResourceAgent;
        this.uIDepthAgent = uIDepthAgent;
        return this;
    }

    /// <summary>
    /// Open a UI.
    /// </summary>
    /// <param name="name">The name of the UI.</param>
    /// <param name="arguments">The arguments passed to the UI.</param>
    /// <param name="swallow">Whether the UI should swallow UIs beneath it.</param>
    /// <param name="duplicate">Whether the UI can be duplicated.</param>
    public void Open(string name, IUIArguments arguments = null, bool swallow = false, bool duplicate = false)
    {
        if (duplicate)
        {
            Create(name, arguments, swallow);
        }
        else
        {
            int index = FindUIIndex(name);
            if (index >= 0)
            {
                CloseTo(index, true);
            }
            else 
            {
                Create(name, arguments, swallow);
            }
        }
    }

    /// <summary>
    /// Close the UI at the top.
    /// </summary>
    public void CloseTop()
    {
        CloseTo(1);
    }

    /// <summary>
    /// Close UIs from the top to the bottom, until a specific UI is at the top.
    /// </summary>
    /// <param name="name">The name of the UI that should finally be at the top.</param>
    public void CloseTo(string name)
    {
        int index = FindUIIndex(name);
        if (index >= 0)
        {
            CloseTo(index);
        }
    }

    /// <summary>
    /// Close all UIs.
    /// </summary>
    public void CloseAll()
    {
        while (uiStack.Count > 0)
        {
            CloseTop();
        }
    }

    private void CloseTo(int index, bool onOpen = false)
    {
        if (index <= 0 || index >= uiStack.Count)
        {
            return;
        }

        while (index > 0)
        {
            var uiInfo = uiStack.Pop();

            uiInfo.controller.OnClose();

            uIResourceAgent.Release(uiInfo.controller);

            index--;
        }

        var peek = uiStack.Peek();
        if (peek != null)
        {
            SetUIVisible(peek, true);

            CheckSwallow();

            if (onOpen)
            {
                var depth = uiStack.Count - 1;
                this.uIDepthAgent.SetDepth(peek.controller.gameObject, depth);

                peek.controller.OnOpen(peek.arguments, depth);
            }

            peek.controller.OnTop();
        }
    }

    private void Create(string name, IUIArguments arguments, bool swallow)
    {
        UIController controller = uIResourceAgent.Load(name);
        if (controller == null)
        {
            return;
        }

        if (controller.Name != name)
        {
            Debug.LogWarning($"The name of UIController '{controller.Name}' does not match the UI name '{name}'!");
        }

        var uiInfo = new UIInfo
        {
            controller = controller,
            arguments  = arguments,
            swallow    = swallow,
        };

        uiStack.Push(uiInfo);

        CheckSwallow();

        var depth = uiStack.Count - 1;
        this.uIDepthAgent.SetDepth(controller.gameObject, depth);

        controller.OnCreate(arguments);
        controller.OnOpen(arguments, depth);
        controller.OnTop();
    }

    private void CheckSwallow()
    {
        var peek = uiStack.Peek();
        if (peek == null) return;

        var array = uiStack.ToArray();
        if (peek.swallow)
        {
            for (int i = 1; i < array.Length; i++)
            {
                SetUIVisible(array[i], false);
            }
        }
        else
        {
            for (int i = 1; i < array.Length; i++)
            {
                var ui = array[i];

                SetUIVisible(ui, true);

                if (ui.swallow)
                {
                    break;
                }
            }
        }
    }

    private void SetUIVisible(UIInfo uiInfo, bool visible)
    {
        uiInfo.controller.gameObject.SetActive(visible);
    }

    private int FindUIIndex(string name)
    {
        var array = uiStack.ToArray();
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].controller.Name == name)
            {
                return i;
            }
        }
        return -1;
    }
}
