using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{

    public const float liquidChange = 0.02f;
    public const float liquidRange = 4.0f;

    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticle = null;

    private bool pourRoutine = false;

    private Vector3 targetPosition = Vector3.zero;

    private PourDetector pourDetector;
    private LiquidRecipient recipient;

    private float width = 0.1f;

    private void Awake()
    {
        pourDetector = GetComponentInParent<PourDetector>();
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
        recipient = GetComponentInParent<LiquidRecipient>();

    }

    private void UpdateScale()
    {
        lineRenderer.widthMultiplier = width;

        if (pourDetector)
        {
            //lineRenderer.widthMultiplier = pourDetector.pourScale;

            splashParticle.transform.localScale = Vector3.one * 12.0f;
        }
        else
        {
            //lineRenderer.widthMultiplier = 0.1f;
            splashParticle.transform.localScale = Vector3.one * 12.0f;
        }
    }


    private void Update()
    {
        UpdateScale();
        UpdateParticle();
        if (pourRoutine)
            BeginPour();
        else
            EndPour();
    }

    public void Begin()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, FindEndPoint());
        pourRoutine = true;
    }

    private void BeginPour()
    {

        if (gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);

            AnimateToPosition(1, targetPosition);
            //MoveToPosition(1, targetPosition);
        }

    }

    private Vector3 FindEndPoint()
    {

        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out RaycastHit hit, liquidRange);
        Vector3 endPoint;

        //si il touche qqc
        if (hit.collider)
        {
            endPoint = hit.point;
            if (recipient != null)
            {
                recipient.RemoveLiquid(liquidChange * Time.deltaTime);
                if (hit.collider.GetComponent<LiquidRecipient>() != null)
                    hit.collider.GetComponent<LiquidRecipient>().AddLiquid(liquidChange * Time.deltaTime);
            }
            else//chaudron
            {
                if (hit.collider.GetComponent<LiquidRecipient>() != null)
                    hit.collider.GetComponent<LiquidRecipient>().AddLiquid(liquidChange * 10 * Time.deltaTime);

            }
        }
        else { //s'il touche rien
            endPoint = ray.GetPoint(liquidRange);
        }


        return endPoint;

    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    public void End()
    {
        pourRoutine = false;
    }

    private void EndPour()
    {
        if (!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
        }

    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {

        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private void UpdateParticle()
    {
        if (gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;
            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(true);
        }
    }

}