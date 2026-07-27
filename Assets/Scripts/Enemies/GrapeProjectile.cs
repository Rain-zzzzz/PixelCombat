using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField][Tooltip("持续时长")] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectileShadow;
    [SerializeField] private GameObject splatterPrefab;

    private void Start()
    {
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

        StartCoroutine(ProjectileCurveRoutine(transform.position, PlayerController.Instance.transform.position));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadow.transform.position, PlayerController.Instance.transform.position));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 starPosition, Vector3 endPositon)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = (timePassed / duration);
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);
            //基础直线插值 + 抛物线高度偏移
            transform.position = Vector2.Lerp(starPosition, endPositon, linearT) + new Vector2(0f, height);
            yield return null;
        }
        Instantiate(splatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject gameShaow, Vector3 starPosition, Vector3 endPositon)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = (timePassed / duration);
            //基础直线插值 + 抛物线高度偏移
            gameShaow.transform.position = Vector2.Lerp(starPosition, endPositon, linearT);
            yield return null;
        }
        Destroy(gameShaow);
    }
}