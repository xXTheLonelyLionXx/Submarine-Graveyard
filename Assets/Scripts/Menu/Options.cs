using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Options : MonoBehaviour {
	public Slider Music_Bar;
	public Slider SFX_Bar;
	public AudioMixer MasterMixer;
	public float Music_Vol;
	public float SFX_Vol;

	private bool _music_active;
	private bool _sfx_active;

	// Use this for initialization
	void Start () {
		MasterMixer.GetFloat("BGM_Vol", out Music_Vol);
		Music_Bar.value = Music_Vol+80;
		MasterMixer.GetFloat("SFX_Vol", out SFX_Vol);
		SFX_Bar.value = SFX_Vol+80;
		_music_active = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Control

		if (_music_active == true) {
			if (Input.GetKey ("down") && Music_Bar.value >= 0) {
				Music_Bar.value -= 1;
			}
			if (Input.GetKey ("up") && Music_Bar.value <= 80) {
				Music_Bar.value += 1;
			}
			if (Input.GetKeyDown ("escape")) {
				SceneManager.LoadScene ("Menu");
			}
			if (Input.GetKeyDown ("right")) {
				_music_active = false;
				_sfx_active = true;
			}
			//Change Mixer
			MasterMixer.SetFloat ("BGM_Vol", Music_Bar.value - 80);
		}
		if (_sfx_active == true) {
			if (Input.GetKey ("down") && SFX_Bar.value >= 0) {
				SFX_Bar.value -= 1;
			}
			if (Input.GetKey ("up") && SFX_Bar.value <= 80) {
				SFX_Bar.value += 1;
			}
			if (Input.GetKeyDown ("escape")) {
				SceneManager.LoadScene ("Menu");
			}
			if (Input.GetKeyDown ("left")) {
				_music_active = true;
				_sfx_active = false;
			}
			//Change Mixer
			MasterMixer.SetFloat ("SFX_Vol", SFX_Bar.value - 80);
		}
	}
}
