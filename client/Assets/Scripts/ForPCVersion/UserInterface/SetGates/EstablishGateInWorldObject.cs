using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMPPC
{
    public class EstablishGateInWorldObject : MonoBehaviour
    {
        public static WorldObject.Gates gate;

        public delegate void EstablishPositionGate(bool state);
        public static event EstablishPositionGate ConfirmPositionOfGateEvent;

        public static bool enableGatePositioning;
        private static Transform GateRepresentator;
 
        void Start()
        {
            enableGatePositioning = false;
            GateRepresentator = GameObject.Find("CanvasForGateRepresentation").transform;
            subscribeToEvent();
        }

        public void closePositioning()
        {
            enableGatePositioning = false;         
            ConfirmPositionOfGateEvent(false);
            closeFunction();
            Qbit.deleteUnconfirmedGates();
        }

        public static void closeFunction()
        {
            gate = WorldObject.Gates.None;
            Gestures.gate = gate;
            //SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableGatePositioning(false);
            removeGatesFromRepresentator();
        }

        public void confirmPositioning()
        {
            //SharedStateSwitch.enableDisableMenu(true);
            gate = WorldObject.Gates.None;
            Gestures.gate = gate;
            enableGatePositioning = false;
            ConfirmPositionOfGateEvent(true);
            SharedStateSwitch.enableDisableGatePositioning(false);
            //SharedStateSwitch.enableDisableContent(true);
            Qbit.deleteUnconfirmedGates();
            removeGatesFromRepresentator();
            //ProbabilityCalculation.calculateProbability();
        }

        private void subscribeToEvent()
        {
            Content.OnSelectedWorldObjectGatesEvent += enablePositioning;
        }

        private void enablePositioning(WorldObject.Gates wGate)
        {     
            gate = wGate;
            checkNumberOfGatesAreas();
            enableGatePositioning = true;

            //SharedStateSwitch.enableDisableMenu(false);
            //SharedStateSwitch.enableDisableContent(false);
            //SharedStateSwitch.setAllButtonsInactive();
            //Menu.isMenuActive = false;
            SharedStateSwitch.enableDisableQubitsAcceptGatesButton(true);
            SharedStateSwitch.enableDisableGatePositioning(true);
            addGatesToRepresentator();
        }

        private void addGatesToRepresentator()
        {
            removeGatesFromRepresentator();
            GameObject _gate;
            if (QbitsGates.numberOfGateAreas <= 1)
            {

                _gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _gate.name = gate.ToString() + "conf";
                _gate.transform.parent = GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform;
                _gate.transform.localPosition = new Vector3(0, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + _gate.name, typeof(Material)) as Material;

            }
            else if(QbitsGates.numberOfGateAreas <= 2)
            {

                _gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _gate.name = gate.ToString() + "conf";
                _gate.transform.parent = GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform;
                _gate.transform.localPosition = new Vector3(-60, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "conf", typeof(Material)) as Material;


                _gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gate.name = gate.ToString() + "addconf1";
                _gate.transform.parent = GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform;
                _gate.transform.localPosition = new Vector3(60, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "addconf", typeof(Material)) as Material;
            }
            else
            {

                _gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _gate.name = gate.ToString() + "conf";
                _gate.transform.parent = GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform;
                _gate.transform.localPosition = new Vector3(-105, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "conf", typeof(Material)) as Material;

                _gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gate.name = gate.ToString() + "addconf1";
                _gate.transform.parent = GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform;
                _gate.transform.localPosition = new Vector3(0, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "addconf", typeof(Material)) as Material;

                _gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gate.name = gate.ToString() + "addconf2";
                _gate.transform.parent = GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform;
                _gate.transform.localPosition = new Vector3(105, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "addconf", typeof(Material)) as Material;

            }
            
        }

        private void hideUsedGates()
        {
            if(QbitsGates.tempNumberOfGateAreas == 1 && QbitsGates.buildingOnMultipleGatesAreas)
            {
                GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform.Find(gate.ToString() + "conf").GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "hide", typeof(Material)) as Material;
            }
            else if(QbitsGates.tempNumberOfGateAreas == 2)
            {
                GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform.Find(gate.ToString() + "addconf1").GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "add" + "hide", typeof(Material)) as Material;
            }
            else
            {
                //TODO: check if object isn't set
                GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform.Find(gate.ToString() + "conf").GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "conf", typeof(Material)) as Material;
                if(QbitsGates.numberOfGateAreas > 1)
                {
                    GateRepresentator.Find("Landscape").transform.Find("GateRepresentation").transform.Find(gate.ToString() + "addconf1").GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/" + gate.ToString() + "addconf", typeof(Material)) as Material;
                }               
            }
        }

        private static void removeGatesFromRepresentator()
        {

            foreach (Transform gate in GateRepresentator.transform.Find("Landscape").transform.Find("GateRepresentation").transform)
            {
                GameObject.Destroy(gate.gameObject);
            }
        }

        private void checkNumberOfGatesAreas()
        {
            Gestures.gate = gate;
            if (gate == WorldObject.Gates.CNotgate)
            {
                QbitsGates.numberOfGateAreas = 2;
                QbitsGates.buildingOnMultipleGatesAreas = true;
            }else if(gate == WorldObject.Gates.Toffoligate)
            {
                QbitsGates.numberOfGateAreas = 3;
                QbitsGates.buildingOnMultipleGatesAreas = true;
            }
            else
            {
                QbitsGates.numberOfGateAreas = 1;
                QbitsGates.buildingOnMultipleGatesAreas = false;
            }             
        }


        // Update is called once per frame
        void Update()
        {
            //TODO: if screen rotates move objects to another GateRepresentation
            if (enableGatePositioning)
                hideUsedGates();

        }

        
    }
}

