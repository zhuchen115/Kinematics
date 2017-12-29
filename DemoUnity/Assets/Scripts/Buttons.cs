using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Kinematic;

public class Buttons : MonoBehaviour {

    public Button btnL, btnR, btnU, btnD, btnI, btnO;
    public Slider th0, th1, th2;
    public GameObject endpoint;
    public float speed;

    private Vector3F tf3d;
    private bool changed;

    // Use this for initialization
    void Start () {
        btnL.onClick.AddListener(delegate { OnBtnMoveClick(0); });
        btnR.onClick.AddListener(delegate { OnBtnMoveClick(1); });
        btnU.onClick.AddListener(delegate { OnBtnMoveClick(2); });
        btnD.onClick.AddListener(delegate { OnBtnMoveClick(3); });
        btnI.onClick.AddListener(delegate { OnBtnMoveClick(4); });
        btnO.onClick.AddListener(delegate { OnBtnMoveClick(5); });
	}
	
    void OnBtnMoveClick(int dir)
    {
        Vector3F pos = new Vector3F(endpoint.transform.position.x, endpoint.transform.position.z, endpoint.transform.position.y);
        Vector3F posnew = pos;
        switch (dir)
        {
            case 0:
                posnew = pos + new Vector3F(-speed, 0, 0);
                break;
            case 1:
                posnew = pos + new Vector3F(speed, 0, 0);
                break;
            case 2:
                posnew = pos + new Vector3F(0, 0, speed);
                break;
            case 3:
                posnew = pos + new Vector3F(0, 0, -speed);
                break;
            case 4:
                posnew = pos + new Vector3F(0, speed, 0);
                break;
            case 5:
                posnew = pos + new Vector3F(0, -speed, 0);
                break;
        }
        List<IJoint> joints = Robots3DOF.GetArticulated(2, 2, 2);
        //Transform3D base_axis = new Transform3D(new Vector3F(0, 0, 0), new Vector3F(0, 0, 0));

        tf3d = Robots3DOF.ArticulatedInverse(ref joints, posnew);
        changed = true;
       
    }

	// Update is called once per frame
	void Update () {
        if (tf3d != null)
        {
            if (tf3d.IsValid&&changed)
            {
                if (tf3d.X > 0)
                    th0.value = (float)rad2deg(tf3d.X);
                else
                    th0.value = 360 + (float)rad2deg(tf3d.X);

                th1.value = (float)rad2deg(tf3d.Y);
                th2.value = (float)rad2deg(tf3d.Z);
                changed = false;
            }
        }
	}

    private double rad2deg(double rad)
    {
        return rad / Math.PI * 180;
    }
}
