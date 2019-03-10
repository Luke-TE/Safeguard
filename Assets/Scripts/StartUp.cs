using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace healthHack
{
//    [InitializeOnLoad]
    public class StartUp : MonoBehaviour
    {
        static StartUp()
        {
            Debug.Log("YEET");
            Screen.SetResolution(1280, 720, true);
        }
    }

}