using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Meteor Config")]
public class MeteorConfig : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] GameObject spotSpawn;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfMeteors = 1;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return meteorPrefab; }
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in spotSpawn.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfMeteors; }
    public float GetMoveSpeed() { return moveSpeed; }
}
