using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMovement : MonoBehaviour
{
    float timer;
    string keyword;
    public float speed;
    //Needed to recognize words, import Windows.Speech lib
    private KeywordRecognizer WordRecognizer;

    // Confidence => How ambiguous are treated. Low, med, high
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    //Keywords
    public string[] words = new string[] { "right", "jump", "left", "start over", "hello" };
   

    void Start()
    {
        timer = 0;
        speed = 5;

        //Can't convert 'string' to 'string[]'
        WordRecognizer = new KeywordRecognizer(words, confidence);
        WordRecognizer.OnPhraseRecognized += RecognizedWord;
        WordRecognizer.Start();

         
    }

    
    void Update()
    {
        timer += Time.deltaTime;

    
        if (keyword == "right")
        {
            transform.position += new Vector3(1 * Time.deltaTime * speed, 0, 0);
        }

        if (keyword == "left")
        {
            transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);

           
        }

        if (keyword == "jump")
        {
            transform.position += new Vector3(0, 1 * Time.deltaTime * speed, 0);

            

        }

        if (keyword == "start over")
        {

            transform.position = Vector3.zero;

        }

        if (keyword == "hello")
        {

            transform.position += new Vector3(0,0, 1 * Time.deltaTime * speed);

        }

    }

    public void RecognizedWord(PhraseRecognizedEventArgs word)
    {
        Debug.Log(word.text);

        keyword = word.text;
    }

}
