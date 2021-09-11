using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class EstablishWorldObjects : MonoBehaviour
    {
        private WorldObject.EnvironmentObject worldObject;
        public delegate void EstablishPositionBoard ();
        public static event EstablishPositionBoard ConfirmPositionOfBoardEvent;

        public delegate void EstablishPositionSphere ();
        public static event EstablishPositionSphere ConfirmPositionOfSphereEvent;

        public delegate void EstablishPositionProbabilitiesGraph();
        public static event EstablishPositionProbabilitiesGraph ConfirmPositionOfProbabilitiesGraphEvent;

        private void Start()
        {
            subscribeToEvent();
        }

        public void closePositioning()
        {
            //SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisablePositioning(false);
        }

        public void confirmPositioning()
        {
            //SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisablePositioning(false);
            SharedStateSwitch.disableAllAreaGatesButtons();
            QbitsBoard.maxNumberOfAreas = 3;

            if (worldObject == WorldObject.EnvironmentObject.Board)
                ConfirmPositionOfBoardEvent();
            else if (worldObject == WorldObject.EnvironmentObject.BlochSphere)
                ConfirmPositionOfSphereEvent();
            else
                ConfirmPositionOfProbabilitiesGraphEvent();
        }


        private void subscribeToEvent()
        {
            Content.OnSelectedWorldObjectEvent += enablePositioning;
        }

        private void enablePositioning(WorldObject.EnvironmentObject wObject)
        {
            worldObject = wObject;

            SetDropDownMenu();
            //SharedStateSwitch.enableDisableMenu(false);
            //SharedStateSwitch.enableDisableContent(false);
            //SharedStateSwitch.setAllButtonsInactive();
            //Menu.isMenuActive = false;
            SharedStateSwitch.enableDisablePositioning(true);
        }

        private void SetDropDownMenu()
        {
            if (worldObject == 0)
                SharedStateSwitch.enableDisableQubitsMenu(true);

            else
                SharedStateSwitch.enableDisableQubitsMenu(false);
            
        }
    }
}

