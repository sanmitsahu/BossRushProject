using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject start;
    public static bool swung = false;
    public float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.0f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    IEnumerator SwordDespawn(GameObject sword)
    {
        yield return new WaitForSeconds(0.25f);
        swung = false;
        Destroy(sword);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !swung)
        {
            swung = true;
            GameObject sword = Instantiate(swordPrefab, start.transform.position, start.transform.rotation);
            sword.transform.parent = transform;
            StartCoroutine(SwordDespawn(sword));
        }
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirect = new Vector3(horizontalInput, 0, verticalInput);

        transform.Translate(moveDirect * speed * Time.deltaTime, Space.World);

        if (moveDirect != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirect), 1.0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            StartCoroutine(RestartLevel());
        }
    }
}
