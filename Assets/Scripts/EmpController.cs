using UnityEngine;
using System.Collections;

public class EmpController: MonoBehaviour {
	private GameObject player;  //Reference to Cat GameObject
	private CatBehaviour catBehavior;  //Referece to CatBehavior
	private MixerCatController mixerCatController;  //Reference to MixerCatController
	public float batteryDrainPerSecond = 5f;  //BatteryDrainPerSecond
	private bool triggered = false;  //Player triggered Emp by moving through


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");  //Initialization of Cat GameObject
		catBehavior = player.GetComponent<CatBehaviour> ();  //Initalization of CatBehavior;
		mixerCatController = player.GetComponent<MixerCatController> ();  //Initalization of MixerCatController;
	}

	void FixedUpdate() {
		if (triggered && mixerCatController.batteryCurrent > 0)  //Emp is triggered
			mixerCatController.batteryCurrent = mixerCatController.batteryCurrent - (batteryDrainPerSecond / 50f);  //Reduce batteryLevel by 1/50 of perSecond value
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && catBehavior.currentAbility != null && catBehavior.currentAbility.ToString() == "MixerCat")  //Triggering object must be MixerCat
			triggered = true;  //Emp is triggered
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player")  //Triggering object must be Cat
			triggered = false;  //AbilitySpot isn't triggered any more after leaving it
	}
	
	public bool hitCatEmp() {
		return triggered;
	}
}
