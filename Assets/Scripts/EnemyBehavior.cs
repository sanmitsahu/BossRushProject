using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

using Debug = UnityEngine.Debug;
using UnityEngine.Networking;
using System;
using System.Runtime.CompilerServices;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    public GameObject fireBall;
    public GameObject resetPane;
    public static float projectileTime = 2.0f;
    public float resetPaneTMAX = 2.0f;
    private float resetPanetimer = 0;
    public Light light;
    [SerializeField]
    private int originalHealth;
    public static int health;
    public float knockBack = 5.0f;
    public bool wallTouch = false;
    public static bool fired = false;
    public bool startDelay = true;
    public float knockBackTimer = 0.5f;
    public float shockTimer = 5.0f;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private Rigidbody rb;
    Scene scene;
    
    public int no_of_tries = 0;
    public float[,] locations = new float[100, 100];
    private string URL_blocks = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSfkVBkGzZ9kS2AIiRUbRBfmfkyHXdaP1gnOObQaXEaadvs1GQ/formResponse";
    private string URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSd8oLR_OzoW0uwK2OmbssF7nDTgOBva4IXGj-17CwF3ZJrFSg/formResponse";
    private long _sessionID;
    private Vector3 temploc;
    private int level = 0;
    private int temphits = 0;
    private int nforward = 0, npushable = 0, nstun = 0, nblock = 0, healthred = 0, ndirecthits=0;
    public enum State
    {
        NORMAL,
        HIT,
        COMBO,
    }
    public static State st = State.NORMAL;
    // Start is called before the first frame update
    void Start()
    {
        health = originalHealth;
        player = GameObject.FindWithTag("Player");
        originalPos = transform.position;
        originalRot = transform.rotation;
        EventManager.OnRestart += OnDeath;
        light.intensity = 0.0f;
        rb = gameObject.GetComponent<Rigidbody>();
        scene = SceneManager.GetActiveScene();
        resetPane.SetActive(false);
        
        _sessionID = DateTime.Now.Ticks;
        
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        temploc = GameObject.Find("Player").transform.position;
        locations[no_of_tries, 0] = temploc.x;
        locations[no_of_tries, 1] = temploc.y;
        locations[no_of_tries, 2] = temploc.z;
        
        //UnityEngine.Debug.Log(locations[no_of_tries, 0]+" "+locations[no_of_tries, 1]+" "+locations[no_of_tries, 2]+" "+(no_of_tries + 1));
        if(no_of_tries>0)
        {
            if (scene.buildIndex == 7)
                level = 4;
            else if (scene.buildIndex == 5)
                level = 3;
            else if (scene.buildIndex == 3)
                level = 2;
            else
            {
                level = 1;
            }
            
            //UnityEngine.Debug.Log("no_of_tries: "+no_of_tries+" "+ Math.Abs(level));
            StartCoroutine(Post_tries(_sessionID.ToString())); 
        }
        no_of_tries = 0;
        Debug.Log("Restart cus no_of_tries");
        Restart();
    }

    IEnumerator Fireball()
    {
        if (startDelay)
        {
            yield return new WaitForSeconds(3.0f);
            startDelay = false;
        }
        light.intensity = 20.0f;
        yield return new WaitForSeconds(0.5f);
        light.intensity = 0.0f;
        GameObject fire = Instantiate(fireBall, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        fired = false;
        Destroy(fire);
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

        if (wallTouch)
        {
            shockTimer -= Time.deltaTime;
            if (shockTimer <= 0.0f)
            {
                Debug.Log("Restart cus shockTimer");
                Restart();
            }
        }

        if (st == State.HIT)
        {
            if (ChaseBlock.chasing)
            {
                ChaseBlock.chasing = false;
            }
            knockBackTimer -= Time.deltaTime;
            if (knockBackTimer <= 0.0f)
            {
                if (SwitchOn.on)
                {
                    ChaseBlock.chasing = true;
                }
                Debug.Log("Restart cus Switch");
                Restart();
            }
        }
        else if (st == State.COMBO)
        {
            if (ChaseBlock.chasing)
            {
                ChaseBlock.chasing = false;
            }
        }
        if (resetPane.activeSelf)
        {
            resetPanetimer -= Time.deltaTime;
            if (resetPanetimer <= 0.0f)
            {
                resetPane.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sword" && PlayerController.swung)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(other.gameObject.transform.forward * knockBack, ForceMode.Impulse);
            st = State.HIT;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && PlayerController.swung)
        {
            //UnityEngine.Debug.Log("Collission happens now");
            ndirecthits+=1;
            temphits = ndirecthits;
            //UnityEngine.Debug.Log("DIRECT HITTTT"+ndirecthits);
        }
    }
    
    private void Restart()
    {
        
        if (nforward + npushable + nstun != 0)
        {
            if (scene.buildIndex == 7)
                level = 4;
            else if (scene.buildIndex == 5)
                level = 3;
            else if (scene.buildIndex == 3)
                level = 2;
            else
            {
                level = 1;
            }
            UnityEngine.Debug.Log("F"+nforward+"  P"+npushable+"  S"+nstun+"  B"+nblock+"   health"+healthred+"  Level"+scene.buildIndex);
            //temphits = ndirecthits;
            StartCoroutine(Post_blocks(_sessionID.ToString()));
        }
        //COPY
        nforward = 0;
        npushable = 0;
        nstun = 0;
        nblock = 0;
        healthred = 0;
        ndirecthits = 0;

        if (no_of_tries>0) { resetPane.SetActive(true);
            resetPanetimer = resetPaneTMAX; }
        rb.velocity = Vector3.zero;
        transform.position = originalPos;
        transform.rotation = originalRot;
        st = State.NORMAL;
        startDelay = true;
        wallTouch = false;
        knockBackTimer = 0.5f;
        health = originalHealth;
        projectileTime = 2.0f;
        fired = false;
        shockTimer = 5.0f;
        light.intensity = 0.0f;
        if (SwitchOn.on)
        {
            ChaseBlock.chasing = true;
        }
        
        no_of_tries += 1;
    }

    private void Res()
    {
        rb.velocity = Vector3.zero;
        st = State.NORMAL;
        startDelay = true;
        wallTouch = false;
        fired = false;
        projectileTime = 2.0f;
        light.intensity = 0.0f;
        SceneManager.LoadScene(scene.buildIndex);
    }

    private void beatBoss()
    {
        rb.velocity = Vector3.zero;
        st = State.NORMAL;
        startDelay = true;
        wallTouch = false;
        fired = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PushBlock")
        {
            npushable++;
            health--;
            healthred++;
            if (health <= 0)
            {
                beatBoss();
            }
            else
            {
                Debug.Log("Restart cus health");
                
                Restart();
            }
        }
        else if (other.gameObject.tag == "ForwardBlock")
        {
            nforward++;
            if (st == State.HIT || st == State.NORMAL)
            {
                knockBackTimer = 0.5f;
                st = State.COMBO;
            }
            health--;
            healthred++;
            rb.velocity = Vector3.zero;
            rb.AddForce(other.gameObject.transform.forward * knockBack/2.0f, ForceMode.Impulse);
            if (health <= 0)
            {
                beatBoss();
            }
        }
        else if (other.gameObject.tag == "StunBlock")
        {
            nstun++;
            if (!wallTouch)
            {
                knockBackTimer = 0.5f;
                health--;
                healthred++;
                if (health <= 0)
                {
                    beatBoss();
                }
                else
                {
                    st = State.NORMAL;
                    wallTouch = true;
                }
            }
            else
            {
                Debug.Log("Restart stun collision");
                Restart();
            }
        }
        else if (other.gameObject.tag == "Block")
        {
            nblock++;
            Debug.Log("Restart cus block");
            Restart();
        }
        else if (other.gameObject.tag == "Switch")
        {
            Debug.Log("Restart cus switch");
            Restart();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "StunBlock")
        {
            wallTouch = false;
        }
    }
    
    public IEnumerator Post_tries(string sessionID)
    {
        UnityEngine.Debug.Log("POSTIE");
        WWWForm form = new WWWForm();
        
        form.AddField("entry.728675539", sessionID);
        form.AddField("entry.1829062957", no_of_tries);
        //for(int t=0;t<no_of_tries+1;t++)
        //{
        form.AddField("entry.1492841611", (temploc.x).ToString());
        form.AddField("entry.475872908", (temploc.y).ToString());
        form.AddField("entry.432244748", (temploc.z).ToString());
        //}
        form.AddField("entry.596243283", (level).ToString());
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
           
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                UnityEngine.Debug.Log("Form upload complete!");
            }
        }
    }
    
    public IEnumerator Post_blocks(string sessionID)
    {
        //UnityEngine.Debug.Log("POSTBlocks");
        WWWForm form = new WWWForm();
        
        form.AddField("entry.1580530392", sessionID);
        form.AddField("entry.1986196806", nforward);
        
        form.AddField("entry.542647140", npushable);
        form.AddField("entry.136227332", nstun);
        form.AddField("entry.511319478", nblock);
        form.AddField("entry.657771776", temphits);
        form.AddField("entry.188267377", healthred);
        form.AddField("entry.1786672107", level);
        UnityEngine.Debug.Log("FUNCDIRECT HITTTT"+temphits);
        //UnityEngine.Debug.Log("F"+nforward+"  P"+npushable+"  S"+nstun+"  B"+nblock+"   health"+healthred+"  Level"+scene.buildIndex);
        UnityEngine.Debug.Log(ndirecthits);
        using (UnityWebRequest www = UnityWebRequest.Post(URL_blocks, form))
        {
           
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                UnityEngine.Debug.Log("Form upload complete !!!");
            }
        }
    }
}
