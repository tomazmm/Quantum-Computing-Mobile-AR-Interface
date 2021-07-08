using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class EstablishWorldObjects : MonoBehaviour
    {
        private WorldObject.EnvironmentObject worldObject;
        public delegate void EstablishPositionBoard ();
        public static event EstablishPositionBoard ConfirmPositionOfBoardEvent;

        public delegate void EstablishPositionGate ();
        public static event EstablishPositionGate ConfirmPositionOfGateEvent;

        private void Start()
        {
            subscribeToEvent();
        }

        public void closePositioning()
        {
            SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisablePositioning(false);
        }

        public void confirmPositioning()
        {
            //SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisablePositioning(false);

            if (worldObject == WorldObject.EnvironmentObject.Board)
                ConfirmPositionOfBoardEvent();
            else
                ConfirmPositionOfGateEvent();
        }

        private void subscribeToEvent()
        {
            Content.OnSelectedWorldObjectEvent += enableOrDisableMarker;
        }

        private void enableOrDisableMarker(WorldObject.EnvironmentObject wObject)
        {
            worldObject = wObject;

            SharedStateSwitch.enableDisableMenu(false);
            SharedStateSwitch.enableDisableToggleMenuButton(false);
            SharedStateSwitch.enableDisablePositioning(true);
        }
    }
}

