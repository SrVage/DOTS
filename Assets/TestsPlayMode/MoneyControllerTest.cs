using System.Collections;
using System.Collections.Generic;
using Code.UI;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MoneyControllerTest
{
    private bool _sceneLoaded;
    private MoneyController _moneyController;
    private const string MoneyKey = "Money";
    private const int ChangeMoneyValue = 100;
    
    [UnityTest]
    public IEnumerator MoneyControllerChangeAndSaveMoney()
    {
        _moneyController = new GameObject().AddComponent<MoneyController>();
        yield return new WaitForSeconds(1);
        int moneyBeforeChange = PlayerPrefs.GetInt(MoneyKey);
        _moneyController.ChangeMoney(ChangeMoneyValue);
        int moneyAfterChange = PlayerPrefs.GetInt(MoneyKey);
        UnityEngine.Assertions.Assert.AreEqual(moneyBeforeChange + ChangeMoneyValue, moneyAfterChange);
    }

    [UnityTest]
    public IEnumerator MoneyControllerChangeUIText()
    {
        var text = new GameObject().AddComponent<TextMeshProUGUI>();
        _moneyController.MoneyText = text;
        yield return new WaitForSeconds(1);
        _moneyController.ChangeMoney(ChangeMoneyValue);
        int moneyInPlayerPrefs = PlayerPrefs.GetInt(MoneyKey);
        int moneyInUI = int.Parse(text.text);
        UnityEngine.Assertions.Assert.AreEqual(moneyInPlayerPrefs, moneyInUI);
    }
}
