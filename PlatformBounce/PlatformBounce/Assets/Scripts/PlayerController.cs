﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using EZCameraShake;
public class PlayerController : MonoBehaviour {

    #region Variables
    [Header("Player Stats")]
    public float speed = 6.0F;
    public float DeathTimer;
    float velocity;

    public GameManager GM;

    [SerializeField] List<Material> Colors;
    Rigidbody rb;
    private MeshRenderer MaterialColor;
    private MeshRenderer ColorMatch;
    private bool PlayerStarted;
    [SerializeField] public PlayerColor playerColor;
    public UnityEvent PlatformCollisionExit;

    private Vector3 moveDirection = Vector3.zero;

    private bool PlayerIsNeutral;
    public Image DeathWhiteOut;
    private bool PlayerNotHitPlatform;
    private CameraShakeInstance PlayerDeathCameraShake;


    #endregion

    #region PlayerColors
    public enum PlayerColor
    {
        neutral,
        Kill,
        Blue,
        Red,
        Yellow,
        Green,
        Purple

    }
    #endregion




    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        MaterialColor = gameObject.GetComponent<MeshRenderer>();
        ColorMatch = gameObject.GetComponent<MeshRenderer>();

        DeathWhiteOut = DeathWhiteOut.gameObject.GetComponent<Image>();

    }

    void Update()
    {

        if (PlayerStarted == false)
        {
            PlayerStart();
        }
        SwitchColor();
        PlayerMovement();
    
       

        if(PlayerNotHitPlatform == true)
        {
            PlayerDeathStateWhiteOut();

        }
        else
        {
            PlayerDeathStateWhiteOut();

            DeathTimer = 0;
        }

    }
    private void FixedUpdate()
    {
        velocity = rb.velocity.y;

    }
    void PlayerStart()
    {
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(new Vector3(0, 25, 0), ForceMode.Impulse);
            PlayerStarted = true;
        }
    }
    void PlayerMovement()
    {
        if (PlayerStarted == true)
        {

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            moveDirection *= speed;
            rb.velocity = new Vector3(x * speed, rb.velocity.y, y * speed);

        }
    }

    private void OnCollisionExit(Collision col)
    {
        PlayerNotHitPlatform = true;

        if (col.gameObject.name.Contains("Platform"))
        {
            PlatformCollisionExit.Invoke();
        }
        if (PlayerIsNeutral != true && col.gameObject.tag == "Neutral_Platform")
        {
            Debug.Log("Player Repainted");
            GM.CurrentStreak = 0;
        }
        else if (PlayerIsNeutral !=true && col.gameObject.tag != "Neutral_Platform" )
        {
            if (ColorMatch.material !=Colors[5] && col.gameObject.GetComponent<MeshRenderer>().material.color != ColorMatch.material.color && PlayerStarted == true)
            {
                Debug.Log("lost life");
                CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
                GM.CurrentStreak = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {


        PlayerNotHitPlatform = false;

        if (col.gameObject.tag == "Neutral_Platform")
        {
            Debug.Log("Color Reset");
            playerColor = PlayerController.PlayerColor.neutral;
        }
        if (col.gameObject.tag == "Kill_Platform")
        {
            Debug.Log("Game Over!");
        }
        if (col.gameObject.tag == "Red_Platform")
        {
            playerColor = PlayerController.PlayerColor.Red;
            GM.CurrentStreak++;
            Debug.Log("Player red");
        }
        if (col.gameObject.tag == "Blue_Platform")
        {
            playerColor = PlayerController.PlayerColor.Blue;
            GM.CurrentStreak++;
            Debug.Log("Player blue");
        }
        if (col.gameObject.tag == "Yellow_Platform")
        {
            playerColor = PlayerController.PlayerColor.Yellow;
            GM.CurrentStreak++;
            Debug.Log("Player yellow");
        }
        if (col.gameObject.tag == "Purple_Platform")
        {
            playerColor = PlayerController.PlayerColor.Purple;
            GM.CurrentStreak++;
            Debug.Log("Player purple");
        }
        if (col.gameObject.tag == "Green_Platform")
        {
            playerColor = PlayerColor.Green;
            GM.CurrentStreak++;
            Debug.Log("Player green");
        }      
    }

    private void PlayerDeathStateWhiteOut()
    {
        DeathTimer= DeathTimer+0.001f;
        //PlayerDeathCameraShake = CameraShaker.Instance.StartShake(1, 10, 1);
        //Debug.Log(PlayerDeathCameraShake.CurrentState);
        if (PlayerStarted == true && velocity<-50)
        {
            DeathWhiteOut.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.fixedTime * -velocity * 0.002f*DeathTimer, 255f));
            //PlayerDeathCameraShake.StartFadeIn(0);
            if(DeathWhiteOut.color.a >= 1f)
            {

                PlayerDeathState();
            }
        }
        else
        {
            DeathTimer = 0;
            DeathWhiteOut.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    private void PlayerDeathState()
    {
        GM.GameOver();
    }

    void SwitchColor()
    {
        while (PlayerStarted == true)
        {
            switch (playerColor)
            {
            case PlayerColor.neutral:
                {
                    MaterialColor.material = Colors[5];
                    PlayerIsNeutral = true;
                    break;
                }
            case PlayerColor.Red:
                {
                    MaterialColor.material = Colors[0];
                    PlayerIsNeutral = false;
                    GM.StreakFocus = GameManager.Streak_Focus.Red;
                    break;
                }
            case PlayerColor.Blue:
                {
                    MaterialColor.material = Colors[1];
                    PlayerIsNeutral = false;
                    GM.StreakFocus = GameManager.Streak_Focus.Blue;
                    break;
                }
            case PlayerColor.Purple:
                {
                    MaterialColor.material = Colors[2];
                    PlayerIsNeutral = false;
                    GM.StreakFocus = GameManager.Streak_Focus.Purple;
                    break;
                }
            case PlayerColor.Green:
                {
                    MaterialColor.material = Colors[3];
                    PlayerIsNeutral = false;
                    GM.StreakFocus = GameManager.Streak_Focus.Green;
                    break;
                }

            case PlayerColor.Yellow:
                {
                    MaterialColor.material = Colors[4];
                    PlayerIsNeutral = false;
                    GM.StreakFocus = GameManager.Streak_Focus.Yellow;

                    break;
                }
            }
            break;
        }
    }
}
