using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] List<MeteorConfig> meteorConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllMeteors());
        }
        while (looping);
    }

    private IEnumerator SpawnAllMeteors()
    {
        //for (int meteorIndex = startingWave; meteorIndex <= meteorConfigs.Count; meteorIndex++)
        //{
            var currentWave = meteorConfigs[startingWave];
            yield return StartCoroutine(SpawnMeteor(currentWave));
       //}
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
