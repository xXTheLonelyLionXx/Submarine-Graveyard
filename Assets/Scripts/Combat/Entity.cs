using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity: MonoBehaviour {

    private DamageEffect _dmgEffect;
    private SpriteChanger _spriteChanger;

    protected virtual void Awake()
    {
        _dmgEffect = GetComponent<DamageEffect>();
        _spriteChanger = GetComponent<SpriteChanger>();
    }

    protected void InvokeDamageEffect()
    {
        _dmgEffect.InvokeEffect();
    }

    protected void InvokeSpriteChanger(float amount)
    {
        _spriteChanger.UpdateSprites(amount);
    }

}
