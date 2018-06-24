using System.Collections;
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

    public GameObject PlayerDeathPortalPosition;

    private bool PlayerIsNeutral;
    public Image DeathWhiteOut;
    public Image DeathBlackOut;
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
        DeathBlackOut = DeathBlackOut.gameObject.GetComponent<Image>();


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

    private void OnTriggerExit(Collider col)
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
      
    }
    private void OnCollisionExit(Collision col)
    {
        if (PlayerIsNeutral != true && col.gameObject.tag != "Neutral_Platform")
        {
            if (ColorMatch.material != Colors[5] && col.gameObject.GetComponent<MeshRenderer>().material.color != ColorMatch.material.color && PlayerStarted == true)
            {
                Debug.Log("lost life");
                GM.CurrentStreak = 0;

                CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {

        PlayerNotHitPlatform = false;

        if (col.gameObject.tag == "Neutral_Platform")
        {
            Debug.Log("Color Reset");
            playerColor = PlayerController.PlayerColor.neutral;
        }
        if (col.gameObject.tag == "Kill_Platform")
        {

            transform.position = PlayerDeathPortalPosition.transform.position;


           PlayerDeathStateBlackOut(255f,0.5f);



            Debug.Log("Game Over!");
        }
        if (col.gameObject.tag == "Red_Platform")
        {
            if(playerColor != PlayerController.PlayerColor.Red)
            {
                if (GM.CurrentStreak != 0)
                {
                    GM.CurrentStreak = 0;

                    CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
                }
            }
            playerColor = PlayerController.PlayerColor.Red;
            GM.CurrentStreak++;
            Debug.Log("Player red");
        }
        if (col.gameObject.tag == "Blue_Platform")
        {
            if(playerColor != PlayerController.PlayerColor.Blue)
            {
                if (GM.CurrentStreak != 0)
                {
                    GM.CurrentStreak = 0;

                    CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
                }
            }
            playerColor = PlayerController.PlayerColor.Blue;
            GM.CurrentStreak++;
            Debug.Log("Player blue");
        }
        if (col.gameObject.tag == "Yellow_Platform")
        {
            if (playerColor != PlayerController.PlayerColor.Yellow)
            {
                if (GM.CurrentStreak != 0)
                {
                    GM.CurrentStreak = 0;

                    CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
                }
            }
            playerColor = PlayerController.PlayerColor.Yellow;
            GM.CurrentStreak++;
            Debug.Log("Player yellow");
        }
        if (col.gameObject.tag == "Purple_Platform")
        {
            if (playerColor != PlayerController.PlayerColor.Purple)
            {
                if (GM.CurrentStreak != 0)
                {
                    GM.CurrentStreak = 0;

                    CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
                }
            }
            playerColor = PlayerController.PlayerColor.Purple;
            GM.CurrentStreak++;
            Debug.Log("Player purple");
        }
        if (col.gameObject.tag == "Green_Platform")
        {
            if (playerColor != PlayerController.PlayerColor.Green)
            {
                if(GM.CurrentStreak!=0)
                {
                    GM.CurrentStreak = 0;

                    CameraShaker.Instance.ShakeOnce(1, 10, 0, 2);
                }

            }
            playerColor = PlayerColor.Green;
            GM.CurrentStreak++;
            Debug.Log("Player green");
        }      
    }

    private void PlayerDeathStateWhiteOut()
    {
       
    }
    private IEnumerator PlayerDeathStateBlackOut(float aValue, float aTime)
    {
      
        Debug.Log("Blacking out");
        float alpha = DeathBlackOut.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            DeathBlackOut.color = newColor;
            yield return null;
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
