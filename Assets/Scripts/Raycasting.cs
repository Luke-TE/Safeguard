using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        // this code show nameobject with click   
        if (Input.GetMouseButtonDown(0))
        {            
            //empty RaycastHit object which raycast puts the hit details into             
            //ray shooting out of the camera from where the mouse is
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //print out the name if the raycast hits something
                Debug.Log(hit.collider.name);
                gameObject.SendMessage("updateStats", hit.collider.name);
                //freeclup= hit.collider.name;
            }
        }
    }
}
