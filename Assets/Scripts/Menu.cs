using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if(UNITY_EDITOR)
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    public void StartNew()
    {
        GameManagement.Instance.GetName();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        #if(UNITY_EDITOR)
        {
            GameManagement.Instance.Save();
            EditorApplication.ExitPlaymode();
        }
        #else
        {
            GameManagement.Instance.Save();
            Application.Quit();
        }
        #endif
    }
}
