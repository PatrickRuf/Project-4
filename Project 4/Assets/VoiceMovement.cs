using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceMovement : MonoBehaviour
{
    float timer;
    string keyword;
    public float speed;
    public int lives;
    
    public Rigidbody rb;
    public bool onGround = true;
    public bool isWordJump;
    public bool jumping = false;
    public GameObject test;

    //Needed to recognize words, import Windows.Speech lib
    private KeywordRecognizer WordRecognizer;

    // Confidence => How ambiguous are treated. Low, med, high
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    //Keywords
    public string[] words = new string[] { "right", "jump", "left", "start over", "stop" };
   

    void Start()
    {
        timer = 0;
        speed = 5;
        lives = 99;
        //Can't convert 'string' to 'string[]'
        WordRecognizer = new KeywordRecognizer(words, confidence);
        WordRecognizer.OnPhraseRecognized += RecognizedWord;
        WordRecognizer.Start();

        rb = GetComponent<Rigidbody>();

        
    }


    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(keyword);
        //Debug.Log(lives);


        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1 * Time.deltaTime * speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
        }




        if (keyword == "right")
        {
            transform.position += new Vector3(1 * Time.deltaTime * speed, 0, 0);

        }

        if (keyword == "left")
        {
            transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0);


        }

        if (keyword == "jump" && onGround && isWordJump)
        {
            timer = 0;
            //transform.position += new Vector3(transform.position.x, 5 * Time.deltaTime * speed, 0);

            //rb.AddForce(Vector3.up * speed);
            //onGround = true;

            //isWordJump = true;
            rb.AddForce(transform.up * 300);
           jumping = true;
            //rb.AddForce(new Vector3(transform.position.x, 5, 0));

            isWordJump = false;

            //isJumping = false;
            GameObject.Destroy(test);
        }
        if (jumping == true) { 
               //rb.AddForce(Vector3.up * 200);
            jumping = false;
        }

        if (timer > 10&&keyword == "jump")
        {
            
            isWordJump = true;
            Instantiate(test,  Vector3.zero, Quaternion.identity);
            
        }


        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {

            //transform.position += new Vector3(0, 1 * Time.deltaTime * speed, 0);

            //rb.AddForce(Vector3.up * speed);
            //onGround = true;

             rb.AddForce(Vector3.up * 200);   
            jumping = true;
        }

        if (keyword == "start over")
        {

            transform.position = Vector3.zero;

        }

        if (keyword == "stop")
        {

            transform.position = new Vector3(transform.position.x, 0, 0);

        }

        if (transform.position.y < -5)
        {
            lives = lives - 1;
            transform.position = new Vector3(0, 1, 0);
        }

        /*if (lives == 0)
        {
            Time.timeScale = 0;
        }
*/
       
    }

    public void RecognizedWord(PhraseRecognizedEventArgs word)
    {

        keyword = word.text;
        Debug.Log(keyword);
        Debug.Log(isWordJump);
        //keyword = word.text;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
