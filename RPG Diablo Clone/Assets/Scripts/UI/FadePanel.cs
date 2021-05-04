using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public float _desiredFadeDuration = 1f;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeImmediately()
    {
        _canvasGroup.alpha = 1f;
    }

    public IEnumerator IEFadeOut()
    {
        float timer = 0f;
        _canvasGroup.alpha = 1f;

        while (_canvasGroup.alpha > 0f)
        {
            timer += Time.deltaTime;
            _canvasGroup.alpha = 1 - (timer / _desiredFadeDuration);
            yield return null;
        }
    }

    public IEnumerator IEFadeIn()
    {
        float timer = 0f;
        _canvasGroup.alpha = 0f;

        while (_canvasGroup.alpha < 1)
        {
            timer += Time.deltaTime;
            _canvasGroup.alpha = timer / _desiredFadeDuration;
            yield return null;
        }
    }

}
