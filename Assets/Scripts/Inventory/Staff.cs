using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;
    private Animator myAnimator;
    private readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFllowWithOffSet();
    }

    public void Attach()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnStaffAniEvent()
    {
        GameObject newMagicLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newMagicLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFllowWithOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        float angle = Mathf.Atan2((mousePos.y - playerScreenPos.y), Mathf.Abs(mousePos.x - playerScreenPos.x)) * Mathf.Rad2Deg;//玩家中心角度偏移
        if (mousePos.x < playerScreenPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}