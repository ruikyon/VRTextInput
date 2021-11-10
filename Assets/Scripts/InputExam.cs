using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputExam : MonoBehaviour
{
    [SerializeField] private Text problemText;
    private int progress;
    private readonly int numberOfProblem;
    private bool underTask;
    private string currentProblem;
    private string[] problems = {
        "Nice to meet you!",
        "I am going to see Mike.",
        "What day is tomorrow?"
    };

    public void StartTask(int inputMethod)
    {
        progress = 0;
        underTask = true;

        AskQuestions();
        Logger.StartTask(inputMethod);
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
            // 初期状態に戻す(そのまま再度タスクを初めから開始できる状態)
            Logger.EndTask();
        }
    }

    private void AskQuestions()
    {
        currentProblem = problems[progress];
        problemText.text = currentProblem;

        Logger.StartProblem();
    }
}
