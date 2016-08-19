using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	public Slider chargeBar;
	public static float maxCharge = 110f;
	public float Akku = maxCharge;
	bool Strom = true;

	void start(){
		chargeBar.minValue = 0;
		chargeBar.maxValue = maxCharge;
		chargeBar.onValueChanged.AddListener (delegate {
			chargeChange ();

		});
	}

	public void verbrauch (float value){
		if (Strom)
			Akku -= value;
		chargeBar.value = Akku / maxCharge;
		Debug.Log ("setting to" + Akku);
	}
	void FixedUpdate () {
		verbrauch (0.5f);
	}
	public void chargeChange(){
		Debug.Log (chargeBar.value);
	}
}
