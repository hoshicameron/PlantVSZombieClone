using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	[SerializeField] private float speed=0;
	[SerializeField] private int damage=0;
	[SerializeField] private GameObject hitEffectPrefab;
	[SerializeField] private GameObject hitSoundPrefab;

	private int shredderLayer;

	// Use this for initialization
	void Start ()
	{
		shredderLayer = LayerMask.NameToLayer("Shredder");
	}

	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.right * Time.deltaTime * speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
	    if (collision.gameObject.layer == shredderLayer)
	    {
		    gameObject.SetActive(false);

	    } else
	    {
		    collision.GetComponent<Health>().TakeDamage(damage);

		    GameObject effect =
			    PoolManager.Instance.ReuseObject(hitEffectPrefab, transform.position, Quaternion.identity);
		    effect.SetActive(true);

		    GameObject sound =
			    PoolManager.Instance.ReuseObject(hitSoundPrefab, transform.position, Quaternion.identity);
		    sound.SetActive(true);
		    sound.GetComponent<AudioEffectController>().PlaySoundEffect();

		    gameObject.SetActive(false);
	    }




    }
}
