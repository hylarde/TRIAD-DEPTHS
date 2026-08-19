using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraperseguidora : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition = player.position;

        // Mantém a câmera na mesma posição Z
        targetPosition.z = transform.position.z;

        // Movimento suave
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
