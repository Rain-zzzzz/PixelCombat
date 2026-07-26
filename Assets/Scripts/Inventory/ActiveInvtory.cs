using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveInvtory : MonoBehaviour
{
    private int activeSlotNum = 0;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        ToggleActiveHidhtLight(activeSlotNum);
        playerControls.Inventory.Kay.performed += ctx =>
        {
            activeSlotNum = (int)ctx.ReadValue<float>() - 1; // 更新当前选中槽位
            ToggleActiveHidhtLight(activeSlotNum);//数据包 回调
        };
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveHidhtLight(int numValue)
    {
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(numValue).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }

    //鼠标滚动
    private void Update()
    {
        int slotCount = this.transform.childCount;
        if (slotCount == 0) return;
        int lastActiveSlotNum = activeSlotNum;

        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0f)
        {
            // 向上滚动：选择上一个槽（循环）
            activeSlotNum = (activeSlotNum - 1 + slotCount) % slotCount;
            ToggleActiveHidhtLight(activeSlotNum);
        }
        else if (scroll < 0f)
        {
            // 向下滚动：选择下一个槽（循环）
            activeSlotNum = (activeSlotNum + 1) % slotCount;
            ToggleActiveHidhtLight(activeSlotNum);
        }
        if (lastActiveSlotNum != activeSlotNum)//鼠标只有滚动切换武器时才去消除和实例化武器，避免一直实例化
        {
            ChangeActiveWeapon();
        }
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }
        Transform childTransform = this.transform.GetChild(activeSlotNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponToSpawn == null)
        {
            ActiveWeapon.Instance.NullWeapon();
            return;
        }
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>(), activeSlotNum);
    }
}