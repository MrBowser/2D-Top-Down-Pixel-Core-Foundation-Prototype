using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weapoonInfo;

    public WeaponInfo GetWeaponInfo() 
    {
        return weapoonInfo; 
    }
}
