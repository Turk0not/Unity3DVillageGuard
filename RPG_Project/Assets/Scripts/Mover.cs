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

    //Bast���n yere karakterin gitmesini sa�layacak metod
    private void MoveToCursor()
    {
        //kameradan t�klad���n ���n g�nderen kod
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
