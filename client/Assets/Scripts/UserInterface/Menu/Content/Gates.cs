using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class Gates : Content
    {
        private void Start()
        {
            objectName = "Gates";
            nameOfSection = Section.Gates;
            subscribeToSectionEvent();
            gameObject.SetActive(false);
        }

        public void HGate()
        {
            selectWorldObjectGate(WorldObject.Gates.Hgate);
        }

        public void NotGate()
        {
            selectWorldObjectGate(WorldObject.Gates.Notgate);
        }
    }
}

