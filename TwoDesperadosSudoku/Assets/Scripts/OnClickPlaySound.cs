using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickPlaySound : MonoBehaviour {

	public AudioSource soundSource;
	public AudioClip soundClip;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
        Debug.Log("On click");
		soundSource.PlayOneShot (soundClip);
	}
}
