using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster1 : Entity
{
    [SerializeField] private AudioSource deadSound;

    private void Start()
    {
        lives = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            //deadSound.Play();
            lives--;
            Debug.Log("у конусы " + lives + " жизняей");
        }

        if (lives < 1)
            Die();
    }
}
