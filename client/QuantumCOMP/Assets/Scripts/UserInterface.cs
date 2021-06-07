
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace QuantomCOMP
{
    public class UserInterface : MonoBehaviour
    {
        private ARRaycastManager arRaycastManager;
        private Pose worldPosition;
        private bool positionIsValid = false;

        // Start is called before the first frame update
        void Start()
        {
            Debug.LogError(Screen.width + "  " + Screen.height + "  " + gameObject.name);
            gameObject.transform.localScale = new Vector3(Screen.width / 100, Screen.height / 100, 1);
            foreach (Transform child in transform)
            {
                Debug.LogError(child.name);
            }
            //arRaycastManager = FindObjectOfType<ARRaycastManager>();
        }

        // Update is called once per frame
        void Update()
        {
            //UpdateGlobalPosition();
            //UpdateUserInterface();
            //Debug.LogError(gameObject.name);
        }

        private void UpdateUserInterface()
        {
            if (positionIsValid)
            {
                gameObject.SetActive(true);
                gameObject.transform.SetPositionAndRotation(worldPosition.position, worldPosition.rotation);
            }
            else
                gameObject.SetActive(false);
        }

        private void UpdateGlobalPosition()
        {
            Vector3 screenPoints = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
            // check if raycast hits surface
            arRaycastManager.Raycast(screenPoints, raycastHits, TrackableType.Planes);

            positionIsValid = raycastHits.Count > 0;
            if (positionIsValid)
                worldPosition = raycastHits[0].pose;
        }


    }
}



