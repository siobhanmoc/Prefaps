using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnoreAttack : MonoBehaviour {

    public LayerMask PlayerLayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }

        if (collision.gameObject.layer == PlayerLayer)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }
}
