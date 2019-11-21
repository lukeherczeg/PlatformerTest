using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour {

	void Update () {
        if (gameObject.transform.position.y < -7) {
            Destroy(gameObject);
        }
	}
}
