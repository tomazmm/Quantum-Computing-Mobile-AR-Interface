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
        public bool usedinState;
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

            for (int y = 0; y < QbitsBoard.listOfQbits.Count; y++)
            {
                var _qbit = QbitsBoard.listOfQbits[y];
                var lastGate = 1;
                for (int x = 0; x < _qbit.areas.Count(); x++)
                {

                    if (_qbit.areas[x].qbitGate != null || _qbit.areas[x].connectedGateArea != null)
                    {
                        if (lastGate <= (x + 2))
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
                if (_qbit.areas.Count() >= 3)
                {
                    if (_qbit.areas.Last().qbitGate == null && _qbit.areas[_qbit.areas.Count() - 2].qbitGate == null && _qbit.areas.Count() - 1 >= 3)
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
                QbitsBoard.maxNumberOfAreas = number;

                var qbitArea = new QbitArea();

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/GatePoint", typeof(Material)) as Material;
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
                qbitArea.usedinState = false;
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
                GameObject.Destroy(_qbit.areas.Last().qbitArea);
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

        public static int getMaxPositionOfGateArea()
        {
            var maxNumberOfAreas = 0;

            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                var tempNumberOfGates = 0;
                for (int x = 0; x < _qbit.areas.Count(); x++)
                {
                    if (_qbit.areas[x].qbitGate != null || _qbit.areas[x].connectedGateArea != null)
                    {
                        tempNumberOfGates = x + 1;
                    }
                }
                //foreach (QbitArea _qbitArea in _qbit.areas)
                //{
                //    if (_qbitArea.qbitGate != null || _qbitArea.connectedGateArea != null)
                //    {
                //        tempNumberOfGates++;
                //    }
                //}
                if (maxNumberOfAreas <= tempNumberOfGates)
                {
                    maxNumberOfAreas = tempNumberOfGates;
                }
            }
            //one additional for the area free row
            maxNumberOfAreas++;
            if (maxNumberOfAreas > 3)
                return maxNumberOfAreas;
            else
                return 3;
        }

        public static void showAllForQbitAreas(int maxNumberOfAreas)
        {

            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                for (int x = 0; x < maxNumberOfAreas; x++)
                {
                    if (_qbit.areas[x].qbitGate == null && _qbit.areas[x].connectedGateArea == null)
                    {
                        _qbit.areas[x].qbitArea.SetActive(true);
                    }
                }
            }
        }

        public static void removeAllConnectedAreas(int position)
        {
            int x = 0;
            //TODO: Remove all connected gates if qbit is destroyed
            foreach (QbitArea _tempQbitArea in QbitsBoard.listOfQbits[position].areas)
            {
                if(_tempQbitArea.qbitGate != null)
                {
                    if (_tempQbitArea.connectedGateArea != null
                    || _tempQbitArea.positionsOfConnectedQbits.Count() != 0
                    || _tempQbitArea.qbitGate.name.Contains("Measurementgate"))
                    {
                        foreach (Qbit _qbit in QbitsBoard.listOfQbits)
                        {
                            var _qbitArea = _qbit.areas[x];
                            if ((_qbitArea != _tempQbitArea) 
                                && (_qbitArea.connectedGateArea != null || (_qbitArea.isMainArea && _qbitArea.positionsOfConnectedQbits.Count != 0)) 
                                && ((_qbitArea.connectedGateArea == _tempQbitArea.connectedGateArea)
                                        || _tempQbitArea.connectedGateArea == _qbitArea)
                                        || (_qbitArea.connectedGateArea == _tempQbitArea))
                            {
                                if (_qbitArea.qbitGate != null)
                                    GameObject.Destroy(_qbitArea.qbitGate);
                                QbitsGates.gates.Remove(_qbitArea.qbitGate);
                                _qbitArea.qbitGate = null;
                                _qbitArea.isConfirmed = false;
                                _qbitArea.connectedGateArea = null;
                                _qbitArea.isMainArea = false;
                                _qbitArea.usedinState = false;
                                _qbitArea.positionsOfConnectedQbits.Clear();
                            }
                        }
                    }
                }               
                x++;
            }
            Qbit.hideQbitAreas();
        }

        public static void removeFromGatesList(Qbit qbit)
        {
            foreach(QbitArea _qbitArea in qbit.areas)
            {
                QbitsGates.gates.Remove(_qbitArea.qbitGate);
            }
        }

        public static int getPositionOfGateArea()
        {
            var maxNumberOfAreas = 0;

            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                var tempNumberOfGates = 0;
                for (int x = 0; x < _qbit.areas.Count(); x++)
                {
                    if (_qbit.areas[x].qbitGate != null || _qbit.areas[x].connectedGateArea != null)
                    {
                        tempNumberOfGates = x + 1;
                    }
                }
                if (maxNumberOfAreas <= tempNumberOfGates)
                {
                    maxNumberOfAreas = tempNumberOfGates;
                }
            }
            maxNumberOfAreas++;
            return maxNumberOfAreas;
        }

        public static void decreaseNumberOfAdditionalGates()
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                var x = 0;
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.isMainArea && _qbitArea.positionsOfConnectedQbits.Count() != 0)
                    {
                        var z = 0;
                        var t = 0;
                        foreach (Qbit _tempQbit in QbitsBoard.listOfQbits)
                        {
                            if (_tempQbit.areas[x].connectedGateArea == _qbitArea && _tempQbit.areas[x].qbitGate != null)
                            {
                                var list = _qbitArea.positionsOfConnectedQbits;
                                list[t].qbit = z;
                                t++;
                            }
                            z++;
                        }
                    }
                    x++;
                }
            }
        }

        internal static void assignAdditionalMeasurementareas()
        {
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                var x = 0;
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if(_qbitArea.qbitGate != null)
                    {
                        if (_qbitArea.isMainArea && _qbitArea.qbitGate.name.Contains("Measurementgate"))
                        {
                            foreach (Qbit _tempQbit in QbitsBoard.listOfQbits)
                            {
                                _tempQbit.areas[x].connectedGateArea = _qbitArea;
                            }
                        }
                    }                   
                   x++;
                }
            }
        }
    }

    public class QbitsBoard : MonoBehaviour
    {
        private GameObject board;
        private GameObject boardBackground;
        private GameObject classicRegister;
        private GameObject addButton;
        private List<GameObject> deleteButtons;
        private List<GameObject> listPhaseDisks;
        private float boardConstant = 0.4f;
        private float areaPositionConstant = 0.25f;
        public static List<Qbit> listOfQbits;
        public static int maxNumberOfAreas;
        private int numberBits = 1;
        private int selectedNumberBits = 1;
        private LineRenderer lineRenderer;
        private float boardHeight;
        Material lineMaterial;
        private float lineWidth = 0.009f;

        public Transform addButtonPrefab;
        public Transform deleteButtonPrefab;
        public Transform phaseDiskPrefab;

        void Start()
        {
            board = GameObject.Find("Board").gameObject;
            boardBackground = GameObject.Find("BoardBackground").gameObject;
            subscribeToConfirmPositionEvent();
            board.SetActive(false);
            listOfQbits = new List<Qbit>();
            deleteButtons = new List<GameObject>();
            listPhaseDisks = new List<GameObject>();
            maxNumberOfAreas = 3;
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
            if(numberBits < 5)
                createLine(numberBits, "AddButton");
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
                    GameObject.Destroy(child.gameObject);
            }
            numberBits = selectedNumberBits;
            listOfQbits.Clear();
            deleteButtons.Clear();
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

                createNameTag("Qbit " + number, qbit, number);
                createAddPhaseDisk(qbit);
                setAreasForGates(qbit, line, qbitobject);


                listOfQbits.Add(qbitobject);
            }
            else if(type.Contains("ClassicalRegister"))
            {
                classicRegister = new GameObject(type);
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
                createNameTag("C" + number, classicRegister, number);
            }else
            {
                showAddButton();
            }
            
        }

        private void createAddPhaseDisk(GameObject qbit)
        {
            Transform phaseDisk = createPhaseDisk();
            phaseDisk.transform.parent = qbit.transform;
            setPhaseDiskPosition(phaseDisk);
            listPhaseDisks.Add(phaseDisk.gameObject);
        }

        private void setPhaseDiskPosition(Transform phaseDisk)
        {
            phaseDisk.transform.localPosition = new Vector3(1.1f, 0, 0);
            phaseDisk.transform.localRotation = Quaternion.Euler(0, 180, 0);
            phaseDisk.transform.localScale = new Vector3(0.4f, 0.8f, 0.1f);
        }

        private void setAddButtonPosition(Transform button)
        {
            Vector3 buttonPosition = new Vector3(0 - 0.63f, 0 + boardBackground.transform.localScale.y / 2 - (numberBits * boardConstant) - boardConstant/2, 0);
            button.transform.localPosition = buttonPosition;
            button.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        public void addAnotherQubit()
        {
            //Debug.Log("Add line was clicked");
            //createLine(numberBits, "qubit");
            numberBits++;
            resizeBoard();
            for(int x = 0; x < numberBits; x++)
            {
                if (x != numberBits - 1)
                    repositionOfQbits(x);
                else
                    createLine(numberBits - 1, "qbit");
            }
            repositionOfClassicalRegister();
            if(numberBits < 5)
            {
                repositionOfAddButton();
            }else
                deleteAddButton();

            rearangeAllConntectedLinesAndMeasurements("add", numberBits - 1);
            Qbit.assignAdditionalMeasurementareas();
        }

        private void resizeBoard()
        {
            boardHeight = 2 * boardConstant + (numberBits * boardConstant);
            //Debug.Log(boardHeight);
            boardBackground.transform.localPosition = new Vector3(boardBackground.transform.localPosition.x, 0, 0);
            boardBackground.transform.localRotation = new Quaternion(0, 0, 0, 0);
            boardBackground.transform.localScale = new Vector3(boardBackground.transform.localScale.x, boardHeight, 0);
        }

        private void deleteAddButton()
        {
            GameObject.Destroy(addButton);
        }

        private void repositionOfAddButton()
        {
            Vector3 buttonPosition = new Vector3(0 - 0.63f, 0 + boardBackground.transform.localScale.y / 2 - (numberBits * boardConstant) - boardConstant / 2, 0);
            addButton.transform.localPosition = buttonPosition;
        }

        private void repositionOfClassicalRegister()
        {
            var textMesh = classicRegister.transform.Find("NameTag").gameObject.GetComponent<TextMesh>();
            textMesh.text = "C" + numberBits;
            classicRegister.transform.localPosition = new Vector3(0 - 0.5f, 0 + boardBackground.transform.localScale.y / 2 - (numberBits * boardConstant) - boardConstant, 0);
        }

        private void repositionOfQbits(int x)
        {
            listOfQbits[x].qbit.gameObject.name = "Qbit" + x;
            listOfQbits[x].qbit.transform.Find("NameTag").GetComponent<TextMesh>().text = "Qbit" + x;
            listOfQbits[x].qbit.gameObject.transform.localPosition = new Vector3(0 - 0.5f, 0 + boardBackground.transform.localScale.y / 2 - ((x + 1) * (boardBackground.transform.localScale.y / (numberBits + 2))), 0);
            //Debug.Log(x +": "+ (0 + boardBackground.transform.localScale.y / 2 - (x + 1 * (boardBackground.transform.localScale.y / (numberBits + 2)))));
        }

        private Transform createAddButton()
        {          
            Transform prefabButton;
            prefabButton = Instantiate(addButtonPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            prefabButton.transform.parent = this.gameObject.transform;
            prefabButton.name = "AddButton";
            return prefabButton;
        }

        private Transform createPhaseDisk()
        {
            Transform prefabPhaseDisk;
            prefabPhaseDisk = Instantiate(phaseDiskPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            prefabPhaseDisk.transform.parent = this.gameObject.transform;
            prefabPhaseDisk.name = "PhaseDisk";
            return prefabPhaseDisk;
        }

        private void createNameTag(string name, GameObject qbit, int number)
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

            if(!qbit.name.Contains("ClassicalRegister"))
                createDeleteButton(qbit, number);
        }

        private void createDeleteButton(GameObject qbit, int number)
        {
            Transform deleteButton;
            deleteButton = Instantiate(deleteButtonPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            deleteButton.transform.parent = qbit.transform;
            deleteButton.name = "DeleteButton";

            deleteButton.transform.localPosition = new Vector3(-0.28f, 0, 0);
            deleteButton.transform.localRotation = new Quaternion(0, 0, 0, 0);
            deleteButton.transform.localScale = new Vector3(0.1f, 0.15f, 0.1f);
            deleteButton.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate () { this.removeAnotherQubit(number); });
            deleteButtons.Add(deleteButton.gameObject);

        }

        private void removeAnotherQubit(int position)
        {
            //Debug.Log(position);
            if(numberBits == 5)
                showAddButton();
            if (listOfQbits.Count() > 1)
            {
                Qbit.removeAllConnectedAreas(position);
                GameObject.Destroy(listOfQbits[position].qbit);
                Qbit.removeFromGatesList(listOfQbits[position]);
                listOfQbits.RemoveAt(position);
                deleteButtons.RemoveAt(position);              
                numberBits--;
                Qbit.decreaseNumberOfAdditionalGates();
            }          

            //Debug.Log("Delete line was clicked");
            resizeBoard();
            listPhaseDisks.Remove(listPhaseDisks.Last());
            for (int x = 0; x < numberBits; x++)
            {
                var button_n = x;
                repositionOfQbits(x);
                deleteButtons[x].transform.Find("Canvas").transform.Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
                deleteButtons[x].transform.Find("Canvas").transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate () { this.removeAnotherQubit(button_n); });
            }
            repositionOfClassicalRegister();
            repositionOfAddButton();
            rearangeAllConntectedLinesAndMeasurements("remove", position);           
        }


        private void rearangeAllConntectedLinesAndMeasurements(string type, int position)
        {
            foreach(Qbit _qbit in listOfQbits)
            {
                var y = 0;
                foreach (QbitArea _qbitArea in _qbit.areas)
                {        
                    if(_qbitArea.qbitGate != null)
                    {
                        // connected lined
                        if (_qbitArea.isMainArea && _qbitArea.positionsOfConnectedQbits.Count() != 0 && type.Contains("remove"))
                        {
                            var x = 2;
                            foreach (QbitAdditionalAreaPosition _qbitAddAreaPosition in _qbitArea.positionsOfConnectedQbits)
                            {
                                //var mainPosition = int.Parse(_qbit.qbit.name.Substring(4))
                                //if (_qbitAddAreaPosition.qbit > mainPosition && mainPosition >)
                                //    _qbitAddAreaPosition.qbit -= 1;

                                int positionInList = _qbitAddAreaPosition.qbit;
                                int positionOfArea = _qbitAddAreaPosition.area - 1;

                                float distance = Vector3.Distance(listOfQbits[positionInList].areas[positionOfArea].qbitArea.transform.position, _qbitArea.qbitGate.transform.position) * 10;
                                var line = _qbitArea.qbitGate.transform.Find("lineto" + _qbitArea.qbitGate.name + "add" + x);
                                line.transform.localScale = new Vector3(0.1f, distance + distance / 10, 0.1f);
                                if (positionInList > int.Parse(_qbit.qbit.name.Substring(4)))
                                    line.transform.localPosition = new Vector3(0, 1 * (-distance / 2 - distance / 20), 0);
                                else
                                    line.transform.localPosition = new Vector3(0, -1 * (-distance / 2 - distance / 20), 0);
                                x++;
                            }
                        }

                        if (_qbitArea.qbitGate.name.Contains("Measurementgate"))
                        {
                            float distance = Vector3.Distance(QbitsBoard.listOfQbits[numberBits - 1].areas[y].qbitArea.transform.position, _qbitArea.qbitGate.transform.position) * 10;
                            float additionalForC = 2;

                            var line = _qbitArea.qbitGate.transform.Find("lineto" + _qbitArea.qbitGate.name + "add" + 0);
                            line.transform.localScale = new Vector3(0.1f, (additionalForC + distance) + additionalForC / 10 + distance / 10, 0.1f);
                            line.transform.localPosition = new Vector3(0, -distance / 2 - additionalForC / 2 - additionalForC / 20 - distance / 20, 0);
                        }
                    }             
                    y++;
                }              
            }
        }

        private void showAddButton()
        {
            Transform prefabButton = createAddButton();
            setAddButtonPosition(prefabButton);
            prefabButton.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate () { this.addAnotherQubit(); });
            addButton = prefabButton.gameObject;
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
            lineMaterial = Resources.Load("GateMaterials/LineMaterial", typeof(Material)) as Material;
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
            for(int x = 1; x <= maxNumberOfAreas; x++)
            {
                QbitArea qbitArea = new QbitArea();

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.GetComponent<MeshRenderer>().material = Resources.Load("GateMaterials/GatePoint", typeof(Material)) as Material;
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
                qbitArea.usedinState = false;
                qbitArea.positionsOfConnectedQbits = new List<QbitAdditionalAreaPosition>();
                qbitObject.areas.Add(qbitArea);
                //Debug.Log(qbitArea.qbitGate);
            }
        }

        public void getNumberOfBits(GameObject _object)
        {
            selectedNumberBits = _object.GetComponent<Dropdown>().value + 1;
            numberBits = selectedNumberBits;
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
                repositionPfaseDisks("right");
                Qbit.createAdditionalSpaceAreas(areaPositionConstant);
            }
        }

        private void repositionPfaseDisks(string direction)
        {         
            foreach(GameObject pfDisk in listPhaseDisks)
            {
                Vector3 pfDiskOldPosition = pfDisk.transform.localPosition;
                if(direction.Contains("right"))
                    pfDisk.transform.localPosition = new Vector3(pfDiskOldPosition.x + areaPositionConstant, pfDiskOldPosition.y, pfDiskOldPosition.z);
                else
                    pfDisk.transform.localPosition = new Vector3(pfDiskOldPosition.x - areaPositionConstant, pfDiskOldPosition.y, pfDiskOldPosition.z);
            }   
        }

        public void removeAdditionalSpace()
        {
            bool state = Qbit.tooMuchSpaceForAreas();
            if (state)
            {
                boardBackground.transform.localPosition = new Vector3(boardBackground.transform.localPosition.x - areaPositionConstant / 2, 0, 0);
                float y = boardBackground.transform.localScale.y;
                float z = boardBackground.transform.localScale.z;
                boardBackground.transform.localScale = new Vector3(boardBackground.transform.localScale.x - areaPositionConstant, y, z);
                repositionPfaseDisks("left");
                Qbit.removeAdditionalSpaceAreas(areaPositionConstant);
            }         
        }

        public static bool finished = false;

        void Update()
        {
            //TODO: add if for remove gates
            if (EstablishGateInWorldObject.enableGatePositioning){
                maxNumberOfAreas = Qbit.getMaxPositionOfGateArea();
                createAdditionalSpace();
                removeAdditionalSpace();
                if (!QbitsGates.buildingOnMultipleGatesAreas)
                {
                    //Debug.Log("here");
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
                        var maxNumberOfArea = Qbit.getPositionOfGateArea();
                        //Debug.Log(maxNumberOfArea);
                        Qbit.showAllForQbitAreas(maxNumberOfArea);
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
                removeAdditionalSpace();
            }
        }
    }
}

