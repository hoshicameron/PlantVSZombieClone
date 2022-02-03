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
        EventHandler.DefenderSelectionEvent+=OnDefenderSelected;
        renderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        defaultColor = renderer.color;
        defaultScale = transform.GetChild(0).localScale;

    }

    private void OnDisable()
    {
        EventHandler.DefenderSelectionEvent-=OnDefenderSelected;
    }

    private void OnDefenderSelected(GameObject defender,int cost)
    {
        this.defenderPrefab = defender;
        defenderCost = cost;
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        if(isdeployed)    return;


        if (defenderPrefab != null && StarManager.Instance.UseStars(defenderCost))
        {
            //GameObject InstantiatedDefender=Instantiate(defenderPrefab, transform.position, Quaternion.identity,parent);
            GameObject defender =
                PoolManager.Instance.ReuseObject(defenderPrefab, transform.position, Quaternion.identity);
            defender.SetActive(true);
            defender.GetComponent<Defender>().spot = this;
            isdeployed = true;

            audioSource.clip = deployClip;
            audioSource.Play();

            SetToDefault();
            //Todo Play deploy sound
        }
    }

    private void OnMouseOver()
    {
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        if(isdeployed)    return;

        renderer.color = hoverColor;
        transform.GetChild(0).localScale=new Vector3(0.9f,0.9f,0.9f);


    }

    private void OnMouseEnter()
    {
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

        if(isdeployed)    return;

        audioSource.clip = selectClip;
        audioSource.Play();
    }

    private void OnMouseExit()
    {
        if(GameManager.Instance.IsGameEnded ||GameManager.Instance.IsGamePaused)    return;

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
