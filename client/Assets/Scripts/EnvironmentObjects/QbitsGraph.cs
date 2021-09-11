using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class QbitsGraph : MonoBehaviour
    {
        // Start is called before the first frame update
        public static GameObject graph;

        void Start()
        {
            graph = GameObject.Find("PGraph").gameObject;
            subscribeToConfirmPositionEvent();
            graph.SetActive(false);
        }

        private void subscribeToConfirmPositionEvent()
        {
            EstablishWorldObjects.ConfirmPositionOfProbabilitiesGraphEvent += setGraph;
        }


        private void setGraph()
        {
            graph.SetActive(true);
            graph.transform.position = MarkerIndicator.staticPlacementPose.position;
            graph.transform.rotation = MarkerIndicator.staticPlacementPose.rotation;           
        }
    }
}

