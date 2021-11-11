using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Config params
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 10;
    [Header("Enemy Laser")]
    [SerializeField] Laser laserPrefab;
    [SerializeField] float laserShotSpeed = 8f;
    [SerializeField] float laserShotInterval = 1f;
    [SerializeField] float laserShotRandom = 0.25f;
    [Header("Enemy Explotion")]
    [SerializeField] GameObject ExplotionParticles;
    [SerializeField] float durationOfExplotion = 1f;
    [Header("Player Sound")]
    [SerializeField] AudioClip explotionSFX;
    [SerializeField] [Range(0f, 1f)] float explotionVolume = 1f;
    [SerializeField] AudioClip laserShotSFX;
    [SerializeField] [Range(0f, 1f)] float laserVolume = 0.7f;


    private float shotsCounter;


    // Start is called before the first frame update
    void Start()
    {
        SetRandomShotCounter();
    }

    // Update is called once per frame
    void Update()
    {
        CountAndShoot();
    }

    private void CountAndShoot()
    {
        shotsCounter -= Time.deltaTime;
        if (shotsCounter <= 0)
        {
            Shot();
            SetRandomShotCounter();
        }

    }

    private void Shot()
    {
        Laser laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserShotSpeed);
        AudioSource.PlayClipAtPoint(laserShotSFX, Camera.main.transform.position, laserVolume);
    }

    private void SetRandomShotCounter()
    {
        shotsCounter = Random.Range(laserShotInterval - laserShotRandom, laserShotInterval + laserShotRandom);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser != null)
            ProcessHit(laser);
    }

    private void ProcessHit(Laser laser)
    {
        health -= laser.GetDamage();
        if (health <= 0)
            DestroyEnemy();
        laser.Hit();
    }

    private void DestroyEnemy()
    {
        AudioSource.PlayClipAtPoint(explotionSFX, Camera.main.transform.position, explotionVolume);

        if (ExplotionParticles != null)
        {
            GameObject explotion = Instantiate(ExplotionParticles,
                transform.position, Quaternion.identity);
            Destroy(explotion, durationOfExplotion);
        }

        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
    }
}
