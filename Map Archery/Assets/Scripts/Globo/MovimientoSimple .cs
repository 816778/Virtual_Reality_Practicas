using System.Collections;
using UnityEngine;

public class MovimientoSimple : GloboMovimiento
{
    public float speed = 1f;

    void Update()
    {
        Mover();
    }

    public override void Mover()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}