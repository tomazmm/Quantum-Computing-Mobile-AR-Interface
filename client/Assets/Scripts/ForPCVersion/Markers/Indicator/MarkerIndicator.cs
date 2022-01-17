
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace QuantomCOMPPC
{
    public class MarkerIndicator : Marker
    {
        private GameObject placementIndicator;
        private Pose placementPose;
        public static Pose staticPlacementPose;
        private ARRaycastManager aRRaycastManager;
        private bool placementPoseIsValid = false;

        void Start()
        {
            placementIndicator = gameObject.transform.Find("Marker").gameObject;
            aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        }

        void Update()
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            staticPlacementPose = placementPose;
        }

        private void UpdatePlacementIndicator()
        {
            if (placementPoseIsValid)
            {
                placementIndicator.SetActive(true);
                placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else
            {
                placementIndicator.SetActive(false);
            }
        }

        private void UpdatePlacementPose()
        {
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                placementPose = hits[0].pose;
                var forwardCamera = Camera.current.transform.forward;
                var cameraRotation = new Vector3(forwardCamera.x, 0, forwardCamera.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraRotation);
            }
        }
    }
}



