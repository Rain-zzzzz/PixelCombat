using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可视化脚本对象
[CreateAssetMenu(menuName = "WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public int weaponDamage;
    public float weaponRange;
}