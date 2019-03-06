
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class LookAtCharacters : MonoBehaviour
{
    private Transform target;
    public float maxAngle = 35.0f;
    private Quaternion baseRotation;
    private Quaternion targetRotation;


    void Start()
    {
        if (GameManager.level == 3)
        {
            maxAngle = maxAngle / 2f;
        }

        baseRotation = transform.rotation;
        target = GameObject.FindGameObjectWithTag("HeadCollider").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 look = target.transform.position - transform.position;
        look.y = 0;

        Quaternion q = Quaternion.LookRotation(look);
        if (Quaternion.Angle(q, baseRotation) <= maxAngle) { 
            targetRotation = q;  
        }        

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
