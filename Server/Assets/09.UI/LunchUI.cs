using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class LunchUI
{
    private TextField _dateTextField;
    private Label _lunchLabel;
    private VisualElement _root;
    public LunchUI(VisualElement root)
    {
        _root = root;
        _dateTextField = root.Q<TextField>("DateTextField");
        root.Q<Button>("LoadBtn").RegisterCallback<ClickEvent>(OnLoadButtonHandle);
        root.Q<Button>("CloseBtn").RegisterCallback<ClickEvent>(OnCloseButtonHandle);
        _lunchLabel = root.Q<Label>("LunchLabel");
    }

    private void OnLoadButtonHandle(ClickEvent evt)
    {
        string dateStr = _dateTextField.value;
        Regex regex = new Regex(@"20[0-9]{2}[0-1][0-9][0-3][0-9]");
        if (!regex.IsMatch(dateStr))
        {
            Debug.LogError("올바르지 않아요. 숫자 8자리로 입력하세요");
            return;
        }

        NetworkManager.Instance.GetRequest("lunch", $"?date={dateStr}", (type, json) =>
        {
            if (type == MessageType.SUCCESS)
            {
                LunchVO vo = JsonUtility.FromJson<LunchVO>(json);
                string menuStr = vo.menus.Aggregate("", (sum, x) => sum + x + "\n");


                _lunchLabel.text = menuStr;
            }
            else
            {
                Debug.LogError(json);
            }
        });
    }

    private void OnCloseButtonHandle(ClickEvent evt)
    {
        _root.RemoveFromHierarchy();
    }
}
