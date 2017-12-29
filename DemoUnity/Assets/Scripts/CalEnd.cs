using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kinematic;

public class CalEnd : MonoBehaviour {

    public Slider th1, th2, th3;
    public Text txtCalPos;
    private List<IJoint> joints;

    
    // Use this for initialization
    void Start () {
        joints = Robots3DOF.GetArticulated(2, 2, 2);
        
    }
	
	// Update is called once per frame
	void Update () {
        Transform3D base_axis = new Transform3D(new Vector3F(0, 0, 0), new Vector3F(0, 0, 0));
        Transform3D tf3d = base_axis;
        joints[0].Transform = new Vector3F(0, 0, deg2rad(th1.value));
        joints[1].Transform = new Vector3F(deg2rad(th2.value), 0, 0);
        joints[2].Transform = new Vector3F(deg2rad(th3.value), 0, 0);
        foreach (IJoint jo in joints)
        {
            tf3d = jo.GetEndPointState(tf3d);
        }
        txtCalPos.text = System.String.Format("({0:G8},{1:G8},{2:G8})", tf3d.Position.X, tf3d.Position.Z, tf3d.Position.Y);
    }

    private double deg2rad(double deg)
    {
        return deg / 180 * System.Math.PI;
    }
}
