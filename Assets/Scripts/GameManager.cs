using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField] InputExam exam;
    [SerializeField] GameObject modeSelector, problem, proposedKeyboard, existingKeyboard;
    [SerializeField] GameObject[] pointers;
    [SerializeField] Text debugText, idText, proposedButton, existedButton;

    public string debugMessage;

    public enum InputMethod
    {
        ProposedMethod,
        ExistingMethod
    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        if (!PlayerPrefs.HasKey("id"))
        {
            StartCoroutine("GetId");
        }
        else
        {
            StartCoroutine(GetLog(PlayerPrefs.GetInt("id", 0)));
        }
    }

    private void Update()
    {
        debugText.text = debugMessage;
    }

    public void StartTask(int inputMethod)
    {
        modeSelector.SetActive(false);
        problem.SetActive(true);

        switch (inputMethod)
        {
            case (int)InputMethod.ProposedMethod:
                proposedKeyboard.SetActive(true);
                break;
            case (int)InputMethod.ExistingMethod:
                existingKeyboard.SetActive(true);
                break;
        }

        foreach (var pointer in pointers)
        {
            pointer.SetActive(false);
        }

        exam.StartTask(inputMethod);
    }

    public void EndTask()
    {
        modeSelector.SetActive(true);
        problem.SetActive(false);

        proposedKeyboard.SetActive(false);
        existingKeyboard.SetActive(false);

        foreach (var pointer in pointers)
        {
            pointer.SetActive(true);
        }
    }

    public void RegistResult(WWWForm form)
    {
        StartCoroutine(Regist(form));
    }

    IEnumerator GetId()
    {
        var uri = "https://sunu.club/experiment/get_id";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success: // 多分ここが成功
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    var id = Int32.Parse(webRequest.downloadHandler.text);
                    PlayerPrefs.SetInt("id", id);
                    idText.text = id.ToString();
                    break;
            }
        }
    }

    IEnumerator GetLog(int id)
    {
        var uri = "https://sunu.club/experiment/get_log/" + id;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success: // 多分ここが成功
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    var counts = webRequest.downloadHandler.text.Split(',');
                    proposedButton.text = counts[0];
                    existedButton.text = counts[1];
                    break;
            }
        }
    }

    IEnumerator Regist(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://sunu.club/experiment/regist", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

}
