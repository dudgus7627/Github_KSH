using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // ���� ����
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float magnitude = 0.4f;

    // ���� ���ӽð�
    [SerializeField]
    [Range(0.1f, 1f)]
    private float duration = 0.3f;

    // ī�޶� ��ġ
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
            //Random.insideUnitCircle �ݰ� magnitude + cameraPos ���� ������ġ
            Vector3 pos = (Vector3)Random.insideUnitCircle * magnitude + cameraPos;

            transform.localPosition = pos;
            timer += Time.deltaTime;
            yield return null;
        }

        // ��鸰 ī�޶� ���� ��ġ�� ����
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
