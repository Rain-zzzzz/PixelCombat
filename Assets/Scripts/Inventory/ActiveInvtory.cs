using System.Collections;
using System.Collections.Generic;
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

    //private void ToggleActiveSlot(int numValue)
    //{
    //    ToggleActiveHidhtLight(numValue);
    //}

    private void ToggleActiveHidhtLight(int numValue)
    {
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(numValue).GetChild(0).gameObject.SetActive(true);
    }

    //鼠标滚动
    private void Update()
    {
        int slotCount = this.transform.childCount;
        if (slotCount == 0) return;

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
    }
}