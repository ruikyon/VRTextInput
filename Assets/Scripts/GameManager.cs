using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] InputExam exam;
    [SerializeField] GameObject modeSelector, problem, proposedKeyboard, existingKeyboard;
    [SerializeField] GameObject[] pointers;
    [SerializeField] GameObject[] cursors;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        // idがPlayerPrefsになかったらサーバーから取得？
        // Awakeでもいいかも
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
                existingKeyboard.SetActive(true); // cursorのアクティブ管理に関してはこことまとめられる説
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
}
