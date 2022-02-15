﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LiquidRecipient))]
public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;

    [Range(0.1f,2.0f)]
    [HideInInspector]
    public float pourScale = 0.1f;

    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream;

    private LiquidRecipient recipient;

    [Header("Infos")]
    public float pourAngle;

    private void Start()
    {
        recipient = GetComponent<LiquidRecipient>();
        currentStream = CreateStream();
    }

    private void Update()
    {
        pourAngle = CalculatePourAngle();
        bool pourCheck = pourAngle > pourThreshold;
        if (recipient.fillAmount == 0)
        {
            EndPour();
            return;
        }

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;
            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }

    }

    public void StartPour()
    {
        print("start");
        currentStream.gameObject.SetActive(true);
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream.gameObject.SetActive(false);
    }

    private float CalculatePourAngle() {

        Transform t = transform;
        if (tag == "Bottle")
            t = transform.parent;

        float zAngle = 180 - Mathf.Abs(180 - t.rotation.eulerAngles.z);
        float xAngle = 180 - Mathf.Abs(180 - t.rotation.eulerAngles.x);
        return Mathf.Max(zAngle, xAngle);
        //return transform.forward.y * Mathf.Rad2Deg;

    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        streamObject.SetActive(false);
        return streamObject.GetComponent<Stream>();
    }

}