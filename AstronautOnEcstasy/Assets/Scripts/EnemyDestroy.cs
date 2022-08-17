using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    //[SerializeField] private ParticleSystem particlePlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerController.isDashing)
        {
            Destroy(gameObject);
            //particlePlay.Play();

            if (!(PlayerController.staticDestroySound.isPlaying))
            {
                PlayerController.staticDestroySound.Play();
            }

            EnemyController.scoreCount++;
        }

        if (collision.gameObject.CompareTag("Player") && !PlayerController.isDashing)
        {
            Destroy(gameObject);
            EnemyController.scoreCount--;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            EnemyController.scoreCount--;
        }
    }
}
