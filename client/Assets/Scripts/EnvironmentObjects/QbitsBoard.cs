using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class QbitAdditionalAreaPosition
    {
        public int qbit;
        public int area;
    }

    public class QbitArea
    {
        public bool isConfirmed;
        public GameObject qbitArea;
        public GameObject qbitGate;
        public QbitArea connectedGateArea;
        public bool isMainArea;
        public List<QbitAdditionalAreaPosition> positionsOfConnectedQbits;
    }

    public class Qbit
    {
        public GameObject qbit;
        public GameObject line;
        public List<QbitArea> areas;

        public static QbitArea getQbitArea(GameObject gateArea)
        {
            return (from sublist in QbitsBoard.listOfQbits
                    from item in sublist.areas
                    where item.qbitGate == gateArea
                    select item).FirstOrDefault();
        }

        public static void showQbitAreas()
        {
            List<int> maxNumberOfAreas = new List<int>();

            for(int y = 0; y < QbitsBoard.listOfQbits.Count; y++)
            {
                var _qbit = QbitsBoard.listOfQbits[y];
                var lastGate = 1;
                for(int x = 0; x < _qbit.areas.Count(); x++)
                {
                    
                    if (_qbit.areas[x].qbitGate != null || _qbit.areas[x].connectedGateArea != null)
                    {
                        if(lastGate <= (x + 2))
                            lastGate = (x + 2);
                    }
                }
                maxNumberOfAreas.Add(lastGate);
            }
            //one additional for the area free row

            for (int y = 0; y < QbitsBoard.listOfQbits.Count; y++)
            {
                var _qbit = QbitsBoard.listOfQbits[y];
                for (int x = 0; x < maxNumberOfAreas[y]; x++)
                {
                    if (_qbit.areas[x].qbitGate == null && _qbit.areas[x].connectedGateArea == null)
                    {
                        _qbit.areas[x].qbitArea.SetActive(true);
                    }
                }
            }
        }

        public static void hideQbitAreas()
        {
            //Debug.Log("gg");
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (!_qbitArea.isConfirmed && _qbitArea.qbitGate == null)
                        _qbitArea.qbitArea.SetActive(false);
                }
            }
        }

        public static void deleteUnconfirmedGates()
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (!_qbitArea.isConfirmed && _qbitArea.qbitGate == null)
                    {
                        _qbitArea.qbitGate = null;
                        //_qbitArea.connectedGateArea = null;
                    }
                        
                }
            }
        }

        public static bool aditionalSpaceForAreas()
        {
            bool additionalSpaceRequired = false;
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                if (_qbit.areas.Last().qbitGate != null)
                {
                    additionalSpaceRequired = true;
                    break;
                }
            }
            return additionalSpaceRequired;
        }

        public static bool tooMuchSpaceForAreas()
        {          
            int availableQbits = 0;
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                if(_qbit.areas.Count() >= 3)
                {
                    if (_qbit.areas.Last().qbitGate == null && _qbit.areas[_qbit.areas.Count()-2].qbitGate == null && _qbit.areas.Count()-1 >= 3)
                    {
                        availableQbits += 1;                     
                    }                  
                }
            }
            if (availableQbits == QbitsBoard.listOfQbits.Count())
                return true;
            else
                return false;
        }

        public static void createAdditionalSpaceAreas(float position)
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                //var lastAreainQbit = _qbit.areas.Last();
                var number = _qbit.areas.Count() + 1;

                var qbitArea = new QbitArea();

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.GetComponent<MeshRenderer>().material = Resources.Load("GatePoint", typeof(Material)) as Material;
                sphere.SetActive(false);
                sphere.transform.parent = _qbit.qbit.transform;
                sphere.name = sphere.name + "Area" + number;
                sphere.transform.localScale = new Vector3(0.05f, 0.1f, 0.05f);
                sphere.transform.localPosition = new Vector3(0 + position * number, 0, 0);
                sphere.transform.localRotation = new Quaternion(0, 0, 0, 0);
                sphere.GetComponent<SphereCollider>().radius = 0.6f;
                qbitArea.isConfirmed = false;
                qbitArea.qbitArea = sphere;
                qbitArea.qbitGate = null;
                qbitArea.isMainArea = false;
                qbitArea.positionsOfConnectedQbits = new List<QbitAdditionalAreaPosition>();
                qbitArea.connectedGateArea = null;

                _qbit.line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(_qbit.line.GetComponent<LineRenderer>().GetPosition(1).x + position, 0, 0));             

                _qbit.areas.Add(qbitArea);
            }
            GameObject.Find("ClassicalRegister").transform.Find("Line0").GetComponent<LineRenderer>().SetPosition(1, new Vector3(GameObject.Find("ClassicalRegister").transform.Find("Line0").GetComponent<LineRenderer>().GetPosition(1).x + position, 0, 0));
            GameObject.Find("ClassicalRegister").transform.Find("Line1").GetComponent<LineRenderer>().SetPosition(1, new Vector3(GameObject.Find("ClassicalRegister").transform.Find("Line1").GetComponent<LineRenderer>().GetPosition(1).x + position, 0, 0));
        }

        public static void removeAdditionalSpaceAreas(float position)
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                _qbit.areas.Remove(_qbit.areas.Last());
                _qbit.line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(_qbit.line.GetComponent<LineRenderer>().GetPosition(1).x - position, 0, 0));
            }

            GameObject.Find("ClassicalRegister").transform.Find("Line0").GetComponent<LineRenderer>().SetPosition(1, new Vector3(GameObject.Find("ClassicalRegister").transform.Find("Line0").GetComponent<LineRenderer>().GetPosition(1).x - position, 0, 0));
            GameObject.Find("ClassicalRegister").transform.Find("Line1").GetComponent<LineRenderer>().SetPosition(1, new Vector3(GameObject.Find("ClassicalRegister").transform.Find("Line1").GetComponent<LineRenderer>().GetPosition(1).x - position, 0, 0));
        }

        public static void shiftGates()
        {
            //TODO: make an algorithm, which is going to shift gates left if it can.
        }

        public static void enableQbitSameRowAreas()
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.qbitArea.name.Contains(QbitsGates.areasInRow.ToString()))
                    {
                        _qbitArea.qbitArea.SetActive(true);
                    }
                    else
                    {
                        _qbitArea.qbitArea.SetActive(false);
                    }
                }
            }
        }

        public static void disableQbitSameRowAreas()
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.qbitGate != null)
                        _qbitArea.qbitArea.SetActive(true);
                    else
                        _qbitArea.qbitArea.SetActive(false);
                }
            }
        }

        public static void showAllForQbitAreas()
        {
            var maxNumberOfAreas = 0;

            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                var tempNumberOfGates = 0;
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.qbitGate != null || _qbitArea.connectedGateArea != null)
                    {
                        tempNumberOfGates++;
                    }                     
                }
                if (maxNumberOfAreas <= tempNumberOfGates)
                {
                    maxNumberOfAreas = tempNumberOfGates;
                }
            }
            //one additional for the area free row
            maxNumberOfAreas++;

            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                for(int x = 0; x < maxNumberOfAreas; x++)
                {
                    if (_qbit.areas[x].qbitGate == null && _qbit.areas[x].connectedGateArea == null)
                    {
                        _qbit.areas[x].qbitArea.SetActive(true);
                    }
                }
            }
        }
    }

    public class QbitsBoard : MonoBehaviour
    {
        private GameObject board;
        private GameObject boardBackground;
        private float boardConstant = 0.4f;
        private float areaPositionConstant = 0.25f;
        public static List<Qbit> listOfQbits;
        private int numberBits = 1;
        private LineRenderer lineRenderer;
        private float boardHeight;
        Material lineMaterial;
        private float lineWidth = 0.009f;

        void Start()
        {
            board = GameObject.Find("Board").gameObject;
            boardBackground = GameObject.Find("BoardBackground").gameObject;
            subscribeToConfirmPositionEvent();
            board.SetActive(false);
            listOfQbits = new List<Qbit>();
        }

        private void subscribeToConfirmPositionEvent()
        {
            EstablishWorldObjects.ConfirmPositionOfBoardEvent += setBoardWithLines;
        }

        private void setBoardWithLines()
        {
            deleteBoard();
            setBoard();
           
            for (int x = 0; x < numberBits; x++)
                createLine(x,"qbit");

            //add line for classical register
            createLine(numberBits, "ClassicalRegister");
            Canvas.isBoardActive = true;
            SharedStateSwitch.enableNavigationButtons();
        }

        private void setBoard()
        {
            board.SetActive(true);
            board.transform.position = MarkerIndicator.staticPlacementPose.position;
            board.transform.rotation = MarkerIndicator.staticPlacementPose.rotation;
            board.transform.localScale = new Vector3(1, 0.5f, 1);

            setBoardBackground(0);
        }

        private void setBoardBackground(int v)
        {
            boardHeight = 2* boardConstant + (numberBits * boardConstant);

            boardBackground.transform.localPosition = new Vector3(0, 0, 0);
            boardBackground.transform.localRotation = new Quaternion(0, 0, 0, 0);
            boardBackground.transform.localScale = new Vector3(1, boardHeight, 0);
        }

        private void deleteBoard()
        {
            foreach(Transform child in gameObject.transform)
            {
                if(!child.name.Contains("BoardBackground"))
                    Destroy(child.gameObject);
            }
            listOfQbits.Clear();
        }

        private void createLine(int number, string type)
        {
            

            if (type.Contains("qbit")){
                Qbit qbitobject = new Qbit();
                GameObject qbit = new GameObject("Qbit" + number);
                qbit.transform.parent = this.gameObject.transform;
                setQbitPosition(qbit, number + 1);
                GameObject line = new GameObject("Line");
                line.transform.parent = qbit.gameObject.transform;

                lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.useWorldSpace = false;
                setLinePositions();
                setLineWidth();
                setLineMaterial();
                setLineInBoard(line, number + 1);

                qbitobject.qbit = qbit;
                qbitobject.line = line;

                createNameTag("Qbit " + number, qbit);
                setAreasForGates(qbit, line, qbitobject);


                listOfQbits.Add(qbitobject);
            }
            else
            {
                GameObject classicRegister = new GameObject(type);
                classicRegister.transform.parent = this.gameObject.transform;
                setQbitPosition(classicRegister, number + 1);
                for(int x = 0; x < 2; x++)
                {
                    GameObject line = new GameObject("Line" +x);
                    line.transform.parent = classicRegister.gameObject.transform;
                    lineRenderer = line.AddComponent<LineRenderer>();
                    lineRenderer.useWorldSpace = false;
                    setLinePositions();
                    setLineWidth();
                    setLineMaterial();
                    setTwoLineInBoard(line, x);
                }              
                createNameTag("C" + number, classicRegister);
            }
            
        }

        private void createNameTag(string name, GameObject qbit)
        {
            GameObject nameTag = new GameObject();
            nameTag.transform.parent = qbit.transform;
            nameTag.name = "NameTag";
            var textMesh = nameTag.AddComponent<TextMesh>();
            textMesh.text = name;
            textMesh.anchor = TextAnchor.MiddleLeft;
            nameTag.transform.localPosition = new Vector3(-0.18f, 0, 0);
            nameTag.transform.localRotation = new Quaternion(0, 0, 0, 0);
            nameTag.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);      
        }

        private void setQbitPosition(GameObject qbit, int number)
        {
            //qbit.transform.localPosition = new Vector3(0 - 0.5f, 0 + 0.5f - (number * ((float)1 / (numberBits + 1))), 0);
            qbit.transform.localPosition = new Vector3(0 - 0.5f, 0 + boardBackground.transform.localScale.y/2 - (number * (boardBackground.transform.localScale.y / (numberBits + 2))), 0);
            qbit.transform.localRotation = new Quaternion(0, 0, 0, 0);
            qbit.transform.localScale = new Vector3(1, 1, 1);
        
        }

        private void setLineInBoard(GameObject line, int number)
        {
            line.transform.localPosition = new Vector3(0,  0, 0);
            line.transform.localRotation = new Quaternion(0, 0, 0, 0);
            line.transform.localScale = new Vector3(1, 0, 0);
        }

        private void setTwoLineInBoard(GameObject line, int number)
        {
            if(number == 0)
                line.transform.localPosition = new Vector3(0, 0.02f, 0);
            else
                line.transform.localPosition = new Vector3(0,-0.02f, 0);
            line.transform.localRotation = new Quaternion(0, 0, 0, 0);
            line.transform.localScale = new Vector3(1, 0, 0);
        }

        private void setLineMaterial()
        {
            lineMaterial = Resources.Load("LineMaterial", typeof(Material)) as Material;
            lineRenderer.material = lineMaterial;
        }

        private void setLineWidth()
        {
            lineRenderer.startWidth = lineWidth;
            //lineRenderer.endWidth = 0.009f;
        }

        private void setLinePositions()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(1, 0, 0));
        }

        private void setAreasForGates(GameObject qbit, GameObject line, Qbit qbitObject)
        {
            var position = areaPositionConstant;
            qbitObject.areas = new List<QbitArea>();
            for(int x = 1; x <= 3; x++)
            {
                QbitArea qbitArea = new QbitArea();

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.GetComponent<MeshRenderer>().material = Resources.Load("GatePoint", typeof(Material)) as Material;
                sphere.SetActive(false);
                sphere.transform.parent = qbit.transform;
                sphere.name = sphere.name + "Area" + x;
                sphere.transform.localScale = new Vector3(0.05f, 0.1f, 0.05f);
                sphere.transform.localPosition = new Vector3(0 + position * x, 0, 0);
                sphere.transform.localRotation = new Quaternion(0, 0, 0, 0);
                sphere.GetComponent<SphereCollider>().radius = 0.6f;
                qbitArea.isConfirmed = false;
                qbitArea.qbitArea = sphere;
                qbitArea.qbitGate = null;
                qbitArea.isMainArea = false;
                qbitArea.positionsOfConnectedQbits = new List<QbitAdditionalAreaPosition>();
                qbitObject.areas.Add(qbitArea);
                //Debug.Log(qbitArea.qbitGate);
            }
        }

        public void getNumberOfBits(GameObject _object)
        {
            numberBits = _object.GetComponent<Dropdown>().value + 1;
        }

        public void createAdditionalSpace()
        {
            bool state = Qbit.aditionalSpaceForAreas();
            if (state)
            {
                boardBackground.transform.localPosition = new Vector3(boardBackground.transform.localPosition.x + areaPositionConstant/2, 0, 0);
                float y = boardBackground.transform.localScale.y;
                float z = boardBackground.transform.localScale.z; 
                boardBackground.transform.localScale = new Vector3(boardBackground.transform.localScale.x + areaPositionConstant, y, z);
                Qbit.createAdditionalSpaceAreas(areaPositionConstant);
            }
        }

        public void removeAdditionalSpace()
        {
            bool state = Qbit.tooMuchSpaceForAreas();
            if (state)
            {
                //Debug.Log(state);
                boardBackground.transform.localPosition = new Vector3(boardBackground.transform.localPosition.x - areaPositionConstant / 2, 0, 0);
                float y = boardBackground.transform.localScale.y;
                float z = boardBackground.transform.localScale.z;
                boardBackground.transform.localScale = new Vector3(boardBackground.transform.localScale.x - areaPositionConstant, y, z);
                Qbit.removeAdditionalSpaceAreas(areaPositionConstant);
            }
            
        }

        public static bool finished = false;

        // Update is called once per frame
        void Update()
        {
            //TODO: add if for remove gates
            if(EstablishGateInWorldObject.enableGatePositioning){
                createAdditionalSpace();
                removeAdditionalSpace();
                if (!QbitsGates.buildingOnMultipleGatesAreas)
                {
                    Qbit.disableQbitSameRowAreas();
                    Qbit.showQbitAreas();
                }
                else
                {
                    
                    if(QbitsGates.tempNumberOfGateAreas == 0)
                    {
                        if(finished)
                            Qbit.disableQbitSameRowAreas();
                        finished = false;
                        Qbit.showAllForQbitAreas();
                        SharedStateSwitch.enableDisableQubitsAcceptGatesButton(true);
                    }
                    else
                    {
                        finished = true;
                        Qbit.enableQbitSameRowAreas();
                        SharedStateSwitch.enableDisableQubitsAcceptGatesButton(false);
                    }
                }                          
            }
            else
            {
                Qbit.hideQbitAreas();
            }
        }
    }
}

