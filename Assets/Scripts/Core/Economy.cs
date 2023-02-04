using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
   
    public float Money
    {
        get => _money;
        set
        {
            _money = value;
            _moneyTextUI.text = $"Dolares : {_money}$$";
        }
        
    }

    private float _money;

    public TMPro.TextMeshProUGUI _moneyTextUI;



}
