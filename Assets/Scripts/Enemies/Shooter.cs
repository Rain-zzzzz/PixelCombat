using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;

    [Tooltip("发射次数")]
    [SerializeField] private int burstCount;//发射次数

    [Tooltip("一轮发射中，每次小单位发射的间隔时间")]
    [SerializeField] private float timeBetweenBursts;//一轮发射中，每次小单位发射的间隔时间

    [Tooltip("一轮发射结束后的休息时间")]
    [SerializeField] private float restTime;//一轮发射结束后的休息时间

    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;

    [Tooltip("一轮发射中，小单位发射的投射物个数")]
    [SerializeField] private float projectilesBurst;//一轮发射中，小单位发射的投射物个数

    [SerializeField] private bool stagger;//交错
    [SerializeField] private bool oscillate;//振荡

    private bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) { stagger = true; }
        else { stagger = false; }
    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        //锥
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;//一轮发射中，小单位发射中，每个抛射物的间隔时间
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesBurst; }
        //发射
        for (int i = 0; i < burstCount; i++)
        {
            //来回振荡
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            if (oscillate && i % 2 == 1)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }
            //一轮发射中的小单位发射
            for (int j = 0; j < projectilesBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                bullet.transform.right = bullet.transform.position - transform.position;

                if (bullet.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }
                currentAngle += angleStep;
                if (stagger)
                {
                    yield return new WaitForSeconds(timeBetweenProjectiles);//交错扫射的感觉
                }
            }
            currentAngle = startAngle;
            if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
        }
        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    //角度锥
    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetPosition = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesBurst - 1);
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)//平面极坐标转直角坐标
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector2(x, y);
    }
}