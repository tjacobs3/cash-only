using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public GUIText timerText;
	private float timeLeft;
	private bool timerStarted = false;

	public delegate void TimeEndedAction();
	public static event TimeEndedAction TimerEnded;

	// Use this for initialization
	void Start () {
		timerText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(timeLeft > 0f) {
			timeLeft -= Time.deltaTime;
			timerText.text = timeLeft.ToString("F0");
		} else if(timerStarted) {
			if(TimerEnded != null) {
				TimerEnded();
				timerText.text = "";
			}
		}
	}

	public void startTimer(float amount){
		timeLeft = amount;
		timerStarted = true;
	}
}
