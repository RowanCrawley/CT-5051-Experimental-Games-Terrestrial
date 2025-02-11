using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject StartPos;
    [SerializeField] GameObject EndPos;
    [SerializeField] GameObject GameObject;

    public float speed = 1.0f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        {
            Vector2 vector2 = rb.velocity;
            vector2 *= speed;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(StartPos.transform.position, 0.5f);
        Gizmos.DrawWireSphere(EndPos.transform.position, 0.5f);
    }

}
