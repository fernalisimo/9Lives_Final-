using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{

    public HeroCat cat;

    void Update()
    {
        Transform catTransform = cat.transform;

        Transform cameraTransform = this.transform;

        Vector3 catPosition = catTransform.position;
        Vector3 cameraPosition = cameraTransform.position;

        cameraPosition.x = catPosition.x;
        cameraPosition.y = catPosition.y;

        cameraTransform.position = cameraPosition;
    }
}
