using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace healthHack
{
    public class Actions : MonoBehaviour
    {        
        private City currentCity;

        public Text LastActionText;
        public InputField PercentInput;
        public Board Board;
      
        public void DoAction(string action)
        {                   
            currentCity = Board.CurrentCityTransform?.gameObject.GetComponent<City>();

            if (currentCity != null)
            {
                int.TryParse(PercentInput.text, out int percentage);                
                percentage = Mathf.Clamp(percentage, 0, 100);
                
                SwitchAction(action, percentage);            
                LastActionText.text = string.Format("{0}: {1} by {2}%.", action, currentCity.name, percentage);
            }
        }        

        private void SwitchAction(string action, int percentage = 0)
        {                                   
            switch (action)
            {
                case "Delete Path":
                    break;
                case "Vaccinate City":
                    currentCity.GetModel().SetProportionVaccinated((float)percentage / 100);
                    break;
                case "Isolate City":
                    break;
                case "Introduce Drug Treatment":
                    currentCity.GetModel().SetProportionTreated((float)percentage / 100);
                    break;
                default:
                    throw new Exception("The action (" + action + ") is not supported");
            }

        }
    }
}
