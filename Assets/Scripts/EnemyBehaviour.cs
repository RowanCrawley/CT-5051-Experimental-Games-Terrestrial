using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject StartPos;
    [SerializeField] GameObject EndPos;
    [SerializeField] Rigidbody2D rb2D;

    public float speed = 2;

    private void Start()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        {
            //transform.position = new Vector3(26,6,0);
            //transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(StartPos.transform.position, 0.5f);
        Gizmos.DrawWireSphere(EndPos.transform.position, 0.5f);
    }
}
