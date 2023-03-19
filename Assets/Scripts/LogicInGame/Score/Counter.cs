using System;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] 
    private World _world;

    [SerializeField] 
    private TMP_Text _textCounter;

    private void OnCollisionEnter(Collision collision)
    {
        Coin coin = collision.gameObject.GetComponent<Coin>();
        if (coin != null)
        {
            _world.GeneralData.Score++;
            _textCounter.text = _world.GeneralData.Score.ToString();
            
            Destroy(collision.gameObject);
        }
    }
}
