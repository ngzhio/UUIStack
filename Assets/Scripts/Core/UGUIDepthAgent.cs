using UnityEngine;

public class UGUIDepthAgent : IUIDepthAgent
{
    public void SetDepth(GameObject gameObject, int depth)
    {
        var canvas = gameObject.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = depth;
        }
    }
}
