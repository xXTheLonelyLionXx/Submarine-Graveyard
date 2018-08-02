using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteChanger))]
public class CraftingUI : MonoBehaviour {

    private SpriteChanger _spriteChanger;

    void Awake()
    {
        _spriteChanger = GetComponent<SpriteChanger>();
    }

    void OnEnable()
    {
        PlayerController.OnMaterialGathered += InvokeSpriteChanger;
        PlayerController.OnCrafting += InvokeSpriteChanger;
    }

    void OnDisable()
    {
        PlayerController.OnMaterialGathered -= InvokeSpriteChanger;
        PlayerController.OnCrafting -= InvokeSpriteChanger;
    }

    private void InvokeSpriteChanger(float amount)
    {
        _spriteChanger.UpdateSprites(amount);
    }
}
