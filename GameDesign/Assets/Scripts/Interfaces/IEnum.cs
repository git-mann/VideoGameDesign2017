using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IEnum
{
    public enum StatUpgrades
    {
        maxH, loss
    }
    public enum ShipUpgrades
    {
        speed, capacity, effeciency
    }
    public static readonly int[] Costs = { 100, 188, 479, 1000 };
}
