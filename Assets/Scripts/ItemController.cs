using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	public Sprite rocketPack;
	public Sprite burner;
	public Sprite mixer;
	private GameObject player;
	private SpriteRenderer spriteRenderer;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void SwitchItem(string ability) {
		if (ability == "RocketCat")
			spriteRenderer.sprite = rocketPack;
		else if (ability == "BurnerCat")
			spriteRenderer.sprite = burner;
		else if (ability == "mixerCat")
			spriteRenderer.sprite = mixer;

		Debug.Log (ability);
	}
}
