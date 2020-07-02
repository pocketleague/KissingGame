using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonListener : MonoBehaviour
{
    public static System.Action NoButtonPressed;
    public static System.Action YesButtonPressed;
    public static System.Action SendMessageButtonPressed;

    

    private void Awake()
    {
        if (gameObject.name == "No")
        {
            GetComponent<Button>().onClick.AddListener(() => StartCoroutine(nameof(No)));
        }
        else if (gameObject.name == "Yes")
        {
            GetComponent<Button>().onClick.AddListener(() => StartCoroutine(nameof(Yes)));
        }
        else if (gameObject.name == "SendMessage")
        {
            GetComponent<Button>().onClick.AddListener(() => StartCoroutine(nameof(SendMessage)));
        }
    }

    private IEnumerator No()
    {
        Debug.Log("gggggggggggggggggg No");

        yield return null;
        NoButtonPressed?.Invoke();
    }

    private IEnumerator Yes()
    {
        Debug.Log("gggggggggggggggggg Yes");

        yield return null;
        YesButtonPressed?.Invoke();
    }

    private IEnumerator SendMessage()
    {
        Debug.Log("gggggggggggggggggg SendMessage");

        yield return null;
        SendMessageButtonPressed?.Invoke();
    }
}
