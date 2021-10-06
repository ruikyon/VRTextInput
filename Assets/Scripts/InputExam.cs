using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputExam : MonoBehaviour
{
    private int time = 0;
    private int errorCount = 0;
    private string currentProblem;
    private string[] problems = {
        "Nice to meet you!",
        "I am going to see Mike.",
        "What day is tomorrow?"
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Submit(string answer)
    {
        // 完全一致にしようかなと思っているけど、どこまで許容するか検討

        if (answer == currentProblem)
        {
            // correct

            // 次の問題へ
        }
        else
        {
            // incorrect

            // 継続
        }
    }
}
