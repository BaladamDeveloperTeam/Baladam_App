using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Box
{
    private double cost;
    private double time;
    private string[] feature;
    private Delivery delivery;
}

[System.Serializable]
public class Delivery
{
    private double cost;
    private int repeat;
}
