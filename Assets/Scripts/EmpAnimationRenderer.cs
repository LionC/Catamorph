using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmpAnimationRenderer : MonoBehaviour {

	private EmpController empController;

	void Start() {
		empController = GameObject.FindGameObjectWithTag("EMP").GetComponent<EmpController> ();
		gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,0);
	}

	void FixedUpdate () {		
		if (empController.hitCatEmp()) {
			GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
			gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,255);
		} 
		else
			gameObject.GetComponent<Image> ().color = new Color32 (255,255,255,0);
	}
}
