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
    public static int health = 1;
    public float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 originalPos;
    private Quaternion originalRot;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        swung = false;
        //transform.position = originalPos;
        //transform.rotation = originalRot;
        SceneManager.LoadScene(0);
    }

    IEnumerator SwordDespawn(GameObject sword)
    {
        yield return new WaitForSeconds(0.2f);
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
            health--;
        }
    }
}
