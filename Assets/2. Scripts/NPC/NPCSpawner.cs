using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Spawner
{
    // ���� ������Ʈ�� �ֱ������� ����
    public class NPCSpawner : MonoBehaviour
    {
        public Transform mNPCParent;
        public GameObject[] npcPrefabs; // ������ NPC��
        public Transform[] spawnPoints; // NPC ���� ������
        //public Animation animation;
        public int npcCount;

        public float maxDistance = 3f; // spwnPoint�κ��� NPC�� ��ġ�� �ִ� �ݰ�

        private void Start()
        {
            Spawn(); // ���� NPC ����
        }

        // ���� NPC ���� ó��
        private void Spawn()
        {
            // NPC �� �ϳ��� �������� ��� ���� ��ġ�� �����մϴ�.
            for(int i = 0; i < npcCount; i++)
            {
                // �÷��̾� ��ó�� �׺� �޽����� ���� ��ġ�� �����ɴϴ�.
                //Vector3 spawnPosition = Utility.GetRandomPointOnNavMesh(spawnPoints[Random.Range(0, spawnPoints.Length)].position, maxDistance, NavMesh.AllAreas);

                GameObject npc = Instantiate(npcPrefabs[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                npc.transform.parent = mNPCParent;
            }
        }
    }
}
