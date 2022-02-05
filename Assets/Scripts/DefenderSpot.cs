using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpot : MonoBehaviour
{
    [SerializeField] private Transform parent = null;
    [SerializeField] private Color hoverColor;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip deployClip;

    private bool isdeployed = false;
    private GameObject defenderPrefab = null;
    private int defenderCost;
    private SpriteRenderer renderer;
    private Color defaultColor;
    private Vector3 defaultScale;
    private AudioSource audioSource;



    private void OnEnable()
    {
        // Participant in defender selection event
        EventHandler.DefenderSelectionEvent+=OnDefenderSelected;

        // Get components
        renderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // store default values
        defaultColor = renderer.color;
        defaultScale = transform.GetChild(0).localScale;

    }

    private void OnDisable()
    {
        // unsubscribe
        EventHandler.DefenderSelectionEvent-=OnDefenderSelected;
    }

    private void OnDefenderSelected(GameObject defender,int cost)
    {
        this.defenderPrefab = defender;
        defenderCost = cost;
    }

    private void OnMouseDown()
    {
        // If game paused or ended then return
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        // if already deploy defender then return
        if(isdeployed)    return;


        if (defenderPrefab != null && StarManager.Instance.UseStars(defenderCost))
        {
            // Get gameobject from pool and activate it
            GameObject defender =
                PoolManager.Instance.ReuseObject(defenderPrefab, transform.position, Quaternion.identity);
            defender.SetActive(true);

            // set defender related spot
            defender.GetComponent<Defender>().spot = this;

            isdeployed = true;

            // play deploy sound
            audioSource.clip = deployClip;
            audioSource.Play();

            // reset spot visuals
            SetToDefault();
        }
    }

    private void OnMouseOver()
    {
        // If game paused or ended then return
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        // if already deploy defender then return
        if(isdeployed)    return;

        // change on mouse get over the object

        renderer.color = hoverColor;
        transform.GetChild(0).localScale=new Vector3(0.9f,0.9f,0.9f);


    }

    private void OnMouseEnter()
    {
        // If game paused or ended then return
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        // if already deploy defender then return
        if(isdeployed)    return;

        // Play sound
        audioSource.clip = selectClip;
        audioSource.Play();
    }

    private void OnMouseExit()
    {
        // If game paused or ended then return
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        // if already deploy defender then return
        if(isdeployed)    return;

        SetToDefault();
    }

    private void SetToDefault()
    {
        renderer.color = defaultColor;
        transform.GetChild(0).localScale = defaultScale;
    }

    public void ResetSpot()
    {
        isdeployed = false;
    }
}
