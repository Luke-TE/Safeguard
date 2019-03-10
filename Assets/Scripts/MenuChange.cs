using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace healthHack
{
    public class MenuChange : MonoBehaviour
    {
        public void changemenuscene(string scenename)
        {
            Application.LoadLevel(scenename);
        }
    }

}
