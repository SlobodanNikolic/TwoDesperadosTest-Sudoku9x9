using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickPlaySound : MonoBehaviour {

	public AudioSource soundSource;
	public AudioClip soundClip;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
        soundSource = GetComponent<AudioSource> ();
		soundClip = soundSource.clip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		soundSource.PlayOneShot (soundClip);
	}
}
