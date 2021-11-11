using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool loopEnemyWaves = false;

    // Object variables
    private int startingWave;

    // Start is called before the first frame update
    void Start()
    {
        startingWave = 0;
        StartCoroutine(SpawnAllWaves());
        
    }

    private IEnumerator SpawnAllWaves()
    {
        do
        {
            for (int i = startingWave; i < waveConfigs.Count; i++)
            {
                yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[i]));
            }
        }
        while (loopEnemyWaves);     
    }


    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(
            wave.GetEnemyPrefab,
            wave.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);

            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
