using System;
using Abilities;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] 
    private GameObject _textPrefab;

    [SerializeField] 
    private Health _damage;

    [SerializeField] 
    private float _speedText = 2;
    [SerializeField] 
    private float _timeDisableText = 1;

    private void Start()
    {
        _damage.DoDamage += CreateText;
    }

    private void CreateText(float text)
    {
        CreateText(text.ToString());
    }
    
    private void CreateText(string text)
    {
        GameObject textGO = Instantiate(_textPrefab, transform);
        textGO.GetComponentInChildren<TMP_Text>().text = text;
        textGO.AddComponent<DisableText>()
            .Construct(_speedText, _timeDisableText);
        textGO.SetActive(true);
    }
}