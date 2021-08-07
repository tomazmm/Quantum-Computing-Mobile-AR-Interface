using System;
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
        private GameObject GateRepresentator;
 
        void Start()
        {
            enableGatePositioning = false;
            GateRepresentator = GameObject.Find("GateRepresentation");
            subscribeToEvent();
        }

        public void closePositioning()
        {
            enableGatePositioning = false;
            gate = WorldObject.Gates.None;
            Gestures.gate = gate;
            ConfirmPositionOfGateEvent(false);
            SharedStateSwitch.enableDisableMenu(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisableGatePositioning(false);
            Qbit.deleteUnconfirmedGates();
            removeGatesFromRepresentator();
        }

        public void confirmPositioning()
        {
            //SharedStateSwitch.enableDisableMenu(true);
            gate = WorldObject.Gates.None;
            Gestures.gate = gate;
            enableGatePositioning = false;
            ConfirmPositionOfGateEvent(true);
            SharedStateSwitch.enableDisableToggleMenuButton(true);
            SharedStateSwitch.enableDisableGatePositioning(false);
            Qbit.deleteUnconfirmedGates();
            removeGatesFromRepresentator();
            
            var qasm = Interpreter.boardToQasm();
            ApiConnector.runCircuit(qasm);

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

            setTitle();
            SharedStateSwitch.enableDisableMenu(false);
            SharedStateSwitch.enableDisableToggleMenuButton(false);
            SharedStateSwitch.enableDisableQubitsAcceptGatesButton(true);
            SharedStateSwitch.enableDisableGatePositioning(true);
            addGatesToRepresentator();
        }

        private void addGatesToRepresentator()
        {
            GameObject _gate;
            if (QbitsGates.numberOfGateAreas <= 1)
            {
                _gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _gate.name = gate.ToString() + "conf";
                _gate.transform.parent = GateRepresentator.transform;
                _gate.transform.localPosition = new Vector3(0, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load(_gate.name, typeof(Material)) as Material;
            }else if(QbitsGates.numberOfGateAreas <= 2)
            {
                _gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _gate.name = gate.ToString() + "conf";
                _gate.transform.parent = GateRepresentator.transform;
                _gate.transform.localPosition = new Vector3(-60, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "conf", typeof(Material)) as Material;

                _gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gate.name = gate.ToString() + "addconf1";
                _gate.transform.parent = GateRepresentator.transform;
                _gate.transform.localPosition = new Vector3(60, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "addconf", typeof(Material)) as Material;
            }
            else
            {
                _gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _gate.name = gate.ToString() + "conf";
                _gate.transform.parent = GateRepresentator.transform;
                _gate.transform.localPosition = new Vector3(-105, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "conf", typeof(Material)) as Material;

                _gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gate.name = gate.ToString() + "addconf1";
                _gate.transform.parent = GateRepresentator.transform;
                _gate.transform.localPosition = new Vector3(0, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "addconf", typeof(Material)) as Material;

                _gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gate.name = gate.ToString() + "addconf2";
                _gate.transform.parent = GateRepresentator.transform;
                _gate.transform.localPosition = new Vector3(105, 0, 0);
                _gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _gate.transform.localScale = new Vector3(90, 90, 90);
                _gate.GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "addconf", typeof(Material)) as Material;
            }
            
        }

        private void hideUsedGates()
        {
            if(QbitsGates.tempNumberOfGateAreas == 1 && QbitsGates.buildingOnMultipleGatesAreas)
            {
                GateRepresentator.transform.Find(gate.ToString() + "conf").GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString()+"hide", typeof(Material)) as Material;
            }else if(QbitsGates.tempNumberOfGateAreas == 2)
            {
                GateRepresentator.transform.Find(gate.ToString() + "addconf1").GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "add" + "hide", typeof(Material)) as Material;
            }
            else
            {
                GateRepresentator.transform.Find(gate.ToString() + "conf").GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "conf", typeof(Material)) as Material;
                if(QbitsGates.numberOfGateAreas > 1)
                {
                    GateRepresentator.transform.Find(gate.ToString() + "addconf1").GetComponent<MeshRenderer>().material = Resources.Load(gate.ToString() + "addconf", typeof(Material)) as Material;
                }               
            }
        }

        private void removeGatesFromRepresentator()
        {
            foreach(Transform gate in GateRepresentator.transform)
            {
                Debug.Log(gate.name);
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

        private void setTitle()
        {
            GameObject.Find("Canvas").transform.Find("Portrait").transform.Find("SetGates").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set " + WorldObject.listOfGates[(int)gate];
            GameObject.Find("Canvas").transform.Find("Landscape").transform.Find("SetGates").transform.Find("Content").transform.Find("SubTop").transform.Find("Text").gameObject.GetComponent<Text>().text = "Set " + WorldObject.listOfGates[(int)gate];     
        }

        // Update is called once per frame
        void Update()
        {
            //TODO: if screen rotates move objects to another GateRepresentation
            hideUsedGates();
            GateRepresentator = GameObject.Find("GateRepresentation");
        }

        
    }
}

