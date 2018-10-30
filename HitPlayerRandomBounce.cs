using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS
{
    public class HitPlayerRandomBounce : MonoBehaviour
    {
        public Transform m_HitTransform;
        public float bounceHeight = 0.5f;
        public float maxRayDistance = 30.0f;
        public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer

        bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    Debug.Log("Got hit!");
                    m_HitTransform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    m_HitTransform.position += new Vector3(0f, bounceHeight, 0f);
                    //m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
                    m_HitTransform.rotation = Random.rotation;
                    return true;
                }
            }
            return false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && m_HitTransform != null)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
                    ARPoint point = new ARPoint {
                        x = screenPosition.x,
                        y = screenPosition.y
                    };
                    HitTestWithResultType(point, ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
                }
            }
        }
    }
}
