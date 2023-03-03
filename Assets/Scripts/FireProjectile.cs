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
                GameObject fire = Instantiate(fireBall, transform.position, Quaternion.identity);
                fired = true;
                projectileTime = 2.0f;
            }
        }
    }
}
