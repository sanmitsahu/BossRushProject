using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public static bool fired = false;
    public static float projectileTime = 2.0f;
    public GameObject fireBall;
    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        light.intensity = 0.0f;
        projectileTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fired)
        {
            projectileTime -= Time.deltaTime;

            if (projectileTime <= 1.0f && projectileTime > 0.0f)
            {
                light.intensity = 20.0f;
            }
            else if (projectileTime <= 0.0f)
            {
                light.intensity = 0.0f;
                float yPos = fireBall.transform.position.y;
                Vector3 bossPos = transform.position;
                Vector3 projectilePos = new Vector3(bossPos.x, yPos, bossPos.z);
                GameObject fire = Instantiate(fireBall, projectilePos, Quaternion.identity);
                fired = true;
                projectileTime = 2.0f;
            }
        }
    }
}
