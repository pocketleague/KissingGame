using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonClass : MonoBehaviour
{
    public static SingletonClass instance = null;
    public GameObject CURRENT_LEVEL;
    public int LEVEL_NO, TOTAL_LEVELS_PLAYED;
    public bool IS_KISSING;


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {

   //         PlayerPrefs.DeleteAll();

            LEVEL_NO = PlayerPrefs.GetInt("level_no", 1);
            TOTAL_LEVELS_PLAYED = PlayerPrefs.GetInt("total_levels_played", 1);


            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            //If instance already exists and it's not this:
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }
}
