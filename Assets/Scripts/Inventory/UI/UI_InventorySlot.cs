using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class UI_InventorySlot : MonoBehaviour
    {
        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private int inventorySlotIndex;

        [SerializeField]
        private Image itemIcon;

        [SerializeField]
        private Image activeIndicator;
        
        [SerializeField]
        private TMP_Text numberOfItems;
        
        private InventorySlot slot;

        private void Start()
        {
            AssignSlot(inventorySlotIndex);
        }

        public void AssignSlot(int slotIndex)
        {
            if (slot != null) slot.StateChanged -= OnStateChanged;
            inventorySlotIndex = slotIndex;
            if (inventory == null) inventory = GetComponentInParent<UI_Inventory>().Inventory;
            slot = inventory.Slots[inventorySlotIndex];
            slot.StateChanged += OnStateChanged;
            UpdateViewState(slot.State, slot.Active);
        }

        private void UpdateViewState(ItemStack state, bool active)
        {
            activeIndicator.enabled = active;
            var item = state?.Item;
            var hasItem = item != null;
            var isStackable = hasItem && item.IsStackable;
            itemIcon.enabled = hasItem;
            numberOfItems.enabled = isStackable;
            if (!hasItem) return;
            
            itemIcon.sprite = item.UISprite;
            if (isStackable) numberOfItems.SetText(state.NumberOfItems.ToString());
        }

        private void OnStateChanged(object sender, InventorySlotStateChangedArgs args)
        {
            UpdateViewState(args.NewState, args.Active);
        }
    }
}