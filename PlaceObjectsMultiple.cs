using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS
{
    public class PlaceObjectsMultiple : MonoBehaviour
    {
        public GameObject[] prefabObjects;

        private int objectIndex;
        private Vector3 hitPosition;
        private Quaternion hitRotation;

        // ARKit function to hit test against planes found in the physical environment.
        // The hitPosition and hitRotation variables will be set if hit succedes.
        bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    hitPosition = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    hitRotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                    return true;
                }
            }
            return false;
        }

        // Use this for initialization
        void Start()
        {
            objectIndex = 0;
        }

        public void SetObjectIndex(int idx)
        {
            objectIndex = idx;
        }

        // Update is called once per frame
        // Update is called once per frame
        void Update()
        {
            // If user touches screen
            if (Input.touchCount > 0 && prefabObjects[objectIndex] != null)
            {
                // Get touch of the first finger and check that touch is not ending
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    // Convert touch to camera's coordinate system
                    var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
                    ARPoint point = new ARPoint
                    {
                        x = screenPosition.x,
                        y = screenPosition.y
                    };
                    // Check if touch point when projected out from the camera hits a plane in the the physical environment
                    if (HitTestWithResultType(point, ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent))
                    {
                        // Let's create a game object at hit location and rotation
                        GameObject clone = Instantiate(prefabObjects[objectIndex]);
                        clone.transform.SetPositionAndRotation(hitPosition, hitRotation);
                        UnityARUserAnchorComponent uac = clone.AddComponent<UnityARUserAnchorComponent>() as UnityARUserAnchorComponent;
                    }
                }
            }
        }
    }
}

