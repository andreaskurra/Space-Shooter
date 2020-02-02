using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Meteor Config")]
public class MeteorConfig : ScriptableObject
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] GameObject spotSpawn;
    [SerializeField] GameObject addOnPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfMeteors = 1;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetMeteorPrefab() { return meteorPrefab; }
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
    public int GetNumberOfMeteors() { return numberOfMeteors; }
    public float GetMoveSpeed() { return moveSpeed; }

    public GameObject GetAddOnPrefab () {return addOnPrefab;}
}
