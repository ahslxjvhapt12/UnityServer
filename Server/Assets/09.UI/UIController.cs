using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private VisualTreeAsset _lunchUIAsset; //UIÀÇ ÇÁ¸®ÆÕ

    private UIDocument _uiDocument;
    private VisualElement _contentParent;
    private LunchUI _lunchUI;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple UI Manager is running");
        }
        Instance = this;

        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        Button lunchBtn = _uiDocument.rootVisualElement.Q<Button>("LunchBtn");
        lunchBtn.RegisterCallback<ClickEvent>(OnOpenLunchHandle);
        _contentParent = _uiDocument.rootVisualElement.Q<VisualElement>("Content");
    }

    private void OnOpenLunchHandle(ClickEvent evt)
    {
        VisualElement lunchRoot = _lunchUIAsset.Instantiate().Q<VisualElement>("LunchContainer");
        _contentParent.Add(lunchRoot);
        _lunchUI = new LunchUI(lunchRoot);
    }
}
