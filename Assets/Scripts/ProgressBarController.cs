using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public UnityEvent ProgressBarUpdated;

    [SerializeField] Image progressBarFilledImage;
    [SerializeField] float filledTime = .15f;
    [Header("Optionally")]
    [SerializeField] TextMeshProUGUI progressPercentText;
    [SerializeField] string percentPrefix, percentSuffix;
    [SerializeField] TextMeshProUGUI currentAmountText;
    [SerializeField] string currentAmountPrefix, currentAmountSuffix;

    Tween currentTween;

    public void UpdateProgressBar(float _currentValue, float _maxValue)
    {
        ProgressBarUpdated?.Invoke();

        if (currentTween != null)
        {
            currentTween.Kill();
            currentTween = null;
        }

        float _fillAmount = progressBarFilledImage.fillAmount;

        currentTween = DOTween.To(() => _fillAmount, x => _fillAmount = x, Mathf.InverseLerp(0, _maxValue, _currentValue), 0.15f).OnUpdate(() =>
        {
            progressBarFilledImage.fillAmount = _fillAmount;
        });

        if (progressPercentText != null)
        {
            progressPercentText.SetText($"{percentPrefix}{_fillAmount}{percentSuffix}");
        }
        if (currentAmountText != null)
        {
            currentAmountText.SetText($"{currentAmountPrefix}{_currentValue}{currentAmountSuffix}");
        }
    }

    public void ResetProgressBar()
    {
        gameObject.SetActive(true);
        progressBarFilledImage.fillAmount = 1f;
    }
    public void HideProgressBar()
    {
        gameObject.SetActive(false);
    }
    public void ShowProgressBar()
    {
        gameObject.SetActive(true);
    }
}
