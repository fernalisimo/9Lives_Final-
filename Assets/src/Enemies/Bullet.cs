using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletLife = 4;
    public float speed = 9;

    private Vector3 direction;

    public bool isLaunched = false;

    void Update()
    {
        if (isLaunched)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void Launch(Vector3 direction)
    {
        this.direction = direction;

        isLaunched = true;

        StartCoroutine(dieLater());
    }

    IEnumerator dieLater()
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(this.gameObject);
    }

}
