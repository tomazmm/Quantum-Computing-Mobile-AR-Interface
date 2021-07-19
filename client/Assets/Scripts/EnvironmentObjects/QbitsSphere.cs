using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantomCOMP
{
    public class QbitsSphere : MonoBehaviour
    {
        private GameObject sphere;

        void Start()
        {
            sphere = GameObject.Find("QSphere").gameObject;
            subscribeToConfirmPositionEvent();
            sphere.GetComponent<MeshRenderer>().enabled = false;
        }

        private void subscribeToConfirmPositionEvent()
        {
            EstablishWorldObjects.ConfirmPositionOfSphereEvent += setSphere;
        }


        private void setSphere()
        {

            sphere.GetComponent<MeshRenderer>().enabled = true;
            sphere.transform.position = MarkerIndicator.staticPlacementPose.position;
            sphere.transform.rotation = MarkerIndicator.staticPlacementPose.rotation;
            //sphere.transform.localScale = new Vector3(1, 1, 1);
        }


    }
}

