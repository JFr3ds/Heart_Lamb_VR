using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour
{
    public GameObject emptyObject;
    public GameObject object1;
    public GameObject object2;

    private Vector3 object1InitialPosition;
    private Vector3 object2InitialPosition;
    private Quaternion object1InitialRotation;
    private Quaternion object2InitialRotation;
    private Transform object1InitialParent;
    private Transform object2InitialParent;
    private Rigidbody object1Rigidbody;
    private Rigidbody object2Rigidbody;
    private Collider object1Collider;
    private Collider object2Collider;

    private void Start()
    {
 
        object1InitialParent = object1.transform.parent;
        object2InitialParent = object2.transform.parent;
        object1Rigidbody = object1.GetComponent<Rigidbody>();
        object2Rigidbody = object2.GetComponent<Rigidbody>();
        object1Collider = object1.GetComponent<Collider>();
        object2Collider = object2.GetComponent<Collider>();

        // Hacer que object1 y object2 sean hijos de emptyObject
        object1.transform.SetParent(emptyObject.transform);
        object2.transform.SetParent(emptyObject.transform);

        // Desactivar las físicas y colisiones de object1 y object2
        SetPhysicsAndCollisions(false);
    }

   /* private void Update()
    {
        // Verificar si se presionó la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Llamar a la función ResetToInitialState()
            ResetToInitialState();
        }
    }*/

    public void ResetToInitialState()
    {
        // Devolver los objetos a su posición, rotación y padre inicial
        object1.transform.SetParent(object1InitialParent);
        object2.transform.SetParent(object2InitialParent);

        // Activar las físicas y colisiones de object1 y object2
        SetPhysicsAndCollisions(true);
    }

    private void SetPhysicsAndCollisions(bool enabled)
    {
        if (object1Rigidbody != null)
        {
            object1Rigidbody.isKinematic = !enabled;
            object1Rigidbody.useGravity = enabled;
        }

        if (object2Rigidbody != null)
        {
            object2Rigidbody.isKinematic = !enabled;
            object2Rigidbody.useGravity = enabled;
        }

        if (object1Collider != null)
        {
            object1Collider.enabled = enabled;
        }

        if (object2Collider != null)
        {
            object2Collider.enabled = enabled;
        }
    }
}