using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool lookAtCouple, lookAway;
    public float originalAngle, lookAngle;
    public Animator animator;
    public GameObject mainModel, duplicateModel;
    void Start()
    {
        //  Invoke("LookAt", 4);
        //   InvokeRepeating("ChangeBooleans", 2, 2);

        Invoke("LookAt", Random.Range(2, 5));

    }

    void Update()
    {
        if (true)
        {
            if (lookAtCouple)
            {
                //if (transform.eulerAngles.y < lookAngle)
                //{
                //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, lookAngle, 0), 2 * Time.deltaTime);
                //}
                //else
                //{
                //    lookAtCouple = false;
                //}

                lookAtCouple = false;

                animator.SetInteger("enemy", 2);
            }
            if (lookAway)
            {
                //if (transform.eulerAngles.y > originalAngle)
                //{
                //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, originalAngle, 0), 2 * Time.deltaTime);
                //}
                //else
                //{
                //    lookAway = false;
                //}

                lookAway = false;
                animator.SetInteger("enemy", 3);

            }
        }
    }

    void LookAt()
    {
        animator.SetInteger("enemy", 1);
        Invoke("LookAway", Random.Range(1, 4));
    }

    void LookAway()
    {
        if (SingletonClass.instance.IS_KISSING)
        {
            CatchCouple();
        }
        else
        {
            animator.SetInteger("enemy", 2);
            Invoke("LookAt", Random.Range(1, 5));
        }
    }

    void ChangeBooleans()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0)
        {
            lookAtCouple = false;
            lookAway = false;
        }
        else if (rand == 1)
        {
            lookAtCouple = false;
            lookAway = true;
        }
        else if (rand == 2)
        {
            lookAtCouple = true;
            lookAway = true;
        }
        else
        {
            lookAtCouple = true;
            lookAway = false;
        }
    }

    void ChangeAnim()
    {
        int rand = Random.Range(1, 3);
        animator.SetInteger("enemy", rand);
    }

    public void CatchCouple()
    {
        duplicateModel.SetActive(true);
        mainModel.SetActive(false);
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.OnLevelFailed();
    }
}
