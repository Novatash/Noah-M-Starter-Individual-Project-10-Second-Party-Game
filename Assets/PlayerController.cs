using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool groundcheck = false;

    int GameStart = 0;

    public float speed = 5f;
    private int dir = 1;
    public Rigidbody2D rigidbody2d;
    public float UpVe = 100f;

    public float TimeRemaining = 10;

    public Text Text;

    public LayerMask platformLayerMask;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public AudioSource audioSource;
    public AudioClip IntroNoise;
    public AudioClip PlayNoise;
    public AudioClip LoseNoise;
    public AudioClip WinNoise;

    public AudioSource audioSource2;
    public AudioClip Jump;

    public GameObject Particles;

    // Start is called before the first frame update
    void Start()
    {
        audioSource2.clip = Jump;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Hello");
        }

        if (GameStart == 0)
        {
            if (Input.GetKeyDown("space"))
            {
                GameStart = 1;
                audioSource.Stop();
                audioSource.clip = PlayNoise;
                audioSource.Play();
            }
        }

        if (GameStart == 4 || GameStart == 3)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene("Level One");
            }
        }

        if (GameStart == 1)
        {

            if (Physics2D.Raycast(transform.position, Vector2.down, 10f, platformLayerMask))
            {
                groundcheck = true;
            }
            else
            {
                groundcheck = false;
            }


            TimeRemaining -= Time.deltaTime;
            Text.text = Mathf.Round(TimeRemaining).ToString();

            if (TimeRemaining <= 0)
            {
                Text.text = "Sorry, you ran out of time. It became just like the rest. Press space to see try again with another one";
                GameStart = 4;
                spriteRenderer.sprite = newSprite;

                audioSource.Stop();
                audioSource.clip = LoseNoise;
                audioSource.Play();
                Destroy(Particles);
            }

            if (groundcheck == true)
            {
                if (Input.GetKeyDown("space"))
                {
                    rigidbody2d.velocity = Vector2.up * UpVe;
                    audioSource2.Stop();
                    audioSource2.Play();
                }
            }
            if (Input.GetKeyDown("d"))
            {
                dir = 2;
            }
            if (Input.GetKeyUp("d"))
            {
                if (dir == 2)
                {
                    dir = 1;
                }
            }

            if (Input.GetKeyDown("a"))
            {
                dir = 0;
            }
            if (Input.GetKeyUp("a"))
            {
                if (dir == 0)
                {
                    dir = 1;
                }
            }

            if (dir == 2)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            if (dir == 0)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }

       if (transform.position.y >= 200)
        {
            Text.text = "I'm glad you made it before you ran out of time. Just in time, too. Press space to see another's run back";
            GameStart = 3;

            audioSource.Stop();
            audioSource.clip = WinNoise;
            audioSource.Play();
            Destroy(Particles);

        }
    }
}
