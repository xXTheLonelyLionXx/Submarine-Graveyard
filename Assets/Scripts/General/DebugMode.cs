using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMode : MonoBehaviour {

    private bool _isEnabled = false;

    private bool _infiniteTime;
    private bool _infiniteAmmo;
    private bool _infiniteHealth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ApplyDebug(_infiniteTime, _infiniteAmmo, _infiniteHealth);

        if (_isEnabled)
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                _infiniteTime = ToggleDebugs(_infiniteTime, "InfiniteTime", "F2");
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                _infiniteAmmo = ToggleDebugs(_infiniteAmmo, "InfiniteAmmo", "F3");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                _infiniteHealth = ToggleDebugs(_infiniteHealth, "InfiniteHealth", "F4");
            }
        }
    }

    private void ApplyDebug(bool time, bool ammo, bool health)
    {
        GameObject player = GameObject.Find("Submarine_Player");

        if (time)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SetTimeSceneStart(Time.time);
        }
        if (ammo)
        {
            player.GetComponent<PlayerController>().SetAmmo(player.GetComponent<PlayerController>().MaxAmmo);
        }
        if (health)
        {
            player.GetComponent<PlayerController>().SetHp(player.GetComponent<PlayerController>().MaxHp);
        }
    }

    public void ToggleDebugMode()
    {
        switch(_isEnabled)
        {
            case true:
                _isEnabled = false;
                _infiniteTime = false;
                _infiniteAmmo = false;
                _infiniteHealth = false;
                gameObject.GetComponent<UnityEngine.UI.Text>().text = "";
                GameObject.Find("InfiniteTime").GetComponent<UnityEngine.UI.Text>().text = "";
                GameObject.Find("InfiniteAmmo").GetComponent<UnityEngine.UI.Text>().text = "";
                GameObject.Find("InfiniteHealth").GetComponent<UnityEngine.UI.Text>().text = "";
                break;

            case false:
                _isEnabled = true;
                gameObject.GetComponent<UnityEngine.UI.Text>().text = "F1 Debug Mode ON";
                GameObject.Find("InfiniteTime").GetComponent<UnityEngine.UI.Text>().text = "F2 InfiniteTime OFF";
                GameObject.Find("InfiniteAmmo").GetComponent<UnityEngine.UI.Text>().text = "F3 InfiniteAmmo OFF";
                GameObject.Find("InfiniteHealth").GetComponent<UnityEngine.UI.Text>().text = "F4 InfiniteHealth OFF";
                break;
        }
    }

    private bool ToggleDebugs(bool mod, string textbox, string key)
    {
        switch (mod)
        {
            case true:
                mod = false;
                GameObject.Find(textbox).GetComponent<UnityEngine.UI.Text>().text = key + " " + textbox + " OFF";
                break;

            case false:
                mod = true;
                GameObject.Find(textbox).GetComponent<UnityEngine.UI.Text>().text = key + " " + textbox + " ON";
                break;
        }

        return mod;
    }
}
