using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    GameObject scoreCounter;
    float delay = 4f;
    static int currentLevel = 1;
    public int Point = 0;

    bool safe = false;
    bool alive = true;
    public int getPoint()
    {
        return Point;
    }

    public int getLevel()
    {
        return currentLevel;
    }

    void Start()
    {

        GetComponent<MeshRenderer>().material.color = chooseColor();
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter");

    }

    Color chooseColor()
    {
        Color color;

        System.Random rd = new System.Random();
        int num = rd.Next();
        if (num % 3 == 0) color = Color.red;
        else if (num % 3 == 1) color = Color.green;
        else color = Color.blue;
        return color;
    }

    // Update is called once per frame
    void Update()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, 0, zValue);

        if (alive == true) scoreCounter.GetComponent<TextMeshProUGUI>().text = "Score: " + Point.ToString();
        if (Point == 10)
        {
            safe = true;
            if (currentLevel < 3)
            {
                scoreCounter.GetComponent<TextMeshProUGUI>().text = "Next level!";
                StartCoroutine(LoadLevelAfterDelay(delay));
            }

            else
            {
                scoreCounter.GetComponent<TextMeshProUGUI>().text = "Congratulation! You won.";
                StartCoroutine(LoadToMenuAfterDelay(delay + 5f));
            }

        }

        if (alive == false)
        {
            scoreCounter.GetComponent<TextMeshProUGUI>().text = "You lose!";
            StartCoroutine(LoadToMenuAfterDelay(delay));
        }

        if (alive == true && transform.position.y < -1)
        {
            scoreCounter.GetComponent<TextMeshProUGUI>().text = "BUG! Please try again.";
            StartCoroutine(LoadBugAfterDelay(delay));
        }

    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

    IEnumerator LoadToMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadBugAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(currentLevel); 
    }


    private void OnTriggerEnter(Collider other)
    {

        if (safe == false && Point < 10 && other.tag == "Hit Point" &&
            (other.GetComponent<MeshRenderer>().material.color == gameObject.GetComponent<MeshRenderer>().material.color))
        {
            Point++;
            Debug.Log(Point);
            other.gameObject.SetActive(false);
        }


        else if (safe == false && other.tag == "Hit Point" &&
            (other.GetComponent<MeshRenderer>().material.color != gameObject.GetComponent<MeshRenderer>().material.color))

        {
            alive = false;
            gameObject.GetComponent<Collider>().enabled = false;

        }
    }


}
