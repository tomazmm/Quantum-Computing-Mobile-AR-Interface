using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace QuantomCOMPPC
{
    public class Gestures : MonoBehaviour
    {
        public new Camera camera;
        private GameObject selectedArea;
        public static WorldObject.Gates gate;

        public delegate void PositionGate(GameObject gateArea, WorldObject.Gates gate);
        public static event PositionGate PositionOfGateEvent;

        public delegate void RemoveGate(GameObject gateArea, WorldObject.Gates gate);
        public static event RemoveGate RemoveGateEvent;

        public delegate void SwitchGate(GameObject gateArea, WorldObject.Gates gate);
        public static event SwitchGate SwitchGateEvent;

        void Update()
        {
            if (((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) || Input.GetMouseButtonDown(0))
            {
                Ray raycast_m = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit_m;
                if (Physics.Raycast(raycast_m, out raycastHit_m))
                {
                    if(raycastHit_m.collider.name.Contains("SphereArea"))
                    {
                        //Debug.Log("SphereArea clicked");
                        selectedArea = raycastHit_m.collider.gameObject;
                        if(EstablishGateInWorldObject.enableGatePositioning)
                            PositionOfGateEvent(selectedArea, gate);
                    }

                    if (raycastHit_m.collider.name.Contains("gate"))
                    {
                        //Debug.Log("gate clicked");
                        selectedArea = raycastHit_m.collider.gameObject;
                        if (EstablishGateInWorldObject.enableGatePositioning)
                        {                          
                            if (selectedArea.name.Contains(gate.ToString()) && !selectedArea.name.Contains(WorldObject.Gates.CNotgate.ToString()) && !selectedArea.name.Contains(WorldObject.Gates.Toffoligate.ToString()))
                                RemoveGateEvent(selectedArea, gate);
                            else
                                SwitchGateEvent(selectedArea, gate);
                        }                       
                    }

                    //TODO: check if line between areas is clicked (remove, switch)

                }
            }
        }
    }
}

