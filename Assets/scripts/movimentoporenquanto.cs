using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoporenquanto : MonoBehaviour
{
    public int speed;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetAxis("Horizontal") != 0)
       {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime * Vector3.right);
       }
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Input.GetAxis("Vertical") * speed * Time.deltaTime * Vector3.up);
        }
    }
}
