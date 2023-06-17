using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padre : MonoBehaviour
{
    public GameObject emptyObject;
    public GameObject object1;
    public GameObject object2;

    private void Start()
    {
        // Hacer que object1 y object2 sean hijos de emptyObject
        object1.transform.SetParent(emptyObject.transform);
        object2.transform.SetParent(emptyObject.transform);

        // Desactivar las físicas y colisiones de object1 y object2
        Rigidbody rb1 = object1.GetComponent<Rigidbody>();
        if (rb1 != null)
        {
            rb1.isKinematic = true;
            rb1.useGravity = false;
        }
        Collider coll1 = object1.GetComponent<Collider>();
        if (coll1 != null)
        {
            coll1.enabled = false;
        }

        Rigidbody rb2 = object2.GetComponent<Rigidbody>();
        if (rb2 != null)
        {
            rb2.isKinematic = true;
            rb2.useGravity = false;
        }
        Collider coll2 = object2.GetComponent<Collider>();
        if (coll2 != null)
        {
            coll2.enabled = false;
        }

        // Establecer la rotación local de object1 y object2 en -90 grados en el eje Z
        object1.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        object2.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);

        // Establecer las nuevas posiciones de object1 y object2 relativas a emptyObject
        object1.transform.localPosition = new Vector3(-0.177f, 0.198f, 0.1576664f);
        object2.transform.localPosition = new Vector3(-0.174f, -0.152f, 0.1576666f);
    }

    public void ReiniciarEstado()
    {
        Start();
    }
}