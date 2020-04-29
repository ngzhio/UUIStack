using UnityEngine;

/// <summary>
/// The agent to handle the depth of UIs.
/// </summary>
public interface IUIDepthAgent
{
    void SetDepth(GameObject gameObject, int depth);
}
