using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel Instance;

    private CanvasGroup canvasGroup;

    public TextMeshProUGUI title;
    public Image image;
    public TextMeshProUGUI description;
    public TextMeshProUGUI additional;
    public Button closeButton;

    [Space(20)]
    [Header("Data")]
    public List<ConstellationInfo> infos = new List<ConstellationInfo>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        Hide();
    }

    public void Init(string title, Sprite sprite, string description, string additional)
    {
        this.title.text = title;
        this.image.sprite = sprite;
        this.description.text = description;
        this.additional.text = additional;
    }

    public void DisplayPanel(string TriggerName)
    {
        if (infos == null || infos.Count == 0)
            return;

        foreach (ConstellationInfo info in infos)
        {
            if (info.TriggerName == TriggerName)
            {
                Init(info.title, info.sprite, info.description, info.additional);
                Show();
            }
        }
    }

    public void Show()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);
    }

    public void Hide()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, 0.5f);
    }
}

[Serializable]
public class ConstellationInfo
{
    public string TriggerName;
    public string title;
    public Sprite sprite;
    public string description;
    public string additional;
}
