using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Crafts Config")]
    public class CraftsCfg:ScriptableObject
    {
        [Serializable]
        public class Receipt
        {
            public string[] ItemsNames;
            public string NewItem;
        }
        public List<Receipt> Receipts;
    }
}