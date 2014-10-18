using UnityEngine;
using System.Collections;

public class Cash : MonoBehaviour {
	public int amount = 100;
	public RegisterScreen register;
	public TextMesh amountText;

	// Use this for initialization
	void Start () {
		if(amount >= 100) {
			amountText.text = (amount / 100f).ToString("#");
		} else {
			amountText.text = amount.ToString();
		}

		amountText.renderer.sortingLayerID = 1;
		amountText.renderer.sortingOrder = 1;
		renderer.sortingLayerID = 0;
		renderer.sortingOrder = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		if(register != null) {
			register.payoutAmount(amount);
			animation.Stop();
			animation.Play();
		}
	}
}
