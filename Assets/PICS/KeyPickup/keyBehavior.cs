using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, есть ли у объекта тег "Player" (не забудьте задать тег "Player" для вашего игрока)
        if (collision.CompareTag("Player"))
        {
            // Получаем компонент игрока, в котором хранится поле collectedKeys
            CharacterMoves player = collision.GetComponent<CharacterMoves>();

            if (player != null)
            {
                // Увеличиваем количество собранных ключей
                player.collectedKeys += 1;

                // Удаляем ключ из сцены
                Destroy(gameObject);
            }
        }
    }
}
