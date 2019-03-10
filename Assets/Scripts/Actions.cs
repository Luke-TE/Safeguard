using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace healthHack
{

    public class Actions : MonoBehaviour
    {

        public Text t;
        string lastAction;
        int percentage;
        Transform cityChange;

        void setPercentage(int Percentage)
        {
            percentage = Percentage;
        }

        void setCity(Transform city)
        {
            cityChange = city;
        }

        private void Update()
        {
            t.text = lastAction + (cityChange.gameObject.GetComponent("City") as City).name + " by" + percentage;
        }


        void Complete_Action(string currentAction)
        {
            lastAction = currentAction;
            switch (currentAction)
            {
                case "Delete Path":
                    break;
                case "Vaccinate City":
                    (cityChange.gameObject.GetComponent("City") as City).getModel().setVaccine(percentage / 100);
                    break;
                case "Isolate City":
                    break;
                case "Introduce Drug Treatments":
                    (cityChange.gameObject.GetComponent("City") as City).getModel().setDrugTreatment(percentage / 100);
                    break;

            }

        }
    }
}
