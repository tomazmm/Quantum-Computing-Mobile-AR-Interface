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
            Content.OnSelectedWorldObjectEvent += enablePositioning;
        }

        private void enablePositioning(WorldObject.EnvironmentObject wObject)
        {
            worldObject = wObject;

            setTitle();
            SharedStateSwitch.enableDisableMenu(false);
            SharedStateSwitch.enableDisableToggleMenuButton(false);
            SharedStateSwitch.enableDisablePositioning(true);
        }

        private void setTitle()
        {
            if(worldObject == 0)
            {
                GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetPositionScreen").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set board";
                GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetPositionScreen").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set board";
                SharedStateSwitch.enableDisableQubitsMenu(true);

            }
            else
            {
                GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetPositionScreen").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set sphere";
                GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetPositionScreen").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set sphere";
                SharedStateSwitch.enableDisableQubitsMenu(false);
            }         
        }
    }
}

