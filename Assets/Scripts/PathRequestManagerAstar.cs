using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathRequestManagerAstar : MonoBehaviour
{

    Queue<PathResultAstar> results = new Queue<PathResultAstar>();

    static PathRequestManagerAstar instance;
    PathfindingAstar pathfinding;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<PathfindingAstar>();
    }

    void Update()
    {
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResultAstar result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }

    public static void RequestPath(PathRequestAstar request)
    {
        ThreadStart threadStart = delegate {
            instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }

    public void FinishedProcessingPath(PathResultAstar result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }



}

public struct PathResultAstar
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResultAstar(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }

}

public struct PathRequestAstar
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequestAstar(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }

}