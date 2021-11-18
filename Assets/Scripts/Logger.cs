using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    private static Logger instance;
    private static Logger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Logger();
            }

            return instance;
        }
    }

    [Serializable]
    public class ActionLog
    {
        public float time;
        public string action;

        public ActionLog(float time, string action)
        {
            this.time = time;
            this.action = action;
        }
    }

    [Serializable]
    public class ProblemLog
    {
        public string problem;
        public string answer;
        public ActionLog[] actionList;

        public ProblemLog(string problem, string answer, ActionLog[] actionList)
        {
            this.problem = problem;
            this.answer = answer;
            this.actionList = actionList;
        }
    }

    [Serializable]
    public class TaskLog
    {
        public int inputMethod;
        public string startTime;
        public int id;
        public ProblemLog[] problemLogList;
    }

    private List<ActionLog> actionList = new List<ActionLog>();
    private List<ProblemLog> problemLogList = new List<ProblemLog>();
    private TaskLog taskLog;
    private float startTime;

    public static void StartTask(int inpurMethod)
    {
        Instance.taskLog = new TaskLog();
        Instance.taskLog.inputMethod = inpurMethod;
        Instance.taskLog.id = 0; // todo: playerprefsからとる？
        Instance.taskLog.startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Instance.problemLogList = new List<ProblemLog>();
    }

    public static void EndTask()
    {
        Instance.taskLog.problemLogList = Instance.problemLogList.ToArray();
        var data = JsonUtility.ToJson(Instance.taskLog);

        // local保存
        var path = Application.persistentDataPath + "/task_logs/" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        Debug.Log("export path: " + path);

        if (!Directory.Exists(Application.persistentDataPath + "/task_logs/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/task_logs/");
        }

        using (var streamWriter = new StreamWriter(path))
        {
            streamWriter.WriteLine(data);
        }

        // server保存
        // TODO: server request
    }

    public static void StartProblem()
    {
        Instance.startTime = Time.time;
        Instance.actionList = new List<ActionLog>();
        AddAction("start");
    }

    public static void EndProblem(string problem, string answer)
    {
        AddAction("end");
        Instance.problemLogList.Add(new ProblemLog(problem, answer, Instance.actionList.ToArray()));
    }

    public static void AddAction(string action)
    {
        Instance.actionList.Add(new ActionLog(Time.time - Instance.startTime, action));
    }
}
