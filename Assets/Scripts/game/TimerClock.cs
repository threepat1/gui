using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimerClock : MonoBehaviour {
    //time in float to be converted to clock time
    public float timer;
    //displayable clocktime
    public string clockTime;
    // how the front looks
    public GUIStyle text;

    public DateTime time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        time = DateTime.Now;

        if (timer != 0)
        {

            timer -= Time.deltaTime;
        }
        if(timer < 0)
        {
            timer = 0;
        }
	}
    private void OnGUI()
    {
        int mins = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - mins * 60);
        clockTime = string.Format("{0:0}:{1:00}", mins, seconds);
        GUI.Label(new Rect(10,10,250,100), clockTime, text);
        GUI.Label(new Rect(10, 200, 250, 100), time.Hour + ":" + time.Minute + ":"+ time.Second, text);

    }
}
