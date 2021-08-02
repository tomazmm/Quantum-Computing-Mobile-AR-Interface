using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace QuantomCOMP
{
    public class Gestures : MonoBehaviour
    {
        public new Camera camera;
        private GameObject selectedGameGateArea;

        public delegate void PositionGate(GameObject gateArea);
        public static event PositionGate PositionOfGateEvent;

        void Update()
        {
            if (((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetMouseButtonDown(0))
            {
                Ray raycast_m = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit_m;
                //Debug.Log(raycast_m.direction.x);
                if (Physics.Raycast(raycast_m, out raycastHit_m))
                {
                    //Debug.Log(raycastHit_m.collider.name);
                    if (raycastHit_m.collider.name.Contains("SphereArea"))
                    {
                        Debug.Log("SphereArea clicked");
                        selectedGameGateArea = raycastHit_m.collider.gameObject;
                        if(EstablishGateInWorldObject.enableGatePositioning)
                            PositionOfGateEvent(selectedGameGateArea);
                    }

                }
            }
        }
    }
}

