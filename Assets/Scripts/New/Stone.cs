using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    private Animator animator;
    private static readonly int Shake = Animator.StringToHash("shake");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //only being use as a tag for now
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>().Type!=Enemy.EnemyType.Lizard) return;

        animator.SetTrigger(Shake);
    }


}
