using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByTarget : MonoBehaviour
{
    public Transform target;
    public float angleOffset;
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angleOffset;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }
}
