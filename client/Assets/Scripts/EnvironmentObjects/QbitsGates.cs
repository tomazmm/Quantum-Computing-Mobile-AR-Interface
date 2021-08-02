using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class QbitsGates : MonoBehaviour
    {
        private GameObject gateArea;
        private List<GameObject> gates;
        
        void Start()
        {
            subscribeToEvent();
            gates = new List<GameObject>();
        }

        private void subscribeToEvent()
        {
            Gestures.PositionOfGateEvent += positioning;
            EstablishGateInWorldObject.ConfirmPositionOfGateEvent += confirmPositioning;
        }

        private void confirmPositioning(bool state)
        {
            if (!state)
            {
                foreach(GameObject _gate in gates){
                    GameObject.Destroy(_gate);
                }
            }
            gates.Clear();
            
        }

        private void positioning(GameObject gate)
        {
            gateArea = gate;
            positionGate();
        }

        private void positionGate()
        {
            Debug.Log(EstablishGateInWorldObject.gate);
            //TODO: build gates with more qbits at once
            var gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gate.transform.parent = gateArea.transform;
            gate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gate.transform.localPosition = new Vector3(0, 0, 0);
            gate.GetComponent<MeshRenderer>().material = Resources.Load(EstablishGateInWorldObject.gate.ToString(), typeof(Material)) as Material;
            gates.Add(gate);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

