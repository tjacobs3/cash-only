using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RegisterScreen : MonoBehaviour {
	public GUIText screenText;

	public GameController gameController;

	protected List<int> changePaid = new List<int>();
	protected int owed = 565;
	protected int paid = 600;
	private bool drawOpen = false;

	// Update is called once per frame
	void Update () {

	}

	public void setAmountOwed(int amount) {
		owed = amount;
		amountsUpdated(false);
	}

	public void setAmountPaid(int amount) {
		paid = amount;
		amountsUpdated(false);
	}

	public void payoutAmount(int amount) {
		changePaid.Add(amount);
		amountsUpdated();
	}

	public void resetChangePaid() {
		changePaid.Clear ();
		amountsUpdated(false);
	}

	public void openDrawer() {
		drawOpen = true;
	}

	public void closeDrawer() {
		drawOpen = false;
	}

	public void setScreenText(string s) {
		screenText.text = s;
	}

	private void amountsUpdated(bool alertController=true) {
		if(drawOpen) {
			string displayText = "";
			if(screenText != null) {
				displayText += "Amount Paid: " + (paid / 100f) + "\n";
				displayText += "Amount Owed: " + (owed / 100f) + "\n";
				displayText += "Change: " + (sumList(changePaid) / 100f).ToString("#.00") + "\n";
				screenText.text = displayText;
			}
			if(alertController)
				gameController.checkWinCondition(paid, owed, changePaid);
		}
	}

	public static float sumList(List<int> list) {
		int sum = 0;
		foreach(int num in list) {
			sum += num;
		}
		return sum;
	}
}
