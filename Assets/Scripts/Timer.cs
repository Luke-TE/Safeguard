using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace healthHack
{
    public class Timer : MonoBehaviour
    {
        public int TimePassed;
        private float elapsedTime;
        private int secondsPerDay;
        public Text timeText;
        public Board board;

        // Start is called before the first frame update
        void Start()
        {
            TimePassed = 0;
            elapsedTime = 0;
            secondsPerDay = 1;
            timeText.text = "Time: " + TimePassed + "Days";
        }

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > secondsPerDay)
            {
                elapsedTime -= secondsPerDay;
                TimePassed++;
    board.refresh();

            }

            timeText.text = "Time: " + TimePassed + " Days";
        }
    }

}
