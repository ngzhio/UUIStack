using UnityEngine;

public class SceneHome : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.Init(
            new DefaultUIResourceAgent("Prefabs/UI"),
            new UGUIDepthAgent()
        ).Open(UINames.UIHome);
    }
}