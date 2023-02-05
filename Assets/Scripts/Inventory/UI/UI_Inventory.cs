using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField]
        private GameObject inventorySlotPrefab;

        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private List<UI_InventorySlot> slots;

        public Inventory Inventory => inventory;

        [ContextMenu("Initalize Inventory")]
        private void InitializeInventoryUI()
        {
            if (inventory == null || inventorySlotPrefab == null) return;

            slots = new List<UI_InventorySlot>(inventory.Size);
            for (int i = 0; i < inventory.Size; i++)
            {
                var uiSlot = PrefabUtility.InstantiatePrefab(inventorySlotPrefab) as GameObject;
                uiSlot.transform.SetParent(transform, false);

                var uiSlotScript = uiSlot.GetComponent<UI_InventorySlot>();
                uiSlotScript.AssignSlot(i);
                slots.Add(uiSlotScript);
            }
        }
    }
}