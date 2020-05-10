using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    public float hitEffectLifetime;
    public GameObject hitEffectPrefab;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update projectile transform
    void FixedUpdate() {
        if( rb == null) return;

        rb.position += transform.forward * speed * Time.deltaTime;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;

        ContactPoint contactPoint = other.contacts[0];

        var rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        var effectObject = Instantiate(hitEffectPrefab, contactPoint.point, rotation) as GameObject;

        Destroy(effectObject, hitEffectLifetime);
        Destroy(this.gameObject);
    }

}
