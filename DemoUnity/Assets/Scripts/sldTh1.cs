using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sldTh1 : MonoBehaviour {

    public GameObject goJoint;
    public Slider slider;
    public Text txtAngle;
    private float angle;

    // Use this for initialization
	void Start () {
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
	}

    void OnValueChanged()
    {
        angle = slider.value;
        goJoint.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        txtAngle.text = angle.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
        
        
	}
}
