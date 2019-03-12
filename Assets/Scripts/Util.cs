using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace healthHack
{
    public class Util : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);            
        }

        public void Exit()
        {
            Application.Quit();
        }
    }

}
