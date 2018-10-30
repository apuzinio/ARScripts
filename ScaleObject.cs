using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class ScaleObject : MonoBehaviour {

    public GameObject prefab;
    public Vector3 offset;
    public Slider scaleSlider;

    private GameObject clone;
    private Vector3 prefabScale;

    public void scaleObject() {
        float v = scaleSlider.value;
        float vs = 1f + (v - 0.5f);
        clone.transform.localScale = new Vector3(vs * prefabScale.x,
                                                 vs * prefabScale.y,
                                                 vs * prefabScale.z);
    }

	// Use this for initialization
	void Start () {
        clone = Instantiate(prefab);
        //Fetch the Collider from the GameObject
        Collider m_Collider = clone.GetComponent<Collider>();
        //Fetch the size of the Collider volume
        Vector3 m_Size = m_Collider.bounds.size;

        //Reposition control box based on prefab size
        transform.position = new Vector3(0.5f * m_Size.x + offset.x, 
                                         0.5f * m_Size.y + offset.y, 
                                         0.5f * m_Size.z + offset.z);

        //Store prefab scale for later use
        prefabScale = clone.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 relativePos = Camera.main.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(-relativePos);
        transform.rotation = rotation;
    }
}
