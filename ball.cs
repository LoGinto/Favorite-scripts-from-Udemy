
using UnityEngine;

public class ball : MonoBehaviour
{

    // config params
    [SerializeField] Paddle paddle1; 
    [SerializeField] float xPush = 2f;//we set our velocity  of rigit body to this vector
    [SerializeField] float yPush = 15f;//same as in line above but for y coordinates
    [SerializeField] AudioClip[] ballSounds;//array with ball sounds
    [SerializeField] float rant = 0.2f;//random factor


    // state
    Vector2 paddleToBallVector;//pretty understandable
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigitBody;


    // Use this for initialization
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;//distance vector
        myAudioSource = GetComponent<AudioSource>();//initialization of Audiosource component
        myRigitBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))//if LMB is pressed
        {
            hasStarted = true;
            myRigitBody.velocity = new Vector2(xPush, yPush);//set the velocity of rigitbody to xPush,yPush
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);//a new vector made from paddle transform
        transform.position = paddlePos + paddleToBallVector;//ball's position equals to paddle position vector+distance vector
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityChange = new Vector2(Random.Range(0f,rant), Random.Range(0f, rant));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];//play sounds from array
            myAudioSource.PlayOneShot(clip);
            myRigitBody.velocity += velocityChange;



        }
    }
}
