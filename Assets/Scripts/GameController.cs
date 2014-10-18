using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {
	public RegisterScreen registerScreen;
	public float timePerGame = 30f;
	public Timer timer;
	public GUIStyle buttonStyle;
	public List<int> money = new List<int>() {1, 5, 10, 25, 100, 500, 1000, 2000};

	private bool gameStarted = false;
	private int score = 0;

	// Use this for initialization
	void Start () {
		registerScreen.gameController = this;
		if(buttonStyle != null)
			buttonStyle.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnEnable()
	{
		Timer.TimerEnded += endGame;
	}
	
	
	void OnDisable()
	{
		Timer.TimerEnded -= endGame;
	}

	public void checkWinCondition(int paid, int owed, List<int> changePaid) {
		float sumChange = RegisterScreen.sumList(changePaid);
		if((paid - owed) - sumChange == 0) {
			if(isOptimalChange((paid - owed), changePaid, money)) {
				win (5);
			} else {
				win (1);
			}
		} else if(sumChange > (paid - owed)) {
			registerScreen.resetChangePaid();
		}
	}

	private void startGame() {
		registerScreen.openDrawer(); // This has to be first
		setAmountOwedAndPaid();
		registerScreen.resetChangePaid();
		gameStarted = true;
		score = 0;
		if(timer)
			timer.startTimer(timePerGame);
	}

	private void endGame() {
		gameStarted = false;
		registerScreen.closeDrawer();
		registerScreen.setScreenText("Score: " + score);
	}

	private void win(int points) {
		setAmountOwedAndPaid();
		registerScreen.resetChangePaid();
		score += points;
	}

	private bool isOptimalChange(int amount, List<int> input, List<int> referenceChange) {
		List<int> correctChange = optimalChangeGreedy(amount, referenceChange);
		referenceChange.OrderByDescending(x => x);
		input.OrderByDescending(x => x);

		return input.SequenceEqual(correctChange);
	}

	private List<int> optimalChange(int amount, List<int> coins) {
		if(amount == 0)
			return new List<int>();
		else {
			List<int> curBest = null;
			for (int i = 0; i < coins.Count; i++) {
				if(coins[i] > amount) continue;
				List<int> result = optimalChange(amount - coins[i], coins);
				if(curBest == null || (result.Count + 1) < curBest.Count) {
					curBest = result;
					curBest.Add(coins[i]);
				}
			}
			return curBest;
		}
	}

	private List<int> optimalChangeGreedy(int amount, List<int> coins) {
		List<int> change = new List<int>();

		while(amount > 0) {
			int max = 1;
			foreach(int c in coins) {
				if(c <= amount && c > max)
					max = c;
			}
			change.Add(max);
			amount -= max;
		}
		return change;
	}
	
	private void setAmountOwedAndPaid() {
		float owed = (Mathf.Round(Random.value * 10f * 100f) / 100f) + 0.01f;
		registerScreen.setAmountOwed((int)(owed * 100));
		registerScreen.setAmountPaid((int)(Mathf.Ceil(owed) * 100));
	}

	void OnGUI() {
		if (!gameStarted && GUI.Button(new Rect(10, 10, 600, 400), "Start Game!", buttonStyle))
			startGame();
	}
}
