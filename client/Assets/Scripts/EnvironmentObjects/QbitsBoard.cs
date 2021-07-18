using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class QbitsBoard : MonoBehaviour
    {
        private GameObject board;
        private float boardConstant = 0.25f;
        private List<GameObject> listOfLines;
        private int numberBits = 1;
        private LineRenderer lineRenderer;
        private float boardHeight;
        Material lineMaterial;

        void Start()
        {
            board = GameObject.Find("Board").gameObject;
            subscribeToConfirmPositionEvent();
            board.GetComponent<MeshRenderer>().enabled = false;
            listOfLines = new List<GameObject>();
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
        }

        private void createLines(int number)
        {
            GameObject line = new GameObject("Line"+number);
            line.transform.parent = this.gameObject.transform;

            lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            setLinePositions();
            setLineWidth();
            setLineMaterial();
            setLineInBoard(line, number + 1);
            

            listOfLines.Add(line);
            Debug.Log(this.gameObject);

        }

        private void setLineInBoard(GameObject line, int number)
        {
            // y is 0.5 because upper or lower half of the whole board is at this position start or end
            line.transform.localPosition = new Vector3(-0.5f,  0.5f - (number * ((float)1 / (numberBits + 1))), 0);
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
            lineRenderer.startWidth = 0.009f;
            //lineRenderer.endWidth = 0.009f;
        }

        private void setLinePositions()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(1, 0, 0));
        }

        public void getNumberOfBits(GameObject _object)
        {
            //Debug.Log(_object.GetComponent<Dropdown>().value + 1);
            numberBits = _object.GetComponent<Dropdown>().value + 1;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

