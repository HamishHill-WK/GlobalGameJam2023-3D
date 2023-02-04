using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        [SerializeField]
        private ItemDefinition item;

        [SerializeField]
        private int numberOfItems;

        public bool IsStackable => item.IsStackable;
        public ItemDefinition Item => item;

        public int NumberOfItems
        {
            get => numberOfItems;
            set
            {
                value = value < 0 ? 0 : value;
                numberOfItems = item != null && item.IsStackable ? value : 1;
            }
        }

        public ItemStack(ItemDefinition item, int numberOfItems)
        {
            this.item = item;
            this.numberOfItems = numberOfItems;
        }

        public ItemStack() {}
    }
}