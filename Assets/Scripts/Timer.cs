using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace healthHack
{
    public class Timer : MonoBehaviour
    {
        public static int DaysPassed;
        private float elapsedTime;
        private int secondsPerDay;
        public Text timeText;
        public Board board;
        public static event EventHandler<EventArgs> DayPassed;

        void Awake()
        {
            DayPassed = null;    
        }

        // Start is called before the first frame update
        void Start()
        {                           
            DaysPassed = 0;
            elapsedTime = 0;
            secondsPerDay = 1;
            timeText.text = "Time: " + DaysPassed + "Days";
        }

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > secondsPerDay)
            {
                elapsedTime -= secondsPerDay;
                DaysPassed++;                
                DayPassed.Invoke(this, EventArgs.Empty);

            }

            timeText.text = "Time: " + DaysPassed + " Days";
        }
    }

}
