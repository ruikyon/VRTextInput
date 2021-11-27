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
    [SerializeField] GameObject rig;
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
            idText.text = "ID: " + PlayerPrefs.GetInt("id", 0).ToString();
            SetData();
        }
    }

    private void SetData()
    {
        proposedButton.text = "提案手法 (" + PlayerPrefs.GetInt("pCount", 0).ToString() + "回)";
        existedButton.text = "既存手法 (" + PlayerPrefs.GetInt("eCount", 0).ToString() + "回)";
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

        SetData();
    }

    public void AddCount(int inputMethod)
    {
        var key = inputMethod == (int)InputMethod.ProposedMethod ? "pCount" : "eCount";
        var count = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, count + 1);
    }

    public void RegistResult(WWWForm form)
    {
        StartCoroutine(Regist(form));
    }

    IEnumerator GetId()
    {
        var uri = "https://api.sunu.club/experiment/get_id";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    GameManager.Instance.debugMessage = "Request Error: " + webRequest.error;
                    break;
                case UnityWebRequest.Result.Success: // 多分ここが成功
                    Debug.Log(webRequest.downloadHandler.text);
                    var id = Int32.Parse(webRequest.downloadHandler.text);
                    PlayerPrefs.SetInt("id", id);
                    idText.text = "ID: " + id.ToString();
                    break;
            }
        }
    }

    IEnumerator Regist(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://api.sunu.club/experiment/regist", form))
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
