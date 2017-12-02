using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Debit
{
    //Contains every image type for each matching event category
    public enum Category { TRANSPORT, ENTERTAINMENT, FINANCIAL, WORK, GAMBLING, SOCIAL, TECHNOLOGY, GAMING, SHOPPING, FOOD, EDUCATION };

    public string name;
    public Category debitCategory;
    public int monthlyCost;

    public Debit(string name, Category debitCategory, int monthlyCost)
    {
        this.name = name;
        this.debitCategory = debitCategory;
        this.monthlyCost = monthlyCost;
    }
}
