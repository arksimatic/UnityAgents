using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject robots;
    public GameObject foods;
    public int maxFoods = 0;
    public int currentFoods = 0;
    private float _timer = 0.0f;
    private String fileName = "score.txt";
    private Boolean _isGameOver = false;
    private Int32 currentPercentage = 100;
    void Start()
    {
        maxFoods = foods.GetComponent<FoodSpawner>().FoodCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGameOver)
        {
            _timer += Time.deltaTime;
            currentFoods = foods.transform.childCount;
            var foodPercentage = Convert.ToSingle(currentFoods * 100) / Convert.ToSingle(maxFoods);

            if(Convert.ToInt32(foodPercentage) != currentPercentage)
            {
                currentPercentage = Convert.ToInt32(foodPercentage);
                Debug.Log($"Food Percentage: {currentPercentage}%");
            }

            if(foodPercentage < 5.0f)
            {
                Debug.Log("Game Over");
                Debug.Log($"Time elapsed: {_timer}");
                //Time.timeScale = 0;
                _isGameOver = true;
                SaveScore();
                SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);
            }
        }
    }

    private void SaveScore()
    {
        var score = new Score
        {
            Robots = robots.transform.childCount,
            Time = _timer,
        };
        var scoreString = JsonUtility.ToJson(score);
        System.IO.File.AppendAllText(fileName, scoreString);
    }

    public class Score
    {
        public int Robots;
        public float Time;
    }
}
