using UnityEngine;
using System.Collections;

public class TouchPaper : MonoBehaviour {

	public static bool touchedPapers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		touchedPapers = true;
		GameObject.Find("Game Manager").GetComponent<HappinessSystem>().SignBill();
	}
}
