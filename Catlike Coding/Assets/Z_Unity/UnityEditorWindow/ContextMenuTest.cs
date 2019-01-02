using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("Context Menu Test")]
    void Test()
    {
        Debug.Log("Context Menu is called");
    }
}
