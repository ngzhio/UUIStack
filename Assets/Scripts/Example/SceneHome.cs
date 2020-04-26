using UnityEngine;

public class SceneHome : MonoBehaviour
{
    void Start()
    {
        UIStack.GetInstance().Init(new DefaultUIResourceDelegate("Prefabs/UI"))
               .Open("UIHome");
    }
}