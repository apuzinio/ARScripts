using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour {

    public float speed = 4f;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

    public void ToggleActive() {
        bool isActive = gameObject.activeSelf;
        if (isActive) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }

	// Update is called once per frame
	void Update () {
        var startRange = 2f;    //your chosen start value
        var endRange = 4f;    //your chose end value
        var oscilationRange = (endRange - startRange) / 2;
        var oscilationOffset = oscilationRange + startRange;
        float size = oscilationOffset + oscilationRange * Mathf.Sin(speed * Time.time);
        Debug.Log("size: " + size);
        transform.localScale = new Vector3(size, size, size);
	}
}
