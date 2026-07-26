using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject slashAnimPre;
    private Transform slashAnimSpawnPoint;
    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAnim;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFllowWithOffSet();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = PlayerController.Instance.GetSlashAnimSpawnPoint();
    }

    public void Attach()
    {
        weaponCollider.gameObject.SetActive(true);
        myAnimator.SetTrigger("Attach");

        slashAnim = Instantiate(slashAnimPre, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    //动画事件触发=>在动画中添加事件
    public void DoneAttack()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    //鼠标跟随武器翻转
    private void MouseFllowWithOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        float angle = Mathf.Atan2((mousePos.y - playerScreenPos.y), Mathf.Abs(mousePos.x - playerScreenPos.x)) * Mathf.Rad2Deg;//玩家中心角度偏移
        if (mousePos.x < playerScreenPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}