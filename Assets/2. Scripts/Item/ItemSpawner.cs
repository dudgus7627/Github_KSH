using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Spawner
{
    // ���� ������Ʈ�� �ֱ������� ����
    public class ItemSpawner : MonoBehaviour
    {
        public Transform mItemParent;
        public GameObject[] itemPrefabs; // ������ �����۵�
        public Transform[] spawnPoints; // ������ ���� ������
        public int itemCount;

        private float lastSpawnTime; // ������ ���� ����
        public float maxDistance = 3f; // �÷��̾� ��ġ�κ��� �������� ��ġ�� �ִ� �ݰ�

        private float timeBetSpawn; // ���� ����

        public float timeBetSpawnMax = 7f; // �ִ� �ð� ����
        public float timeBetSpawnMin = 2f; // �ּ� �ð� ����

        private void Start()
        {
            // ���� ���ݰ� ������ ���� ���� �ʱ�ȭ
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            lastSpawnTime = 0;

            Spawn(); // ���� ������ ����
        }

        // �ֱ������� ������ ���� ó�� ����
        private void Update()
        {
            
        }

        // ������ �ð����� �Ĺֿ� ������ �ѷ��ֱ�
        private void RandomItemDrop()
        {
            if (Time.time >= lastSpawnTime + timeBetSpawn)
            {
                lastSpawnTime = Time.time; // ������ ���� �ð� ����
                timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax); // ���� �ֱ⸦ �������� ����
                Spawn();
                // ������ �������� 5�� �ڿ� �ı�
                //Destroy(item, 5f);
            }
        }

        // ���� ������ ���� ó��
        private void Spawn()
        {
            // ������ �� �ϳ��� �������� ��� ���� ��ġ�� �����մϴ�.
            for(int i = 0; i < itemCount; i++)
            {
                //Vector3 spawnPosition = Utility.GetRandomPointOnNavMesh(spawnPoints[Random.Range(0, spawnPoints.Length)].position, maxDistance, NavMesh.AllAreas);
                //spawnPosition += Vector3.up * 0.5f; // �ٴڿ��� 0.5��ŭ ���� �ø��ϴ�.

                GameObject item = Instantiate(itemPrefabs[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                item.transform.parent = mItemParent;
                
                item.transform.localScale = new Vector3(.25f, .25f, .25f);
            }
        }
    }
}
