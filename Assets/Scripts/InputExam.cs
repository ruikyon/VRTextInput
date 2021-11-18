using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputExam : MonoBehaviour
{
    [SerializeField] private Text problemText;
    private int progress;
    private readonly int numberOfProblem = 3; // TODO: データ入れたら変える(5の予定？)
    private bool underTask;
    private string currentProblem;
    private string[] problems = { // TODO: データセットちゃんと検討する
        "Nice to meet you!",
        "I am going to see Mike.",
        "What day is tomorrow?"
    };

    public void StartTask(int inputMethod)
    {
        Logger.StartTask(inputMethod);

        progress = 0;
        underTask = true;

        AskQuestions();
    }

    public void Submit(string answer)
    {
        if (!underTask)
        {
            return;
        }

        Logger.EndProblem(currentProblem, answer);

        progress++;
        if (progress < numberOfProblem)
        {
            AskQuestions();
        }
        else
        {
            underTask = false;
            Logger.EndTask();
            GameManager.Instance.EndTask();
        }
    }

    private void AskQuestions()
    {
        currentProblem = problems[progress];  // TODO: 問題の決定はちゃんとする
        problemText.text = currentProblem;

        Logger.StartProblem();
    }
}
