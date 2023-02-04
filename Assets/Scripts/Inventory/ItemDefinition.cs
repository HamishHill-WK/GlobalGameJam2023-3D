using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/Item Definition", fileName = "New Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private bool isStackable;

        [SerializeField]
        private Mesh mesh;

        [SerializeField]
        private Sprite uiSprite;

        public string Name => name;
        public bool IsStackable => isStackable;
        public Mesh Mesh => mesh;
        public Sprite UISprite => uiSprite;
    }
}