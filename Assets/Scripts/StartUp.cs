using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


[InitializeOnLoad]
public class StartUp : MonoBehaviour
{
    static StartUp()
    {
        Debug.Log("Up and Running");
        //SceneManager.LoadScene("Scenes/Menu.unity");
        //EditorSceneManager.OpenScene("Scenes/Menu.unity");
    }


    //// Start is called before the first frame update
    //void Start()
    //{
    //    //GameObject[] Cities = new 
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
