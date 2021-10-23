using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //gameObject.SetActive(false);
            SharedStateSwitch.disableAllAreaGatesButtons();
        }

        public void HGate()
        {
            selectWorldObjectGate(WorldObject.Gates.Hgate);
        }

        public void NotGate()
        {
            selectWorldObjectGate(WorldObject.Gates.Notgate);
        }

        public void CNotGate()
        {
            selectWorldObjectGate(WorldObject.Gates.CNotgate);
        }

        public void ToffoliGate()
        {
            selectWorldObjectGate(WorldObject.Gates.Toffoligate);
        }

        public void Measurement()
        {
            selectWorldObjectGate(WorldObject.Gates.Measurementgate);
        }

        public void ResetGate()
        {
            selectWorldObjectGate(WorldObject.Gates.Resetgate);
        }

        private void Update()
        {
            if(QbitsBoard.listOfQbits.Count() != 0)
            {
                if(QbitsBoard.listOfQbits.Count() == 1)
                    SharedStateSwitch.enableOneAreaGatesButtons();
                else if (QbitsBoard.listOfQbits.Count() == 2)
                    SharedStateSwitch.enableTwoAreaGatesButtons();
                else
                    SharedStateSwitch.enableThreeAreaGatesButtons();
            }
        }
    }
}

