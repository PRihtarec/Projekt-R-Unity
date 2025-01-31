using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

public class LSLOutletController : MonoBehaviour
{
    [Header("LSL Stream Settings")]
    public LSL.channel_format_t channelFormat = LSL.channel_format_t.cf_float32;
    public string streamName = "unity_lsl_stream";
    public string streamType = "Unity";
    public int channelNum = 10;  // We now have 11 channels in total
    public float nominalSamplingRate = 256.0f;

    [Header("Stream Status")]
    public StreamOutlet streamOutlet;
    public float start_time;
    public float sent_samples = 0.0f;
    private MonsterController monsterController;
    private GamesController gamesController;
    private Flashlight flashlight;
    private GameObject player;

    public bool croucha = true;
    public bool hoda = true;
    public bool trci = true;
    public bool igraMinigame = true;
    public bool flashlightUpaljen = true;
    public bool presaoMinigame = true;
    public bool failoMinigame = true;
    public float udaljenost = 0f;
    public bool mrtav = false;
    public bool chasea = true;
    public bool vidljivoCudoviste = true;
    public bool saferoom = true;



    void Start()
    {
        gamesController = FindObjectOfType<GamesController>();
        flashlight = FindObjectOfType<Flashlight>();
        GameObject monster = GameObject.FindGameObjectWithTag("monster");
        player = GameObject.FindGameObjectWithTag("Player");
        monsterController = monster.GetComponent<MonsterController>();
        start_time = Time.time;

        StreamInfo streamInfo = new StreamInfo(streamName,
                                                streamType,
                                                channelNum,
                                                nominalSamplingRate,
                                                channelFormat
                                                );
        streamOutlet = new StreamOutlet(streamInfo);
    }

    // Update is called once per frame update
    void Update()
    {
        float elapsed_time = Time.time - start_time;
        int required_samples = (int)(elapsed_time * nominalSamplingRate) - (int)sent_samples;

        for (int i = 0; i < required_samples; i++)
        {
            // Create an array to send the data (11 channels total)
            float[] data = new float[channelNum];

            if (Input.GetKey(KeyCode.LeftShift))
            {
                hoda = false;
                trci = true;
            }
            else
            {
                hoda = true;
                trci = false;
            }
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            {
                croucha = false;
                hoda = false;
                trci = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    croucha = true;
                    hoda = false;
                    trci = false;
                }
                else
                {
                    croucha = false;
                    hoda = true;
                    trci = false;
                }
            }
            chasea = monsterController.getAggro();
            udaljenost = monsterController.getPlayerDistance();
            saferoom = monsterController.isPlayerInSafeRoom();
            //    presaoMinigame = gamesController.isMinigameWon();
            //     failoMinigame = gamesController.isMinigameLost();
            flashlightUpaljen = flashlight.isFlashlightActive();
            vidljivoCudoviste = monsterController.isInViewOfPlayer();
            igraMinigame = gamesController.isMinigameInProgress();
            mrtav = (player == null);

            data[0] = croucha ? 8.0f : 0.0f;
            data[1] = hoda ? 10.0f : 0.0f;
            data[2] = trci ? 12.1f : 0.0f;
            data[3] = igraMinigame ? 14.2f : 0.0f;
            data[4] = flashlightUpaljen ? 16.3f : 0.0f;
            //    data[4] = presaoMinigame ? 18.4f : 0.0f;
            //    data[5] = failoMinigame ? 20.5f : 0.0f;


            data[5] = udaljenost;
            data[6] = saferoom ? 22.6f : 0.0f;
            data[7] = chasea ? 24.7f : 0.0f;
            data[8] = vidljivoCudoviste ? 26.8f : 0.0f;

            data[9] = mrtav ? 28.9f : 0.0f;

            // Push the sample to the outlet
            streamOutlet.push_sample(data);
        }
        sent_samples += required_samples;
    }
}
