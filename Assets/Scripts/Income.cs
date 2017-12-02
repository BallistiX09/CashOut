using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Income
{
    public string name;
    public int monthlyAmount;

    public Income(string name, int monthlyCost)
    {
        this.name = name;
        this.monthlyAmount = monthlyCost;
    }
}
