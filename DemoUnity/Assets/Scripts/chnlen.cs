using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chnlen : MonoBehaviour {

    public GameObject link1,link2,link3;
    public GameObject joint2, joint3,endpoint;
	// Use this for initialization
	void Start () {
        string[] args = System.Environment.GetCommandLineArgs();
        double[] inLinkLen = new double[3] { 2, 2, 2 };

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-link1")
            {
                try
                {
                    inLinkLen[0] = System.Double.Parse(args[i + 1]);
                }
                catch
                {
                    Debug.LogError("Start Parameter Error");

                }
            }
            if (args[i] == "-link2")
            {
                try
                {
                    inLinkLen[1] = System.Double.Parse(args[i + 1]);
                }
                catch
                {
                    Debug.LogError("Start Parameter Error");
                    inLinkLen[1] = 2;
                }
            }
            if (args[i] == "-link3")
            {
                try
                {
                    inLinkLen[2] = System.Double.Parse(args[i + 1]);
                }
                catch
                {
                    Debug.LogError("Start Parameter Error");
                    inLinkLen[2] = 2;
                }
            }
        }

        link1.transform.localScale = new Vector3(0.2f, (float)inLinkLen[0] / 2, 0.2f);
        link1.transform.localPosition = new Vector3(0, (float)inLinkLen[0] / 2, 0);
        link2.transform.localScale = new Vector3(0.2f, (float)inLinkLen[1] / 2, 0.2f);
        link2.transform.localPosition = new Vector3(0, (float)inLinkLen[1] / 2, 0);
        link3.transform.localScale = new Vector3(0.2f, (float)inLinkLen[2] / 2, 0.2f);
        link3.transform.localPosition = new Vector3(0, (float)inLinkLen[2] / 2, 0);
        joint2.transform.localPosition = new Vector3(0, (float)inLinkLen[0], 0);
        joint3.transform.localPosition = new Vector3(0, (float)inLinkLen[1], 0);
        endpoint.transform.localPosition = new Vector3(0, (float)inLinkLen[2]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
