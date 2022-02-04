using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

#if UNITY_EDITOR

[CustomEditor(typeof(Sink))]
public class SinkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Sink si = (Sink)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Fill Cauldron"))
        {
            si.FillCauldron();
        }
        if (GUILayout.Button("Empty Cauldron"))
        {
            si.EmptyCauldron();
        }

    }

}

#endif
public class Sink : MonoBehaviour
{

    public int pourThreshold = 45;

    [Range(0.1f, 2.0f)]
    public float pourScale = 1.0f;

    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream;

    public Cauldron chaudron;

    public void Start()
    {
        currentStream = CreateStream();
    }

    //code sale
    private void Update()
    {
        if (isPouring && chaudron.recipient.fillAmount == 1.0f)
        {
            EndPour();
        }
    }

    public void FillCauldron()
    {
        print("we fill the cauldron");
        StartPour();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream.gameObject.SetActive(false);
        isPouring = false;
    }

    public void EmptyCauldron()
    {
        chaudron.EmptyCauldron();
    }

    public void StartPour()
    {
        print("start");
        currentStream.gameObject.SetActive(true);
        currentStream.Begin();
        isPouring = true;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        streamObject.SetActive(false);
        return streamObject.GetComponent<Stream>();
    }

}
