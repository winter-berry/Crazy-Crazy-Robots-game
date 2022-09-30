using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    public bool DetectLayer(float radius, Vector2 direction, float range, string layerName)
    {
        bool detected = false;
        LayerMask layerMask = LayerMask.GetMask(layerName);      
        RaycastHit2D hits = Physics2D.CircleCast(transform.position, radius, direction, range, layerMask);

        if (hits.collider != null)
        {
            detected = true;
            Debug.Log("hit");
        }

        return detected;
    }
}
