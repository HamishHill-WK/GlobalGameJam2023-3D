using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField]
        private ItemStack stack;

        [SerializeField]
        private MeshFilter meshFilter;

        public ItemStack Stack => stack;

        private void OnValidate()
        {
            SetupGameObject();
        }

        private void SetupGameObject()
        {
            if (stack.Item == null) return;
            SetGameMesh();
            AdjustNumberOfItems();
            UpdateObjectName();
        }

        private void SetGameMesh()
        {
            meshFilter.mesh = stack.Item.Mesh;
        }

        private void UpdateObjectName()
        {
            var name = stack.Item.Name;
            var number = stack.IsStackable ? stack.NumberOfItems.ToString() : "ns";
            gameObject.name = $"{name} ({number})";
        }

        private void AdjustNumberOfItems()
        {
            stack.NumberOfItems = stack.NumberOfItems;
        }

        public ItemStack PickUp()
        {
            Destroy(gameObject);
            return stack;
        }
    }
}