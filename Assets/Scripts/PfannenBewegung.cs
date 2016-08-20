using UnityEngine;
using System.Collections;

public class PfannenBewegung : MonoBehaviour {
	private float timeLeft=0f;
	private Rigidbody2D rigidbodyComponent;
	public bool SetActive=false;
	private bool SetActive2=false;
	public int Time_until_Drop=1;
	public int Time_until_disapear=3;
	private Vector2 Move;

	void Start () {
		Move = new Vector2 (0, -1.0f);
		rigidbodyComponent = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		if (SetActive == true) {
			timeLeft += Time.deltaTime;
			rigidbodyComponent.rotation = Mathf.Sin ((timeLeft) * (Mathf.PI / 180) * 100) * 8;
			StartCoroutine(Time_until_Dropfunc());
		}
		if (SetActive2 == true) {
			rigidbodyComponent.rotation = 0;
			rigidbodyComponent.isKinematic = false;
			StartCoroutine(Time_until_Dropfunc2());
		}
	}

	IEnumerator Time_until_Dropfunc()
	{
		yield return new WaitForSeconds (Time_until_Drop);
			SetActive = false;
			SetActive2 = true;
	}

	IEnumerator Time_until_Dropfunc2()
	{
		yield return new WaitForSeconds (Time_until_disapear);
		Destroy (gameObject);
	}
}
