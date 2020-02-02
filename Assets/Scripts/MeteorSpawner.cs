using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] List<MeteorConfig> meteorConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] int minimumScore = 200;
    GameSession gameSession;
    // Start is called before the first frame update

    IEnumerator Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        do
        {
            do
            {
                yield return StartCoroutine(SpawnAllMeteors());
            }
            while (gameSession.GetScore() >= minimumScore);
        } while (looping);
    }

    private IEnumerator SpawnAllMeteors()
    {
        if (gameSession.GetScore() >= minimumScore)
        {
            for (int meteorIndex = startingWave; meteorIndex <= meteorConfigs.Count - 1; meteorIndex++)
            {
                var currentWave = meteorConfigs[meteorIndex];
                yield return StartCoroutine(SpawnMeteor(currentWave));
            }
        }
    }
    private IEnumerator SpawnMeteor(MeteorConfig meteorConfig)
    {
        //for (int meteorCount = 0; meteorCount < meteorConfig.GetNumberOfMeteors(); meteorCount++)
        //{

            var newMeteor = Instantiate(
                meteorConfig.GetMeteorPrefab(),
                meteorConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
                );
            newMeteor.GetComponent<MetorSpot>().SetWaveConfig(meteorConfig);
            
            yield return new WaitForSeconds(meteorConfig.GetTimeBetweenSpawns());
        //}
    }
}
