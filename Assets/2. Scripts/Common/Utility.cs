using UnityEngine;
using UnityEngine.AI;

public static class Utility
{
    // NavMesh���� ���� ������Ʈ ������ġ
    public static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask)
    {
        var randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask);

        return hit.position;
    }

    // ���� ������ ���� �������� �������� �޼���
    // ���� ���� ���Ļ��
    // GedRandomNormalDistribution(��հ�, ǥ��������)
    public static float GetRandomNormalDistribution(float mean, float standard)
    {
        var x1 = Random.Range(0f, 1f);
        var x2 = Random.Range(0f, 1f);
        return mean + standard * (Mathf.Sqrt(-2.0f * Mathf.Log(x1)) * Mathf.Sin(2.0f * Mathf.PI * x2));
    }
}