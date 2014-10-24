using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CashDrawer : MonoBehaviour {
    public Cash billPrefab;
    public Cash coinPrefab;
    public RegisterScreen register;

    private List<Transform> billLocations = new List<Transform>();
    private List<Transform> coinLocations = new List<Transform>();

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            if (child.name == "BillLocation")
                billLocations.Add(child);
            else if (child.name == "CoinLocation")
                coinLocations.Add(child);
        }
	}


	
	// Update is called once per frame
	void Update () {
	
	}

    public void setCashValues(List<int> values)
    {
        int billCount = 0;
        int coinCount = 0;

        foreach (int value in values)
        {
            Cash cash = null;
            if (value >= 100) {
                if (billCount >= billLocations.Count) continue;
                cash = (Cash) Instantiate(billPrefab, billLocations[billCount].position, billLocations[billCount].rotation);  
                billCount++;
            }
            else {
                if (coinCount >= coinLocations.Count) continue;
                cash = (Cash)Instantiate(coinPrefab, coinLocations[coinCount].position, coinLocations[coinCount].rotation);
                coinCount++;
            }
            cash.setAmount(value);
            cash.register = register;
            cash.transform.parent = transform;
        }
    }

    public void removeCash()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Cash>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
