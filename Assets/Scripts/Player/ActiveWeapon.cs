using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ActiveWeapon : SingleTon<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    public int CurrentActiveWeaponIndex { get; private set; }
    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attak.started += _ => Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon, int index)
    {
        CurrentActiveWeapon = newWeapon;
        CurrentActiveWeaponIndex = index;
    }

    public void NullWeapon()
    {
        CurrentActiveWeapon = null;
        CurrentActiveWeaponIndex = -1;
    }

    private void Attack()
    {
        if (CurrentActiveWeapon)
        {
            (CurrentActiveWeapon as IWeapon).Attach();//不是拆箱单纯的类型转换，IWeapon是接口，MonoBehaviour是类，MonoBehaviour实现了IWeapon接口，所以可以直接转换
        }
    }
}