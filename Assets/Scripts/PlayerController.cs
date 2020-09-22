using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, RollABallControls.IPlayerActions
{
    public float speed;
    public RollABallControls controls;
    private Rigidbody rb;
    public Vector2 motion;
    private int count;
    public Text countText;
    public Text winText;
    private int lives;
    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 3;
        SetCountText ();
        SetLivesText ();
        winText.text = "";
    }

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new RollABallControls();

            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        motion = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(motion.x, 0.0f, motion.y);
        rb.AddForce(movement * speed);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            lives = lives - 1;
            SetLivesText ();
        }
        
        if (count == 12)
        {
            transform.position = new Vector3(-5.0f, 0.5f, -25.0f);
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >=20)
        {
            winText.text = "Congrats! Made by Cheyenne M";
            Destroy(this);
        }
    }

    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives == 0)
        {
            winText.text = "You Lose!";
            Destroy(this);
        }
    }
}