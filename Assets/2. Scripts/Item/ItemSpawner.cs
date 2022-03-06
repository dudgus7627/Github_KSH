using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Spawner
{
    // 게임 오브젝트를 주기적으로 생성
    public class ItemSpawner : MonoBehaviour
    {
        public Transform mItemParent;
        public GameObject[] itemPrefabs; // 생성할 아이템들
        public Transform[] spawnPoints; // 아이템 생성 지점들
        public int itemCount;

        private float lastSpawnTime; // 마지막 생성 시점
        public float maxDistance = 3f; // 플레이어 위치로부터 아이템이 배치될 최대 반경

        private float timeBetSpawn; // 생성 간격

        public float timeBetSpawnMax = 7f; // 최대 시간 간격
        public float timeBetSpawnMin = 2f; // 최소 시간 간격

        private void Start()
        {
            // 생성 간격과 마지막 생성 시점 초기화
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            lastSpawnTime = 0;

            Spawn(); // 실제 아이템 생성
        }

        // 주기적으로 아이템 생성 처리 실행
        private void Update()
        {
            
        }

        // 일정한 시간마다 파밍용 아이템 뿌려주기
        private void RandomItemDrop()
        {
            if (Time.time >= lastSpawnTime + timeBetSpawn)
            {
                lastSpawnTime = Time.time; // 마지막 생성 시간 갱신
                timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // 생성 주기를 랜덤으로 변경
                Spawn();
                // 생성된 아이템을 5초 뒤에 파괴
                //Destroy(item, 5f);
            }
        }

        // 실제 아이템 생성 처리
        private void Spawn()
        {
            // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성합니다.
            for(int i = 0; i < itemCount; i++)
            {
                //Vector3 spawnPosition = Utility.GetRandomPointOnNavMesh(spawnPoints[Random.Range(0, spawnPoints.Length)].position, maxDistance, NavMesh.AllAreas);
                //spawnPosition += Vector3.up * 0.5f; // 바닥에서 0.5만큼 위로 올립니다.

                GameObject item = Instantiate(itemPrefabs[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                item.transform.parent = mItemParent;
                
                item.transform.localScale = new Vector3(.25f, .25f, .25f);
            }
        }
    }
}
