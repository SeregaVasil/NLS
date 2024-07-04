using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        // Проверяем, был ли клик левой кнопкой мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем позицию клика в мировых координатах
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        // Если персонаж должен двигаться
        if (isMoving)
        {
            // Вычисляем направление движения
            Vector2 currentPosition = transform.position;
            Vector2 moveDirection = (targetPosition - currentPosition).normalized;

            // Двигаем персонажа
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Проверяем, достиг ли персонаж цели
            if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}