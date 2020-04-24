using UnityEngine;

public class UGUIController : UIController
{
    public override void SetDepth(int depth)
    {
        base.SetDepth(depth);

        var canvas = gameObject.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = depth;
        }
    }
}