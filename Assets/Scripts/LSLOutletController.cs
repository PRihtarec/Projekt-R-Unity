using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

public class LSLOutletController : MonoBehaviour
{
    [Header("LSL Stream Settings")]
    public LSL.channel_format_t channelFormat = LSL.channel_format_t.cf_float32;
    public string streamName = "unity_lsl_my_stream_name";
    public string streamType = "LSL";
    public int channelNum = 10;  // We now have 10 channels in total
    public float nominalSamplingRate = 100.0f;

    [Header("Stream Status")]
    public StreamOutlet streamOutlet;
    public float start_time;
    public float sent_samples = 0.0f;

    // Your game variables
    public bool hoda = false;
    public bool trci = false;
    public bool igraMinigame = false;
    public bool flashlightUpaljen = false;
    public bool presaoMinigame = false;
    public bool failoMinigame = false;
    public float udaljenost = 0f;
    public bool strasanZvukAktivan = false;
    public bool chasea = false;
    public bool vidljivoCudoviste = false;
    public bool saferoom = false;

    // Start is called before the first frame update
    void Start()
    {
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
            // Create an array to send the data (10 channels total)
            float[] data = new float[channelNum];

            if (Input.GetKey(KeyCode.LeftShift)){
                hoda = false;
                trci = true;
            }
            else{
                hoda = true;
                trci = false;
            }

            // First array of bool values converted to floats (1 for true, 0 for false)
            data[0] = hoda ? 1.0f : 0.0f;
            data[1] = trci ? 1.0f : 0.0f;
            data[2] = igraMinigame ? 1.0f : 0.0f;
            data[3] = flashlightUpaljen ? 1.0f : 0.0f;
            data[4] = presaoMinigame ? 1.0f : 0.0f;
            data[5] = failoMinigame ? 1.0f : 0.0f;

            // Second array of mixed float and bool values
            data[6] = udaljenost;  // float value
            data[7] = strasanZvukAktivan ? 1.0f : 0.0f;
            data[8] = chasea ? 1.0f : 0.0f;
            data[9] = vidljivoCudoviste ? 1.0f : 0.0f;
            // saferoom is just a boolean, we map it to float (1 or 0)
            // If you want this in a specific order, adjust the index accordingly.
            // For example, you can put saferoom in the 9th index, as an optional change.
            // data[9] = saferoom ? 1.0f : 0.0f; 

            // Push the sample to the outlet
            streamOutlet.push_sample(data);
        }
        sent_samples += required_samples;
    }
}
