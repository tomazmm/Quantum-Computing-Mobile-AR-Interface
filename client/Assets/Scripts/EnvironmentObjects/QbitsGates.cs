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
        public static List<GameObject> gates;
        private List<GameObject> tempGateWithManyAreas;
        private QbitArea mainArea;
        private bool switchState = false;
        public static bool buildingOnMultipleGatesAreas = false;
        public static int numberOfGateAreas = 0;
        public static int tempNumberOfGateAreas = 0;
        public static int areasInRow;

        void Start()
        {
            subscribeToEvent();
            gates = new List<GameObject>();
            tempGateWithManyAreas = new List<GameObject>();
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
            if (isTheSameGate(_gateArea))
                return;

            switchState = true;
            var qbitArea = Qbit.getQbitArea(_gateArea);
            qbitArea.isConfirmed = false;
            gateArea = qbitArea.qbitArea;
            _gate = gate;
            removing(_gateArea, gate);
            positionGate();
            switchState = false;
        }

        private bool isTheSameGate(GameObject _gateArea)
        {
            if (tempGateWithManyAreas.Contains(_gateArea))
            {
                return true;
            }
            return false;
        }

        private void removing(GameObject _gateArea, WorldObject.Gates gate)
        {
            QbitArea qbitArea = Qbit.getQbitArea(_gateArea);
            if (!qbitArea.isConfirmed || switchState)
            {
                gates.Remove(_gateArea);            
                GameObject.Destroy(qbitArea.qbitGate);
                var parent = _gateArea.transform.parent.transform.parent;
                var positionInList = parent.name.Substring(4);
                var positionOfArea = _gateArea.transform.parent.name.Substring(10);
                QbitArea mainQbitArea = QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].connectedGateArea;
                if (mainQbitArea == null)
                    mainQbitArea = QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1];
                removeAllConnectedAreas(int.Parse(positionOfArea) - 1, mainQbitArea);
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].qbitGate = null;
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].isMainArea = false;
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].positionsOfConnectedQbits.Clear();
            }

            // hide point if gate is deleted
            if(!switchState)
                Qbit.hideQbitAreas();
        }

        private void removeAllConnectedAreas(int position, QbitArea mainQbitArea)
        {
            foreach(Qbit _qbit in QbitsBoard.listOfQbits)
            {
                if(_qbit.areas[position].connectedGateArea == mainQbitArea || _qbit.areas[position] == mainQbitArea)
                {
                    if (_qbit.areas[position].qbitGate != null)
                        GameObject.Destroy(_qbit.areas[position].qbitGate);
                    gates.Remove(_qbit.areas[position].qbitGate);
                    _qbit.areas[position].qbitGate = null;
                    _qbit.areas[position].isConfirmed = false;
                    _qbit.areas[position].connectedGateArea = null;
                    _qbit.areas[position].isMainArea = false;
                    _qbit.areas[position].positionsOfConnectedQbits.Clear();
                }
            }
        }

        private void confirmPositioning(bool state)
        {
            if (!state)
            {
                foreach (GameObject _gate in gates) {
                    GameObject.Destroy(_gate);
                    QbitArea qbitArea = Qbit.getQbitArea(_gate);
                    findConnectedToTheGate(qbitArea);
                    qbitArea.qbitGate = null;
                    qbitArea.connectedGateArea = null;
                    qbitArea.isMainArea = false;
                    qbitArea.positionsOfConnectedQbits.Clear();
                    tempNumberOfGateAreas = 0;
                    numberOfGateAreas = 0;
                }           
            }
            else
            {
                foreach (GameObject _gate in gates)
                {                
                    QbitArea qbitArea = Qbit.getQbitArea(_gate);
                    qbitArea.isConfirmed = true;
                    if(qbitArea.connectedGateArea != null)
                        qbitArea.connectedGateArea.isConfirmed = true;
                    //Debug.Log(qbitArea.qbitGate);
                    qbitArea.qbitGate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/"+qbitArea.qbitGate.name + "conf", typeof(Material)) as Material;
                }
            }
            gates.Clear();
            
        }

        private void findConnectedToTheGate(QbitArea qbitArea)
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.connectedGateArea == qbitArea)
                    {
                        _qbitArea.connectedGateArea = null;
                    }
                }
            }
        }

        private void positioning(GameObject _gateArea, WorldObject.Gates gate)
        {
            gateArea = _gateArea;
            _gate = gate;
            positionGate();
        }

        private void positionGate()
        {
            GameObject gate;

            if(tempNumberOfGateAreas == 0)
            {
                gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gate.name = _gate.ToString();
                gate.transform.parent = gateArea.transform;
                gate.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                gate.transform.localPosition = new Vector3(0, 0, 0);
                gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/"+EstablishGateInWorldObject.gate.ToString(), typeof(Material)) as Material;
                var parent = gateArea.transform.parent;
                var positionInList = parent.name.Substring(4);
                var positionOfArea = gateArea.name.Substring(10);
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].qbitGate = gate;
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].isMainArea = true;
                mainArea = QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1];

                if(_gate == WorldObject.Gates.Measurementgate)
                {
                    connectAreasToMeasurementGate(int.Parse(positionInList), int.Parse(positionOfArea) - 1);
                }

                tempNumberOfGateAreas++;
                areasInRow = int.Parse(positionOfArea);
                gates.Add(gate);
                tempGateWithManyAreas.Add(gate);
            }          
            else
            {                  
                gate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gate.name = _gate.ToString()+"add"/*+tempNumberOfGateAreas*/;
                gate.transform.parent = gateArea.transform;
                gate.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                gate.transform.localRotation = new Quaternion(0, 0, 0, 0);
                gate.transform.localPosition = new Vector3(0, 0, 0);
                gate.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/"+EstablishGateInWorldObject.gate.ToString() + "add", typeof(Material)) as Material;
                var parent = gateArea.transform.parent;
                var positionInList = parent.name.Substring(4);
                var positionOfArea = gateArea.name.Substring(10);
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].qbitGate = gate;
                QbitAdditionalAreaPosition additionalAreaPosition = new QbitAdditionalAreaPosition();
                additionalAreaPosition.qbit = int.Parse(positionInList);
                additionalAreaPosition.area = int.Parse(positionOfArea);
                mainArea.positionsOfConnectedQbits.Add(additionalAreaPosition);
                QbitsBoard.listOfQbits[int.Parse(positionInList)].areas[int.Parse(positionOfArea) - 1].connectedGateArea = mainArea;

                tempNumberOfGateAreas++;

                connectManyAreasToMainGate(int.Parse(positionInList), int.Parse(positionOfArea) - 1);

                gates.Add(gate);
                tempGateWithManyAreas.Add(gate);
            }
        }

        private void createMeasurementLine(int positionInList, int positionOfArea)
        {        
            float distance = Vector3.Distance(QbitsBoard.listOfQbits[positionInList].areas[positionOfArea].qbitArea.transform.position, mainArea.qbitGate.transform.position) * 10;
            float additionalForC = 2;

            

            var line = GameObject.CreatePrimitive(PrimitiveType.Cube);
            line.name = "lineto" + _gate.ToString() + "add" + tempNumberOfGateAreas;
            line.transform.parent = mainArea.qbitGate.gameObject.transform;
            line.transform.localScale = new Vector3(0.1f, (additionalForC + distance) + additionalForC/10 + distance / 10, 0.1f);
            line.transform.localRotation = new Quaternion(0, 0, 0, 0);
            line.transform.localPosition = new Vector3(0, - distance / 2 - additionalForC / 2 - additionalForC/20 - distance / 20, 0);
            line.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/MeasurementLine", typeof(Material)) as Material;
        }

        private void connectAreasToMeasurementGate(int positionInList, int positionOfArea)
        {
            for (int x = positionInList + 1; x < QbitsBoard.listOfQbits.Count; x++)
            {
                if (QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate != null &&
                    QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea != mainArea)
                {
                    QbitArea mainQbitArea = QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea;
                    if (mainQbitArea == null)
                        mainQbitArea = QbitsBoard.listOfQbits[x].areas[positionOfArea];
                    removeAllConnectedAreas(positionOfArea, mainQbitArea);
                }
                QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea = mainArea;
            }
            createMeasurementLine(QbitsBoard.listOfQbits.Count - 1, positionOfArea);
        }

        private void connectManyAreasToMainGate(int positionInList, int positionOfArea)
        {
            var mainGateInList = QbitsBoard.listOfQbits.FirstOrDefault(x => x.areas.Any(y => y == mainArea));
            var positionOfMainInList = int.Parse(mainGateInList.qbit.name.Substring(4));
            int x;
            //Debug.Log(positionInList + "   " + positionOfMainInList);
            if (positionInList > positionOfMainInList)
            {
                for (x = positionOfMainInList + 1; x < positionInList; x++)
                {
                    
                    if(QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate != null && 
                        QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea != mainArea)
                    {
                        //GameObject.Destroy(QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate);
                        //QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate = null;
                        QbitArea mainQbitArea = QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea;
                        if (mainQbitArea == null)
                            mainQbitArea = QbitsBoard.listOfQbits[x].areas[positionOfArea];
                        removeAllConnectedAreas(positionOfArea, mainQbitArea);
                    }
                    QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea = mainArea;
                }
                createLine(QbitsBoard.listOfQbits[positionInList].areas[positionOfArea], 1);
            }
            else
            {
                for (x = positionInList + 1; x < positionOfMainInList; x++)
                {                   
                    if (QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate != null && 
                        QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea != mainArea)
                    {
                        //GameObject.Destroy(QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate);
                        //QbitsBoard.listOfQbits[x].areas[positionOfArea].qbitGate = null;
                        QbitArea mainQbitArea = QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea;
                        if (mainQbitArea == null)
                            mainQbitArea = QbitsBoard.listOfQbits[x].areas[positionOfArea];
                        removeAllConnectedAreas(positionOfArea, mainQbitArea);
                    }
                    QbitsBoard.listOfQbits[x].areas[positionOfArea].connectedGateArea = mainArea;
                }
                createLine(QbitsBoard.listOfQbits[positionInList].areas[positionOfArea], -1);
            }           
        }

        private void createLine(QbitArea additionalGate, int reverse)
        {
            float distance = Vector3.Distance(additionalGate.qbitGate.transform.position, mainArea.qbitGate.transform.position) * 10;

            var line = GameObject.CreatePrimitive(PrimitiveType.Cube);
            line.name = "lineto"+_gate.ToString() + "add" + tempNumberOfGateAreas;
            line.transform.parent = mainArea.qbitGate.gameObject.transform;
            line.transform.localScale = new Vector3(0.1f, distance + distance/10, 0.1f);
            line.transform.localRotation = new Quaternion(0, 0, 0, 0);
            line.transform.localPosition = new Vector3(0, reverse *(-distance/2 - distance/20), 0);
            line.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/Betweenline", typeof(Material)) as Material;
        }

        private void resetNumberOfGateAreas()
        {
            if (tempNumberOfGateAreas == numberOfGateAreas)
            {               
                tempNumberOfGateAreas = 0;
                mainArea = null;
                tempGateWithManyAreas.Clear();
            }
                
        }

        // Update is called once per frame
        void Update()
        {
            resetNumberOfGateAreas();

        }

        
    }
}

