using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= 8.0f)
        {
            //check if gameobject has a parent
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroy the parent too
            Destroy(this.gameObject);
        }
    }
}
