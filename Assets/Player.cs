using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    public Text scoreText;
    public Text highscoreText;
    int score;
    int highscore;
    Rigidbody2D rb;
    bool over = false;
    float speed = 1f;
    float inputDirection = 0f;
    float speedIncrement = 0.00005f;

    void Start()
    {
        
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best : " + highscore;
        score = -1;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(scoreIncrement());
        StartCoroutine(speedIncrease());
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.GetComponent<PauseMenu>().Pause();
        }
        Debug.Log(inputDirection);
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
        {
            inputDirection = Mathf.Lerp(inputDirection, -1f, Time.deltaTime*12 );
        }
        else if(Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2)
        {
            inputDirection = Mathf.Lerp(inputDirection, +1f, Time.deltaTime*12);
        }
        else
        {
            inputDirection = Mathf.Lerp(inputDirection, 0f, Time.deltaTime * 12); ;
        }
        scoreText.text = "Score : " + score;
        if (!over)
        {
            new Vector2(Input.gyro.gravity.x, Input.gyro.gravity.y);
            transform.Rotate(0, 0, 1.3f*Input.GetAxis("Horizontal") * speed * 90 * Time.deltaTime);
            transform.Rotate(0, 0, 1.3f*inputDirection * speed * 90 * Time.deltaTime);
            transform.Translate(speed * Vector3.down *1.5f * Time.deltaTime);
            Input.gyro.enabled = true;
            transform.Translate(speed * new Vector2(Input.gyro.gravity.x, Input.gyro.gravity.y) * 4f * Time.deltaTime);
        }
        Debug.Log(Input.GetAxis("Horizontal"));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(gameover());
        over = true;
    }
    IEnumerator speedIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            speed = speed+speedIncrement;
        }
    }
    IEnumerator scoreIncrement()
    {
        while (!over)
        {
            score += 1;
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator gameover()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best : " + highscore;
        }
        int i = 0;
        while (i<100)
        {
            yield return new WaitForSeconds(0.01f);
            this.transform.localScale += new Vector3(0.2f,0.2f,0);
            i++;
        }
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
