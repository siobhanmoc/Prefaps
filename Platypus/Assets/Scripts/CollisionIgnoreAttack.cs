using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnoreAttack : MonoBehaviour {

    public LayerMask PlayerLayer;
//    public GameManager manager;
    [SerializeField]
    private float forceOnFire = 1.0f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
            return;
        }

        if (collision.gameObject.layer == PlayerLayer)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
            return;
        }

        if (collision.gameObject.tag == "Clutter")
            {
            Rigidbody rb = GetComponent<Rigidbody>();

            rb.AddForceAtPosition(transform.forward * forceOnFire, transform.forward);

            int clutterScore = 10;

            manager.GetComponent<GameManager>().addScore(clutterScore);

            collision.gameObject.tag = "AttackedObject" ;
        }
    }
}
