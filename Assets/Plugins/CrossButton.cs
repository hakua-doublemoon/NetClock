//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CrossButton : MonoBehaviour
{
    public void on_click()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }
}
