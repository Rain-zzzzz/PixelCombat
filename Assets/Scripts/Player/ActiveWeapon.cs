using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ActiveWeapon : SingleTon<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;
    private PlayerControls playerControls;
    private bool isAttacking = false;

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

    private void Attack()
    {
        isAttacking = true;
        (currentActiveWeapon as IWeapon).Attach();//不是拆箱单纯的类型转换，IWeapon是接口，MonoBehaviour是类，MonoBehaviour实现了IWeapon接口，所以可以直接转换
    }
}