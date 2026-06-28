using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPre;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;// 定位鼠标位置
    private ActiveWeapon activeWeapon;//基于父对象进行翻转

    private GameObject slashAnim;

    private void Awake()
    {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attak.started += _ => Attach();
    }

    private void Update()
    {
        MouseFllowWithOffSet();
    }

    private void Attach()
    {
        weaponCollider.gameObject.SetActive(true);
        myAnimator.SetTrigger("Attach");
        slashAnim = Instantiate(slashAnimPre, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void DoneAttack()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnim()//动画事件触发
    {
        slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()//动画事件触发
    {
        slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFllowWithOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(playerController.transform.position);
        float angle = Mathf.Atan2((mousePos.y - playerScreenPos.y), Mathf.Abs(mousePos.x - playerScreenPos.x)) * Mathf.Rad2Deg;//玩家中心角度偏移
        if (mousePos.x < playerScreenPos.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}