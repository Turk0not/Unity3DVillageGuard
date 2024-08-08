using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    Ray lstRay;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
             
    }

    //Bastýðýn yere karakterin gitmesini saðlayacak metod
    private void MoveToCursor()
    {
        //kameradan týkladýðýn ýþýn gönderen kod
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
