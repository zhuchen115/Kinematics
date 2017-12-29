using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sldTh2 : MonoBehaviour {

    public GameObject goJoint;
    public Slider slider;
    public Text txtAngle;
    private float angle;


    // Use this for initialization
    void Start () {
        slider.onValueChanged.AddListener(delegate { OnValueChanged(1); });
    }

    void OnValueChanged(int jnum)
    {

        angle = slider.value;
        goJoint.transform.localEulerAngles = new Vector3(angle, 0, 0);
        txtAngle.text = angle.ToString();
    }

    // Update is called once per frame
    void Update () {

        

    }
}
