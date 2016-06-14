using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HappinessSystem : MonoBehaviour {

	public static int happiness = 60;

	InteractScript foodInteract;
	InteractScript showerInteract;

	PhoneScript phoneScript;

	float lowerHapfood = 5;
	float lowerHapShower = 5;
	float lowerHapEgo = 5;

	float raiseHapFood = 5;
	float raiseHapShower = 5;
	float raiseHapEgo = 5;

	int round = 0;

	public Slider foodSlider;
	public Slider showerSlider;
	public Slider phoneSlider;

	public GameObject papersObj;
	public GameObject billTextObj;
	public GameObject billObj;
	public GameObject signature;

	public Text billText;
	public string[] bills;

	public Camera mySky;
	public Color skyColor1;
	public Color skyColor2;
	public Color skyColor3;

	public GameObject particles1;
	public GameObject particles2;
	public GameObject particles3;

	public Sprite deadSprite;

	public AudioSource mySource;
	public AudioClip alarmClip;

	public AudioClip[] trumpClips;

	public Color startCol;
	public Color endCol;
	public SpriteRenderer skinRenderer;

	public float colChange;


	// Use this for initialization
	void Start () {
		foodInteract = GameObject.Find("Food Area").GetComponent<InteractScript>();
		showerInteract = GameObject.Find("Orange Shower").GetComponent<InteractScript>();
		phoneScript = GameObject.Find("Phone").GetComponent<PhoneScript>();
		StartCoroutine(ReduceFullness());
		StartCoroutine(ReduceOrangeness());
		StartCoroutine(ReduceEgo());
	}
	
	// Update is called once per frame
	void Update () {
		foodSlider.value = foodInteract.myValue/100;
		showerSlider.value = showerInteract.myValue/100;
		phoneSlider.value = phoneScript.myValue/100;

		Debug.Log(showerSlider.value);
		colChange = showerSlider.value;
		skinRenderer.color = Color.Lerp(endCol, startCol, colChange);

		if(foodInteract.myValue == 0 || showerInteract.myValue == 0 || phoneScript.myValue == 0){
			Debug.Log ("DEAD");
			gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
			StartCoroutine(EndGame());
		}

		Debug.Log("happniess is " + happiness);

		if(foodInteract.myValue <= 30){
			lowerHapfood -= Time.deltaTime;
			if(lowerHapfood <= 0){
				happiness--;
				lowerHapfood = 5;
			}
		} else if(foodInteract.myValue >= 60){
			raiseHapFood -= Time.deltaTime;
			if(raiseHapFood <= 0){
				happiness++;
				raiseHapFood = 5;
			}
		}
		if(showerInteract.myValue <= 30){
			lowerHapShower -= Time.deltaTime;
			if(lowerHapShower <= 0){
				happiness--;
				lowerHapShower = 5;;
			}
		} else if(showerInteract.myValue >= 60){
			raiseHapShower -= Time.deltaTime;
			if(raiseHapShower <= 0){
				happiness++;
				raiseHapShower = 5;
			}
		}
		if(phoneScript.myValue <= 30){
			lowerHapEgo -= Time.deltaTime;
			if(lowerHapEgo <= 0){
				happiness--;
				lowerHapEgo = 5;
			}
			}else if(phoneScript.myValue >= 60){
				raiseHapEgo -= Time.deltaTime;
				if(raiseHapEgo <= 0){
					happiness++;
					raiseHapEgo = 5;
				}
		}

		if(happiness >= 100){
			happiness = 50;
			papersObj.SetActive(true);
		}

		if(TouchPaper.touchedPapers){

		}
	}

	IEnumerator EndGame(){
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(0);
	}

	public void SignBill(){
		mySource.PlayOneShot(trumpClips[Random.Range(0, trumpClips.Length)]);
		billObj.SetActive(true);
		billTextObj.SetActive(true);
		papersObj.SetActive(false);
		billText.text = bills[Random.Range(0, bills.Length)];
	}
		

	IEnumerator ReduceFullness(){
		yield return new WaitForSeconds(5);
		foodInteract.myValue--;
		StartCoroutine(ReduceFullness());
	}

	IEnumerator ReduceOrangeness(){
		yield return new WaitForSeconds(5);
		showerInteract.myValue--;
		StartCoroutine(ReduceOrangeness());
	}

	IEnumerator ReduceEgo(){
		yield return new WaitForSeconds(5);
		phoneScript.myValue--;
		StartCoroutine(ReduceEgo());
	}

	public void CallNewRound(){
		StartCoroutine(NewRound());
	}

	IEnumerator NewRound(){
		Debug.Log("COME ON");
		round++;
		signature.SetActive(true);
		yield return new WaitForSeconds(3);
		if(round == 1){
			phoneScript.myValue = 50;
			showerInteract.myValue = 50;
			foodInteract.myValue = 50;
			happiness = 50;
			RoundOne();
		}
		if(round == 2){
			phoneScript.myValue = 40;
			showerInteract.myValue = 40;
			foodInteract.myValue = 40;
			happiness = 40;
			RoundTwo();
		}
		if(round == 3){
			phoneScript.myValue = 30;
			showerInteract.myValue = 30;
			foodInteract.myValue = 30;
			happiness = 30;
			RoundThree();
		}
		if(round == 4){
			mySource.PlayOneShot(alarmClip);
			StartCoroutine(EndGame());
		}
		signature.SetActive(false);
		billTextObj.SetActive(false);
		billObj.SetActive(false);
	}

	void RoundOne(){
		mySky.backgroundColor = skyColor1;
		particles1.SetActive(true);
	}

	void RoundTwo(){
		mySky.backgroundColor = skyColor2;
		particles2.SetActive(true);
	}

	void RoundThree(){
		mySky.backgroundColor = skyColor3;
		particles3.SetActive(true);
	}
		
}