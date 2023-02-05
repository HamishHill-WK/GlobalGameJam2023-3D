using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Profiling;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private int _size = 8;

        [SerializeField]
        private List<InventorySlot> slots;

        private int activeSlotIndex;

        public int Size => _size;

        public List<InventorySlot> Slots => slots;

        public int ActiveSlotIndex
        {
            get => activeSlotIndex;
            private set
            {
                slots[activeSlotIndex].Active = false;
                activeSlotIndex = value < 0 ? _size - 1 : value % Size;
                slots[activeSlotIndex].Active = true;
                Debug.Log("active slot index changed");
            }
        }

        private void Awake()
        {
            if (_size > 0)
            {
                slots[0].Active = true;
            }
        }

        private void OnValidate()
        {
            AdjustSize();
        }

        private void AdjustSize()
        {
            slots ??= new List<InventorySlot>();

            if (slots.Count > _size)
                slots.RemoveRange(_size, slots.Count - _size);

            if (slots.Count < _size)
                slots.AddRange(new InventorySlot[_size - slots.Count]);
        }

        public bool IsFull()
        {
            return slots.Count(slot => slot.HasItem) >= _size;
        }

        public bool CanAcceptItem(ItemStack itemStack)
        {
            var slotWithStackableItem = FindSlot(itemStack.Item, true);
            return !IsFull() || slotWithStackableItem != null;
        }

        private InventorySlot FindSlot(ItemDefinition item, bool onlyStackable = false)
        {
            return slots.FirstOrDefault(slot => slot.Item == item && item.IsStackable || !onlyStackable);
        }

        public bool HasItem(ItemStack itemStack, bool checkNumberOfItems = false)
        {
            var itemSlot = FindSlot(itemStack.Item);
            if (itemSlot == null) return false;
            if (!checkNumberOfItems) return true;
            
            if (itemStack.Item.IsStackable)
            {
                return itemSlot.NumberOfItems >= itemStack.NumberOfItems;
            }
            else
            {
                return slots.Count(slot => slot.Item == itemStack.Item) >= itemStack.NumberOfItems;
            }

        }
        
        public ItemStack AddItem(ItemStack itemStack)
        {
            var relevantSlot = FindSlot(itemStack.Item, true);
            if (IsFull() && relevantSlot == null)
            {
                throw new InventoryException(InventoryOperation.Add, "Inventory is Full!");
            }

            if (relevantSlot != null)
            {
                relevantSlot.NumberOfItems += itemStack.NumberOfItems;
            }
            else
            {
                relevantSlot = slots.First(slot => !slot.HasItem);
                relevantSlot.State = itemStack;
            }

            return relevantSlot.State;
        }

        public ItemStack RemoveItem(int atIndex, bool spawn = false)
        {
            if (!slots[atIndex].HasItem)
            {
                throw new InventoryException(InventoryOperation.Remove, "Slot is Empty!");
            }

            if (spawn)
            {
                
            }
            
            ClearSlot(atIndex);
            return new ItemStack();
        }

        public ItemStack RemoveItem(ItemStack itemStack)
        {
            var itemSlot = FindSlot(itemStack.Item);
            if (itemSlot == null)
                throw new InventoryException(InventoryOperation.Remove, "No Item in the Inventory!");
            
            if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems < itemStack.NumberOfItems)
                throw new InventoryException(InventoryOperation.Remove, "Not enough items!");

            itemSlot.NumberOfItems -= itemStack.NumberOfItems;
            if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems > 0)
                return itemSlot.State;
            
            itemSlot.Clear();

            return new ItemStack();
        }

        public void ClearSlot(int atIndex)
        {
            slots[atIndex].Clear();
        }

        public void ActivateSlot(int atIndex)
        {
            ActiveSlotIndex = atIndex;
        }
    }
}