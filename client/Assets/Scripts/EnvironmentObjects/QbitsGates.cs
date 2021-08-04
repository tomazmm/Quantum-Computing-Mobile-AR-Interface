using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuantomCOMP
{
    public class QbitsGates : MonoBehaviour
    {
        private GameObject gateArea;
        private WorldObject.Gates _gate;
        private List<GameObject> gates;
        private bool switchState = false;

        void Start()
        {
            subscribeToEvent();
            gates = new List<GameObject>();
        }

        private void subscribeToEvent()
        {
            Gestures.PositionOfGateEvent += positioning;
            Gestures.RemoveGateEvent += removing;
            Gestures.SwitchGateEvent += switching;
            EstablishGateInWorldObject.ConfirmPositionOfGateEvent += confirmPositioning;
        }

        private void switching(GameObject _gateArea, WorldObject.Gates gate)
        {
            //Debug.Log("Switch");
            switchState = true;
            var qbitArea = Qbit.getQbitArea(_gateArea);
            qbitArea.isConfirmed = false;
            gateArea = qbitArea.qbitArea;
            _gate = gate;
            removing(_gateArea, gate);
            positionGate();
            switchState = false;
        }

        private void removing(GameObject _gateArea, WorldObject.Gates gate)
        {
            //Debug.Log("Remove");
            QbitArea qbitArea = Qbit.getQbitArea(_gateArea);
            if (!qbitArea.isConfirmed || switchState)
            {
                gates.Remove(_gateArea);            
                GameObject.Destroy(qbitArea.qbitGate);
                var parent = _gateArea.transform.parent.transform.parent;
                var positionInList = parent.name.Substring(4);
                //Debug.Log(_gateArea.name);
                var positionOfArea = _gateArea.transform.parent.name.Substring(10);
                //Debug.Log(positionInList + "  " + positionOfArea);
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].qbitGate = null;              
            }

            // hide point if gate is deleted
            if(!switchState)
                Qbit.hideQbitAreas();
        }

        private void confirmPositioning(bool state)
        {
            if (!state)
            {
                foreach (GameObject _gate in gates) {
                    GameObject.Destroy(_gate);
                }
            }
            else
            {
                foreach (GameObject _gate in gates)
                {                
                    QbitArea qbitArea = Qbit.getQbitArea(_gate);
                    qbitArea.isConfirmed = true;
                    Debug.Log(_gate.name + "conf");
                    qbitArea.qbitGate.GetComponent<MeshRenderer>().material = Resources.Load(_gate.name+"conf", typeof(Material)) as Material;
                }
            }
            gates.Clear();
            
        }

        private void positioning(GameObject _gateArea, WorldObject.Gates gate)
        {
            gateArea = _gateArea;
            _gate = gate;
            positionGate();
        }

        private void positionGate()
        {
            //Debug.Log(QbitsBoard.listOfQbits[0].areas[1].qbitGate);
            //TODO: build gates with more qbits at once
            var gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gate.name = _gate.ToString(); 
            gate.transform.parent = gateArea.transform;
            gate.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gate.transform.localPosition = new Vector3(0, 0, 0);
            gate.GetComponent<MeshRenderer>().material = Resources.Load(EstablishGateInWorldObject.gate.ToString(), typeof(Material)) as Material;
            var parent = gateArea.transform.parent;
            var positionInList = parent.name.Substring(4);
            var positionOfArea = gateArea.name.Substring(10);
            QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea)-1].qbitGate = gate;

            gates.Add(gate);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

