using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{

    public class QbitArea
    {
        public bool isConfirmed;
        public GameObject qbitArea;
        public GameObject qbitGate;
        //TODO: more than one position
        public List<QbitArea> connectedGateAreas;
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
            foreach (Qbit _qbit in QbitsBoard.listOfQbits)
            {
                foreach (QbitArea _qbitArea in _qbit.areas)
                {
                    if (_qbitArea.qbitGate == null)
                    {
                        _qbitArea.qbitArea.SetActive(true);
                        break;
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
                        _qbitArea.qbitGate = null;
                }
            }
        }

        public static void shiftGates()
        {
            //TODO: make an algorithm, which is going to shift gates left if it can.
        }

    }

    public class QbitsBoard : MonoBehaviour
    {
        private GameObject board;
        private float boardConstant = 0.25f;
        public static List<Qbit> listOfQbits;
        private int numberBits = 1;
        private LineRenderer lineRenderer;
        private float boardHeight;
        Material lineMaterial;
        private float lineWidth = 0.009f;

        void Start()
        {
            board = GameObject.Find("Board").gameObject;
            subscribeToConfirmPositionEvent();
            board.GetComponent<MeshRenderer>().enabled = false;
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
                createLines(x);            
        }

        private void setBoard()
        {
            //boardConstant = (float)1 / numberBits;
            boardHeight = boardConstant + (numberBits * boardConstant);

            board.GetComponent<MeshRenderer>().enabled = true;
            board.transform.position = MarkerIndicator.staticPlacementPose.position;
            board.transform.rotation = MarkerIndicator.staticPlacementPose.rotation;
            board.transform.localScale = new Vector3(1, boardHeight, 1);
        }

        private void deleteBoard()
        {
            foreach(Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
            listOfQbits.Clear();
        }

        private void createLines(int number)
        {
            Qbit qbitobject = new Qbit();

            GameObject qbit = new GameObject("Qbit" + number);
            qbit.transform.parent = this.gameObject.transform;
            setQbitPosition(qbit, number+ 1);
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

            setAreasForGates(qbit, line, qbitobject);


            listOfQbits.Add(qbitobject);
            //Debug.Log(listOfLines[0]);

        }

        private void setQbitPosition(GameObject qbit, int number)
        {
            qbit.transform.localPosition = new Vector3(0 - 0.5f, 0 + 0.5f - (number * ((float)1 / (numberBits + 1))), 0);
            qbit.transform.localRotation = new Quaternion(0, 0, 0, 0);
            qbit.transform.localScale = new Vector3(1, 1 - 0.2f * (numberBits - 1), 1);
        }

        private void setLineInBoard(GameObject line, int number)
        {
            // y is 0.5 because upper or lower half of the whole board is at this position start or end
            line.transform.localPosition = new Vector3(0,  0, 0);
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
            var position = 0.25f;
            qbitObject.areas = new List<QbitArea>();
            for(int x = 1; x <= 3; x++)
            {
                //var point = new GameObject(qbit.name + "PointArea" + x);
                //point.transform.parent = qbit.transform;
                //var pointRenderer = point.AddComponent<LineRenderer>();
                //pointRenderer.useWorldSpace = false;
                //pointRenderer.startWidth = 0.1f;
                //pointRenderer.positionCount = 2;
                //pointRenderer.SetPosition(0, new Vector3(0, 0.05f, 0));
                //pointRenderer.SetPosition(1, new Vector3(0, 0, 0));
                //point.gameObject.transform.localPosition = new Vector3(0 + position * x, 0, 0);

                ////box colider
                //var boxColider = point.AddComponent<BoxCollider>();
                //boxColider.center = new Vector3(0.025f, 0.025f, 0);
                //boxColider.size = new Vector3(0.05f, 0.05f, 0.1f);

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
                qbitObject.areas.Add(qbitArea);
                //Debug.Log(qbitArea.qbitGate);
            }
        }

        public void getNumberOfBits(GameObject _object)
        {
            numberBits = _object.GetComponent<Dropdown>().value + 1;
        }

        //TODO: create additional space on board
        public void createAdditionalSpace()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //TODO: add if for remove gates
            if(EstablishGateInWorldObject.enableGatePositioning){
                Qbit.showQbitAreas();
            }else
            {
                Qbit.hideQbitAreas();
            }
        }
    }
}

