using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 생성할 적 프리팹들
    public Transform[] spawnPoints; // 아이템 생성 지점들
    public int enemyCount;

    
    private float lastSpawnTime; // 마지막 생성 시점
    public float maxDistance = 3f; // 플레이어 위치로부터 에너미가 배치될 최대 반경

    private float timeBetSpawn; // 생성 간격

    public float timeBetSpawnMax = 7f; // 최대 시간 간격
    public float timeBetSpawnMin = 2f; // 최소 시간 간격

    private void Start()
    {                                       
        // 생성 간격과 마지막 생성 시점 초기화
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
        Spawn();
    }


    // 주기적으로 아이템 생성 처리 실행
    private void Update()
    {
        
    }

    // 실제 에너미 생성 처리
    private void Spawn()
    {
        // 플레이어 근처의 네브 메쉬위의 랜덤 위치를 가져옵니다.
        

        // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성합니다.
        for (int i = 0; i < enemyCount; i++)
        {
            //Vector3 spawnPosition = Utility.GetRandomPointOnNavMesh(spawnPoints[Random.Range(0, spawnPoints.Length)].position, maxDistance, NavMesh.AllAreas);
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }

    private void ContinueSpawn()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time; // 마지막 생성 시간 갱신
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 생성 주기를 랜덤으로 변경
            Spawn(); // 실제 아이템 생성
        }
    }
}