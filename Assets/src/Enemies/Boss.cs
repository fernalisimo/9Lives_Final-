using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static Boss lastBoss;
    
    public float shootInterval = 1f;
    private float bulletTime = 0;

    private GameObject bulletPrefab;
    private bool defeated = false;

    private float currentAngle = 0;
    private float angleChangeSpeed = -30;

    public int lifeCount = 3;

    // Use this for initialization
    void Start ()
    {
        lastBoss = this;
        bulletPrefab = (GameObject) Resources.Load("Prefabs/Enemies/Bullet", typeof(GameObject));
    }
	
	// Update is called once per frame
	void Update () {
        // check if defeated
	    if (lifeCount <= 0)
	    {
            Defeat();
	    }

	    if (!defeated)
	    {
	        // bullet shoot cooldown
	        if (bulletTime > 0)
	            bulletTime -= Time.deltaTime;

            AngleTimers();
	        Shoot();
	    }
	}

    private void AngleTimers()
    {
        currentAngle += angleChangeSpeed * Time.deltaTime;
        if (currentAngle <= -45 || currentAngle >= 0)
        {
            angleChangeSpeed *= -1;
        }
    }

    private void Shoot()
    {
        if (bulletTime <= 0.02f)
        {
            // direction will depend on the angle
            Vector3 direction = new Vector3(currentAngle/90f, -1 - currentAngle/90f);

            //Debug.Log((currentAngle / 90f) + " AAAAA " + (-1 - currentAngle / 90f));
            ShootBullet(direction);
        }

    }

    private void ShootBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab);

        bullet.transform.position = transform.position;

        SetRandomSprite(bullet);
        // set appropriate angle
        bullet.transform.Rotate(new Vector3(0,0,1), currentAngle*2);

        Bullet b = bullet.GetComponent<Bullet>();
        b.Launch(direction);

        // reset bullet cd
        bulletTime = shootInterval;
    }

    private void SetRandomSprite(GameObject bullet)
    {
        // set random bullet sprite from 3 variants
        int num = Random.Range(1, 4);
        
        if (num == 4)
            num = 3;

        bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/VacuumCleaner/VC" + num);
    }

    public void Defeat()
    {
        defeated = true;
        // destroy boss
        Destroy(this.gameObject);

        // destroy all currently existing bullets
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in bullets)
        {
            Destroy(obj);
        }
    }

    public bool IsDefeated()
    {
        return defeated;
    }
}
