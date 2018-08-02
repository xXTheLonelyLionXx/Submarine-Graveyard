using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteChanger))]
public class HealthUI : MonoBehaviour {

    private SpriteChanger _spriteChanger;

    void Awake()
    {
        _spriteChanger = GetComponent<SpriteChanger>();
    }

    void OnEnable()
    {
        PlayerController.OnDamageTaken += InvokeSpriteChanger;
    }

    void OnDisable()
    {
        PlayerController.OnDamageTaken -= InvokeSpriteChanger;
    }

    private void InvokeSpriteChanger(float amount)
    {
        _spriteChanger.UpdateSprites(amount);
    }
}
