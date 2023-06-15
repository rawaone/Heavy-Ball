using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 100f;

    public Rigidbody playerRB;
    GameManager gameManager;
    public AudioSource levelCompletedAudio;
    public AudioSource gameOverAudio;
    public AudioSource collectCoinAudio;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    public GameObject congetti_vfx;


    //for fastSwipe
    
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    
    //////////////
    private bool enableSlowSwipe = false;

    


    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        playerRB.constraints = RigidbodyConstraints.FreezeAll;



    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMove();
        // SwipeOnPc();
        // fastSwipe(); //agar lama bet hamw jarek ishi pekarawa

        playerRB.AddForce(Vector3.forward * speed * Time.deltaTime);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    //speed += 300;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //   // speed -= 300;
        //}

      


    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        string tagName = other.transform.tag;

        if(tagName == "GameOver")
        {
            GameManager.gameOver = true;
            
            gameOverAudio.Play();
        }
        else if(tagName == "LastRoad")
        {
            GameManager.levelCompleted = true;
            levelCompletedAudio.Play();
            congetti_vfx.SetActive(true);
            StartCoroutine(StopPlayerMovement());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.transform.tag == "Gem")
        {
            Destroy(other.gameObject);
            gameManager.AddScore();
            collectCoinAudio.Play();
        }
    }



    private void PlayerMove()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);
        playerRB.AddForce(move * speed);
       
        
        
    }


    public void SwipeOnPc()
    {
        //button pressed
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //playerRB.AddForce(Vector3.zero * speed);
        }

        //button release
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            if (playerRB != null)
            {
                //swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    //playerRB.AddForce(Vector3.forward * speed);
                    playerRB.AddTorque(Vector3.forward * speed);
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    playerRB.AddTorque(Vector3.back * speed);
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    playerRB.AddTorque(Vector3.left * speed);
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    playerRB.AddTorque(Vector3.right * speed);
                }

            }

        }


    }

    public void StartTOMove()
    {
        playerRB.constraints = RigidbodyConstraints.None;
    }


    IEnumerator StopPlayerMovement()
    {
        yield return new WaitForSeconds(1.0f);
        playerRB.constraints = RigidbodyConstraints.FreezeAll;
    }


    public void fastSwipe()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {

                if (Distance.x < -swipeRange)
                {
                    //Debug.Log("Left");
                    stopTouch = true;
                    playerRB.AddForce(Vector3.left * speed);
                }
                else if (Distance.x > swipeRange)
                {
                    //Debug.Log("Right");
                    stopTouch = true;
                    playerRB.AddForce(Vector3.right * speed);
                }
                else if (Distance.y > swipeRange)
                {
                    //Debug.Log("Up");
                    stopTouch = true;
                    playerRB.AddForce(Vector3.forward * speed);
                }
                else if (Distance.y < -swipeRange)
                {
                    //Debug.Log("Down");
                    stopTouch = true;
                    playerRB.AddForce(Vector3.back * speed);
                }

            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                //Debug.Log("Tap");
            }

        }
    }


    

}

