using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

	public int speed;

	public static bool atLocation;

	public bool moveLeft;
	public bool moveRight;
	public bool moveCenter;

	public GameObject foodObj;
	public GameObject showerObj;

	public Animator myAnim;

	public Vector3 centerPos;

	public BoxCollider2D[] allClickies;

	InteractScript foodInteract;
	InteractScript showerInteract;

	public GameObject pizza;
	public GameObject mouth;

	public GameObject suit;
	public GameObject suitArm;
	public SpriteRenderer hair;
	public SpriteRenderer leg1;

	public GameObject orangePart;

	public Image showerTime;
	bool showerTimer = false;

	public Image eatTime;
	bool eatTimer = false;

	public GameObject rightSpot;
	public GameObject leftSpot;

	bool walkRight;
	bool walkLeft;

	bool atRight;
	bool atLeft;

	int decideNum;

	public static bool canWalk = true;


	//Making the state machine class a singleton (optional, but kinda nice)
	private static StateMachine instance;
	public static StateMachine GetInstance(){
		return instance;
	}

	//Create your delegate -- the thing that will point to other functions
	private delegate void StateUpdate();
	private StateUpdate stateUpdate;

	//Declaring the enum, which is all the possible states in the game
	public enum State{
		DecideDirection,
		WalkRight,
		WalkLeft,
		WalkCenter,
		WalkShower,
		WalkEat,
		Shower,
		Eat,
		Phone
	}

	//An instance of type State that will tell us what state we are currently in
	public State currentState;

	//A SetState function that we will call to change from one state to another
	public void SetState(State newState){
		//Make our variable for holding the current state equal the new state
		currentState = newState;

		//Switch statement for all the cases of different states
		switch(newState){
		case State.DecideDirection:
			InitDecideDirectionState();
			stateUpdate = DecideDirectionStateUpdate;
			break;
		case State.WalkRight:
			//Call the function to initialize that state
			//This is kind of like a start function for moving to that state in that it
			//only gets called when you first enter that state
			InitWalkLeftState();

			//Set the delegate equal to state one's update function
			stateUpdate = WalkLeftStateUpdate;

			//You must break out of cases in switch statements
			break;

			//Do the exact same thing for the next state
		case State.WalkLeft:
			InitWalkRightState ();
			stateUpdate = WalkRightStateUpdate;
			break;
		case State.WalkCenter:
			InitWalkCenterState ();
			stateUpdate = WalkCenterStateUpdate;
			break;
		case State.WalkShower:
			InitWalkShowerState ();
			stateUpdate = WalkShowerStateUpdate;
			break;
		case State.WalkEat:
			InitWalkEatState ();
			stateUpdate = WalkEatStateUpdate;
			break;
		case State.Shower:
			InitShowerState ();
			stateUpdate = ShowerStateUpdate;
			break;
		case State.Eat:
			InitEatState ();
			stateUpdate = EatStateUpdate;
			break;
		case State.Phone:
			InitPhoneState ();
			stateUpdate = PhoneStateUpdate;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		//Set your singleton instance (this is optional, but kinda nice I think)
		instance = this;

		foodInteract = GameObject.Find("Food Area").GetComponent<InteractScript>();
		showerInteract = GameObject.Find("Orange Shower").GetComponent<InteractScript>();

		//In start, you want to set anything that will be shared between states,
		//and also set whatever state you want to start it
		SetState(State.DecideDirection);
	}

	// Update is called once per frame
	void Update () {
		//In update, you just want to do a safety check to make sure your delegate
		//points to SOME function, and if so run that the delegate every frame
		if (stateUpdate != null) {
			stateUpdate ();
		}
	}

	#region WALK LEFT STATE
	void InitWalkLeftState(){
		//MoveLeft();
	}

	void WalkLeftStateUpdate(){
		FlipLeft();
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, leftSpot.transform.position, Time.deltaTime);
		myAnim.SetBool("walk", true);
		if(gameObject.transform.position == leftSpot.transform.position){
			myAnim.SetBool("walk", false);
			SetState(State.DecideDirection);
		}
	}

	void MoveLeft(){

	}
	#endregion

	#region WALK RIGHT STATE
	void InitWalkRightState(){
		//MoveRight();
	}

	void WalkRightStateUpdate(){
		FlipRight();
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, rightSpot.transform.position, Time.deltaTime);
		myAnim.SetBool("walk", true);
		if(gameObject.transform.position == rightSpot.transform.position){
			myAnim.SetBool("walk", false);
			SetState(State.DecideDirection);
		}
	}

	void MoveRight(){

	}
	#endregion

	#region WALK CENTER STATE

	void InitWalkCenterState(){
		//MoveCenter();
	}

	void WalkCenterStateUpdate(){
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, centerPos, Time.deltaTime);
		myAnim.SetBool("walk", true);
		if(gameObject.transform.position == centerPos){
			myAnim.SetBool("walk", false);
			SetState(State.DecideDirection);
		}
	}

	void MoveCenter(){

	}

	#endregion

	#region WALK SHOWER STATE

	void InitWalkShowerState(){
		//MoveToShower();	
	}

	void WalkShowerStateUpdate(){
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, showerObj.transform.position, Time.deltaTime);
		canWalk = false;
		moveCenter = false;
		FlipLeft();
		if(gameObject.transform.position == showerObj.transform.position && !atLocation){
			atLocation = true;
			SetState(State.Shower);
		}
	}

	void MoveToShower(){

	}

	#endregion

	#region WALK EAT STATE

	void InitWalkEatState(){
		//MoveToEat();
	}

	void WalkEatStateUpdate(){
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, foodObj.transform.position, Time.deltaTime);
		canWalk = false;
		moveCenter = false;
		FlipRight();
		if(gameObject.transform.position == foodObj.transform.position && !atLocation){
			atLocation = true;
			SetState(State.Eat);
		}
	}

	void MoveToEat(){

	}

	#endregion

	#region SHOWER STATE

	void InitShowerState(){
		StartCoroutine(WaitToShower());
	}

	void ShowerStateUpdate(){
		if(showerTimer){
			showerTime.fillAmount -= Time.deltaTime/5;
		}
	}

	IEnumerator WaitToShower(){
		showerTime.enabled = true;
		showerTime.fillAmount = 1;
		orangePart.SetActive(true);
		moveLeft = false;
		suit.SetActive(false);
		suitArm.SetActive(false);
		myAnim.SetBool("shower", true);
		hair.sortingOrder = 2;
		myAnim.SetBool("walk", false);
		for(int i = 0; i < allClickies.Length; i++){
			allClickies[i].enabled = false;
		}
		showerTimer = true;
		yield return new WaitForSeconds(5);
		showerInteract.myValue += 20;
		for(int i = 0; i < allClickies.Length; i++){
			allClickies[i].enabled = true;
		}
		suit.SetActive(true);
		suitArm.SetActive(true);
		InteractScript.canClickPhone = true;
		hair.sortingOrder = -2;
		myAnim.SetBool("shower", false);
		atLocation = false;
		moveCenter = true;
		orangePart.SetActive(false);
		gameObject.transform.localScale = new Vector2 (-1, 1);
		showerTimer = false;
		showerTime.enabled = false;
		SetState(State.WalkLeft);
	}

	#endregion

	#region EAT STATE

	void InitEatState(){
		StartCoroutine(WaitToEat());
	}

	void EatStateUpdate(){
		if(eatTimer){
			eatTime.fillAmount -= Time.deltaTime/5;
		}
	}

	IEnumerator WaitToEat(){
		eatTime.enabled = true;
		eatTime.fillAmount = 1;
		moveRight = false;
		gameObject.transform.localScale = new Vector2 (1, 1);
		myAnim.SetBool("walk", false);
		myAnim.SetBool("eat", true);
		pizza.SetActive(true);
		mouth.SetActive(true);
		for(int i = 0; i < allClickies.Length; i++){
			allClickies[i].enabled = false;
		}
		eatTimer = true;
		yield return new WaitForSeconds(5);
		foodInteract.myValue += 20;
		for(int i = 0; i < allClickies.Length; i++){
			allClickies[i].enabled = true;
		}
		atLocation = false;
		InteractScript.canClickPhone = true;
		moveCenter = true;
		myAnim.SetBool("eat", false);
		pizza.SetActive(false);
		mouth.SetActive(false);
		eatTimer = false;
		eatTime.enabled = false;
		StartCoroutine(PoopSome());
		SetState(State.WalkRight);
	}

	#endregion

	#region PHONE STATE

	void InitPhoneState(){
		gameObject.transform.position = gameObject.transform.position;
		myAnim.SetBool("walk", false);
	}

	void PhoneStateUpdate(){

	}

	#endregion

	#region DECIDE DIRECTION STATE

	void InitDecideDirectionState(){
		decideNum = Random.Range(0, 20);
		myAnim.SetBool("walk", false);
	}

	void DecideDirectionStateUpdate(){
		if(decideNum == 1){
			SetState(State.WalkLeft);
		}
		if(decideNum == 2){
			SetState(State.WalkRight);
		}
		if(decideNum == 3){
			SetState(State.WalkCenter);
		}
		if(decideNum > 3){
			SetState(State.DecideDirection);
		}
	}

	#endregion

	void FlipRight(){
		//moveRight = false;
		gameObject.transform.localScale = new Vector2 (-1, 1);
		myAnim.SetBool("walk", true);
	}

	void FlipLeft(){
		//moveLeft = false;
		gameObject.transform.localScale = new Vector2 (1, 1);
		myAnim.SetBool("walk", true);
	}

	IEnumerator PoopSome(){
		Debug.Log("poop");
		int poopNum = Random.Range(1, 4);
		for(int i = 0; i <= poopNum; i++){
			yield return new WaitForSeconds (Random.Range(10, 20));
			Vector3 aPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .3f, gameObject.transform.position.z);
			Instantiate(Resources.Load("poop"), aPos, Quaternion.identity);
		}
	}

}