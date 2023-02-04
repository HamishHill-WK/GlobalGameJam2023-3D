using System.Collections;
using System.Collections.Generic;
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
    }
}