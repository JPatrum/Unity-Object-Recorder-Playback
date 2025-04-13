using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class RecData
{
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 siz;

    public RecData(Vector3 pos, Vector3 rot, Vector3 siz)
    {
        this.pos = pos;
        this.rot = rot;
        this.siz = siz;
    }
}

public class Recorder : MonoBehaviour
{
    public int delay;
    private int dTimer;

    public int cloneLimit;
    public bool isClone = true;
    private bool ready = false;

    private RecData pData;
    private LinkedList<RecData> playback;
    private LinkedList<RecData> clonePath;

    public GameObject clone;
    private GameObject myClone;

    private Vector3 iPos;
    private Quaternion iRot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pData = new RecData(transform.position, transform.eulerAngles, transform.localScale);
        playback = new LinkedList<RecData>();
        iPos = transform.position;
        iRot = transform.rotation;
        ready = delay > 0;
        if(ready)
        {
            dTimer = delay;
        }
    }

    // Create and set reference to clone
    void SpawnClone()
    {
        if (myClone != null || dTimer > 0) return;
        if (cloneLimit <= 0) return;

        myClone = Instantiate(clone, iPos, iRot);
        myClone.GetComponent<Recorder>().InitClone(delay, cloneLimit - 1, playback);
    }

    // Pass data to clone
    void PassData(RecData data)
    {
        if (myClone == null) return;
        myClone.GetComponent<Recorder>().ReceiveData(data);
    }

    // As a clone, receive data
    public void ReceiveData(RecData data)
    {
        if (!ready) return;
        clonePath.AddLast(data);
    }

    // Store current transform data
    void Record()
    {
        Vector3 nPos = transform.position;
        Vector3 nRot = transform.eulerAngles;
        Vector3 nSiz = transform.localScale;

        RecData entry = new RecData(nPos - pData.pos, nRot - pData.rot, nSiz - pData.siz);
        playback.AddLast(entry);

        pData = new RecData(nPos, nRot, nSiz);
    }

    // Either act on that data or pass it
    void Play()
    {
        if (isClone)
        {
            RecData nextFrame = clonePath.First.Value;
            clonePath.RemoveFirst();

            transform.position += nextFrame.pos;
            transform.Rotate(nextFrame.rot);
            transform.localScale += nextFrame.siz;

            PassData(nextFrame);
        }
        else
        {
            RecData nextFrame = playback.Last.Value;
            PassData(nextFrame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready) return;

        Record();
        Play();
        SpawnClone();

        if(dTimer > 0) dTimer--;
    }

    // Initialize clone after creation
    public void InitClone(int oDelay, int limit, LinkedList<RecData> iPath)
    {
        delay = oDelay;
        dTimer = delay;

        cloneLimit = limit;

        clonePath = new LinkedList<RecData>(iPath);

        ready = true;
    }
}
