using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Spawner
{
    // 게임 오브젝트를 주기적으로 생성
    public class NPCSpawner : MonoBehaviour
    {
        public Transform mNPCParent;
        public GameObject[] npcPrefabs; // 생성할 NPC들
        public Transform[] spawnPoints; // NPC 생성 지점들
        //public Animation animation;
        public int npcCount;

        public float maxDistance = 3f; // spwnPoint로부터 NPC가 배치될 최대 반경

        private void Start()
        {
            Spawn(); // 실제 NPC 생성
        }

        // 실제 NPC 생성 처리
        private void Spawn()
        {
            // NPC 중 하나를 무작위로 골라 랜덤 위치에 생성합니다.
            for(int i = 0; i < npcCount; i++)
            {
                // 플레이어 근처의 네브 메쉬위의 랜덤 위치를 가져옵니다.
                //Vector3 spawnPosition = Utility.GetRandomPointOnNavMesh(spawnPoints[Random.Range(0, spawnPoints.Length)].position, maxDistance, NavMesh.AllAreas);

                GameObject npc = Instantiate(npcPrefabs[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                npc.transform.parent = mNPCParent;
            }
        }
    }
}
