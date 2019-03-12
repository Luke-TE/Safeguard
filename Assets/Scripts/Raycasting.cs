using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace healthHack
{
    public class Raycasting : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    gameObject.SendMessage("SelectCity", hit.collider.name);                    
                    gameObject.SendMessage("UpdatePopulationStats");
                }
            }
        }
    }

}