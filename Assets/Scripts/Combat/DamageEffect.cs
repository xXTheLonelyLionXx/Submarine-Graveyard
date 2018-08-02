using System.Collections;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class DamageEffect : MonoBehaviour
{
    public float EffectTime;
    public AnimationCurve Curve;

    private SpriteRenderer _rend;
    private float _timer;
    private Color _defaultColor;
    private Coroutine _effectRoutine;

    void Awake()
    {
        _rend = GetComponent<SpriteRenderer>();
        _defaultColor = _rend.color;
    }

    public void InvokeEffect()
    {
        if(_effectRoutine != null)
        {
            StopCoroutine(_effectRoutine);
        }
        _effectRoutine = StartCoroutine(DmgEffect());
    }

    private IEnumerator DmgEffect()
    {
        _timer = 0;
        while(_timer <= EffectTime)
        {
            float a = _timer / EffectTime;
            float d = Curve.Evaluate(a);

            Color c = new Color(_defaultColor.r, _defaultColor.g * (1-d), _defaultColor.b * (1-d));

            _rend.color = c;

            _timer += Time.deltaTime;
            yield return null;
        }
        _effectRoutine = null;
    }

}
