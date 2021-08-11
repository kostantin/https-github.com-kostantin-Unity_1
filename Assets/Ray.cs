using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    Camera cam;

    public Transform target;

    public Player Player;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit hit;

            if (Physics.Raycast(worldPosition, Vector3.down, out hit, Mathf.Infinity))
            {
                target.position = hit.point;
                target.gameObject.SetActive(true);
                Player.isTarget = true;
            }
        }
    }
}
