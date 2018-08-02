using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

    public SpriteValue[] Sprites;
    public SpriteRenderer ChangeRenderer;

    public void UpdateSprites(float amount)
    {
        if(Sprites.Length <= 0)
        {
            Debug.LogWarning("No sprites assigned.");
            return;
        }

        amount = Mathf.Clamp01(amount);

        float tempF = 1;
        Sprite tempS = Sprites[0].Image;

        foreach(SpriteValue s in Sprites)
        {
            float v = Mathf.Abs(amount - s.Threshold);

            if(v <= tempF)
            {
                tempS = s.Image;
                tempF = v;
            }
        }

        ChangeRenderer.sprite = tempS;
    }

    [System.Serializable]
    public struct SpriteValue
    {
        public Sprite Image;
        [Range(0,1)]
        public float Threshold;
    }
}
