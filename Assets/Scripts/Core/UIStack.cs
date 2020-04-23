using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class UIInfo
{
    public UIController controller;
    public IUIArguments arguments;
    public bool swallow;
}

/**
 * A manager of the UI stack.
 */
public class UIStack
{
    private Stack<UIInfo> uiStack = new Stack<UIInfo>(); // Top is at the 0 index

    private IUIResourceDelegate uIResourceDelegate;

    private static UIStack instance;

    public UIStack GetInstance()
    {
        if (instance == null)
        {
            instance = new UIStack();
        }
        return instance;
    }

    private UIStack()
    {

    }

    public void Init(IUIResourceDelegate uIResourceDelegate)
    {
        this.uIResourceDelegate = uIResourceDelegate;
    }

    /**
     * Open a UI.
     *
     * @param name       The name of the UI.
     * @param arguments  The arguments passed to the UI.
     * @param swallow    Whether the UI should swallow UIs beneath it.
     * @param duplicate  Whether the UI can be duplicated.
     */
    public void Open(string name, IUIArguments arguments, bool swallow = false, bool duplicate = false)
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

    /**
     * Close the UI at the top.
     */
    public void CloseTop()
    {
        CloseTo(1);
    }

    /**
     * Close UIs from the top to the bottom, until a specific UI is at the top.
     *
     * @param name  The name of the UI that should finally be at the top.
     */
    public void CloseTo(string name)
    {
        int index = FindUIIndex(name);
        if (index >= 0)
        {
            CloseTo(index);
        }
    }

    /**
     * Close all UIs.
     */
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

            uIResourceDelegate.Release(uiInfo.controller);

            index--;
        }

        var peek = uiStack.Peek();
        if (peek != null)
        {
            SetUIVisible(peek, true);

            CheckSwallow();

            if (onOpen)
            {
                peek.controller.OnOpen(peek.arguments);
            }

            peek.controller.OnTop();
        }
    }

    private void Create(string name, IUIArguments arguments, bool swallow)
    {
        UIController controller = uIResourceDelegate.Load(name);

        var uiInfo = new UIInfo
        {
            controller = controller,
            arguments  = arguments,
            swallow    = swallow,
        };

        uiStack.Push(uiInfo);

        CheckSwallow();

        controller.OnCreate(arguments);
        controller.OnOpen(arguments);
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
        uiInfo.controller.SetUIVisible(visible);
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
