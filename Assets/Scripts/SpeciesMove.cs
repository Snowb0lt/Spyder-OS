using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesMove : MonoBehaviour
{
    [SerializeField] private Transform SpecimenMovementPoint;
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            transform.position = Vector3.Lerp(transform.position, SpecimenMovementPoint.position, 3 * Time.deltaTime) ;
        }
        
    }
}
