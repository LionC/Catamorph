using UnityEngine;
using System.Collections;

// @NOTE the attached sprite's position should be "top left" or the children will not align properly
// Strech out the image as you need in the sprite render, the following script will auto-correct it when rendered in the game
[RequireComponent (typeof (SpriteRenderer))]

// Generates a nice set of repeated sprites inside a streched sprite renderer
// @NOTE Vertical only, you can easily expand this to horizontal with a little tweaking
public class BackgroundTiles : MonoBehaviour {
	SpriteRenderer sprite;

	void Awake () {
		// Get the current sprite with an unscaled size
		sprite = GetComponent<SpriteRenderer>();
		Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

		// Generate a child prefab of the sprite renderer
		GameObject childPrefab = new GameObject();
		SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
		childSprite.sortingOrder = sprite.sortingOrder;
		childSprite.sortingLayerID = sprite.sortingLayerID;
		childSprite.sortingLayerName = sprite.sortingLayerName;
		//childSprite.transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		childPrefab.transform.position = transform.position;
		childSprite.sprite = sprite.sprite;

		// Loop through and spit out repeated tiles
		GameObject child;
		float tilesX = sprite.bounds.size.x / spriteSize.x;
		float tilesY = sprite.bounds.size.y / spriteSize.y;
		for (float x = 0; x < tilesX; x+=0.5f) {
			for (float y = 0; y < tilesY; y+=0.5f) {
				child = Instantiate (childPrefab) as GameObject;
				child.transform.position = transform.position + (new Vector3 (spriteSize.x * ( x - (tilesX / 2)) , spriteSize.y * (y - (tilesY / 2)), 0));
				child.transform.parent = transform;
			}

		}

		// Set the parent last on the prefab to prevent transform displacement
		childPrefab.transform.parent = transform;

		// Disable the currently existing sprite component since its now a repeated image
		sprite.enabled = false;
	}
}