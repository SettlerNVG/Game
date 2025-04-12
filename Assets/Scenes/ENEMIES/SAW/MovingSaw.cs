using System.Collections;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    // Точки, между которыми будет двигаться пила
    public Transform pointA;
    public Transform pointB;

    // Скорость движения
    public float speed = 3f;

    // Задержка на конечных точках
    public float delayTime = 1f;

    private Transform targetPoint; // Текущая цель
    private bool isWaiting = false;

    void Start()
    {
        // Устанавливаем начальную цель как первую точку
        targetPoint = pointB;
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveSaw();
        }
    }

    private void MoveSaw()
    {
        // Перемещаем пилу в направлении целевой точки
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Проверяем, достигла ли пила целевой точки
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Начинаем задержку
            StartCoroutine(WaitAtPoint());
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;

        // Ждём заданное время
        yield return new WaitForSeconds(delayTime);

        // Меняем цель на другую точку
        targetPoint = targetPoint == pointA ? pointB : pointA;

        isWaiting = false;
    }
}
