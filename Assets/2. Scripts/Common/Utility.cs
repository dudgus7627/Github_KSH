using UnityEngine;
using UnityEngine.AI;

public static class Utility
{
    // NavMesh에서 게임 오브젝트 랜덤위치
    public static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask)
    {
        var randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask);

        return hit.position;
    }

    // 정규 분포로 부터 랜덤값을 가져오는 메서드
    // 정규 분포 공식사용
    // GedRandomNormalDistribution(평균값, 표준편차값)
    public static float GetRandomNormalDistribution(float mean, float standard)
    {
        var x1 = Random.Range(0f, 1f);
        var x2 = Random.Range(0f, 1f);
        return mean + standard * (Mathf.Sqrt(-2.0f * Mathf.Log(x1)) * Mathf.Sin(2.0f * Mathf.PI * x2));
    }
}