using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryInputHandler : MonoBehaviour
    {
        private Inventory inventory;

        private void Awake()
        {
            inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
                OnPreviousItem();
            else if (Input.GetKeyDown(KeyCode.R))
                OnNextItem();
            else if (Input.GetKeyDown(KeyCode.F))
                OnThrowItem();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnThrowItem()
        {
            
        }

        private void OnNextItem()
        {
            inventory.ActivateSlot(inventory.ActiveSlotIndex + 1);
        }

        private void OnPreviousItem()
        {
            inventory.ActivateSlot(inventory.ActiveSlotIndex - 1);
        }
    }
}