using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class Marker : MonoBehaviour
    {    
        void Start()
        {
            SharedStateSwitch.enableDisableIndicator(false);
            subscribeToEvent();
        }

        private void subscribeToEvent()
        {
            Content.OnSelectedWorldObjectEvent += enableOrDisableMarker;
        }

        private void enableOrDisableMarker(int wObject)
        {
            SharedStateSwitch.enableDisableMenu(false);
            SharedStateSwitch.enableDisableToggleMenuButton(false);
            SharedStateSwitch.enableDisablePositioning(true);
            SharedStateSwitch.enableDisableIndicator(true);

            //if (wObject == (int)WorldObject.EnvironmentObject.Board)
            //{
                
            //}
            //else
            //{

            //}

        }
    }
}

