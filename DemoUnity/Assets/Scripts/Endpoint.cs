using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Endpoint : MonoBehaviour {

    public GameObject endPoint;

    private Text txtEndpoint;

	// Use this for initialization
	void Start () {
        txtEndpoint = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = endPoint.transform.position;

        txtEndpoint.text = System.String.Format("({0},{1},{2})", pos.x, pos.y, pos.z);
	}
}
