using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 500;
    [Header("Player Laser")]
    [SerializeField] Laser laserPrefab;
    [SerializeField] float laserShotSpeed = 8f;
    [SerializeField] float laserShotInterval = 1f;
    [Header("Player Explotion")]
    [SerializeField] GameObject explotionVFX;
    [SerializeField] float durationOfExplotion = 1f;
    [Header("Player Sound")]
    [SerializeField] AudioClip explotionSFX;
    [SerializeField] [Range(0f, 1f)] float explotionVolume = 1f;
    [SerializeField] AudioClip laserShotSFX;
    [SerializeField] [Range(0f, 1f)] float laserVolume = 0.7f;

    // Object variables
    Vector2 minPoint;
    Vector2 maxPoint;
    Coroutine shotCoroutine;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = health;
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shot();
    }

    private void Shot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shotCoroutine = StartCoroutine(FireCountinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shotCoroutine);
        }
    }

    IEnumerator FireCountinously()
    {
        while (true)
        {
            Laser laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserShotSpeed);
            AudioSource.PlayClipAtPoint(laserShotSFX, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(laserShotInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser != null)
            ProcessHit(laser);
    }

    private void ProcessHit(Laser laser)
    {
        currentHP -= laser.GetDamage();
        if (currentHP <= 0)
            DestroyPlayer();
        laser.Hit();
    }

    private void DestroyPlayer()
    {
        AudioSource.PlayClipAtPoint(explotionSFX, Camera.main.transform.position, explotionVolume);
        if (explotionVFX != null)
        {
            GameObject explotion = Instantiate(explotionVFX,
                transform.position, Quaternion.identity);
            Destroy(explotion, durationOfExplotion);
        }
        Destroy(gameObject);
        FindObjectOfType<ScenesLoader>().LoadGameOver();
    }

    void Move()
    {
        var xDelta = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var yDelta = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + xDelta, minPoint.x + padding, maxPoint.x - padding),
            Mathf.Clamp(transform.position.y + yDelta, minPoint.y + padding, maxPoint.y - padding));
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minPoint = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxPoint = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    public int GetMaxHealth() { return health; }
    public int GetCurrentHealth() { return currentHP; }
}
