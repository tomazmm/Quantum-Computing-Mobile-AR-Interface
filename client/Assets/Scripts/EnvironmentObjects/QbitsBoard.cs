using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class QbitsBoard : MonoBehaviour
    {
        private GameObject board;
        void Start()
        {
            board = GameObject.Find("Board").gameObject;
            subscribeToConfirmPositionEvent();
            board.GetComponent<MeshRenderer>().enabled = false;
        }

        private void subscribeToConfirmPositionEvent()
        {
            EstablishWorldObjects.ConfirmPositionOfBoardEvent += SetBoard;
        }

        private void SetBoard()
        {
            board.GetComponent<MeshRenderer>().enabled = true;
            board.transform.position = MarkerIndicator.staticPlacementPose.position;
            board.transform.rotation = MarkerIndicator.staticPlacementPose.rotation;

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

