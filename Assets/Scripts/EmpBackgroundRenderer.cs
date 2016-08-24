using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmpBackgroundRenderer : MonoBehaviour {

	private EmpController empController;

	void Start() {
		empController = GameObject.FindGameObjectWithTag("EMP").GetComponent<EmpController> ();
		gameObject.GetComponent<Image> ().color = new Color32 (0,0,0,0);
	}

	void FixedUpdate () {		
		if (empController.hitCatEmp())
			gameObject.GetComponent<Image> ().color = new Color32 (0,0,0, 180);
		else
			gameObject.GetComponent<Image> ().color = new Color32 (0,0,0,0);
	}
}
