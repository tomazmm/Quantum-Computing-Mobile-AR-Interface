using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class EstablishGateInWorldObject : MonoBehaviour
    {
        public static WorldObject.Gates gate;

        public delegate void EstablishPositionGate(bool state);
        public static event EstablishPositionGate ConfirmPositionOfGateEvent;
        public static bool enableGatePositioning;

 
        void Start()
        {
            enableGatePositioning = false;
            subscribeToEvent();
        }

        public void closePositioning()
        {
            enableGatePositioning = false;
            gate = WorldObject.Gates.None;
            ConfirmPositionOfGateEvent(false);
            SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisableGatePositioning(false);
        }

        public void confirmPositioning()
        {
            //SharedStateSwitch.enableDisableMenu(true);
            gate = WorldObject.Gates.None;
            enableGatePositioning = false;
            ConfirmPositionOfGateEvent(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisableGatePositioning(false);
        }

        private void subscribeToEvent()
        {
            Content.OnSelectedWorldObjectGatesEvent += enablePositioning;
        }

        private void enablePositioning(WorldObject.Gates wGate)
        {
            gate = wGate;
            enableGatePositioning = true;
            setTitle();
            SharedStateSwitch.enableDisableMenu(false);
            SharedStateSwitch.enableDisableToggleMenuButton(false);
            SharedStateSwitch.enableDisableGatePositioning(true);

        }

        private void setTitle()
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetGates").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set " + WorldObject.listOfGates[(int)gate];
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetGates").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set " + WorldObject.listOfGates[(int)gate];     
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
