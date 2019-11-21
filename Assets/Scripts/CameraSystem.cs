using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    private GameObject player;
    public float xMin = -5;
    public float yMin = 0;
    public float xMax = 20;
    public float yMax = 5;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

	void LateUpdate () {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax) + 2;
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
