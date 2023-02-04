using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class InventorySlot
    {
        public event EventHandler<InventorySlotStateChangedArgs> StateChanged;

        [SerializeField]
        private ItemStack state;

        private bool active;

        public ItemStack State
        {
            get => state;
            set
            {
                state = value;
                NotifyAboutStateChanged();
            }
        }

        public bool Active
        {
            get => active;
            set
            {
                active = value;
                NotifyAboutStateChanged();
            }
        }

        public int NumberOfItems
        {
            get => state.NumberOfItems;
            set
            {
                state.NumberOfItems = value;
                NotifyAboutStateChanged();
            }
        }

        public bool HasItem => state?.Item != null;
        public ItemDefinition Item => state?.Item;

        public void Clear()
        {
            State = null;

        }

        private void NotifyAboutStateChanged()
        {
            StateChanged?.Invoke(this, new InventorySlotStateChangedArgs(state, active));

        }
    }
}