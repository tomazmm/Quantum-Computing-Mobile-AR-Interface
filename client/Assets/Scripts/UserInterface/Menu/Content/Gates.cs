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

        public void xxGate()
        {
            selectWorldObjectGate(WorldObject.Gates.xx);
        }

        public void yyGate()
        {
            selectWorldObjectGate(WorldObject.Gates.yy);
        }
    }
}

