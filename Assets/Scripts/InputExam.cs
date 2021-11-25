using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputExam : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI problemText;
    private int progress;
    private readonly int numberOfProblem = 5;
    public bool underTask;
    private string currentProblem;
    private List<string> problems = new List<string>();

    private void Awake()
    {
        var dataSet = Resources.Load<TextAsset>("data_set");
        var dataArray = dataSet.text.Split('\n');

        foreach (var row in dataArray)
        {
            problems.Add(row);
        }
    }

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
        var index = Random.Range(0, problems.Count);
        currentProblem = problems[index];
        problemText.text = currentProblem;

        problems.RemoveAt(index);

        Logger.StartProblem();
    }
}
