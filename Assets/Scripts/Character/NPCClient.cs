using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.UI;

#if UNITY_EDITOR

[CustomEditor(typeof(NPCClient))]
public class NPCClientEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NPCClient client = (NPCClient)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Give Potion"))
        {
            client.GetPotion();
        }
            
    }

}

#endif
public class NPCClient : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    private Transform toPoint;
    private Transform spawnPoint;

    private AudioSource sourceAudio;

    public AudioClip greetingsAudio;
    public AudioClip victoryAudio;

    [Range(1.0f,5.0f)]
    public float speed = 3.5f;

    public bool isWalking;

    private bool hasPotion = false;

    [SerializeField]
    public List<string> texts;

    private Canvas canvas;
    private Text askPotionText;
    public void GetPotion() {
        if (anim != null)
            anim.SetTrigger("hasPotion");
        hasPotion = true;
        canvas.gameObject.SetActive(false);
        sourceAudio.clip = victoryAudio;
        sourceAudio.Play();
    }

    public void ArriveShop() {
        canvas.gameObject.SetActive(true);
        sourceAudio.clip = greetingsAudio;
        sourceAudio.Play();
    }

    public void ArriveSpawn() { 
        if (hasPotion)
        {
            Destroy(gameObject);
        }
    
    }

    void ChooseText() {
        int idSelected = Random.Range(0, texts.Count);
        askPotionText.text = texts[idSelected];
    }

    // Start is called before the first frame update
    void Start()
    {
        sourceAudio = gameObject.GetComponent<AudioSource>();
        toPoint = GameObject.FindGameObjectWithTag("ToPoint").transform;
        spawnPoint = GameObject.FindGameObjectWithTag("FromPoint").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.speed = speed;
        agent.SetDestination(toPoint.position);

        canvas = GetComponentInChildren<Canvas>();
        askPotionText = GetComponentInChildren<Text>();
        ChooseText();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.velocity.magnitude);
        isWalking = (agent.velocity.magnitude > 1.0f);

        if (hasPotion)
        {
            if (anim)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dancing") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    agent.SetDestination(spawnPoint.position);
                }
            }
            else
            {
                agent.SetDestination(spawnPoint.position);
            }
        }

        if (anim != null)
            anim.SetBool("isWalking", isWalking);

    }
}
