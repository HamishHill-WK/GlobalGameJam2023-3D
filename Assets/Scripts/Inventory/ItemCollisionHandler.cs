using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class ItemCollisionHandler : MonoBehaviour
    {
        private Inventory inventory;

        private void Awake()
        {
            inventory = GetComponentInParent<Inventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<GameItem>(out var gameItem) || !inventory.CanAcceptItem(gameItem.Stack)) return;

            inventory.AddItem(gameItem.PickUp());
        }
    }
}