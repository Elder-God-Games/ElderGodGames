using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScrennFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Initiate.Fade("SplashScreen", Color.black, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
