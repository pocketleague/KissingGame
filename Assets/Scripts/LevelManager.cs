﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public bool startedKiss;

 //   public Image filler;

    public bool LEVEL_COMPLETE;

    public List<GameObject> levels;
    public GameObject fadeIn, gameOverPanel;
    public Transform SwipePanel;
    public Image fillingBar;

    public GameObject pf_swipePanelGirl, pf_swipePanelBoy;

    private Transform cardHolder;
    private GameObject swipingPanel, matchPanel;

    public int camIndex;

     private void Awake()
    {

        Application.targetFrameRate = 60;
        ButtonListener.YesButtonPressed += Right;
        ButtonListener.NoButtonPressed += Left;
        ButtonListener.SendMessageButtonPressed += CloseSwipePanel;

    }

    private void OnDestroy()
    {
        ButtonListener.YesButtonPressed -= Right;
        ButtonListener.NoButtonPressed -= Left;
        ButtonListener.SendMessageButtonPressed -= CloseSwipePanel;

    }

    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (SingletonClass.instance.CURRENT_LEVEL)
            Destroy(SingletonClass.instance.CURRENT_LEVEL);

        fillingBar.fillAmount = 0;

        fadeIn.SetActive(true);
        Invoke("DelayLoadLevel", .4f);
    }

    public void DelayLoadLevel()
    {
        if (swipingPanel)
            Destroy(swipingPanel);

        if (cardHolder)
            Destroy(cardHolder);

        if (matchPanel)
            Destroy(matchPanel);

      //  SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().heartFiller.fillAmount = 0;
        LEVEL_COMPLETE = false;
        gameOverPanel.SetActive(false);

        swipingPanel = Instantiate(pf_swipePanelGirl, SwipePanel) as GameObject;
        cardHolder = swipingPanel.transform.Find("CardHolder");
        matchPanel = swipingPanel.transform.Find("MatchPanel").gameObject;
    }

    public void CloseSwipePanel()
    {
        SingletonClass.instance.LEVEL_NO++;
        fadeIn.SetActive(true);
        Invoke("DelayCloseSwipePanel", .4f);
    }

    void DelayCloseSwipePanel()
    {
        swipingPanel.SetActive(false);

        SingletonClass.instance.CURRENT_LEVEL = Instantiate(levels[SingletonClass.instance.LEVEL_NO-1]) as GameObject;
     //   bar.SetActive(true);
    }

    public void TouchDown()
    {
        SingletonClass.instance.IS_KISSING = true;
    }

    public void TouchUp()
    {
        SingletonClass.instance.IS_KISSING = false;
    }

    void Update()
    {
        if (!LEVEL_COMPLETE && SingletonClass.instance.CURRENT_LEVEL != null)
        {
            if (SingletonClass.instance.IS_KISSING)
            {
                fillingBar.fillAmount += .1f * Time.deltaTime;

                if (fillingBar.fillAmount >= 1)
                {
                    LEVEL_COMPLETE = true;
                    Debug.Log("Level complete");

                    OnLevelComplete();
                }
            }
            //if (Input.GetButton("Jump"))
            //{
            //    SingletonClass.instance.IS_KISSING = true;
            //    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().heartFiller.fillAmount += .1f * Time.deltaTime;

            //    if (SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().heartFiller.fillAmount >= 1)
            //    {
            //        LEVEL_COMPLETE = true;
            //        Debug.Log("Level complete");

            //        OnLevelComplete();
            //    }
            //}
            //else
            //{
            //    SingletonClass.instance.IS_KISSING = false;
            //}

            if (SingletonClass.instance.IS_KISSING)
            {
                if (!startedKiss)
                {
                    startedKiss = true;

                    //SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.transform.rotation = Quaternion.Euler(0, -75, 0);
                    //SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.transform.rotation = Quaternion.Euler(0, 75, 0);

                    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.GetComponent<Animator>().SetInteger("kissing", 1);
                    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.GetComponent<Animator>().SetInteger("kissing", 1);

                    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().particleEffect.SetActive(true);

                    Debug.Log("Started kissing ");
                }
            }
            else
            {
                if (startedKiss)
                {
                    startedKiss = false;

                    //SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.transform.rotation = Quaternion.Euler(0, 0, 0);
                    //SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.transform.rotation = Quaternion.Euler(0, 0, 0);

                    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.GetComponent<Animator>().SetInteger("kissing", 2);
                    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.GetComponent<Animator>().SetInteger("kissing", 2);

                    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().particleEffect.SetActive(false);

                    Debug.Log("Stopped kissing ");
                }
            }
        }
        
    }

    public void OnLevelComplete()
    {
        Debug.Log("gggg level complete");
     //   SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().confetti.SetActive(true);

        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boyHappy.SetActive(true);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girlHappy.SetActive(true);

        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boyHappy.GetComponentInChildren<Animator>().SetBool("happy", true);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girlHappy.GetComponentInChildren<Animator>().SetBool("happy", true);


        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.SetActive(false);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.SetActive(false);

        gameOverPanel.SetActive(true);

        StartCoroutine(ConfettiDelay());
    }

    IEnumerator ConfettiDelay()
    {
        if (SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().levelType == 1)
        {

            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().enemy.GetComponent<Enemy>().mainModel.SetActive(false);
            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().enemy.GetComponent<Enemy>().walkAway.SetActive(true);
        }
        else
        {
            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().enemy.GetComponent<Enemy>().mainModel.GetComponent<Animator>().SetBool("WalkAway", true);
        }


        yield return new WaitForSeconds(1.0f);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().confetti.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().confetti_blast1.SetActive(true);

        yield return new WaitForSeconds(0.4f);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().confetti_blast2.SetActive(true);

        yield return new WaitForSeconds(0.4f);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().confetti_blast3.SetActive(true);


        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().enemy.GetComponent<Enemy>().walkAway.GetComponent<Animator>().SetBool("walkAway", true);

        ChangeCamera();
        yield return new WaitForSeconds(1);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().heartMask.SetActive(true);


    }

    public void OnLevelFailed()
    {

        LEVEL_COMPLETE = false;

        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boyHappy.SetActive(true);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girlHappy.SetActive(true);

        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.SetActive(false);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.SetActive(false);

        Invoke("DefeatDelay", 2.4f);
        
        gameOverPanel.SetActive(true);
    }

    void DefeatDelay()
    {
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boyHappy.GetComponentInChildren<Animator>().SetBool("angry", true);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girlHappy.GetComponentInChildren<Animator>().SetBool("angry", true);

        //SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().heartFiller.transform.parent.GetComponent<Animator>().SetBool("break", true);
        //Destroy(SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().heartFiller.transform.parent.gameObject, 2);
    }

    public void Right()
    {
        cardHolder.GetChild(cardHolder.childCount - 1).GetComponent<Animator>().SetBool("right", true);
        Destroy(cardHolder.GetChild(cardHolder.childCount - 1).gameObject, 1);

        Invoke("Match", 1);
    }

    public void Left()
    {
        cardHolder.GetChild(cardHolder.childCount - 1).GetComponent<Animator>().SetBool("left", true);
        Destroy(cardHolder.GetChild(cardHolder.childCount - 1).gameObject, 1);
    }

    void Match()
    {
        matchPanel.SetActive(true);
    }


    public void ChangeCamera()
    {


        if (camIndex == 0)
        {
            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.GetComponent<Animator>().SetBool("headTurn", true);
        }
        else if (camIndex == 1)
        {
            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.GetComponent<Animator>().SetBool("headTurn", true);
        }
        else if (camIndex == 2)
        {
            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().girl.GetComponent<Animator>().SetBool("headTurn", false);
            SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().boy.GetComponent<Animator>().SetBool("headTurn", false);

            fillingBar.transform.parent.gameObject.SetActive(true);
        }

        camIndex++;
        if (camIndex == 2)
        {
            camIndex = 0;
        }

        //for (int i = 0; i < 2; i++)
        //{
        //    SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().cameras[i].SetActive(false);
        //}
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().cameras[0].SetActive(true);
        SingletonClass.instance.CURRENT_LEVEL.GetComponent<LevelData>().cameras[1].SetActive(false);


    }
}