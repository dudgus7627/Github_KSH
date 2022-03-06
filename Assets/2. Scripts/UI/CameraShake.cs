using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 진동 세기
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float magnitude = 0.4f;

    // 진동 지속시간
    [SerializeField]
    [Range(0.1f, 1f)]
    private float duration = 0.3f;

    // 카메라 위치
    private Vector3 cameraPos;
    private bool shakeStart = false;

    private void Start()
    {
        cameraPos = transform.localPosition;
    }

    public void Shake()
    {
        cameraPos = transform.localPosition;

        StartCoroutine("_Shake");
    }

    private IEnumerator _Shake()
    {

        float timer = 0;
        while (timer <= duration)
        {
            //Random.insideUnitCircle 반경 magnitude + cameraPos 안의 랜덤위치
            Vector3 pos = (Vector3)Random.insideUnitCircle * magnitude + cameraPos;

            transform.localPosition = pos;
            timer += Time.deltaTime;
            yield return null;
        }

        // 흔들린 카메라 원래 위치로 설정
        transform.localPosition = cameraPos;
    }

    private void Update()
    {
        if (HomeTrigger.Instance.tryDrug && !shakeStart)
        {
            StartCoroutine("_Shaking");
            shakeStart = true;
        }

        if (GameManager.Instance.sceneName.Equals("Fantasy-Beta"))
        {
            shakeStart = false;
        }
    }

    private IEnumerator _Shaking()
    {
        while (true)
        {
            Shake();
            yield return new WaitForSeconds(3);
            if (!HomeTrigger.Instance.tryDrug) break;
        }
    }
}
